using System.Net;
using System.Net.Mail;

namespace ConferencySystem.BL.Services
{
    public class EmailService
    {
        public void SendEmail(string receiverEmail, string subject, string body)
        {
            var msg = new MailMessage("nakopnetesvojiskolu@gmail.com", receiverEmail, subject, body);

            msg.IsBodyHtml = true;
            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential("nakopnetesvojiskolu@gmail.com", "KonfSysEmail123"),
                EnableSsl = true
            };
            smtpClient.Send(msg);
        }
    }
}