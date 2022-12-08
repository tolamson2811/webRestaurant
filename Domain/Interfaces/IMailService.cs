using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IMailService
    {

        /// <summary>
        /// Gửi email đến người dùng
        /// </summary>
        /// <param name="request"></param>
        /// <param name="mailSettings"></param>
        /// <param name="mailContent"></param>
        /// <returns></returns>
        Task SendWelcomeEmailAsync(WelcomeRequest request, MailSetting mailSettings, MailContent mailContent);
    }
}
