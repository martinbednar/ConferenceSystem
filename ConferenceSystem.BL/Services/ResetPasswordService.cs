using System;
using System.Web;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services.UserManagment;
using ConferencySystem.DAL.Data;
using ConferencySystem.DAL.Data.UserIdentity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace ConferencySystem.BL.Services
{
    public class ResetPasswordService
    {
        public void ResetPassword(int userId, string token, string newPassword)
        {
            AppUserManager userManager = new AppUserManager(new DbContext());
            var provider = new DpapiDataProtectionProvider("KonferencniSystem");
            //userManager.UserTokenProvider = new DataProtectorTokenProvider<AppUser, int>(provider.Create("EmailReset"));
            userManager.UserTokenProvider = new EmailTokenProvider<AppUser, int>();
            userManager.ResetPassword(userId, token, newPassword);
        }

        public void SendResetPasswordEmail(AppUserDTO dataUser)
        {
            EmailService emailService = new EmailService();

            var provider = new DpapiDataProtectionProvider("KonferencniSystem");
            var appUserManager = new AppUserManager(new DbContext());
            //appUserManager.UserTokenProvider = new DataProtectorTokenProvider<AppUser, int>(provider.Create("EmailReset"));
            appUserManager.UserTokenProvider = new EmailTokenProvider<AppUser, int>();

            string token = appUserManager.GeneratePasswordResetToken(dataUser.Id);
            var callbackUrl = new Uri(string.Format(HttpContext.Current.Request.Url + "/reset?userid={0}&token={1}", dataUser.Id, HttpUtility.UrlEncode(token)));

            string mailBodyhtml =
                "<p>Zaznamenali jsme požadavek na obnovení hesla ke Konferenčnímu systému festivalu vzdělání Nakopněte svoji školu (<a href=\"http://nakopnetesvojiskolu.azurewebsites.net/\">http://nakopnetesvojiskolu.azurewebsites.net/</a>).</p><br><p>Pro obnovení hesla klikněte na odkaz:</p><br><p><a href=\"" + callbackUrl + "\">OBNOVIT HESLO</a></p><br><p>Pokud se Vám nedaří na odkaz kliknout, překopírujte do adresního řádku prohlížeče následující adresu: <a href=\"" + callbackUrl + "\">" + callbackUrl + "</a></p><br>Pro získání aktuálních informací můžete sledovat náš <a href=\"http://www.nakopnetesvojiskolu.cz/\" target=\"_blank\">web</a> nebo <a href=\"https://www.facebook.com/nakopnete.svoji.skolu/\" target=\"_blank\">Facebook</a>.";
            string subject = "Nakopněte svoji školu - Resetování hesla";

            emailService.SendEmail(dataUser.Email, subject, mailBodyhtml, null);
        }
    }
}