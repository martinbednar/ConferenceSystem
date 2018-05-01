using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using ConferencySystem.DAL.Data;
using DotVVM.BusinessPack.Controls;
using DotVVM.BusinessPack.Export.Excel;
using DotVVM.Framework.Runtime.Filters;

namespace ConferencySystem.ViewModels.Admin
{
    [Authorize(Roles = new[] { "admin", "super" })]
    public class UsersViewModel : MainMasterPageViewModel
    {
        public GridViewUserSettings UserSettings { get; set; }

        public int TotalUsersCount { get; set; }

        public bool AllColumns { get; set; } = false;
        public bool InvoiceColumns { get; set; } = false;
        public bool OverviewColumns { get; set; } = true;
        public bool AllColumnsEnabled { get; set; } = true;
        public bool InvoiceColumnsEnabled { get; set; } = true;
        public bool OverviewEnabled { get; set; } = false;

        public BusinessPackDataSet<AppUserDTO> Users { get; set; } = new BusinessPackDataSet<AppUserDTO>();

        public override Task PreRender()
        {
            Users.PagingOptions.PageSize = 500;
            Users.SortingOptions.SortExpression = nameof(AppUserDTO.Id);
            Users.SortingOptions.SortDescending = false;

            var adminService = new AdminService();
            adminService.GetUsers(Users);

            TotalUsersCount = Users.PagingOptions.TotalItemsCount;

            RegisterActive = "";
            MainPageActive = "";
            AdminActive = "active";

            return base.PreRender();
        }

        public void ShowAllColumns()
        {
            AllColumns = true;
            InvoiceColumns = true;
            OverviewColumns = true;

            OverviewEnabled = true;
            InvoiceColumnsEnabled = true;
            AllColumnsEnabled = false;
        }

        public void ShowInvoiceColumns()
        {
            AllColumns = false;
            InvoiceColumns = true;
            OverviewColumns = false;

            OverviewEnabled = true;
            InvoiceColumnsEnabled = false;
            AllColumnsEnabled = true;
        }

        public void ShowOverview()
        {
            AllColumns = false;
            InvoiceColumns = false;
            OverviewColumns = true;

            OverviewEnabled = false;
            InvoiceColumnsEnabled = true;
            AllColumnsEnabled = true;
        }

        public void DeleteUser(int id)
        {
            var adminService = new AdminService();
            adminService.DeleteUser(id);
        }

        public void SendEmails()
        {
            var loginService = new LoginService();

            List<AppUserDTO> allUsers = loginService.GetAllUsers();

            string MailBodyhtml;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(Secrets.GmailZN, Secrets.GmailZNPwd),
                EnableSsl = true
            }; //if your from email address is "from@hotmail.com" then host should be "smtp.hotmail.com"**

            foreach (AppUserDTO user in allUsers)
            {
                MailBodyhtml =
                "<p>V�en� ��astn�ku/ V�en� ��astnice festivalu vzd�l�n� Nakopn�te svoji �kolu,</p><p>dnes <b>v 16:00</b> se spust� registrace wokshop� a p�edn�ek. Zat�m si m��ete prohl�dnout t�mata, kter� jsou na v�b�r. Tato registrace bude uzav�ena <b>9.2. ve 12:00.</b></p><p>Registraci ur�it� neodkl�dejte, proto�e workshopy maj� omezenou kapacitu a po napln�n� kapacity ji� nebude mo�n� se na workshop p�ihl�sit.</p><p>V ka�d�m �asov�m bloku si vyberte <b>jednu</b> p�edn�ku �i workshop.</p><p>V�b�r m��ete uskute�nit v Konferen�n�m syst�mu, p�es kter� prob�hala registrace (<a href=\"http://nakopnetesvojiskolu.azurewebsites.net/workshop\" target=\"_blank\">http://nakopnetesvojiskolu.azurewebsites.net/workshop</a>).</p><p>P�ipom�n�me je�t� v�b�r stravov�n�, kter� je otev�eno <b>do 8.2. do 12:00</b>.<p><p>T��me se na V�s.</p><br><p>M��ete sledovat n� <a href=\"http://www.nakopnetesvojiskolu.cz/\" target=\"_blank\">web</a> nebo <a href=\"https://www.facebook.com/nakopnete.svoji.skolu/\" target=\"_blank\">Facebook</a>.</p>";

                var msg = new MailMessage(Secrets.GmailZN, user.Email, "Nakopn�te svoji �kolu - registrace workshop�", MailBodyhtml);
                msg.Bcc.Add(new MailAddress("martinbedn20@seznam.cz"));
                msg.ReplyToList.Add(new MailAddress(Secrets.GmailZN));
                msg.IsBodyHtml = true;
                smtpClient.Send(msg);

                loginService.EmailSent(user.Id);
            }
        }

        public void ExportToExcel()
        {
            var exporter = new GridViewExportExcel<AppUserDTO>(new GridViewExportExcelSettings<AppUserDTO>());
            var gridView = Context.View.FindControlByClientId<GridView>("users", true);

            using (var file = exporter.Export(gridView, Users))
            {
                Context.ReturnFile(file, "NakopneteSvojiSkolu-Ucastnici.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }
    }
}

