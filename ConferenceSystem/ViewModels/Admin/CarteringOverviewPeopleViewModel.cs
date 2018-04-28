using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.BusinessPack.Controls;
using DotVVM.BusinessPack.Export.Excel;
using DotVVM.Framework.Runtime.Filters;

namespace ConferencySystem.ViewModels.Admin
{
    [Authorize(Roles = new[] { "admin", "super" })]
    public class CarteringOverviewPeopleViewModel : MainMasterPageViewModel
    {
        public GridViewUserSettings UserSettings { get; set; }

        public List<AppUserDTO> People { get; set; }

        public BusinessPackDataSet<PersonCartering> PeopleCartering { get; set; } = new BusinessPackDataSet<PersonCartering>();

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                var carteringService = new CarteringService();
                var people = carteringService.GetPeopleOverview();

                foreach (AppUserDTO person in people)
                {
                    PeopleCartering.PagingOptions.PageSize = 500;
                    PeopleCartering.SortingOptions.SortExpression = nameof(PersonCartering.Id);
                    PeopleCartering.SortingOptions.SortDescending = false;

                    PeopleCartering.Items.Add(new PersonCartering()
                    {
                        Id = person.Id,
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        HasSundaySoup = (person.Cartering.Where(c => c.Id == 11).Count() == 0) ? false : true,
                        SundayDinner = (person.Cartering.Where(c => ((c.Id == 1) || (c.Id == 2))).Count() == 0) ? "" : person.Cartering.Where(c => ((c.Id == 1) || (c.Id == 2))).First().Name,
                        HasSundayWine = (person.Cartering.Where(c => c.Id == 3).Count() == 0) ? false : true,
                        HasMondayMorningCoffee = (person.Cartering.Where(c => c.Id == 8).Count() == 0) ? false : true,
                        MondaySoup = (person.Cartering.Where(c => ((c.Id == 6) || (c.Id == 12))).Count() == 0) ? "" : person.Cartering.Where(c => ((c.Id == 6) || (c.Id == 12))).First().Name,
                        MondayLunch = (person.Cartering.Where(c => ((c.Id == 4) || (c.Id == 5))).Count() == 0) ? "" : person.Cartering.Where(c => ((c.Id == 4) || (c.Id == 5))).First().Name,
                        HasMondayAfternoonCoffee = (person.Cartering.Where(c => c.Id == 9).Count() == 0) ? false : true,
                        HasMondayRaut = (person.Cartering.Where(c => c.Id == 7).Count() == 0) ? false : true,
                        HasTuesdayCoffee = (person.Cartering.Where(c => c.Id == 10).Count() == 0) ? false : true
                     });
                }
            }
            return base.PreRender();
        }

        public void ExportToExcel()
        {
            var exporter = new GridViewExportExcel<PersonCartering>(new GridViewExportExcelSettings<PersonCartering>());
            var gridView = Context.View.FindControlByClientId<GridView>("people", true);

            using (var file = exporter.Export(gridView, PeopleCartering))
            {
                Context.ReturnFile(file, "NakopneteSvojiSkolu-Stravovani.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }
    }
}