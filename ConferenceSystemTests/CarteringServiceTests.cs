using System.Collections.Generic;
using System.Linq;
using ConferencySystem.BL;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
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
            carteringService.DeleteBoarder(carteringId, userId);
        }

        [TestMethod]
        public void GetCarterings_GetCarterings_LengthIsHigherThanZero()
        {
            InitializeMapper();
            CarteringService carteringService = new CarteringService();

            Assert.IsTrue(carteringService.GetCarterings().Length > 0);
        }
    }
}
