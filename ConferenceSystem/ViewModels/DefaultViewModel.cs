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

            RegisterActive = "";
            MainPageActive = "active";
            AdminActive = "";
            CarteringActive = "";
            WorkshopsActive = "";
            LecturerInfoActive = "";
            LoginActive = "";
            ProfileActive = "";

            return base.PreRender();
        }
    }
}