using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using ConferencySystem.BL;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.Framework.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConferencySystemTests
{
    [TestClass]
    public class CarteringServiceTests
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
            CarteringService carteringService = new CarteringService();

            for (int userId = 1; userId <= 3; userId++)
            {
                var user = carteringService.GetUser(userId);
                Assert.IsNotNull(user);
            }
        }

        [TestMethod]
        public void GetDefaultUsers_AddOrChangeBoarder_NotNull()
        {
            InitializeMapper();
            CarteringService carteringService = new CarteringService();

            int userId = 1;
            int carteringId = 1;

            carteringService.AddOrChangeBoarder(carteringId, userId);

            List<AppUserDTO> usersCartering = carteringService.GetUsersOverview();

            AppUserDTO newUser = usersCartering.FirstOrDefault(u => u.Id == userId);
            Assert.IsNotNull(newUser);
            Assert.IsNotNull(newUser.Cartering.FirstOrDefault(c => c.Id == carteringId));
            //carteringService.DeleteBoarder(carteringId, userId);
        }

        [TestMethod]
        public void GetCarterings_GetCarterings_LengthIsHigherThanZero()
        {
            InitializeMapper();
            CarteringService carteringService = new CarteringService();

            Assert.IsTrue(carteringService.GetCarterings().Length > 0);
        }

        [TestMethod]
        public void ChangeCarteringAdd_SaveChanges_IsNotNull()
        {
            InitializeMapper();
            CarteringService carteringService = new CarteringService();

            int userId = 1;
            int carteringId = 11;

            carteringService.DeleteBoarder(carteringId, userId);

            carteringService.SaveChanges(true,carteringId,true,0,0,userId);
            List<AppUserDTO> usersCartering = carteringService.GetUsersOverview();

            AppUserDTO user1 = usersCartering.FirstOrDefault(user => user.Id == userId);
            Assert.IsNotNull(user1);
            Assert.IsNotNull(user1.Cartering.FirstOrDefault(cartering => cartering.Id == carteringId));
            carteringService.DeleteBoarder(11,1);
        }

        [TestMethod]
        public void ChangeCarteringDelete_SaveChanges_IsNull()
        {
            InitializeMapper();
            CarteringService carteringService = new CarteringService();

            int userId = 1;
            int carteringId = 11;

            carteringService.SaveChanges(true, carteringId, true, 0, 0, userId);
            carteringService.SaveChanges(true, carteringId, false, 0, 0, userId);
            List<AppUserDTO> usersCartering = carteringService.GetUsersOverview();

            AppUserDTO user1 = usersCartering.FirstOrDefault(user => user.Id == userId);
            Assert.IsNotNull(user1);
            Assert.IsNull(user1.Cartering.FirstOrDefault(cartering => cartering.Id == carteringId));
        }


        [TestMethod]
        public void GetCartering_GetUsersOverView_NotNull()
        {
            InitializeMapper();
            CarteringService carteringService = new CarteringService();

            List<AppUserDTO> cartering = new List<AppUserDTO>();

            cartering = carteringService.GetUsersOverview();
            Assert.IsNotNull(cartering);
        }

        [TestMethod]
        public void GetCartering_GetUsersOverView_CountBiggerZero()
        {
            InitializeMapper();
            CarteringService carteringService = new CarteringService();

            List<AppUserDTO> users = new List<AppUserDTO>();

            users = carteringService.GetUsersOverview();
            Assert.IsTrue(users.Count > 0);
        }


        [TestMethod]
        public void GetCartering_GetCarteringOverview_NotNull()
        {
            InitializeMapper();
            CarteringService carteringService = new CarteringService();

            GridViewDataSet<CarteringDTO> cartering = new GridViewDataSet<CarteringDTO>();

            carteringService.GetCarteringOverview(cartering);
            Assert.IsNotNull(cartering);
        }

        [TestMethod]
        public void GetCartering_GetCarteringOverview_CountBiggerZero()
        {
            InitializeMapper();
            CarteringService carteringService = new CarteringService();

            GridViewDataSet<CarteringDTO> cartering = new GridViewDataSet<CarteringDTO>();

            carteringService.GetCarteringOverview(cartering);
            Assert.IsTrue(cartering.Items.Count > 0);
        }


        [TestMethod]
        public void GetCarteringSumary_GetCarteringSumary_NotNull()
        {
            InitializeMapper();
            CarteringService carteringService = new CarteringService();

            List<CarteringUsers> cartering = new List<CarteringUsers>();

            cartering = carteringService.GetCarteringSumary();
            Assert.IsNotNull(cartering);
        }

        [TestMethod]
        public void GetCarteringSumary_GetCarteringSumary_CountBiggerZero()
        {
            InitializeMapper();
            CarteringService carteringService = new CarteringService();

            List<CarteringUsers> cartering = new List<CarteringUsers>();

            cartering = carteringService.GetCarteringSumary();
            Assert.IsTrue(cartering.Count > 0);
        }
    }
}
