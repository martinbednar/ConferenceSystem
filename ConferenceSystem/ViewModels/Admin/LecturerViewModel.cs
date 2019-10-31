﻿using System;
using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.Framework.Runtime.Filters;

namespace ConferencySystem.ViewModels.Admin
{
    [Authorize(Roles = new[] { "admin", "super" })]
    public class LecturerViewModel : MainMasterPageViewModel
    {
        public AppUserDTO DataUser { get; set; }

        public OrganizationDTO DataOrganization { get; set; }

        public InvoiceDTO DataInvoice { get; set; }

        public string SelectedBirthYear { get; set; }
        public string SelectedBirthMonth { get; set; }
        public string SelectedBirthDay { get; set; }

        /* BirthDate processing */
        public DateProcessing DateProcessing { get; set; }

        public string PositionOther { get; set; }

        public int? UserId
        {
            get { return Convert.ToInt32(Context.Parameters["UserId"]); }
        }
        
        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                var adminService = new AdminService();
                if (UserId != null) DataUser = adminService.GetUser(UserId.Value);
                DataInvoice = adminService.GetInvoice(DataUser.Id);

                DateProcessing = new DateProcessing();

                if (DataUser.BirthDate != null)
                {
                    SelectedBirthYear = DataUser.BirthDate.ToString().Substring(6,4);
                    SelectedBirthMonth = DateProcessing.MonthFromDb(DataUser.BirthDate.ToString().Substring(3, 2));
                    SelectedBirthDay = DateProcessing.DayFromDb(DataUser.BirthDate.ToString().Substring(0, 2));
                }
                else
                {
                    SelectedBirthYear = "";
                    SelectedBirthMonth = "";
                    SelectedBirthDay = "";
                }
                

                if ((DataUser.Position != "ředitel") && (DataUser.Position != "Učitel") && (DataUser.Position != "Student") && (DataUser.Position != "Rodič"))
                {
                    PositionOther = DataUser.Position;
                }
            }

            return base.PreRender();
        }

        private void sendEmail()
        {
            var textService = new TextService();

            EmailService emailService = new EmailService();

            var emailPaidConfirmation = textService.GetText(7723);

            string mailBody = textService.TranslateText(emailPaidConfirmation.Content, DataUser);
            string subject = emailPaidConfirmation.Subject;
            emailService.SendEmail(DataUser.Email, subject, mailBody, null);
        }

        public void SaveUser()
        {
            /*Date processing*/
            if ((SelectedBirthDay == "") || (SelectedBirthMonth == "") || (SelectedBirthYear == ""))
            {
                DataUser.BirthDate = null;
            }
            else
            {
                DataUser.BirthDate = new DateTime(Int32.Parse(SelectedBirthYear), Int32.Parse(DateProcessing.MonthToDb(SelectedBirthMonth)), Int32.Parse(SelectedBirthDay),0,0,0,0);
            }


            var adminService = new AdminService();

            if (UserId != null)
            {
                var dataUserOld = adminService.GetUser(UserId.Value);
                if (dataUserOld.PaidDate == null && DataUser.PaidDate != null)
                {
                    sendEmail();
                }
            }

            adminService.SaveUser(DataUser);
            adminService.SaveOrganization(DataOrganization);

            Context.RedirectToRoute("Users");
        }
    }
}

