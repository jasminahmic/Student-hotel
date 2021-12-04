using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using Studentski_hotel.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Studentski_hotel.Services
{
    public class EmailService:IEmailService
    {
        private IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            var apiKey = _configuration["SendGridAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("hotelstudentski@gmail.com", "Studentski hotel Mostar");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
