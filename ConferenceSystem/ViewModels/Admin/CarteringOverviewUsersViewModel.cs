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
    public class CarteringOverviewUsersViewModel : MainMasterPageViewModel
    {
        public GridViewUserSettings UserSettings { get; set; }

        public List<AppUserDTO> Users { get; set; }

        public BusinessPackDataSet<UserCartering> UsersCartering { get; set; } = new BusinessPackDataSet<UserCartering>();

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                var carteringService = new CarteringService();
                var users = carteringService.GetUsersOverview();

                foreach (AppUserDTO user in users)
                {
                    UsersCartering.PagingOptions.PageSize = 500;
                    UsersCartering.SortingOptions.SortExpression = nameof(UserCartering.Id);
                    UsersCartering.SortingOptions.SortDescending = false;

                    UsersCartering.Items.Add(new UserCartering()
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        HasSundaySoup = (user.Cartering.Where(c => c.Id == 11).Count() == 0) ? false : true,
                        SundayDinner = (user.Cartering.Where(c => ((c.Id == 1) || (c.Id == 2))).Count() == 0) ? "" : user.Cartering.Where(c => ((c.Id == 1) || (c.Id == 2))).First().Name,
                        HasSundayWine = (user.Cartering.Where(c => c.Id == 3).Count() == 0) ? false : true,
                        HasMondayMorningCoffee = (user.Cartering.Where(c => c.Id == 8).Count() == 0) ? false : true,
                        MondaySoup = (user.Cartering.Where(c => ((c.Id == 6) || (c.Id == 12))).Count() == 0) ? "" : user.Cartering.Where(c => ((c.Id == 6) || (c.Id == 12))).First().Name,
                        MondayLunch = (user.Cartering.Where(c => ((c.Id == 4) || (c.Id == 5))).Count() == 0) ? "" : user.Cartering.Where(c => ((c.Id == 4) || (c.Id == 5))).First().Name,
                        HasMondayAfternoonCoffee = (user.Cartering.Where(c => c.Id == 9).Count() == 0) ? false : true,
                        HasMondayRaut = (user.Cartering.Where(c => c.Id == 7).Count() == 0) ? false : true,
                        HasTuesdayCoffee = (user.Cartering.Where(c => c.Id == 10).Count() == 0) ? false : true
                     });
                }
            }
            return base.PreRender();
        }

        public void ExportToExcel()
        {
            var exporter = new GridViewExportExcel<UserCartering>(new GridViewExportExcelSettings<UserCartering>());
            var gridView = Context.View.FindControlByClientId<GridView>("users", true);

            using (var file = exporter.Export(gridView, UsersCartering))
            {
                Context.ReturnFile(file, "NakopneteSvojiSkolu-Stravovani.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }
    }
}