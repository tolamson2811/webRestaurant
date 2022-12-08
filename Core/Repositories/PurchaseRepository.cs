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
    public class PurchaseRepository : IPurchaseRespository
    {
        private readonly IConfiguration _configuration;
        public PurchaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> AddAsync(PurchaseEntity entity)
        {
            var sql = "Insert into purchase (address,userId,total,  phoneNumber, fullName, description, purchaseDate) VALUES (@Address,@UserId,@Total,  @PhoneNumber, @FullName, @Description, @PurchaseDate)";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM purchase WHERE Id = @Id";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<PurchaseEntity>> GetAllAsync()
        {
            var sql = "select * from purchase";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<PurchaseEntity>(sql);
                return result.ToList();
            }
        }

        public async Task<PurchaseEntity> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM purchase WHERE Id = @Id";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<PurchaseEntity>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(PurchaseEntity entity)
        {
            var sql = "UPDATE purchase SET address = @Address, userId = @UserId, total = @Total, status = @Status, phoneNumber = @PhoneNumber, fullName = @FullName, description = @Description  WHERE Id = @Id";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}
