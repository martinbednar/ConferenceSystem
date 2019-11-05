using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using ConferencySystem.BL.Services;
using DotVVM.Framework.Hosting;

namespace ConferencySystem.ViewModels.Admin
{
    public class ProfilePhotoPresenter : IDotvvmPresenter
    {
        public async Task ProcessRequest(IDotvvmRequestContext context)
        {
            var lecturerInfoId = Convert.ToInt32(context.Parameters["LecturerInfoId"]);

            int currentUserId = Int32.Parse(context.GetOwinContext().Authentication.User.Claims.Where(c => c.Type == ClaimTypes.Sid)
                   .Select(c => c.Value).SingleOrDefault());

            var adminService = new AdminService();
            var user = adminService.GetUser(currentUserId);

            if (user.Roles.All(role => role.RoleId != 2 && role.RoleId != 3)) {
                if (user.LecturerInfo.Id != lecturerInfoId) context.RedirectToRoute("Default");
            }
            
            var lecturerInfo = adminService.GetLecturerInfo(lecturerInfoId);

            context.GetOwinContext().Response.Headers["Content-Disposition"] = "attachment; filename=" + lecturerInfo.PhotoName;
            
            await context.GetOwinContext().Response.WriteAsync(lecturerInfo.Photo);
        }
    }
}