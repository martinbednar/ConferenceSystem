using System.Web.Hosting;
using ConferencySystem.BL;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using DotVVM.Framework;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Routing;
using DotVVM.Framework.Controls.Bootstrap;

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
            config.RouteTable.Add("People", "people", "Views/Admin/People.dothtml");
            config.RouteTable.Add("Person", "person/{OrgId}/{PersonId}", "Views/Admin/Person.dothtml");
            config.RouteTable.Add("Register", "register", "Views/User/Register.dothtml");
            config.RouteTable.Add("RegistrationComplete", "complete", "Views/User/RegistrationComplete.dothtml");
            config.RouteTable.Add("Login", "login", "Views/Login.dothtml");
            config.RouteTable.Add("PwdForgotten", "password", "Views/PwdForgotten.dothtml");
            config.RouteTable.Add("CarteringSelect", "cartering", "Views/User/CarteringSelect.dothtml");
            config.RouteTable.Add("CarteringOverviewPeople", "carteringoverviewpeople", "Views/Admin/CarteringOverviewPeople.dothtml");
            config.RouteTable.Add("CarteringOverviewCartering", "carteringoverviewcartering", "Views/Admin/CarteringOverviewCartering.dothtml");
            config.RouteTable.Add("WorkshopSelect", "workshop", "Views/User/WorkshopSelect.dothtml");
            config.RouteTable.Add("WorkshopOverviewPeople", "workshopoverviewpeople", "Views/Admin/WorkshopOverviewPeople.dothtml");
            config.RouteTable.Add("WorkshopOverviewWorkshop", "workshopoverviewworkshop", "Views/Admin/WorkshopOverviewWorkshops.dothtml");
            config.RouteTable.Add("CarteringPerson", "carteringperson/{PersonId}", "Views/Admin/CarteringPerson.dothtml");
            config.RouteTable.Add("Export", "export", "Views/Admin/Export.dothtml");
            config.RouteTable.Add("RegisterForm", "registerform", "Views/User/RegisterForm.dothtml");
            config.RouteTable.Add("WorkshopPerson", "workshopperson/{PersonId}", "Views/Admin/WorkshopPerson.dothtml");

            // Uncomment the following line to auto-register all dothtml files in the Views folder
            // config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));    
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            // register code-only controls and markup controls
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            // register custom resources and adjust paths to the built-in resources
            MapperInitializer.Initialize();
        }
    }
}
