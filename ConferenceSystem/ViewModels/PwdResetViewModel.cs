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
        public string ErrorMessage { get; set; }

        public void ResetPassword()
        {
            if (NewPassword.Length < 6)
            {
                ErrorMessage = "Délka hesla musí být minimálně 6 znaků.";
            }
            else
            {
                ErrorMessage = string.Empty;
                ErrorMessage = null;
                ResetPasswordService resetPasswordService = new ResetPasswordService();
                var res = resetPasswordService.ResetPassword(UserId, Token, NewPassword);
                if (res.Succeeded )
                {
                    SuccessMessage = "Heslo bylo úspěšně obnoveno. Nyní se můžete přihlásit s použitím nového hesla.";
                }
                else
                {
                    ErrorMessage = "Délka hesla musí být minimálně 6 znaků.";
                }
            }
        }
    }
}
