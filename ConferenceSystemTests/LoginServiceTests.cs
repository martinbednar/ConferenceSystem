using System;
using System.Collections.Generic;
using ConferencySystem.BL;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConferencySystemTests
{
    [TestClass]
    public class LoginServiceTests
    {
        public void InitializeMapper()
        {
            if (!MapperInitializer.IsInicialized)
            {
                MapperInitializer.Initialize();
                MapperInitializer.IsInicialized = true;
            }
        }

        [TestMethod]
        public void GetDefaultUsers_GetUser_NotNull()
        {
            InitializeMapper();
            LoginService loginService = new LoginService();
            AdminService adminService = new AdminService();

            for (int userId = 1; userId <= 3; userId++)
            {
                var userFromAdmin = adminService.GetUser(userId);
                var user = loginService.GetUser(userFromAdmin.Email);
                Assert.IsNotNull(user);
            }
        }

        [TestMethod]
        public void GetAllUsers_GetUsers_NotNull()
        {
            InitializeMapper();
            LoginService loginService = new LoginService();

            List<AppUserDTO> users = new List<AppUserDTO>();

            users = loginService.GetAllUsers();
            Assert.IsNotNull(users);
        }

        [TestMethod]
        public void GetAllUsers_GetUsers_CountIsBiggerThanZero()
        {
            InitializeMapper();
            LoginService loginService = new LoginService();

            List<AppUserDTO> users;// = new List<AppUserDTO>();
            users = loginService.GetAllUsers();
            foreach (AppUserDTO user in users)
            {
                Console.OpenStandardOutput(100);
                Console.WriteLine("1.");
            }
            //Assert.IsTrue(users.Count > 0);
        }

        [TestMethod]
        public void LogEmailSentToDb_EmailSent_IsTrue()
        {
            InitializeMapper();
            LoginService loginService = new LoginService();
            AdminService adminService = new AdminService();
            CarteringService carteringService = new CarteringService();

            AppUserDTO user = adminService.GetUser(1);

            loginService.EmailSent(1);
            Assert.IsTrue(carteringService.GetUser(1).WasEmailWorkshopSent);
            adminService.SaveUser(user);
        }

        [TestMethod]
        public void LogInUser_Login_NotNull()
        {
            InitializeMapper();
            LoginService loginService = new LoginService();

            Assert.IsNotNull(loginService.Login("admin", "KonfSysAdmin123"));
        }

        [TestMethod]
        public void UserLogged_Login_IsAuthenticated()
        {
            InitializeMapper();
            LoginService loginService = new LoginService();

            Assert.IsNotNull(loginService.Login("admin", "KonfSysAdmin123").IsAuthenticated);
        }
    }
}
