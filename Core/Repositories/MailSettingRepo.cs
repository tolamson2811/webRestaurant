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
    public class MailSettingRepo : IMailSettingRepo
    {

        private readonly IConfiguration _configuration;
        public MailSettingRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }   
        public async Task<MailSetting> GetMailServer()
        {
            string sql = "select * from mailsetting where AppName  = 'All'";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<MailSetting>(sql);
                return result;
            }
        }
    }
}
