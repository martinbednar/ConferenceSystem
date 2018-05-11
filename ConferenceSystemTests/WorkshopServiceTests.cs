using System.Collections.Generic;
using System.Linq;
using ConferencySystem.BL;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.Framework.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConferencySystemTests
{
    [TestClass]
    public class WorkshopServiceTests
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
            WorkshopService workshopService = new WorkshopService();

            for (int userId = 1; userId <= 3; userId++)
            {
                var user = workshopService.GetUser(userId);
                Assert.IsNotNull(user);
            }
        }

        [TestMethod]
        public void GetDefaultWorkshopsBlocks_GetWorkshopsBlocks_NotNull()
        {
            InitializeMapper();
            WorkshopService workshopService = new WorkshopService();

            var workshopsBlocks = workshopService.GetWorkshopsBlocks(1);
            Assert.IsNotNull(workshopsBlocks);
        }

        [TestMethod]
        public void GetDefaultWorkshopsBlocks_GetWorkshopsBlocks_CountIs4()
        {
            InitializeMapper();
            WorkshopService workshopService = new WorkshopService();

            var workshopsBlocks = workshopService.GetWorkshopsBlocks(1);
            Assert.IsTrue(workshopsBlocks.Count == 4);
        }

        [TestMethod]
        public void WorkshopRegistered_RegisterWorkshop_NotNull()
        {
            InitializeMapper();
            WorkshopService workshopService = new WorkshopService();

            workshopService.RegisterWorkshop(1, 3);
            var workshop3 = workshopService.GetWorkshopsBlocks(1)[0].Workshops.FirstOrDefault(w => w.Id == 3);
            Assert.IsNotNull(workshop3);
            Assert.IsNotNull(workshop3.Users.FirstOrDefault(u => u.Id == 1));
            workshopService.UnregisterWorkshop(1, 3);
        }

        [TestMethod]
        public void WorkshopUnregistered_UnregisterWorkshop_IsNull()
        {
            InitializeMapper();
            WorkshopService workshopService = new WorkshopService();

            workshopService.RegisterWorkshop(1, 3);
            var workshop3 = workshopService.GetWorkshopsBlocks(1)[0].Workshops.FirstOrDefault(w => w.Id == 3);
            Assert.IsNotNull(workshop3);
            Assert.IsNotNull(workshop3.Users.FirstOrDefault(u => u.Id == 1));
            workshopService.UnregisterWorkshop(1, 3);

            workshop3 = workshopService.GetWorkshopsBlocks(1)[0].Workshops.FirstOrDefault(w => w.Id == 3);
            Assert.IsNotNull(workshop3);
            Assert.IsNull(workshop3.Users.FirstOrDefault(u => u.Id == 1));
        }

        [TestMethod]
        public void GetUsersOverview_GetUsersOverview_NotNull()
        {
            InitializeMapper();
            WorkshopService workshopService = new WorkshopService();

            GridViewDataSet<UserWorkshops> usersDataSet = new GridViewDataSet<UserWorkshops>();

            workshopService.GetUsersOverview(usersDataSet);
            Assert.IsNotNull(usersDataSet);
        }

        [TestMethod]
        public void GetUsersOverview_GetUsersOverview_ItemsCountIsBiggerThan2()
        {
            InitializeMapper();
            WorkshopService workshopService = new WorkshopService();

            GridViewDataSet<UserWorkshops> usersDataSet = new GridViewDataSet<UserWorkshops>();

            workshopService.GetUsersOverview(usersDataSet);
            Assert.IsTrue(usersDataSet.Items.Count > 2);
        }

        [TestMethod]
        public void GetWorkshopSumary_GetWorkshopSumary_NotNull()
        {
            InitializeMapper();
            WorkshopService workshopService = new WorkshopService();

            List<WorkshopsBlockDTO> workshopSumary = workshopService.GetWorkshopSumary();
            Assert.IsNotNull(workshopSumary);
        }

        [TestMethod]
        public void GetWorkshopSumary_GetWorkshopSumary_ItemsCountIsBiggerThan3()
        {
            InitializeMapper();
            WorkshopService workshopService = new WorkshopService();

            List<WorkshopsBlockDTO> workshopSumary = workshopService.GetWorkshopSumary();
            Assert.IsTrue(workshopSumary.Count > 3);
        }
    }
}
