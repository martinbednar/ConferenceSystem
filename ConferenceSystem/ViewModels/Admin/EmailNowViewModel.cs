using System.Linq;
using System.Net;
using System.Net.Mail;
using ConferencySystem.BL.Services;
using DotVVM.Framework.Runtime.Filters;

namespace ConferencySystem.ViewModels.Admin
{
    [Authorize(Roles = new[] { "admin", "super" })]
    public class EmailNowViewModel : MainMasterPageViewModel
    {
        public string Subject { get; set; }

        public string Body { get; set; }

        public void Send()
        {
            var registerService = new RegisterService();

            var allParticipants = registerService.GetUsers();

            var firtThird = allParticipants[allParticipants.Count / 3].Id;
            var secondThird = allParticipants[2 * (allParticipants.Count / 3)].Id;

            var participantsPart1 = allParticipants.Where(p => p.Id <= firtThird);
            var participantsPart2 = registerService.GetUsers().Where(p => p.Id > firtThird && p.Id <= secondThird);
            var participantsPart3 = registerService.GetUsers().Where(p => p.Id > secondThird);

            var smtpClient1 = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential("nakopnetesvojiskolu@gmail.com", "KonfSysEmail123"),
                EnableSsl = true
            };

            var msg1 = new MailMessage("nakopnetesvojiskolu@gmail.com", "nakopnetesvojiskolu@gmail.com", Subject, Body)
            {
                IsBodyHtml = true
            };

            foreach (var participant in participantsPart1)
            {
                msg1.Bcc.Add(participant.Email);
            }

            smtpClient1.Send(msg1);


            var smtpClient2 = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential("nakopnetesvojiskolu@gmail.com", "KonfSysEmail123"),
                EnableSsl = true
            };

            var msg2 = new MailMessage("nakopnetesvojiskolu@gmail.com", "nakopnetesvojiskolu@gmail.com", Subject, Body)
            {
                IsBodyHtml = true
            };

            foreach (var participant in participantsPart2)
            {
                msg2.Bcc.Add(participant.Email);
            }

            smtpClient2.Send(msg2);


            var smtpClient3 = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential("nakopnetesvojiskolu@gmail.com", "KonfSysEmail123"),
                EnableSsl = true
            };

            var msg3 = new MailMessage("nakopnetesvojiskolu@gmail.com", "nakopnetesvojiskolu@gmail.com", Subject, Body)
            {
                IsBodyHtml = true
            };

            foreach (var participant in participantsPart3)
            {
                msg3.Bcc.Add(participant.Email);
            }

            smtpClient3.Send(msg3);

            Context.RedirectToRoute("EmailNow");
        }
    }
}

