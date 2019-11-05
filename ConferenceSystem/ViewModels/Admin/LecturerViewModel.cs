using System;
using System.IO;
using System.Threading.Tasks;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.BusinessPack.Controls;
using DotVVM.Framework.Runtime.Filters;
using DotVVM.Framework.Storage;

namespace ConferencySystem.ViewModels.Admin
{
    [Authorize(Roles = new[] { "admin", "super" })]
    public class LecturerViewModel : MainMasterPageViewModel
    {
        public AppUserDTO DataUser { get; set; }

        public string SelectedBirthYear { get; set; }
        public string SelectedBirthMonth { get; set; }
        public string SelectedBirthDay { get; set; }

        /* BirthDate processing */
        public DateProcessing DateProcessing { get; set; }

        public UploadData Upload { get; set; } = new UploadData();

        private readonly IUploadedFileStorage fileStorage;

        public bool ImageUploaded { get; set; }

        public LecturerViewModel(IUploadedFileStorage storage)
        {
            this.fileStorage = storage;
        }

        public bool WantAccomodation { get; set; } = false;

        public bool AccomodationFirstNight { get; set; } = false;

        public bool AccomodationSecondNight { get; set; } = false;

        public int UserId
        {
            get { return Convert.ToInt32(Context.Parameters["UserId"]); }
        }
        
        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                var adminService = new AdminService();
                DataUser = adminService.GetUser(UserId);

                DateProcessing = new DateProcessing();

                if (DataUser.BirthDate != null)
                {
                    SelectedBirthYear = DataUser.BirthDate.ToString().Substring(6,4);
                    SelectedBirthMonth = DateProcessing.MonthFromDb(DataUser.BirthDate.ToString().Substring(3, 2));
                    SelectedBirthDay = DateProcessing.DayFromDb(DataUser.BirthDate.ToString().Substring(0, 2));
                }
                else
                {
                    SelectedBirthYear = "";
                    SelectedBirthMonth = "";
                    SelectedBirthDay = "";
                }

                if ((DataUser.LecturerInfo.Accomodation == null) || (DataUser.LecturerInfo.Accomodation == ""))
                {
                    WantAccomodation = false;
                }
                else
                {
                    WantAccomodation = true;
                    if (DataUser.LecturerInfo.Accomodation.Contains("23.2. - 24.2.2020")) AccomodationFirstNight = true;
                    if (DataUser.LecturerInfo.Accomodation.Contains("24.2. - 25.2.2020")) AccomodationSecondNight = true;
                }


                var lecturerInfoService = new LecturerInfoService();
                ImageUploaded = !(DataUser.LecturerInfo.Photo == null || DataUser.LecturerInfo.Photo.Length == 0);
            }

            return base.PreRender();
        }

        public void SaveUser()
        {
            /*Date processing*/
            if ((SelectedBirthDay == "") || (SelectedBirthMonth == "") || (SelectedBirthYear == ""))
            {
                DataUser.BirthDate = null;
            }
            else
            {
                DataUser.BirthDate = new DateTime(Int32.Parse(SelectedBirthYear), Int32.Parse(DateProcessing.MonthToDb(SelectedBirthMonth)), Int32.Parse(SelectedBirthDay),0,0,0,0);
            }

            if (WantAccomodation)
            {
                if (AccomodationFirstNight && AccomodationSecondNight)
                {
                    DataUser.LecturerInfo.Accomodation = "23.2. - 24.2.2020, 24.2. - 25.2.2020";
                }
                else
                {
                    if (AccomodationFirstNight)
                    {
                        DataUser.LecturerInfo.Accomodation = "23.2. - 24.2.2020";
                    }
                    if (AccomodationSecondNight)
                    {
                        DataUser.LecturerInfo.Accomodation = "24.2. - 25.2.2020";
                    }
                }
            }
            else
            {
                DataUser.LecturerInfo.Accomodation = "";
            }


            var adminService = new AdminService();
            
            adminService.SaveUser(DataUser);

            Context.RedirectToRoute("Lecturers");
        }

        public void ProcessFiles()
        {
            var folderPath = GetFolderdPath();
            var lecturerInfoService = new LecturerInfoService();


            // save all files to disk
            foreach (var file in Upload.Files)
            {
                var filePath = Path.Combine(folderPath, file.FileName);
                fileStorage.SaveAs(file.FileId, filePath);
                lecturerInfoService.SavePhoto(filePath, UserId);
                DataUser.LecturerInfo.PhotoName = DataUser.FirstName + " " + DataUser.LastName + "-profilova fotka" + Path.GetExtension(filePath).ToLower();
                ImageUploaded = true;
                fileStorage.DeleteFile(file.FileId);
            }
            //SaveInfo();
            //Context.RedirectToRoute("LecturerInfo");
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

        public void DeletePhoto()
        {
            var lecturerInfoService = new LecturerInfoService();
            lecturerInfoService.DeletePhoto(UserId);
            Upload.Clear();
            ImageUploaded = false;
        }

        public void WantAccomodationChanged()
        {
            if (WantAccomodation)
            {
                AccomodationFirstNight = true;
                AccomodationSecondNight = true;
            }
            else
            {
                AccomodationFirstNight = false;
                AccomodationSecondNight = false;
            }
        }

        public void AccomodationDateChanged()
        {
            if (!AccomodationFirstNight && !AccomodationSecondNight)
            {
                WantAccomodation = false;
            }
            else
            {
                WantAccomodation = true;
            }
        }
    }
}

