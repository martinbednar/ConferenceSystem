﻿using System.ComponentModel.DataAnnotations;
using DotVVM.Framework.Hosting;
using ConferencySystem.BL.Services;
using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using Microsoft.AspNet.Identity;

namespace ConferencySystem.ViewModels
{
    public class LoginViewModel : MainMasterPageViewModel
    {
        [Required(ErrorMessage = "Vyplňte svůj email - pole \"Email\" je povinné.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vyplňte svoje heslo - pole \"Heslo\" je povinné.")]
        public string Password { get; set; }

        public string LoginErrorMessage { get; set; }

        public override Task PreRender()
        {
            if (IsAuthenticated)
            {
                Context.RedirectToRoute("Default");
            }
            
            RegisterActive = "";
            MainPageActive = "";
            AdminActive = "";
            CarteringActive = "";
            WorkshopsActive = "";
            LecturerInfoActive = "";
            LoginActive = "active";
            ProfileActive = "";
            LecturerProgramsActive = "";

            return base.PreRender();
        }

        public void SignIn()
        {
            LoginService loginService = new LoginService();

            AppUserDTO user = loginService.GetUser(Email);

            if (user == null)
            {
                LoginErrorMessage = "Chybně zadán přihlašovací email. Uživatel nebyl nalezen.";
            }
            else
            {
                PasswordHasher passwordHasher = new PasswordHasher();

                PasswordVerificationResult passwordVerificationResult = passwordHasher.VerifyHashedPassword(user.PasswordHash, Password);

                if (passwordVerificationResult == PasswordVerificationResult.Success)
                {
                    var identity = loginService.Login(Email, Password);

                    Context.GetOwinContext().Authentication.SignIn(identity);

                    Context.RedirectToRoute("Default");
                }
                else
                {
                    LoginErrorMessage = "Chybně zadané heslo.";
                }
            }
        }
    }
}