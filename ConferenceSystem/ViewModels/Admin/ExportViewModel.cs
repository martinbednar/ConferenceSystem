using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.BusinessPack.Controls;
using DotVVM.BusinessPack.Export.Excel;
using DotVVM.Framework.Runtime.Filters;

namespace ConferencySystem.ViewModels.Admin
{
    [Authorize(Roles = new[] { "admin", "super" })]
    public class ExportViewModel : MainMasterPageViewModel
    {
        public GridViewUserSettings UserSettings { get; set; }

        public BusinessPackDataSet<PersonCompletInfo> People { get; set; } = new BusinessPackDataSet<PersonCompletInfo>();

        public override Task PreRender()
        {
            var exportService = new ExportService();


            People.PagingOptions.PageSize = 500;

            exportService.GetAllData(People);

            return base.PreRender();
        }

        public void ExportToExcel()
        {
            var exporter = new GridViewExportExcel<PersonCompletInfo>(new GridViewExportExcelSettings<PersonCompletInfo>());
            var gridView = Context.View.FindControlByClientId<GridView>("people", true);

            using (var file = exporter.Export(gridView, People))
            {
                Context.ReturnFile(file, "NakopneteSvojiSkolu-KompletniPrehled.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }
    }
}