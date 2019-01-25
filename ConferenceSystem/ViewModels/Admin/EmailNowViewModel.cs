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
            var textService = new TextService();

            var participantsPart1 = registerService.GetUsers().Where(p => p.Id <= 100);
            var participantsPart2 = registerService.GetUsers().Where(p => p.Id > 100);

            string translatedBody;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential("nakopnetesvojiskolu@gmail.com", "KonfSysEmail123"),
                EnableSsl = true
            };

            foreach (var participant in participantsPart1)
            {
                translatedBody = textService.TranslateText(Body, participant);


                /*******  Send email  ********/
                var msg = new MailMessage("nakopnetesvojiskolu@gmail.com", participant.Email, Subject, translatedBody)
                {
                    IsBodyHtml = true
                };

                smtpClient.Send(msg);
                /***************/
            }

            foreach (var participant in participantsPart2)
            {
                translatedBody = textService.TranslateText(Body, participant);


                /*******  Send email  ********/
                var msg = new MailMessage("nakopnetesvojiskolu@gmail.com", participant.Email, Subject, translatedBody)
                {
                    IsBodyHtml = true
                };

                smtpClient.Send(msg);
                /***************/
            }

            Context.RedirectToRoute("EmailNow");
        }
    }
}

