using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
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
            var emailService = new EmailService();
            var registerService = new RegisterService();

            var participants = registerService.GetUsers();

            foreach (var participant in participants)
            {
                emailService.SendEmail(participant.Email, Subject, Body, null);
            }

            Context.RedirectToRoute("EmailNow");
        }
    }
}

