using Core.Common;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Domain.Shared.Constants;
using Domain.Shared.Contract;
using Domain.Shared.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repo.WebApi.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repo.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        public AuthController(IUnitOfWork unitOfWork, IConfiguration configuration, IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mailService = mailService;
        }        
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserModel user)
        {
            IActionResult response = Unauthorized();

            var userLogin = await AuthenticateUser(user);

            if (userLogin != null)
            {
                var tokenString = await GenerateJSONWebToken(userLogin);
                response = Ok(new { 
                    status = 200,
                    token = tokenString,
                    userLogin
                });
            }

            return response;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<CustomResult> Register([FromBody] UserModel user)
        {
            CustomResult response = new CustomResult();

            try
            {
                string newSaltKey = UserCommon.CreateSaltKey();

                string newHashedKey = UserCommon.CreateHashFromSaltAndPassword(newSaltKey, user.Password);

                AuthEntity auth = new AuthEntity();
                auth.UserName = user.Username;
                auth.HashedKey = newHashedKey;
                auth.SaltKey = newSaltKey;

                await _unitOfWork.Auths.AddAsync(auth);
                response.Status = StatusCodeEnum.OK;
            }
            catch
            {
                response.Status = StatusCodeEnum.NotFound;
            }            
            return response;
        }

        [HttpGet("Id")]
        public async Task<CustomResponseMessage> GetById(int Id)
        {
            CustomResponseMessage responseMessage = new CustomResponseMessage();
            responseMessage.Data = await _unitOfWork.Auths.GetByIdAsync(Id);
            return responseMessage;
        }

        /// <summary>
        /// Hàm recover mật khẩu
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("recover-password")]
        public async Task<IActionResult> RecoverPassword([FromBody] UserRequest user)
        {
            IActionResult response = Unauthorized();
            string status = "success";
            try
            {
                //kiểm tra email có tồn tại hay không hay bọn nó nhập linh tinh
                AuthEntity userExist = await _unitOfWork.Auths.CheckExistUserName(user.UserName);

                if (userExist == null) return Ok(new { status = "not exists" });

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Recover"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Email, userExist.UserName),
                new Claim(JwtRegisteredClaimNames.Iat,userExist.UserID.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub,userExist.SaltKey.ToString())
                };

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                  _configuration["Jwt:Issuer"],
                  claims,
                  expires: DateTime.Now.AddHours(3),
                  signingCredentials: credentials);

                string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                MailSetting mailSetting = await _unitOfWork.MailSettings.GetMailServer();
                if (mailSetting == null) return Unauthorized();

                MailContent mailContent = await _unitOfWork.MailContents.GetMailContent();

                if (mailContent == null) return Unauthorized();


                WelcomeRequest request = new WelcomeRequest();
                request.UserName = userExist.FullName;
                request.ToEmail = user.UserName;
                request.Action = "Bấm vào đây để đổi mật khẩu";
                request.Link = mailSetting.MainUrl + "?token=" + tokenString;
                request.Subject = "Khôi phục tài khoản - Yira team";

                await _mailService.SendWelcomeEmailAsync(request, mailSetting, mailContent);
                response = Ok(new { status = status });
            }
            catch (Exception ex)
            {
                status = "Fail";
                response = Ok(new { status = status, message = ex.ToString() });
            }

            return response;
        }

        private static DateTime ConvertFromUnixTimestamp(int timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp); //
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> ChangePasswordAnonymous([FromBody] UserChangePasswordRequest user)
        {
            IActionResult response = Unauthorized();
            try
            {
                var obj = new JwtSecurityToken(user.Token);
                var Id = obj.Payload["iat"].ToString();
                int expTime = int.Parse(obj.Payload["exp"].ToString());

                DateTime expDate = ConvertFromUnixTimestamp(expTime);
                var subDate = expDate.CompareTo(DateTimeOffset.UtcNow.UtcDateTime);

                if (subDate >= 0)
                {
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                       AuthEntity auth = await _unitOfWork.Auths.CheckExistUserName(obj.Payload["email"].ToString());

                        if (auth != null)
                        {

                            string oldSaltKey = obj.Payload["sub"].ToString();

                            if(oldSaltKey != auth.SaltKey) return Ok(new { status = "expired" });

                            string newSaltKey = UserCommon.CreateSaltKey();

                            string newHashedKey = UserCommon.CreateHashFromSaltAndPassword(newSaltKey, user.Password);

                            //call update password
                            int numberUpdate = await _unitOfWork.Auths.UpdatePassword(Id, newHashedKey, newSaltKey);

                            if (numberUpdate > 0)
                            {
                                return Ok(new { status = "success" });
                            }
                            else
                            {
                                return Ok(new { status = "fail" });
                            }

                        }
                        else {
                            return Ok(new { status = "fail" });
                        }                        
                    }
                }
                else
                {
                    return Ok(new { status = "expired" });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

            return response;
        }

        [HttpPut]
        public async Task<CustomResponseMessage> Update(AuthEntity auth)
        {
            CustomResponseMessage responseMessage = new CustomResponseMessage();
            string role = HttpContext.User.FindFirstValue(ContextConstant.Role);
            string userId = HttpContext.User.FindFirstValue(ContextConstant.UserId);
            //if (role != RoleConstant.Admin)
            //{
            //    responseMessage.StatusCode = System.Net.HttpStatusCode.Forbidden;
            //    return responseMessage;
            //}
            responseMessage.Data = await _unitOfWork.Auths.UpdateAsync(auth);
            return responseMessage;
        }

        /// <summary>
        /// Hàm sinh token auth
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private async Task<string> GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                new Claim(JwtRegisteredClaimNames.Sid, userInfo.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, userInfo.Role.ToString())
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(1440),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        /// <summary>
        /// Hàm validate thông tin đăng nhập
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        private async Task<UserModel> AuthenticateUser(UserModel login)
        {
            UserModel user = null;

            var userExist = await _unitOfWork.Auths.CheckExistUserName(login.Username);

            if (userExist != null)
            {
                if (userExist.HashedKey.Equals(UserCommon.CreateHashFromSaltAndPassword(userExist.SaltKey, login.Password)))
                {
                    user = new UserModel { Username = login.Username, Password = login.Password, UserId = userExist.Id,Role = userExist.Role };
                }
            }
            return user;
        }
    }
}
