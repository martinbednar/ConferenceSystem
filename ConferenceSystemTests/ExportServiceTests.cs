using System;
using ConferencySystem.BL;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.Framework.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConferencySystemTests
{
    [TestClass]
    public class ExportServiceTests
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
        public void GetAllDataAboutUser_GetAllData_IsNotNull()
        {
            InitializeMapper();
            ExportService exportService = new ExportService();
            GridViewDataSet<UserCompletInfo> usersDataSet = new GridViewDataSet<UserCompletInfo>();

            exportService.GetAllData(usersDataSet);

            Assert.IsNotNull(usersDataSet);
        }

        [TestMethod]
        public void GetAllDataAboutUser_GetAllData_CountIsBiggerThanZero()
        {
            InitializeMapper();
            ExportService exportService = new ExportService();
            GridViewDataSet<UserCompletInfo> usersDataSet = new GridViewDataSet<UserCompletInfo>();

            exportService.GetAllData(usersDataSet);
            
            Assert.IsTrue(usersDataSet.Items.Count > 0);
        }
    }
}
