using System.Collections.Generic;
using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.BusinessPack.Controls;
using DotVVM.Framework.Runtime.Filters;

namespace ConferencySystem.ViewModels.Admin
{
    [Authorize(Roles = new[] { "admin", "super" })]
    public class CarteringOverviewCarteringViewModel : MainMasterPageViewModel
    {
        public List<CarteringPeople> CarteringSumary { get; set; }

        public GridViewUserSettings UserSettings { get; set; }

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                var carteringService = new CarteringService();
                CarteringSumary = carteringService.GetCarteringSumary();
            }

            return base.PreRender();
        }
    }
}

