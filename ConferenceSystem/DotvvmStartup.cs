using ConferencySystem.BL;
using ConferencySystem.BL.Services;
using ConferencySystem.ViewModels.Admin;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Controls.Bootstrap;
using DotVVM.Framework.ResourceManagement;

namespace ConferencySystem
{
    public class DotvvmStartup : IDotvvmStartup
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            config.AddBootstrapConfiguration();
            config.AddBusinessPackConfiguration();
            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);
            config.DefaultCulture = "cs-CZ";
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("Default", "", "Views/default.dothtml");
            config.RouteTable.Add("Users", "users", "Views/Admin/Users.dothtml");
            config.RouteTable.Add("User", "user/{OrgId}/{UserId}", "Views/Admin/User.dothtml");
            config.RouteTable.Add("Register", "register", "Views/User/Register.dothtml");
            config.RouteTable.Add("RegistrationComplete", "complete", "Views/User/RegistrationComplete.dothtml");
            config.RouteTable.Add("Login", "login", "Views/Login.dothtml");
            config.RouteTable.Add("PwdForgotten", "password", "Views/PwdForgotten.dothtml");
            config.RouteTable.Add("CarteringSelect", "cartering", "Views/User/CarteringSelect.dothtml");
            config.RouteTable.Add("CarteringOverviewUsers", "carteringoverviewusers", "Views/Admin/CarteringOverviewUsers.dothtml");
            config.RouteTable.Add("CarteringOverviewCartering", "carteringoverviewcartering", "Views/Admin/CarteringOverviewCartering.dothtml");
            config.RouteTable.Add("WorkshopSelect", "workshop", "Views/User/WorkshopSelect.dothtml");
            config.RouteTable.Add("WorkshopOverviewUsers", "workshopoverviewusers", "Views/Admin/WorkshopOverviewUsers.dothtml");
            config.RouteTable.Add("WorkshopOverviewWorkshop", "workshopoverviewworkshop", "Views/Admin/WorkshopOverviewWorkshops.dothtml");
            config.RouteTable.Add("CarteringUser", "carteringuser/{UserId}", "Views/Admin/CarteringUser.dothtml");
            config.RouteTable.Add("Export", "export", "Views/Admin/Export.dothtml");
            config.RouteTable.Add("RegisterForm", "registerform", "Views/User/RegisterForm.dothtml");
            config.RouteTable.Add("WorkshopUser", "workshopuser/{UserId}", "Views/Admin/WorkshopUser.dothtml");
            config.RouteTable.Add("PwdReset", "password/reset", "Views/PwdReset.dothtml");
            config.RouteTable.Add("AttachmentDownload", "attachment/{UserId}", null, presenterFactory: () => new AttachmentPresenter());
            config.RouteTable.Add("EmailRegister", "emailregister", "Views/Admin/EmailRegister.dothtml");
            config.RouteTable.Add("EmailRegisterFull", "emailregisterfull", "Views/Admin/EmailRegisterFull.dothtml");
            config.RouteTable.Add("EmailNow", "emailnow", "Views/Admin/EmailNow.dothtml");
            config.RouteTable.Add("SettingVariables", "setting", "Views/Admin/SettingVariables.dothtml");
            config.RouteTable.Add("EmailPaidConfirmation", "emailpaidconfirmation", "Views/Admin/EmailPaidConfirmation.dothtml");
            config.RouteTable.Add("RegisterFormLecturer", "registerformlecturer", "Views/Lecturer/RegisterFormLecturer.dothtml");
            config.RouteTable.Add("Lecturers", "lecturers", "Views/Admin/Lecturers.dothtml");
            config.RouteTable.Add("Lecturer", "lecturer/{UserId}", "Views/Admin/Lecturer.dothtml");
            config.RouteTable.Add("LecturerInfo", "lecturerinfo", "Views/Lecturer/LecturerInfo.dothtml");
            config.RouteTable.Add("ProfilePhotoDownload", "profilephoto/{LecturerInfoId}", null, presenterFactory: () => new ProfilePhotoPresenter());
            config.RouteTable.Add("PrivacyStatement", "register/privacystatement", "Views/User/PrivacyStatement.dothtml");
            config.RouteTable.Add("Profile", "profile", "Views/User/Profile.dothtml");
            config.RouteTable.Add("RegisterFormVisitor", "registerformvisitor", "Views/Visitor/RegisterFormVisitor.dothtml");
            config.RouteTable.Add("Visitors", "visitors", "Views/Admin/Visitors.dothtml");
            config.RouteTable.Add("Visitor", "visitor/{UserId}", "Views/Admin/Visitor.dothtml");
            config.RouteTable.Add("MyLectures", "mylectures", "Views/Lecturer/MyLectures.dothtml");
            config.RouteTable.Add("MyLecture", "mylecture/{LectureId}", "Views/Lecturer/MyLecture.dothtml");
            config.RouteTable.Add("MyWorkshop", "myworkshop/{LectureId}", "Views/Lecturer/MyWorkshop.dothtml");
            config.RouteTable.Add("PresentationDownload", "presentation/{LectureId}", null, presenterFactory: () => new PresentationPresenter());
            config.RouteTable.Add("WorklistDownload", "worklist/{LectureId}", null, presenterFactory: () => new WorklistPresenter());
            config.RouteTable.Add("Lectures", "lectures", "Views/Admin/Lectures.dothtml");

            // Uncomment the following line to auto-register all dothtml files in the Views folder
            // config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));    
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            // register code-only controls and markup controls
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            config.Resources.Register("bootstrap-theme", new StylesheetResource(new UrlResourceLocation("maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css")) { Dependencies = new[] { "bootstrap" } });
            config.Resources.Register("my-styles", new StylesheetResource(new FileResourceLocation("~/Content/MyStyles.css")) { Dependencies = new[] { "bootstrap-theme" } });


            // register custom resources and adjust paths to the built-in resources
            MapperInitializer.Initialize();
            
            //first Db call to init Db on aplication start
            AdminService adminService = new AdminService();
            adminService.GetUser(1);
        }
    }
}
