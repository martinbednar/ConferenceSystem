using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.BusinessPack.Controls;
using DotVVM.BusinessPack.Export.Excel;
using DotVVM.Framework.Runtime.Filters;

namespace ConferencySystem.ViewModels.Admin
{
    [Authorize(Roles = new[] { "admin", "super" })]
    public class WorkshopOverviewUsersViewModel : MainMasterPageViewModel
    {
        public GridViewUserSettings UserSettings { get; set; }

        public BusinessPackDataSet<UserWorkshops> UsersWorkshops { get; set; } = new BusinessPackDataSet<UserWorkshops>();

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                UsersWorkshops.PagingOptions.PageSize = 500;
                UsersWorkshops.SortingOptions.SortExpression = nameof(UserWorkshops.Id);
                UsersWorkshops.SortingOptions.SortDescending = false;

                var workshopService = new WorkshopService();
                workshopService.GetUsersOverview(UsersWorkshops);
            }

            return base.PreRender();
        }

        public void ExportToExcel()
        {
            var exporter = new GridViewExportExcel<UserWorkshops>(new GridViewExportExcelSettings<UserWorkshops>());
            var gridView = Context.View.FindControlByClientId<GridView>("users", true);

            using (var file = exporter.Export(gridView, UsersWorkshops))
            {
                Context.ReturnFile(file, "NakopneteSvojiSkolu-Workshopy.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }
    }
}

