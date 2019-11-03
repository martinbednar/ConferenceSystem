using DotVVM.Framework.Runtime.Filters;
using System.Threading.Tasks;

namespace ConferencySystem.ViewModels
{
    [Authorize]
    public class DefaultViewModel : MainMasterPageViewModel
    {
        public override Task PreRender()
        {
            RegisterActive = "";
            MainPageActive = "active";
            AdminActive = "";
            CarteringActive = "";
            WorkshopsActive = "";
            LecturerInfoActive = "";
            LoginActive = "";

            return base.PreRender();
        }
    }
}