using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.Framework.Runtime.Filters;

namespace ConferencySystem.ViewModels.Admin
{
    [Authorize(Roles = new[] { "admin", "super" })]
    public class SettingVariablesViewModel : MainMasterPageViewModel
    {
        public ConstantDTO DueDate { get; set; }
        public ConstantDTO Price { get; set; }

        public override Task PreRender()
        {
            var constantService = new ConstantService();
            DueDate = constantService.GetConstant(2);
            Price = constantService.GetConstant(3);

            return base.PreRender();
        }

        public void Save()
        {
            var constantService = new ConstantService();
            constantService.SaveConstant(DueDate);
            constantService.SaveConstant(Price);
        }
    }
}

