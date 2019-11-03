using System;
using System.Collections.Generic;
using System.Linq;
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

            var adminService = new AdminService();

            var profilePhoto = adminService.GetProfilePhoto(lecturerInfoId);

            context.GetOwinContext().Response.Headers["Content-Disposition"] = "attachment; filename=test.png";
            
            await context.GetOwinContext().Response.WriteAsync(profilePhoto.Photo);
        }
    }
}