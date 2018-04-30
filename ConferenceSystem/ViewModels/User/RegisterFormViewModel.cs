using System;
using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;

namespace ConferencySystem.ViewModels.User
{
    public class RegisterFormViewModel : MainMasterPageViewModel
    {
        public string PositionOther { get; set; }

        public bool DisplayLoader { get; set; } = true;

        public bool Alert { get; set; } = false;

        public string IN { get; set; } = "";

        public DateTime BirthDate;

        /* BirthDate processing */
        public string SelectedBirthDay { get; set; }

        public string SelectedBirthMonth { get; set; }

        public string SelectedBirthYear { get; set; }

        public DateProcessing DateProcessing { get; set; }


        public AppUserDTO DataPerson { get; set; }

        public OrganizationDTO DataOrganization { get; set; }

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                DateProcessing = new DateProcessing();
                DataPerson = new AppUserDTO();
                DataOrganization = new OrganizationDTO();
            }

            RegisterActive = "active";
            MainPageActive = "";
            AdminActive = "";

            return base.PreRender();
        }

        public void SendEmail()
        {
            string mailBody;

            if (DataPerson.IsAlternate)
            {
                //naplnena kapacita
                mailBody =
                "<p>Vážíme si energie, kterou chcete věnovat  svému vzdělání. Je to skvělá cesta pro lepší školy. Děkujeme.</p><br><p>Aktuálně již byla naplněna kapacita festivalu Nakopněte svoji školu. Byl/a jste zaregistrován/a jako náhradník/náhradnice. Může se však stát, že některý z přihlášených nezašle včas platbu nebo se nakonec nebude moci zúčastnit. V tomto případě bychom se Vám ozvali a zjistili zda Váš zájem o účast stále trvá.</p><br><p>Přesný program ještě ladíme. Můžete sledovat náš <a href=\"http://www.nakopnetesvojiskolu.cz/\" target=\"_blank\">web</a> nebo <a href=\"https://www.facebook.com/nakopnete.svoji.skolu/\" target=\"_blank\">Facebook</a>.</p>";
            }
            else
            {
                mailBody =
                "<p>Vážíme si energie, kterou chcete věnovat  svému vzdělání. Je to skvělá cesta pro lepší školy. Děkujeme.</p><br><p>Místo na konferenci Vám závazně potvrdíme po přijetí platby na náš účet. Pokud platbu neobdržíme do 15 dnů od zaslání této zprávy, bude vaše registrace stornována.</p><br><p>Jméno účastníka: " + DataPerson.TitleBefore + " " + DataPerson.FirstName + " " + DataPerson.LastName + " " + DataPerson.TitleAfter + "</p><br><br><p><u>Údaje k provedení platby</u></p><p>Číslo účtu pro platbu: 257996309 / 0300</p><p>Variabilní symbol: " + DataPerson.VariableSymbol + "</p><p>Částka: 1990,- Kč</p><br><p>V případě, že budete potřebovat pro odeslání platby zálohovou fakturu, napište si o ní prosím na adresu <b>lenka.backova@zamecke-navrsi.cz</b>. V případě, že budete provádět platbu za více osob, nezapomeňte v žádosti uvést celá jména všech osob a čísla variabilních symbolů, která jim byla přidělena při registraci.</p><br><p><u>Přihlašovací údaje</u></p>Níže v emailu naleznete přihlašovací email do Konfereèního systému festivalu (<a href=\"http://konferencnisystem.azurewebsites.net/\">http://konferencnisystem.azurewebsites.net/</a>).</p><p>Přes tento systém bude probíhat registrace jednotlivých workshopů a výběr jídel.</p><br><p>Email: " + DataPerson.Email + "</p><br><p>Přesný program ještě ladíme. Všechny informace vám včas zašleme.Můžete sledovat náš <a href=\"http://www.nakopnetesvojiskolu.cz/\" target=\"_blank\">web</a> nebo <a href=\"https://www.facebook.com/nakopnete.svoji.skolu/\" target=\"_blank\">Facebook</a>. Nezapomeňte si také včas rezervovat ubytování. Více informací najdete na webu.</p><br><p>Těšíme se na Vás v Litomyšli.</p>";
            }

            string subject = "Nakopněte svoji školu - Potvrzení registrace";

            EmailService emailService = new EmailService();

            emailService.SendEmail(DataPerson.Email, subject, mailBody);
        }

        public void Save()
        {
            DisplayLoader = false;

            if ((SelectedBirthDay == "") || (SelectedBirthMonth == "") || (SelectedBirthYear == ""))
            {
                DataPerson.BirthDate = null;
            }
            else
            {
                SelectedBirthDay = DateProcessing.DayToDb(SelectedBirthDay);

                SelectedBirthMonth = DateProcessing.MonthToDb(SelectedBirthMonth);

                if (DateTime.TryParse(SelectedBirthYear + "-" + SelectedBirthMonth + "-" + SelectedBirthDay + " 00:00:00.000", out BirthDate))
                {
                    DataPerson.BirthDate = BirthDate;
                }
                else
                {
                    DataPerson.BirthDate = null;
                }

            }

            //nastaveni informaci o zaplaceni
            DataPerson.PaidDate = null;

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

            foreach (AppUserDTO registeredPerson in register.GetPersonEmails())
            {
                if (registeredPerson.Email == DataPerson.Email)
                {
                    alreadyRegistered = true;
                }
            }
            
            if (alreadyRegistered)
            {
                Alert = true;
            }
            else
            {
                int addedPersonId;

                int organizationId = register.GetOrganizationId(DataOrganization.IN);

                if (organizationId == -1)
                {
                    addedPersonId = register.AddOrganization(DataOrganization, DataPerson);
                }
                else
                {
                    addedPersonId = register.UpdateOrganization(organizationId, DataPerson);
                }

                DataPerson = register.GetPerson(addedPersonId);
                SendEmail();

                Context.RedirectToRoute("RegistrationComplete");
            }
        }
    }
}

