using System;
using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.Framework.Runtime.Filters;

namespace ConferencySystem.ViewModels.Admin
{
    [Authorize(Roles = new[] { "admin", "super" })]
    public class VisitorViewModel : MainMasterPageViewModel
    {
        public AppUserDTO DataUser { get; set; }

        public int? UserId
        {
            get { return Convert.ToInt32(Context.Parameters["UserId"]); }
        }
        
        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                var adminService = new AdminService();
                if (UserId != null) DataUser = adminService.GetUser(UserId.Value);
            }

            return base.PreRender();
        }

        public void SaveUser()
        {
            var adminService = new AdminService();
            
            adminService.SaveUser(DataUser);

            Context.RedirectToRoute("Visitors");
        }
    }
}

