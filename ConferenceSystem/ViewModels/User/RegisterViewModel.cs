using System.Threading.Tasks;

namespace ConferencySystem.ViewModels.User
{
    public class RegisterViewModel : MainMasterPageViewModel
    {
        public override Task PreRender()
        {
            Context.RedirectToRoute("RegisterForm");

            RegisterActive = "active";
            MainPageActive = "";
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

