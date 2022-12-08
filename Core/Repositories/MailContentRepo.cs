using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class MailContentRepo : IMailContentRepo
    {
        private readonly IConfiguration _configuration;
        public MailContentRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<MailContent> GetMailContent()
        {
            string sql = "select * from mailcontent where AppName  = 'All'";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<MailContent>(sql);
                return result;
            }
        }
    }
}
