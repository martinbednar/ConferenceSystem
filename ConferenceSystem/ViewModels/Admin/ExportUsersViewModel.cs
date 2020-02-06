using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.BusinessPack.Controls;
using DotVVM.BusinessPack.Export.Excel;
using DotVVM.Framework.Runtime.Filters;

namespace ConferencySystem.ViewModels.Admin
{
    [Authorize(Roles = new[] { "admin", "super" })]
    public class ExportUsersViewModel : MainMasterPageViewModel
    {
        public GridViewUserSettings UserSettings { get; set; }

        public BusinessPackDataSet<UserCompletInfo> Users { get; set; } = new BusinessPackDataSet<UserCompletInfo>();

        public override Task PreRender()
        {
            var exportService = new ExportService();


            Users.PagingOptions.PageSize = 500;

            exportService.GetAllUsersData(Users);

            return base.PreRender();
        }

        public void ExportToExcel()
        {
            var exporter = new GridViewExportExcel<UserCompletInfo>(new GridViewExportExcelSettings<UserCompletInfo>());
            var gridView = Context.View.FindControlByClientId<GridView>("users", true);

            using (var file = exporter.Export(gridView, Users))
            {
                Context.ReturnFile(file, "NakopneteSvojiSkolu-UcastniciKompletniPrehled.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }
    }
}