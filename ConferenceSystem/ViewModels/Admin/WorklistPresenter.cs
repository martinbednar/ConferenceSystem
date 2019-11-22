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
    public class WorklistPresenter : IDotvvmPresenter
    {
        public async Task ProcessRequest(IDotvvmRequestContext context)
        {
            var lectureId = Convert.ToInt32(context.Parameters["LectureId"]);

            int currentUserId = Int32.Parse(context.GetOwinContext().Authentication.User.Claims.Where(c => c.Type == ClaimTypes.Sid)
                   .Select(c => c.Value).SingleOrDefault());

            var adminService = new AdminService();
            var user = adminService.GetUser(currentUserId);

            if (user.Roles.All(role => role.RoleId != 2 && role.RoleId != 3)) {
                var lectures = user.LecturerInfo.Lectures;
                if (lectures.Where(l => l.Id == lectureId).Count() == 0) context.RedirectToRoute("Default");
            }

            var lectureService = new LectureService();
            var lecture = lectureService.GetLecture(lectureId);

            context.GetOwinContext().Response.Headers["Content-Disposition"] = "attachment; filename=" + lecture.WorklistName;
            
            await context.GetOwinContext().Response.WriteAsync(lecture.Worklist);
        }
    }
}