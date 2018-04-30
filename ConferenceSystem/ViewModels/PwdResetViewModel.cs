using ConferencySystem.BL.Services;
using DotVVM.Framework.ViewModel;

namespace ConferencySystem.ViewModels
{
    public class PwdResetViewModel : MainMasterPageViewModel
    {
        [FromQuery("token")]
        public string Token { get; set; }

        [FromQuery("userid")]
        public int UserId { get; set; }

        public string NewPassword { get; set; }

        public string SuccessMessage { get; set; }

        public void ResetPassword()
        {
            ResetPasswordService resetPasswordService = new ResetPasswordService();
            resetPasswordService.ResetPassword(UserId,Token,NewPassword);
            SuccessMessage = "Heslo bylo úspěšně obnoveno. Nyní se můžete přihlásit s použitím nového hesla.";
        }
    }
}
