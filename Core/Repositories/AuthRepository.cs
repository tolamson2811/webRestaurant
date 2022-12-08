using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _configuration;
        public AuthRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> AddAsync(AuthEntity entity)
        {
            //var sql = "INSERT INTO auth (UserID, FullName, UserName, Email, HashedKey, SaltKey, `Role`, address, phoneNumber, gender, birth, avatar) VALUES(uuid(),@FullName, @UserName, @Email, @HashedKey, @SaltKey, 2, @Address, @PhoneNumber, @Gender, @Birth, @Avatar);";
            var sql = "INSERT INTO auth (userID, fullName, userName, email, hashedKey, saltKey, `role`) VALUES(uuid(),@FullName, @UserName, @Email, @HashedKey, @SaltKey, 2);";
            //var sql = "INSERT INTO auth (fullName, userName, email, `role`) VALUES(@FullName, @UserName, @Email, 2);";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<AuthEntity> CheckExistUserName(string UseName)
        {
            var sql = "Select * from auth where UserName = @UseName";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<AuthEntity>(sql, new { UseName = UseName });
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Auth WHERE Id = @Id";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }
        public async Task<IReadOnlyList<AuthEntity>> GetAllAsync()
        {
            var sql = "SELECT * FROM Auth";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<AuthEntity>(sql);
                return result.ToList();
            }
        }
        public async Task<AuthEntity> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Auth WHERE Id = @Id";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<AuthEntity>(sql, new { Id = id });
                return result;
            }
        }
        public async Task<int> UpdateAsync(AuthEntity entity)
        {
            var sql = "UPDATE Auth SET username = @Username, fullname = @Fullname, address = @Address, email = @Email, birth = @Birth, avatar = @Avatar, phoneNumber = @PhoneNumber  WHERE Id = @Id";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> UpdatePassword(string Id, string hashedKey, string saltKey)
        {
            var sql = "update Auth SET HashedKey = @HashedKey, SaltKey = @SaltKey WHERE UserID = @Id";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = Id, HashedKey = hashedKey, SaltKey = saltKey });
                return result;
            }
        }
    }
}
