using System.Threading.Tasks;

namespace ConferencySystem.ViewModels.User
{
    public class RegisterViewModel : MainMasterPageViewModel
    {
        public override Task PreRender()
        {
            Context.RedirectToRoute("RegisterForm");

            return base.PreRender();
        }
    }
}

