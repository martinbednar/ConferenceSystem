using System;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime.Filters;
using Microsoft.AspNet.Identity;

namespace ConferencySystem.ViewModels.Visitor
{
    public class RegisterFormVisitorViewModel : MainMasterPageViewModel
    {
        public bool DisplayLoader { get; set; } = true;

        public bool Alert { get; set; } = false;

        public string AlertValue { get; set; }

        public string PasswordControl { get; set; }

        public AppUserDTO DataUser { get; set; }

        public override Task PreRender()
        {
            /*Context.RedirectToRoute("Register");*/
            if (!Context.IsPostBack)
            {
                DataUser = new AppUserDTO() { PhoneNumber="0" };
            }

            RegisterActive = "active";
            MainPageActive = "";
            AdminActive = "";
            CarteringActive = "";
            WorkshopsActive = "";
            LecturerInfoActive = "";
            LoginActive = "";
            ProfileActive = "";

            return base.PreRender();
        }

        public void DismissAlert()
        {
            Alert = false;
            AlertValue = string.Empty;
        }

        private void RegisterUser()
        {
            var registerVisitor = new RegisterVisitorService();
            var register = new RegisterService();
            var admin = new AdminService();

            string password = DataUser.PasswordHash;

            int addedUserId = registerVisitor.AddUser(DataUser);

            DataUser = register.GetUser(addedUserId);

            LoginService loginService = new LoginService();

            PasswordHasher passwordHasher = new PasswordHasher();

            PasswordVerificationResult passwordVerificationResult = passwordHasher.VerifyHashedPassword(DataUser.PasswordHash, password);

            var identity = loginService.Login(DataUser.Email, password);

            Context.GetOwinContext().Authentication.SignIn(identity);

            Context.RedirectToRoute("CarteringSelect");
        }

        public void Save()
        {
            DisplayLoader = false;

            DataUser.BirthDate = null;
            DataUser.PaidDate = null;
            DataUser.PhoneNumber = "0";

            var register = new RegisterService();

            bool alreadyRegistered = false;

            foreach (AppUserDTO registeredUser in register.GetUserEmails())
            {
                if (registeredUser.Email == DataUser.Email)
                {
                    alreadyRegistered = true;
                }
            }
            
            if (alreadyRegistered)
            {
                Alert = true;
                AlertValue = "Chyba při registraci - email " + DataUser.Email +
                             " byl již jednou při registraci použit! Jestliže jste se již zaregistroval/a, použijte prosím přihlašovací údaje pro přístup do Konferenčního systému.";
            }
            else
            {
                if (DataUser.PasswordHash != PasswordControl)
                {
                    Alert = true;
                    AlertValue = "Chyba kontroly hesla. Zadaná hesla se neshodují";
                    DataUser.PasswordHash = string.Empty;
                    PasswordControl = string.Empty;
                }
                else
                {
                    if (DataUser.PasswordHash.Length < 6)
                    {
                        Alert = true;
                        AlertValue = "Heslo musí mít délku minimálně 6 znaků.";
                        DataUser.PasswordHash = string.Empty;
                        PasswordControl = string.Empty;
                    }
                    else
                    {
                        RegisterUser();
                    }
                }
            }
        }
    }
}

