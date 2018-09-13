using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace ConferencySystem.BL.Services
{
    public class EmailService
    {
        public void SendEmail(string receiverEmail, string subject, string body, byte[] invoice)
        {
            var msg = new MailMessage("nakopnetesvojiskolu@gmail.com", receiverEmail, subject, body);
            
            msg.IsBodyHtml = true;

            if (invoice != null)
            {
                Stream file = new MemoryStream(invoice);
                Attachment sMailAttachment = new Attachment(file,"Zalohova_faktura.pdf");
                msg.Attachments.Add(sMailAttachment);
            }
            
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