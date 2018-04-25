using System;
using ConferencySystem.BL.Services;
using System.Net.Mail;
using System.Net;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services.UserManagment;
using ConferencySystem.DAL.Data;
using ConferencySystem.DAL.Data.UserIdentity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace ConferencySystem.ViewModels
{
    public class PwdForgottenViewModel : MainMasterPageViewModel
    {
        [Required(ErrorMessage = "Vyplňte svůj email - pole \"Email\" je povinné.")]
        public string Email { get; set; }

        public string ErrorMessage { get; set; }

        public override Task PreRender()
        {
            if (IsAuthenticated)
            {
                Context.RedirectToRoute("Default");
            }
            return base.PreRender();
        }

        public void ForgotPassword(AppUserDTO dataPerson)
        {
            var provider = new DpapiDataProtectionProvider("YourAppName");

            var appUserManager = new AppUserManager(new DbContext());

            appUserManager.UserTokenProvider = new DataProtectorTokenProvider<AppUser, int>(provider.Create("UserToken"));

            var user = appUserManager.FindByNameAsync(dataPerson.Email);
            //if (user == null || !(await appUserManager.IsEmailConfirmedAsync(user.Id)))
            //{
            //    // Don't reveal that the user does not exist or is not confirmed
            //    return View("ForgotPasswordConfirmation");
            //}

            string code = appUserManager.GeneratePasswordResetToken(user.Id);
            var callbackUrl = new Uri(string.Format("{0}?userId={1}&code={2}", dataPerson.Email, dataPerson.Id, HttpUtility.UrlEncode(code)));
            appUserManager.SendEmail(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
        }

        public void SendPwdByEmail(AppUserDTO dataPerson)
        {
            string mailBodyhtml =
                "<p>Zaznamenali jsme požadavek na připomenutí hesla ke Konferenčnímu systému festivalu vzdělání Nakopněte svoji školu (<a href=\"http://nakopnetesvojiskolu.azurewebsites.net/\">http://nakopnetesvojiskolu.azurewebsites.net/</a>).</p><br><p>Email: " + dataPerson.Email + "</p><p><b>Heslo: " + dataPerson.PasswordHash + "</b></p><br>Pro získání aktuálních informací můžete sledovat náš <a href=\"http://www.nakopnetesvojiskolu.cz/\" target=\"_blank\">web</a> nebo <a href=\"https://www.facebook.com/nakopnete.svoji.skolu/\" target=\"_blank\">Facebook</a>.";
                    
            var msg = new MailMessage("pronajem@zamecke-navrsi.cz", dataPerson.Email, "Nakopnìte svoji školu - Zaslání hesla", mailBodyhtml);
            
            msg.IsBodyHtml = true;
            var smtpClient = new SmtpClient("smtp.gmail.com", 587); //if your from email address is "from@hotmail.com" then host should be "smtp.hotmail.com"**
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new NetworkCredential("pronajem@zamecke-navrsi.cz", "pronajem49");
            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);
        }

        public void RequestPwd()
        {
            var loginService = new LoginService();
            AppUserDTO person = loginService.GetPerson(Email);

            if(person == null)
            {
                ErrorMessage = "Zadaný email nebyl nalezen. Zkontrolujte, zda jste při registraci nezadali jiný email.";
            }
            else
            {
                ForgotPassword(person);
                //SendPwdByEmail(Person);
                //Context.RedirectToRoute("Login");
            }
        }
    }
}

