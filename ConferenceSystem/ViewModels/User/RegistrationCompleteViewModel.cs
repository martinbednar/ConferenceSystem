using System.Threading.Tasks;

namespace ConferencySystem.ViewModels.User
{
    public class RegistrationCompleteViewModel : MainMasterPageViewModel
    {
        public override Task PreRender()
        {
            RegisterActive = "active";
            MainPageActive = "";
            AdminActive = "";
            CarteringActive = "";
            WorkshopsActive = "";
            LecturerInfoActive = "";
            LoginActive = "";

            return base.PreRender();
        }
    }
}

