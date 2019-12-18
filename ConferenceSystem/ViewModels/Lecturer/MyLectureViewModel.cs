using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.BusinessPack.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime.Filters;
using DotVVM.Framework.Storage;
using DotVVM.Framework.ViewModel;

namespace ConferencySystem.ViewModels.Lecturer
{
    [Authorize(Roles = new[] { "lecturer", "super", "admin" })]
    public class MyLectureViewModel : ConferencySystem.ViewModels.MainMasterPageViewModel
    {
        public LectureDTO DataLecture { get; set; }

        public int CurrentUserId
        {
            get
            {
                return Int32.Parse(Context.GetOwinContext().Authentication.User.Claims.Where(c => c.Type == ClaimTypes.Sid)
                 .Select(c => c.Value).SingleOrDefault());
            }
        }

        public int LectureId
        {
            get { return Convert.ToInt32(Context.Parameters["LectureId"]); }
        }

        public UploadData Upload { get; set; } = new UploadData();

        private readonly IUploadedFileStorage fileStorage;

        public bool PresentationUploaded { get; set; } = false;

        public bool SaveEnabled { get; set; } = true;

        public MyLectureViewModel(IUploadedFileStorage storage)
        {
            this.fileStorage = storage;
        }
        public AppUserDTO SelectedLecturer { get; set; }

        public List<AppUserDTO> Lecturers { get; set; }


        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                var lectureService = new LectureService();
                var adminService = new AdminService();

                Lecturers = adminService.GetLecturers();
                var currentUser = adminService.GetUser(CurrentUserId);
                SelectedLecturer = currentUser;

                if (LectureId == 0)
                {
                    if (IsAdmin || IsSuperAdmin)
                    {
                        SelectedLecturer = Lecturers.First();
                        DataLecture = lectureService.CreateNewLecture(SelectedLecturer.Id);
                    }
                    else
                    {
                        DataLecture = lectureService.CreateNewLecture(CurrentUserId);
                    }
                    DataLecture.Type = "přednáška";
                    DataLecture.Microphone = "do ruky";
                }
                else {
                    //Overeni, ze uzivatel chce otevrit svoji prednasku a ne cizi.
                    if (!(IsAdmin || IsSuperAdmin))
                    {
                        var lectures = currentUser.LecturerInfo.Lectures;
                        if (lectures.Where(l => l.Id == LectureId).Count() == 0) Context.RedirectToRoute("Default");
                    }
                    else
                    {
                        SelectedLecturer = lectureService.GetLecturerOfLecture(LectureId);
                    }

                    DataLecture = lectureService.GetLecture(LectureId);
                }
                
                PresentationUploaded = !(DataLecture.PresentationName == null || DataLecture.PresentationName == "");
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

        public void ProcessFiles()
        {
            SaveEnabled = false;
            var folderPath = GetFolderdPath();
            var lectureService = new LectureService();


            // save all files to disk
            foreach (var file in Upload.Files)
            {
                var filePath = Path.Combine(folderPath, file.FileName);
                fileStorage.SaveAs(file.FileId, filePath);
                lectureService.SavePresentation(filePath, DataLecture.Id);
                DataLecture.PresentationName = file.FileName;
                PresentationUploaded = true;
                fileStorage.DeleteFile(file.FileId);
            }
            SaveEnabled = true;
        }

        private string GetFolderdPath()
        {
            var folderPath = Path.Combine(Context.Configuration.ApplicationPhysicalPath, "temp");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return folderPath;
        }

        public void DeletePresentation()
        {
            var lectureService = new LectureService();
            lectureService.DeletePresentation(DataLecture.Id);
            Upload.Clear();
            PresentationUploaded = false;
        }

        public void SaveLecture()
        {
            var lectureService = new LectureService();
            lectureService.SaveLecture(DataLecture);
            if (IsLecturer)
            {
                Context.RedirectToRoute("MyLectures");
            }
            else
            {
                Context.RedirectToRoute("Lectures");
            }
        }

        public void ChangeLecturer()
        {
            if (IsSuperAdmin || IsAdmin)
            {
                var lectureService = new LectureService();
                lectureService.updateLecturerOfLecture(DataLecture.Id, SelectedLecturer.Id);
            }
        }
    }
}
