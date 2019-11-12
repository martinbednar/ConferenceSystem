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
                        HasSundayCoffeeBreak = (user.Cartering.Where(c => c.Id == 1).Count() == 0) ? false : true,
                        HasSundaySoup = (user.Cartering.Where(c => c.Id == 2).Count() == 0) ? false : true,
                        SundayDinner = (user.Cartering.Where(c => ((c.Id == 3) || (c.Id == 4))).Count() == 0) ? "" : user.Cartering.Where(c => ((c.Id == 3) || (c.Id == 4))).First().Name,
                        HasMondayAMCoffeeBreak = (user.Cartering.Where(c => c.Id == 5).Count() == 0) ? false : true,
                        HasMondaySoup = (user.Cartering.Where(c => c.Id == 6).Count() == 0) ? false : true,
                        MondayLunch = (user.Cartering.Where(c => ((c.Id == 7) || (c.Id == 8))).Count() == 0) ? "" : user.Cartering.Where(c => ((c.Id == 7) || (c.Id == 8))).First().Name,
                        HasMondayPMCoffeeBreak = (user.Cartering.Where(c => c.Id == 9).Count() == 0) ? false : true,
                        HasMondayRaut = (user.Cartering.Where(c => c.Id == 10).Count() == 0) ? false : true,
                        HasTuesdayCoffeeBreak = (user.Cartering.Where(c => c.Id == 11).Count() == 0) ? false : true,
                        HasTuesdayLunch = (user.Cartering.Where(c => c.Id == 15).Count() == 0) ? false : true
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