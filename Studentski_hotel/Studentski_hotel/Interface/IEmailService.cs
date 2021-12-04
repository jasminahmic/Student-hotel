using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Interface
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string toEmail, string subject, string content);

    }
}
