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
    public class VisitorsViewModel : MainMasterPageViewModel
    {
        public GridViewUserSettings UserSettings { get; set; }

        public int TotalUsersCount { get; set; }

        public BusinessPackDataSet<AppUserDTO> Users { get; set; } = new BusinessPackDataSet<AppUserDTO>();

        public override Task PreRender()
        {
            Users.PagingOptions.PageSize = 500;
            Users.SortingOptions.SortExpression = nameof(AppUserDTO.Id);
            Users.SortingOptions.SortDescending = false;

            var adminService = new AdminService();
            adminService.GetVisitors(Users);

            TotalUsersCount = Users.PagingOptions.TotalItemsCount;

            RegisterActive = "";
            MainPageActive = "";
            AdminActive = "active";

            return base.PreRender();
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
                Context.ReturnFile(file, "NakopneteSvojiSkolu-VystavovateleHosteAClenoveTymu.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }
    }
}

