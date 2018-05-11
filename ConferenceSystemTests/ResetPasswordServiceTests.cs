using ConferencySystem.BL;
using ConferencySystem.BL.Services;
using ConferencySystem.BL.Services.UserManagment;
using ConferencySystem.DAL.Data;
using ConferencySystem.DAL.Data.UserIdentity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConferencySystemTests
{
    [TestClass]
    public class ResetPasswordServiceTests
    {
        public void InitializeMapper()
        {
            if (!MapperInitializer.IsInicialized)
            {
                MapperInitializer.Initialize();
                MapperInitializer.IsInicialized = true;
            }
        }

        public string GenerateResetToken(int userId)
        {

            var provider = new DpapiDataProtectionProvider("KonferencniSystem");
            var appUserManager = new AppUserManager(new DbContext());
            appUserManager.UserTokenProvider = new DataProtectorTokenProvider<AppUser, int>(provider.Create("EmailReset"));

            return appUserManager.GeneratePasswordResetToken(userId);
        }

        [TestMethod]
        public void NewUserRegistered_AddUser_NotNull()
        {
            InitializeMapper();
            ResetPasswordService resetPasswordService = new ResetPasswordService();
            LoginService loginService = new LoginService();
            int userId = 1;

            /*Generate token*/
            string token = GenerateResetToken(userId);
            /****************/

            resetPasswordService.ResetPassword(userId, token, "KonfSysUzivatel321");
            
            Assert.IsTrue(loginService.Login("uzivatel", "KonfSysUzivatel321").IsAuthenticated);

            /*Generate token*/
            token = GenerateResetToken(userId);
            /****************/

            resetPasswordService.ResetPassword(userId, token, "KonfSysUzivatel123");
            Assert.IsTrue(loginService.Login("uzivatel", "KonfSysUzivatel123").IsAuthenticated);
        }
    }
}
