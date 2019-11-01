using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;

namespace ConferencySystem.ViewModels.Lecturer
{
    public class LecturerInfoViewModel : ConferencySystem.ViewModels.MainMasterPageViewModel
    {
        public int CurrentUserId
        {
            get
            {
                return Int32.Parse(Context.GetOwinContext().Authentication.User.Claims.Where(c => c.Type == ClaimTypes.Sid)
                 .Select(c => c.Value).SingleOrDefault());
            }
        }

        //public LecturerInfoDTO LecturerInfo { get; set; }

        public AppUserDTO DataUser { get; set; }

        public string SelectedBirthYear { get; set; }
        public string SelectedBirthMonth { get; set; }
        public string SelectedBirthDay { get; set; }

        /* BirthDate processing */
        public DateProcessing DateProcessing { get; set; }

        public UploadedFilesCollection Files { get; set; }

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                var adminService = new AdminService();
                DataUser = adminService.GetUser(CurrentUserId);

                DateProcessing = new DateProcessing();

                if (DataUser.BirthDate != null)
                {
                    SelectedBirthYear = DataUser.BirthDate.ToString().Substring(6, 4);
                    SelectedBirthMonth = DateProcessing.MonthFromDb(DataUser.BirthDate.ToString().Substring(3, 2));
                    SelectedBirthDay = DateProcessing.DayFromDb(DataUser.BirthDate.ToString().Substring(0, 2));
                }
                else
                {
                    SelectedBirthYear = "";
                    SelectedBirthMonth = "";
                    SelectedBirthDay = "";
                }

                /*var lecturerInfoService = new LecturerInfoService();
                LecturerInfo = lecturerInfoService.GetLecturerInfo(CurrentUserId);*/
            }
            Files = new UploadedFilesCollection();

            return base.PreRender();
        }

        public void SaveInfo()
        {
            /*Date processing*/
            if ((SelectedBirthDay == "") || (SelectedBirthMonth == "") || (SelectedBirthYear == ""))
            {
                DataUser.BirthDate = null;
            }
            else
            {
                DataUser.BirthDate = new DateTime(Int32.Parse(SelectedBirthYear), Int32.Parse(DateProcessing.MonthToDb(SelectedBirthMonth)), Int32.Parse(SelectedBirthDay), 0, 0, 0, 0);
            }


            var adminService = new AdminService();
            
            adminService.SaveUser(DataUser);
        }

    }
}