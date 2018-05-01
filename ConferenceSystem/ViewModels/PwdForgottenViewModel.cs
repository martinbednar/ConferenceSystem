using System;
using ConferencySystem.BL.Services;
using System.Net.Mail;
using System.Net;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services.UserManagment;
using ConferencySystem.DAL.Data;
using ConferencySystem.DAL.Data.UserIdentity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace ConferencySystem.ViewModels
{
    public class PwdForgottenViewModel : MainMasterPageViewModel
    {
        [Required(ErrorMessage = "Vyplňte svůj email - pole \"Email\" je povinné.")]
        public string Email { get; set; }

        public string ErrorMessage { get; set; }

        public string SuccessMessage { get; set; }

        public override Task PreRender()
        {
            if (IsAuthenticated)
            {
                Context.RedirectToRoute("Default");
            }
            return base.PreRender();
        }

        public void RequestPwd()
        {
            var loginService = new LoginService();
            AppUserDTO user = loginService.GetUser(Email);

            if(user == null)
            {
                ErrorMessage = "Zadaný email nebyl nalezen. Zkontrolujte, zda jste při registraci nezadali jiný email.";
            }
            else
            {
                ResetPasswordService resetPasswordService = new ResetPasswordService();
                resetPasswordService.SendResetPasswordEmail(user);
                SuccessMessage = "Odkaz pro obnovení hesla jsme Vám zaslali na zadaný email.";
            }
        }
    }
}

