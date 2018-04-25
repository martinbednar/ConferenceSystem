using System.Collections.Generic;
using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.Framework.Runtime.Filters;

namespace ConferencySystem.ViewModels.Admin
{
    [Authorize(Roles = new[] { "admin", "super" })]
    public class WorkshopOverviewWorkshopsViewModel : MainMasterPageViewModel
    {
        public List<WorkshopsBlockDTO> WorkshopSumary { get; set; }

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                var workshopService = new WorkshopService();
                WorkshopSumary = workshopService.GetWorkshopSumary();
            }

            return base.PreRender();
        }
    }
}

