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
    public class FoodRepository : IFoodRepository
    {
        private readonly IConfiguration _configuration;
        public FoodRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> AddAsync(FoodEntity entity)
        {
            var sql = "Insert into food (FoodName,FoodPrice,ImageLink) VALUES (@FoodName,@FoodPrice,@ImageLink)";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM food WHERE Id = @Id";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }
        public async Task<IReadOnlyList<FoodEntity>> GetAllAsync()
        {
            var sql = "select * from food";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<FoodEntity>(sql);
                return result.ToList();
            }
        }
        public async Task<FoodEntity> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM food WHERE Id = @Id";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<FoodEntity>(sql, new { Id = id });
                return result;
            }
        }
        public async Task<int> UpdateAsync(FoodEntity entity)
        {
            var sql = "UPDATE food SET FoodName = @FoodName, FoodPrice = @FoodPrice, ImageLink = @ImageLink  WHERE Id = @Id";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}
