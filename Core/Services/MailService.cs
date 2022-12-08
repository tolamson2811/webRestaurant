using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
//using MailKit.Net.Smtp;
//using MailKit.Security;
using Microsoft.Extensions.Options;
//using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class MailService : IMailService
    {              
        public async Task SendWelcomeEmailAsync(WelcomeRequest request, MailSetting settingMail, MailContent mailContent)
        {
            //string MailText = mailContent.Content;  

            //MailText = MailText.Replace("[username]", request.UserName).Replace("[email]", request.ToEmail).Replace("[action]", request.Action).Replace("[linktoken]", request.Link);
            //var email = new MimeMessage();
            //email.Sender = MailboxAddress.Parse(settingMail.Mail);
            //email.To.Add(MailboxAddress.Parse(request.ToEmail));
            //email.Subject = request.Subject;
            //var builder = new BodyBuilder();
            //builder.HtmlBody = MailText;
            //email.Body = builder.ToMessageBody();
            //using var smtp = new SmtpClient();
            //smtp.Connect(settingMail.Host, settingMail.Port, SecureSocketOptions.StartTls);
            //smtp.Authenticate(settingMail.Mail, settingMail.Password);         
            //await smtp.SendAsync(email);
            //smtp.Disconnect(true);


            string MailText = mailContent.Content;

            MailText = MailText.Replace("[username]", request.UserName).Replace("[email]", request.ToEmail).Replace("[action]", request.Action).Replace("[linktoken]", request.Link);


            string senderID = settingMail.Mail;
            string senderPassword = settingMail.Password;       
            string body = MailText;
            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.To.Add(request.ToEmail);
                mail.From = new MailAddress(senderID);
                mail.Subject = request.Subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = settingMail.Host; 
                smtp.Credentials = new System.Net.NetworkCredential(senderID, senderPassword);
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(mail);

            }
            catch (Exception ex)
            {               
            }            
        }
    }
}
