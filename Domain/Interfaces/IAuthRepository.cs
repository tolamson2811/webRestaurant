using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAuthRepository : IGenericRepository<AuthEntity>
    {
        Task<AuthEntity> CheckExistUserName(string UseName);      
        Task<int> UpdatePassword(string Id,string hashedKey,string saltKey);      


    }
}
