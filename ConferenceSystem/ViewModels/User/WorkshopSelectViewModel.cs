using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime.Filters;

namespace ConferencySystem.ViewModels.User
{
    [Authorize(Roles = new[] { "user", "super", "admin" })]
    public class WorkshopSelectViewModel : MainMasterPageViewModel
    {
        public int CurrentUserId
        {
            get
            {
                return Int32.Parse(Context.GetOwinContext().Authentication.User.Claims.Where(c => c.Type == ClaimTypes.Sid)
                 .Select(c => c.Value).SingleOrDefault());
            }
        }

        public List<WorkshopsBlockDTO> WorkshopsBlocks { get; set; }

        public bool Alert { get; set; } = false;
        public bool AlertDismissed { get; set; } = false;

        public bool RegistrationEnabled { get; set; } = false;

        public override Task PreRender()
        {
            RegistrationEnabled = DateTime.Now <= new DateTime(2020, 02, 13, 11, 00, 00);

            var workshopService = new WorkshopService();
            WorkshopsBlocks = workshopService.GetWorkshopsBlocks(CurrentUserId);

            bool anyWorkshopRegistered = false;

            AppUserDTO actualUser = workshopService.GetUser(CurrentUserId);

            if (actualUser != null)
            {
                foreach (WorkshopsBlockDTO workshopBlock in WorkshopsBlocks)
                {
                    foreach (WorkshopDTO workshop in workshopBlock.Workshops)
                    {
                        foreach (AppUserDTO user in workshop.Users)
                        {
                            if (user.Id == CurrentUserId)
                            {
                                workshop.Registered = true;
                                anyWorkshopRegistered = true;
                            }
                        }
                    }
                    if(anyWorkshopRegistered)
                    {
                        workshopBlock.AnyWorkshopRegistered = true;
                    }
                    anyWorkshopRegistered = false;
                }
            }

            RegisterActive = "";
            MainPageActive = "";
            AdminActive = "";
            CarteringActive = "";
            WorkshopsActive = "active";
            LecturerInfoActive = "";
            LoginActive = "";
            ProfileActive = "";
            LecturerProgramsActive = "";

            return base.PreRender();
        }

         public void Register(int workshopId)
        {
            //var workshopService = new WorkshopService();
            if(!WorkshopService.RegisterWorkshop(CurrentUserId, workshopId))
            {
                AlertDismissed = false;
                Alert = true;
            }
        }

        public  void Unregister(int workshopId)
        {
            var workshopService = new WorkshopService();
            workshopService.UnregisterWorkshop(CurrentUserId, workshopId);
        }
    }
}

