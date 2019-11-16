using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime.Filters;

namespace ConferencySystem.ViewModels.User
{
    [Authorize(Roles = new[] { "user" })]
    public class ProfileViewModel : MainMasterPageViewModel
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

        public bool SaveConfirmationIsVisible { get; set; } = false;

        public bool SaveConfirmationDismissed { get; set; } = false;

        public int CurrentUserId
        {
            get
            {
                return Int32.Parse(Context.GetOwinContext().Authentication.User.Claims.Where(c => c.Type == ClaimTypes.Sid)
                 .Select(c => c.Value).SingleOrDefault());
            }
        }

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                var adminService = new AdminService();
                DataUser = adminService.GetUser(CurrentUserId);
                DataOrganization = adminService.GetOrganization(DataUser.Organization.Id);
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

            RegisterActive = "";
            MainPageActive = "";
            AdminActive = "";
            CarteringActive = "";
            WorkshopsActive = "";
            LecturerInfoActive = "";
            LoginActive = "";
            ProfileActive = "active";
            LecturerProgramsActive = "";

            return base.PreRender();
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


            var profileService = new ProfileService();
            
            profileService.SaveUser(DataUser);

            SaveConfirmationDismissed = false;
            SaveConfirmationIsVisible = true;
        }
    }
}

