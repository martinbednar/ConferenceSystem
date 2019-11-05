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
    public class LecturersViewModel : MainMasterPageViewModel
    {
        public GridViewUserSettings UserSettings { get; set; }

        public int TotalUsersCount { get; set; }

        public bool AllColumns { get; set; } = false;
        public bool InvoiceColumns { get; set; } = false;
        public bool OverviewColumns { get; set; } = true;
        public bool AccomodationColumns { get; set; } = false;
        public bool AllColumnsEnabled { get; set; } = true;
        public bool InvoiceColumnsEnabled { get; set; } = true;
        public bool OverviewEnabled { get; set; } = false;
        public bool AccomodationColumnsEnabled { get; set; } = true;

        public BusinessPackDataSet<AppUserDTO> Users { get; set; } = new BusinessPackDataSet<AppUserDTO>();

        public override Task PreRender()
        {
            Users.PagingOptions.PageSize = 500;
            Users.SortingOptions.SortExpression = nameof(AppUserDTO.Id);
            Users.SortingOptions.SortDescending = false;

            var adminService = new AdminService();
            adminService.GetLecturers(Users);

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
            AccomodationColumns = true;

            OverviewEnabled = true;
            InvoiceColumnsEnabled = true;
            AllColumnsEnabled = false;
            AccomodationColumnsEnabled = true;
        }

        public void ShowInvoiceColumns()
        {
            AllColumns = false;
            InvoiceColumns = true;
            OverviewColumns = false;
            AccomodationColumns = false;

            OverviewEnabled = true;
            InvoiceColumnsEnabled = false;
            AllColumnsEnabled = true;
            AccomodationColumnsEnabled = true;
        }

        public void ShowOverview()
        {
            AllColumns = false;
            InvoiceColumns = false;
            OverviewColumns = true;
            AccomodationColumns = false;

            OverviewEnabled = false;
            InvoiceColumnsEnabled = true;
            AllColumnsEnabled = true;
            AccomodationColumnsEnabled = true;
        }

        public void ShowAccomodation()
        {
            AllColumns = false;
            InvoiceColumns = false;
            OverviewColumns = false;
            AccomodationColumns = true;

            OverviewEnabled = true;
            InvoiceColumnsEnabled = true;
            AllColumnsEnabled = true;
            AccomodationColumnsEnabled = false;
        }

        public void DeleteUser(int id)
        {
            var adminService = new AdminService();
            adminService.DeleteUser(id);
        }

        public void ExportToExcel()
        {
            var exporter = new GridViewExportExcel<AppUserDTO>(new GridViewExportExcelSettings<AppUserDTO>());
            var gridView = Context.View.FindControlByClientId<GridView>("users", true);

            using (var file = exporter.Export(gridView, Users))
            {
                Context.ReturnFile(file, "NakopneteSvojiSkolu-PrednasejiciALektori.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }
    }
}

