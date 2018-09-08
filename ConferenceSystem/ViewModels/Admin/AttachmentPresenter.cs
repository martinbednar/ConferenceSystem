using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ConferencySystem.BL.Services;
using DotVVM.Framework.Hosting;

namespace ConferencySystem.ViewModels.Admin
{
    public class AttachmentPresenter : IDotvvmPresenter
    {
        public async Task ProcessRequest(IDotvvmRequestContext context)
        {
            var userId = Convert.ToInt32(context.Parameters["UserId"]);

            var adminService = new AdminService();

            var invoice = adminService.GetInvoice(userId);

            context.GetOwinContext().Response.Headers["Content-Disposition"] = "attachment; filename=" + invoice.FileName;
            
            await context.GetOwinContext().Response.WriteAsync(invoice.FileBytes);
        }
    }
}