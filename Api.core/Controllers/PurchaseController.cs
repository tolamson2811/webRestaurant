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
    public class PurchaseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public PurchaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //[Authorize]
        [HttpGet]
        public async Task<CustomResponseMessage> Get()
        {
            CustomResponseMessage responseMessage = new CustomResponseMessage();
            responseMessage.Data = await _unitOfWork.Purchase.GetAllAsync();
            return responseMessage;
        }

        //[Authorize]
        [HttpGet("Id")]
        public async Task<CustomResponseMessage> GetById(int Id)
        {
            CustomResponseMessage responseMessage = new CustomResponseMessage();
            responseMessage.Data = await _unitOfWork.Purchase.GetByIdAsync(Id);
            return responseMessage;
        }

        //[Authorize]
        [HttpPost]
        public async Task<CustomResponseMessage> Insert(PurchaseEntity purchase)
        {
            CustomResponseMessage responseMessage = new CustomResponseMessage();
            //string role = HttpContext.User.FindFirstValue(ContextConstant.Role);
            //string userId = HttpContext.User.FindFirstValue(ContextConstant.UserId);
            //if (role != RoleConstant.Admin)
            //{
            //    responseMessage.StatusCode = System.Net.HttpStatusCode.Forbidden;
            //    return responseMessage;
            //}
            responseMessage.Data = await _unitOfWork.Purchase.AddAsync(purchase);
            return responseMessage;
        }
        //[Authorize]
        [HttpDelete]
        public async Task<CustomResponseMessage> Delete(PurchaseEntity purchase)
        {
            CustomResponseMessage responseMessage = new CustomResponseMessage();
            string role = HttpContext.User.FindFirstValue(ContextConstant.Role);
            string userId = HttpContext.User.FindFirstValue(ContextConstant.UserId);
            //if (role != RoleConstant.Admin)
            //{
            //    responseMessage.StatusCode = System.Net.HttpStatusCode.Forbidden;
            //    return responseMessage;
            //}
            responseMessage.Data = await _unitOfWork.Purchase.DeleteAsync(purchase.Id);
            return responseMessage;
        }

        //[Authorize]
        [HttpPut]
        public async Task<CustomResponseMessage> Update(PurchaseEntity purchase)
        {
            CustomResponseMessage responseMessage = new CustomResponseMessage();
            string role = HttpContext.User.FindFirstValue(ContextConstant.Role);
            string userId = HttpContext.User.FindFirstValue(ContextConstant.UserId);
            //if (role != RoleConstant.Admin)
            //{
            //    responseMessage.StatusCode = System.Net.HttpStatusCode.Forbidden;
            //   return responseMessage;
            //}
            responseMessage.Data = await _unitOfWork.Purchase.UpdateAsync(purchase);
            return responseMessage;
        }

    }
}
