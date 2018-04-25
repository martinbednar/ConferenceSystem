using System;
using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.Framework.Runtime.Filters;

namespace ConferencySystem.ViewModels.Admin
{
    [Authorize(Roles = new[] { "admin", "super" })]
    public class PersonViewModel : MainMasterPageViewModel
    {
        public AppUserDTO DataPerson { get; set; }

        public OrganizationDTO DataOrganization { get; set; }

        public string SelectedBirthYear { get; set; }
        public string SelectedBirthMonth { get; set; }
        public string SelectedBirthDay { get; set; }

        /* BirthDate processing */
        public DateProcessing DateProcessing { get; set; }

        public string PositionOther { get; set; }

        public int? OrganizationId
        {
            get { return Convert.ToInt32(Context.Parameters["OrgId"]); }
        }

        public int? PersonId
        {
            get { return Convert.ToInt32(Context.Parameters["PersonId"]); }
        }
        
        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                var adminService = new AdminService();
                if (PersonId != null) DataPerson = adminService.GetPerson(PersonId.Value);
                if (OrganizationId != null) DataOrganization = adminService.GetOrganization(OrganizationId.Value);

                DateProcessing = new DateProcessing();

                if (DataPerson.BirthDate != null)
                {
                    SelectedBirthYear = DataPerson.BirthDate.ToString().Substring(6,4);
                    SelectedBirthMonth = DateProcessing.MonthFromDb(DataPerson.BirthDate.ToString().Substring(3, 2));
                    SelectedBirthDay = DateProcessing.DayFromDb(DataPerson.BirthDate.ToString().Substring(0, 2));
                }
                else
                {
                    SelectedBirthYear = "";
                    SelectedBirthMonth = "";
                    SelectedBirthDay = "";
                }
                

                if ((DataPerson.Position != "ředitel") && (DataPerson.Position != "Učitel") && (DataPerson.Position != "Student") && (DataPerson.Position != "Rodič"))
                {
                    PositionOther = DataPerson.Position;
                }
            }

            return base.PreRender();
        }

        public void SavePerson()
        {
            /*Date processing*/
            DataPerson.BirthDate = new DateTime(Int32.Parse(SelectedBirthYear), Int32.Parse(DateProcessing.MonthToDb(SelectedBirthMonth)), Int32.Parse(SelectedBirthDay),0,0,0,0);

            var adminService = new AdminService();
            adminService.SavePerson(DataPerson);
            adminService.SaveOrganization(DataOrganization);

            Context.RedirectToRoute("People");
        }
    }
}

