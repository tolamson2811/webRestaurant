using Core.UnitOfWorks;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Shared.Constants;
using Domain.Shared.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Exchange.WebServices.Data;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Repo.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public FoodController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //[Authorize]
        [HttpGet]
        public async Task<CustomResponseMessage> Get()
        {
            CustomResponseMessage responseMessage = new CustomResponseMessage();
            responseMessage.Data = await _unitOfWork.Foods.GetAllAsync();
            return responseMessage;
        }

        //[Authorize]
        [HttpGet("Id")]
        public async Task<CustomResponseMessage> GetById(int Id)
        {
            CustomResponseMessage responseMessage = new CustomResponseMessage();
            responseMessage.Data = await _unitOfWork.Foods.GetByIdAsync(Id);
            return responseMessage;
        }

        //[Authorize]
        [HttpPost]
        public async Task<CustomResponseMessage> Insert(FoodEntity food)
        {
            CustomResponseMessage responseMessage = new CustomResponseMessage();
            string role = HttpContext.User.FindFirstValue(ContextConstant.Role);
            string userId = HttpContext.User.FindFirstValue(ContextConstant.UserId);
            //if (role != RoleConstant.Admin)
            //{
            //    responseMessage.StatusCode = System.Net.HttpStatusCode.Forbidden;
            //    return responseMessage;
            //}
            responseMessage.Data = await _unitOfWork.Foods.AddAsync(food);
            return responseMessage;
        }
        //[Authorize]
        [HttpDelete]
        public async Task<CustomResponseMessage> Delete(FoodEntity food)
        {
            CustomResponseMessage responseMessage = new CustomResponseMessage();
            string role = HttpContext.User.FindFirstValue(ContextConstant.Role);
            string userId = HttpContext.User.FindFirstValue(ContextConstant.UserId);
            //if (role != RoleConstant.Admin)
            //{
            //    responseMessage.StatusCode = System.Net.HttpStatusCode.Forbidden;
            //    return responseMessage;
            //}
            responseMessage.Data = await _unitOfWork.Foods.DeleteAsync(food.Id);
            return responseMessage;
        }

        //[Authorize]
        [HttpPut]
        public async Task<CustomResponseMessage> Update(FoodEntity food)
        {
            CustomResponseMessage responseMessage = new CustomResponseMessage();
            string role = HttpContext.User.FindFirstValue(ContextConstant.Role);
            string userId = HttpContext.User.FindFirstValue(ContextConstant.UserId);
            //if (role != RoleConstant.Admin)
            //{
            //    responseMessage.StatusCode = System.Net.HttpStatusCode.Forbidden;
             //   return responseMessage;
            //}
            responseMessage.Data = await _unitOfWork.Foods.UpdateAsync(food);
            return responseMessage;
        }

    }
}
