using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using ConferencySystem.ViewModels.User;
using DotVVM.Framework.Hosting;

namespace ConferencySystem.ViewModels.Admin
{
    public class AttachmentPresenter : IDotvvmPresenter
    {
        public async Task ProcessRequest(IDotvvmRequestContext context)
        {
            var userId = Convert.ToInt32(context.Parameters["UserId"]);

            int currentUserId = Int32.Parse(context.GetOwinContext().Authentication.User.Claims.Where(c => c.Type == ClaimTypes.Sid)
                   .Select(c => c.Value).SingleOrDefault());

            var adminService = new AdminService();
            var user = adminService.GetUser(currentUserId);

            if (user.Roles.All(role => role.RoleId != 2 && role.RoleId != 3))
            {
                if (userId != currentUserId) context.RedirectToRoute("Default");
            }

            var invoice = adminService.GetInvoice(userId);

            context.GetOwinContext().Response.Headers["Content-Disposition"] = "attachment; filename=" + invoice.FileName;
            
            await context.GetOwinContext().Response.WriteAsync(invoice.FileBytes);
        }
    }
}