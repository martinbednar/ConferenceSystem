using DotVVM.Framework.Runtime.Filters;
using System.Threading.Tasks;

namespace ConferencySystem.ViewModels
{
    [Authorize]
    public class DefaultViewModel : MainMasterPageViewModel
    {
        public override Task PreRender()
        {
            if (IsParticipant) Context.RedirectToRoute("Profile");
            if (IsLecturer) Context.RedirectToRoute("LecturerInfo");
            if (IsAdmin || IsSuperAdmin) Context.RedirectToRoute("Users");

            RegisterActive = "";
            MainPageActive = "active";
            AdminActive = "";
            CarteringActive = "";
            WorkshopsActive = "";
            LecturerInfoActive = "";
            LoginActive = "";
            ProfileActive = "";
            LecturerProgramsActive = "";

            return base.PreRender();
        }
    }
}