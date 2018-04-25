using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.BusinessPack.Controls;
using DotVVM.BusinessPack.Export.Excel;
using DotVVM.Framework.Runtime.Filters;

namespace ConferencySystem.ViewModels.Admin
{
    [Authorize(Roles = new[] { "admin", "super" })]
    public class WorkshopOverviewPeopleViewModel : MainMasterPageViewModel
    {
        public GridViewUserSettings UserSettings { get; set; }

        public BusinessPackDataSet<PersonWorkshops> PeopleWorkshops { get; set; } = new BusinessPackDataSet<PersonWorkshops>();

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                PeopleWorkshops.PagingOptions.PageSize = 500;
                PeopleWorkshops.SortingOptions.SortExpression = nameof(PersonWorkshops.Id);
                PeopleWorkshops.SortingOptions.SortDescending = false;

                var workshopService = new WorkshopService();
                workshopService.GetPeopleOverview(PeopleWorkshops);
            }

            return base.PreRender();
        }

        public void ExportToExcel()
        {
            var exporter = new GridViewExportExcel<PersonWorkshops>(new GridViewExportExcelSettings<PersonWorkshops>());
            var gridView = Context.View.FindControlByClientId<GridView>("people", true);

            using (var file = exporter.Export(gridView, PeopleWorkshops))
            {
                Context.ReturnFile(file, "NakopneteSvojiSkolu-Workshopy.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }
    }
}

