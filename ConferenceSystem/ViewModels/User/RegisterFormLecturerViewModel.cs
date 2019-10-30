using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.Framework.ViewModel;

namespace ConferencySystem.ViewModels.User
{
    public class RegisterFormLecturerViewModel : ConferencySystem.ViewModels.MainMasterPageViewModel
    {
        public string PositionOther { get; set; }

        public bool DisplayLoader { get; set; } = true;

        public bool Alert { get; set; } = false;

        public string AlertValue { get; set; }

        public string PasswordControl { get; set; }

        public DateTime BirthDate;

        /* BirthDate processing */
        public string SelectedBirthDay { get; set; }

        public string SelectedBirthMonth { get; set; }

        public string SelectedBirthYear { get; set; }

        public DateProcessing DateProcessing { get; set; }


        public AppUserDTO DataUser { get; set; }

        public override Task PreRender()
        {
            /*Context.RedirectToRoute("Register");*/
            if (!Context.IsPostBack)
            {
                DateProcessing = new DateProcessing();
                DataUser = new AppUserDTO();
            }

            RegisterActive = "active";
            MainPageActive = "";
            AdminActive = "";

            return base.PreRender();
        }

        public void DismissAlert()
        {
            Alert = false;
            AlertValue = string.Empty;
        }

        private void RegisterUser()
        {
            var registerLecturer = new RegisterLecturerService();
            var register = new RegisterService();
            var admin = new AdminService();

            int addedUserId = registerLecturer.AddUser(DataUser);

            DataUser = register.GetUser(addedUserId);

            Context.RedirectToRoute("RegistrationComplete");
        }

        public void Save()
        {
            DisplayLoader = false;

            if ((SelectedBirthDay == "") || (SelectedBirthMonth == "") || (SelectedBirthYear == ""))
            {
                DataUser.BirthDate = null;
            }
            else
            {
                SelectedBirthDay = DateProcessing.DayToDb(SelectedBirthDay);

                SelectedBirthMonth = DateProcessing.MonthToDb(SelectedBirthMonth);

                if (DateTime.TryParse(SelectedBirthYear + "-" + SelectedBirthMonth + "-" + SelectedBirthDay + " 00:00:00.000", out BirthDate))
                {
                    DataUser.BirthDate = BirthDate;
                }
                else
                {
                    DataUser.BirthDate = null;
                }

            }

            //nastaveni informaci o zaplaceni
            DataUser.PaidDate = null;

            //nastaveni pozice
            DataUser.Position = null;


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

