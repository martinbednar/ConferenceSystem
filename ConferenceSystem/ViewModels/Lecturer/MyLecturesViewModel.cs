using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using DotVVM.BusinessPack.Controls;
using DotVVM.Framework.Runtime.Filters;
using ConferencySystem.BL.Services;
using DotVVM.Framework.Hosting;
using System.Security.Claims;

namespace ConferencySystem.ViewModels.Lecturer
{
    [Authorize(Roles = new[] { "lecturer" })]
    public class MyLecturesViewModel : ConferencySystem.ViewModels.MainMasterPageViewModel
    {
        public int CurrentUserId
        {
            get
            {
                return Int32.Parse(Context.GetOwinContext().Authentication.User.Claims.Where(c => c.Type == ClaimTypes.Sid)
                 .Select(c => c.Value).SingleOrDefault());
            }
        }

        public BusinessPackDataSet<LectureDTO> MyLectures { get; set; } = new BusinessPackDataSet<LectureDTO>();
        public BusinessPackDataSet<LectureDTO> MyWorkshops { get; set; } = new BusinessPackDataSet<LectureDTO>();

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                MyLectures.PagingOptions.PageSize = 500;
                MyLectures.SortingOptions.SortExpression = nameof(LectureDTO.Id);
                MyLectures.SortingOptions.SortDescending = false;

                MyWorkshops.PagingOptions.PageSize = 500;
                MyWorkshops.SortingOptions.SortExpression = nameof(LectureDTO.Id);
                MyWorkshops.SortingOptions.SortDescending = false;

                var lectureService = new LectureService();
                lectureService.GetProgramsByUser(CurrentUserId, MyLectures, MyWorkshops);
            }


            RegisterActive = "";
            MainPageActive = "";
            AdminActive = "";
            CarteringActive = "";
            WorkshopsActive = "";
            LecturerInfoActive = "";
            LoginActive = "";
            ProfileActive = "";
            LecturerProgramsActive = "active";

            return base.PreRender();
        }

        public void DeactivateProgram(int id)
        {
            var lectureService = new LectureService();
            lectureService.DeactivateProgram(id);
        }
    }
}

