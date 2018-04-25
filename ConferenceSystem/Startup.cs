using System.Web.Hosting;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using DotVVM.Framework;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Hosting;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(ConferencySystem.Startup))]
namespace ConferencySystem
{
    public class Startup
    {
        // ASP.NET Core 2.0 (DotVVM 1.1.6 and newer)
        /*public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(sharedOptions =>
                {
                    sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => {
                    options.Events = new CookieAuthenticationEvents
                    {
                        OnRedirectToReturnUrl = c => DotvvmAuthenticationHelper.ApplyRedirectResponse(c.HttpContext, c.RedirectUri),
                        OnRedirectToAccessDenied = c => DotvvmAuthenticationHelper.ApplyStatusCodeResponse(c.HttpContext, 403),
                        OnRedirectToLogin = c => DotvvmAuthenticationHelper.ApplyRedirectResponse(c.HttpContext, c.RedirectUri),
                        OnRedirectToLogout = c => DotvvmAuthenticationHelper.ApplyRedirectResponse(c.HttpContext, c.RedirectUri)
                    };
                    options.LoginPath = new PathString("/login");
                });
            // ...
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAuthentication();
            // ...
        }*/


        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication();
        }


        public void Configuration(IAppBuilder app)
        {
            var applicationPhysicalPath = HostingEnvironment.ApplicationPhysicalPath;

            //Owin Security Cookies - authentication
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/login"),
                Provider = new CookieAuthenticationProvider()
                {
                    OnApplyRedirect = context => DotvvmAuthenticationHelper.ApplyRedirectResponse(context.OwinContext, context.RedirectUri)
                }
            });

            // use DotVVM
            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(applicationPhysicalPath, options: options =>
            {
                options.AddDefaultTempStorages("temp");
            });
#if !DEBUG
            dotvvmConfiguration.Debug = false;
#endif

            // use static files
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileSystem = new PhysicalFileSystem(applicationPhysicalPath)
            });
        }
    }
}
