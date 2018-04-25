using System;
using System.Net;
using System.Net.Mail;
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
        public string[] BirthDay { get; set; } = { "", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };

        public string SelectedBirthDay { get; set; }

        public string[] BirthMonth { get; set; } = { "", "Leden", "Únor", "Březen", "Duben", "Květen", "Červen", "Červenec", "Srpen", "Září", "Říjen", "Listopad", "Prosinec" };

        public string SelectedBirthMonth { get; set; }

        public string[] BirthYear { get; set; } = { "", "1920", "1921", "1922", "1923", "1924", "1925", "1926", "1927", "1928", "1929", "1930", "1931", "1932", "1933", "1934", "1935", "1936", "1937", "1938", "1939", "1940", "1941", "1942", "1943", "1944", "1945", "1946", "1947", "1948", "1949", "1950", "1951", "1952", "1953", "1954", "1955", "1956", "1957", "1958", "1959", "1960", "1961", "1962", "1963", "1964", "1965", "1966", "1967", "1968", "1969", "1970", "1971", "1972", "1973", "1974", "1975", "1976", "1977", "1978", "1979", "1980", "1981", "1982", "1983", "1984", "1985", "1985", "1986", "1987", "1988", "1989", "1990", "1991", "1992", "1993", "1994", "1995", "1996", "1997", "1998", "1999", "2000", "2001", "2002", "2003", "2004", "2005", "2006", "2007", "2008", "2009", "2010", "2011", "2012", "2013", "2014", "2015", "2016", "2017" };

        public string SelectedBirthYear { get; set; }


        public AppUserDTO DataPerson { get; set; }

        public OrganizationDTO DataOrganization { get; set; }

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
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
            string mailBodyhtml;

            if (DataPerson.IsAlternate)
            {
                //naplnena kapacita
                mailBodyhtml =
                "<p>Vážíme si energie, kterou chcete vìnovat  svému vzdìlání. Je to skvìlá cesta pro lepší školy. Dìkujeme.</p><br><p>Aktuálnì již byla naplnìna kapacita festivalu Nakopnìte svoji školu. Byl/a jste zaregistrován/a jako náhradník/náhradnice. Mùže se však stát, že nìkterý z pøihlášených nezašle vèas platbu nebo se nakonec nebude moci zúèastnit. V tomto pøípadì bychom se Vám ozvali a zjistili zda Váš zájem o úèast stále trvá.</p><br><p>Pøesný program ještì ladíme. Mùžete sledovat náš <a href=\"http://www.nakopnetesvojiskolu.cz/\" target=\"_blank\">web</a> nebo <a href=\"https://www.facebook.com/nakopnete.svoji.skolu/\" target=\"_blank\">Facebook</a>.</p>";
            }
            else
            {
                mailBodyhtml =
                "<p>Vážíme si energie, kterou chcete vìnovat  svému vzdìlání. Je to skvìlá cesta pro lepší školy. Dìkujeme.</p><br><p>Místo na konferenci Vám závaznì potvrdíme po pøijetí platby na náš úèet. Pokud platbu neobdržíme do 15 dnù od zaslání této zprávy, bude vaše registrace stornována.</p><br><p>Jméno úèastníka: " + DataPerson.TitleBefore + " " + DataPerson.FirstName + " " + DataPerson.LastName + " " + DataPerson.TitleAfter + "</p><br><br><p><u>Údaje k provedení platby</u></p><p>Èíslo úètu pro platbu: 257996309 / 0300</p><p>Variabilní symbol: " + DataPerson.VariableSymbol + "</p><p>Èástka: 1990,- Kè</p><br><p>V pøípadì, že budete potøebovat pro odeslání platby zálohovou fakturu, napište si o ní prosím na adresu <b>lenka.backova@zamecke-navrsi.cz</b>. V pøípadì, že budete provádìt platbu za více osob, nezapomeòte v žádosti uvést celá jména všech osob a èísla variabilních symbolù, která jim byla pøidìlena pøi registraci.</p><br><p><u>Pøihlašovací údaje</u></p>Níže v emailu naleznete pøihlašovací údaje do Konfereèního systému festivalu (<a href=\"http://nakopnetesvojiskolu.azurewebsites.net/\">http://nakopnetesvojiskolu.azurewebsites.net/</a>).</p><p>Pøes tento systém bude probíhat registrace jednotlivých workshopù a vybìr jídel.</p><br><p>Email: " + DataPerson.Email + "</p><p>Heslo: (heslo, které jste zadal/zadala pøi registraci)</p><br><p>Pøesný program ještì ladíme.Všechny informace vám vèas zašleme.Mùžete sledovat náš <a href=\"http://www.nakopnetesvojiskolu.cz/\" target=\"_blank\">web</a> nebo <a href=\"https://www.facebook.com/nakopnete.svoji.skolu/\" target=\"_blank\">Facebook</a>. Nezapomeòte si také vèas rezervovat ubytování. Více informací najdete na webu.</p><br><p>Tìšíme se na Vás v Litomyšli.</p>";
            }
            
            var msg = new MailMessage("pronajem@zamecke-navrsi.cz", DataPerson.Email, "Nakopnìte svoji školu - Potvrzení registrace", mailBodyhtml);
            msg.IsBodyHtml = true;
            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential("pronajem@zamecke-navrsi.cz", "pronajem49"),
                EnableSsl = true
            }; //if your from email address is "from@hotmail.com" then host should be "smtp.hotmail.com"**
            smtpClient.Send(msg);
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
                if (SelectedBirthDay.Length < 2)
                {
                    SelectedBirthDay = "0" + SelectedBirthDay;
                }

                switch (SelectedBirthMonth)
                {
                    case "Leden":
                        SelectedBirthMonth = "01";
                        break;
                    case "Únor":
                        SelectedBirthMonth = "02";
                        break;
                    case "Bøezen":
                        SelectedBirthMonth = "03";
                        break;
                    case "Duben":
                        SelectedBirthMonth = "04";
                        break;
                    case "Kvìten":
                        SelectedBirthMonth = "05";
                        break;
                    case "Èerven":
                        SelectedBirthMonth = "06";
                        break;
                    case "Èervenec":
                        SelectedBirthMonth = "07";
                        break;
                    case "Srpen":
                        SelectedBirthMonth = "08";
                        break;
                    case "Záøí":
                        SelectedBirthMonth = "09";
                        break;
                    case "Øíjen":
                        SelectedBirthMonth = "10";
                        break;
                    case "Listopad":
                        SelectedBirthMonth = "11";
                        break;
                    case "Prosinec":
                        SelectedBirthMonth = "12";
                        break;
                    default:
                        SelectedBirthMonth = "";
                        break;
                }
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

            /*foreach (PersonDTO RegisteredPerson in Register.GetPersonEmails())
            {
                if (RegisteredPerson.Email == DataPerson.Email)
                {
                    AlreadyRegistered = true;
                }
            }*/
            
            if (alreadyRegistered)
            {
                Alert = true;
            }
            else
            {
                int organizationId = register.GetOrganizationId(DataOrganization.IN);

                if (organizationId == -1)
                {
                    register.AddOrganization(DataOrganization, DataPerson);
                }
                else
                {
                    register.UpdateOrganization(organizationId, DataPerson);
                }

                //SendEmail();

                Context.RedirectToRoute("RegistrationComplete");
            }
        }
    }
}

