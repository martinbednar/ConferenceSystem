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
using DotVVM.Framework.Storage;
using DotVVM.Framework.ViewModel;

namespace ConferencySystem.ViewModels.Lecturer
{
    public class AddMyLectureViewModel : ConferencySystem.ViewModels.MainMasterPageViewModel
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

        public UploadData Upload { get; set; } = new UploadData();

        private readonly IUploadedFileStorage fileStorage;

        public bool ImageUploaded { get; set; } = false;

        public AddMyLectureViewModel(IUploadedFileStorage storage)
        {
            this.fileStorage = storage;
        }

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                var lectureService = new LectureService();
                DataLecture = lectureService.CreateNewLecture(CurrentUserId);
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
            var folderPath = GetFolderdPath();
            var lectureService = new LectureService();


            // save all files to disk
            foreach (var file in Upload.Files)
            {
                var filePath = Path.Combine(folderPath, file.FileName);
                fileStorage.SaveAs(file.FileId, filePath);
                lectureService.SavePresentation(filePath, DataLecture.Id);
                DataLecture.PresentationName = file.FileName;
                ImageUploaded = true;
                fileStorage.DeleteFile(file.FileId);
            }
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
            ImageUploaded = false;
        }

        public void SaveLecture()
        {
            var lectureService = new LectureService();
            lectureService.SaveLecture(DataLecture);
            Context.RedirectToRoute("MyLectures");
        }
    }
}
