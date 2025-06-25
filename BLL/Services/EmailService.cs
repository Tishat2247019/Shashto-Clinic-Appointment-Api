using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmailService
    {
        public static bool SendEmail(string to, string subject, string body)
        {
            try
            {
                var message = new MailMessage("ShasthoClinic@gmail.com", to);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                var smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("towsif1528@gmail.com", "xewi jgpl qjbl kvbv");
                smtp.EnableSsl = true;

                smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email Error: " + ex.Message);
                return false;
            }
        }
    }
}
