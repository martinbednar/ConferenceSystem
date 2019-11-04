using System;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.Framework.Runtime.Filters;

namespace ConferencySystem.ViewModels.User
{
    public class RegisterFormViewModel : MainMasterPageViewModel
    {
        public string PositionOther { get; set; }

        public bool DisplayLoader { get; set; } = true;

        public bool Alert { get; set; } = false;

        public string AlertValue { get; set; }

        public string IN { get; set; } = "";

        public string PasswordControl { get; set; }

        public DateTime BirthDate;

        /* BirthDate processing */
        public string SelectedBirthDay { get; set; }

        public string SelectedBirthMonth { get; set; }

        public string SelectedBirthYear { get; set; }

        public DateProcessing DateProcessing { get; set; }


        public AppUserDTO DataUser { get; set; }

        public OrganizationDTO DataOrganization { get; set; }

        public InvoiceDTO DataInvoice { get; set; }

        public override Task PreRender()
        {
            /*Context.RedirectToRoute("Register");*/
            if (!Context.IsPostBack)
            {
                DateProcessing = new DateProcessing();
                DataUser = new AppUserDTO();
                DataOrganization = new OrganizationDTO();
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

        private void SendEmail()
        {
            var textService = new TextService();

            EmailService emailService = new EmailService();

            var emailDefault = textService.GetText(7721);

            var emailAlternate = textService.GetText(7722);

            string mailBody;
            string subject;

            if (DataUser.IsAlternate)
            {
                //naplnena kapacita
                mailBody = textService.TranslateText(emailAlternate.Content,DataUser);
                subject = emailAlternate.Subject;
                emailService.SendEmail(DataUser.Email, subject, mailBody, null);
            }
            else
            {
                mailBody = textService.TranslateText(emailDefault.Content,DataUser);
                subject = emailDefault.Subject;
                emailService.SendEmail(DataUser.Email, subject, mailBody, DataInvoice.FileBytes);
            }
        }

        public void DismissAlert()
        {
            Alert = false;
            AlertValue = string.Empty;
        }

        private void RegisterUser()
        {
            var register = new RegisterService();
            var admin = new AdminService();

            int addedUserId = register.AddOrganization(DataOrganization, DataUser);

            DataUser = register.GetUser(addedUserId);
            DataInvoice = admin.GetInvoice(DataUser.Id);
            SendEmail();

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

            if (IN.Length != 0)
            {
                DataOrganization.IN = Convert.ToInt64(IN.Replace(" ", string.Empty));
            }
            else
            {
                DataOrganization.IN = 0;
            }


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
                        if (DataUser.WantCert)
                        {
                            if (DataUser.BirthDate != null)
                            {
                                if (DataUser.BirthPlace.Length > 0)
                                {
                                    RegisterUser();
                                }
                                else
                                {
                                    Alert = true;
                                    AlertValue = "Pro vystavení certifikátu je požadováno Místo narození.";
                                }
                            }
                            else
                            {
                                Alert = true;
                                AlertValue = "Pro vystavení certifikátu je požadováno Datum narození.";
                            }
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
}

