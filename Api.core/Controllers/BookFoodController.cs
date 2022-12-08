using Domain.Dto;
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
    public class BookFoodController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookFoodController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //[Authorize]
        [HttpGet]
        [Route("GetBooksFood")]
        public async Task<CustomResponseMessage> GetBooksFood()
        {
            CustomResponseMessage responseMessage = new CustomResponseMessage();
            string role = HttpContext.User.FindFirstValue(ContextConstant.Role);
            string userId = HttpContext.User.FindFirstValue(ContextConstant.UserId);
            //if (role != RoleConstant.Admin)
            //{
            //    responseMessage.StatusCode = System.Net.HttpStatusCode.Forbidden;
            //    return responseMessage;
            //}
            responseMessage.Data = await _unitOfWork.BookFoods.GetOrderFood(userId);
            //responseMessage.Data = await _unitOfWork.BookFoods.GetAllAsync();
            return responseMessage;
        }

        [HttpGet]
        public async Task<CustomResponseMessage> Get()
        {
            CustomResponseMessage responseMessage = new CustomResponseMessage();
            string role = HttpContext.User.FindFirstValue(ContextConstant.Role);
            string userId = HttpContext.User.FindFirstValue(ContextConstant.UserId);
            //if (role != RoleConstant.Admin)
            // {
            //   responseMessage.StatusCode = System.Net.HttpStatusCode.Forbidden;
            //   return responseMessage;
            // }
            responseMessage.Data = await _unitOfWork.BookFoods.GetAllAsync();
            return responseMessage;
        }

        //[Authorize]
        [HttpDelete]
        public async Task<CustomResponseMessage> Delete(BookFoodEntity bookFood)
        {
            CustomResponseMessage responseMessage = new CustomResponseMessage();
            string role = HttpContext.User.FindFirstValue(ContextConstant.Role);
            string userId = HttpContext.User.FindFirstValue(ContextConstant.UserId);
            //if (role != RoleConstant.Admin)
           // {
             //   responseMessage.StatusCode = System.Net.HttpStatusCode.Forbidden;
             //   return responseMessage;
           // }
            responseMessage.Data = await _unitOfWork.BookFoods.DeleteAsync(bookFood.Id);
            return responseMessage;
        }

        //[Authorize]
        [HttpPost]
        public async Task<CustomResponseMessage> Insert(BookFoodEntity bookFood)
        {
            CustomResponseMessage responseMessage = new CustomResponseMessage();
            string role = HttpContext.User.FindFirstValue(ContextConstant.Role);
            string userId = HttpContext.User.FindFirstValue(ContextConstant.UserId);
            //if (role != RoleConstant.Admin)
            //{
             //   responseMessage.StatusCode = System.Net.HttpStatusCode.Forbidden;
             //   return responseMessage;
            //}
            responseMessage.Data = await _unitOfWork.BookFoods.AddAsync(bookFood);
            return responseMessage;
        }

        //[Authorize]
        [HttpPut]
        public async Task<CustomResponseMessage> Update(BookFoodEntity bookFood)
        {
            CustomResponseMessage responseMessage = new CustomResponseMessage();
            string role = HttpContext.User.FindFirstValue(ContextConstant.Role);
            string userId = HttpContext.User.FindFirstValue(ContextConstant.UserId);
            if (role != RoleConstant.Admin)
            {
                responseMessage.StatusCode = System.Net.HttpStatusCode.Forbidden;
                return responseMessage;
            }
            responseMessage.Data = await _unitOfWork.BookFoods.UpdateAsync(bookFood);
            return responseMessage;
        }
    }
}
