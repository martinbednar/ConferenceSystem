using System.Linq;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;
using System.Security.Claims;

namespace ConferencySystem.ViewModels
{
    public class MainMasterPageViewModel : DotvvmViewModelBase
    {
        public string RegisterActive { get; set; }
        public string MainPageActive { get; set; }
        public string AdminActive { get; set; }
        public string CarteringActive { get; set; }
        public string WorkshopsActive { get; set; }
        public string LecturerInfoActive { get; set; }
        public string LoginActive { get; set; }

        public string CurrentUserName {
            get { return Context.GetOwinContext().Authentication.User.Claims.Where(c => c.Type == ClaimTypes.GivenName)
                .Select(c => c.Value).SingleOrDefault(); }
         }

        public bool IsAuthenticated
        {
            get { return Context.GetOwinContext().Authentication.User.Identity.IsAuthenticated; }
        }

        public bool IsAdmin
        {
            get { return Context.GetOwinContext().Authentication.User.IsInRole("admin"); }
        }

        public bool IsSuperAdmin
        {
            get { return Context.GetOwinContext().Authentication.User.IsInRole("super"); }
        }

        public bool IsParticipant
        {
            get { return Context.GetOwinContext().Authentication.User.IsInRole("user"); }
        }


        public bool IsLecturer
        {
            get { return Context.GetOwinContext().Authentication.User.IsInRole("lecturer"); }
        }

        public void SignOut()
        {
            Context.GetAuthentication().SignOut();
            Context.RedirectToRoute("Default");
        }
    }
}

