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
    public class MyWorkshopViewModel : ConferencySystem.ViewModels.MainMasterPageViewModel
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

        public UploadData UploadPresentation { get; set; } = new UploadData();

        public UploadData UploadWorklist { get; set; } = new UploadData();

        private readonly IUploadedFileStorage fileStorage;

        public bool PresentationUploaded { get; set; } = false;

        public bool WorklistUploaded { get; set; } = false;

        public bool SaveEnabled { get; set; } = true;

        public MyWorkshopViewModel(IUploadedFileStorage storage)
        {
            this.fileStorage = storage;
        }

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                var lectureService = new LectureService();

                if (LectureId == 0)
                {
                    DataLecture = lectureService.CreateNewLecture(CurrentUserId);
                    DataLecture.Type = "workshop";
                }
                else {
                    var adminService = new AdminService();
                    var user = adminService.GetUser(CurrentUserId);

                    //Overeni, ze uzivatel chce otevrit svoji prednasku a ne cizi.
                    if (user.Roles.All(role => role.RoleId != 2 && role.RoleId != 3))
                    {
                        var lectures = user.LecturerInfo.Lectures;
                        if (lectures.Where(l => l.Id == LectureId).Count() == 0) Context.RedirectToRoute("Default");
                    }


                    DataLecture = lectureService.GetLecture(LectureId);
                }

                PresentationUploaded = !(DataLecture.PresentationName == null || DataLecture.PresentationName == "");
                WorklistUploaded = !(DataLecture.WorklistName == null || DataLecture.WorklistName == "");
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

        public void ProcessFiles(bool isPresentation)
        {
            SaveEnabled = false;
            var folderPath = GetFolderdPath();
            var lectureService = new LectureService();

            if (isPresentation)
            {
                foreach (var file in UploadPresentation.Files)
                {
                    var filePath = Path.Combine(folderPath, file.FileName);
                    fileStorage.SaveAs(file.FileId, filePath);
                    lectureService.SavePresentation(filePath, DataLecture.Id);
                    DataLecture.PresentationName = file.FileName;
                    PresentationUploaded = true;
                    fileStorage.DeleteFile(file.FileId);
                }
            }
            else
            {
                foreach (var file in UploadWorklist.Files)
                {
                    var filePath = Path.Combine(folderPath, file.FileName);
                    fileStorage.SaveAs(file.FileId, filePath);
                    lectureService.SaveWorklist(filePath, DataLecture.Id);
                    DataLecture.WorklistName = file.FileName;
                    WorklistUploaded = true;
                    fileStorage.DeleteFile(file.FileId);
                }
            }
            SaveEnabled = true;
            UploadPresentation.Clear();
            UploadWorklist.Clear();
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

        public void DeleteFile(bool isPresentation)
        {
            var lectureService = new LectureService();
            if (isPresentation)
            {
                lectureService.DeletePresentation(DataLecture.Id);
                PresentationUploaded = false;
                UploadPresentation.Clear();
            }
            else
            {
                lectureService.DeleteWorklist(DataLecture.Id);
                WorklistUploaded = false;
                UploadWorklist.Clear();
            }
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

        public void EquipementChanged()
        {
            if (DataLecture.Nothing)
            {
                DataLecture.Flipchart = false;
                DataLecture.Notebook = false;
                DataLecture.Dataprojector = false;
                DataLecture.NotebookPort = "";
                DataLecture.Speakers = false;
                DataLecture.WorklistsCopies = false;
            }
        }
    }
}
