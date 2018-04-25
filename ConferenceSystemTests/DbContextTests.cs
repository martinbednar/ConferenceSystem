using System.Linq;
using ConferencySystem;
using ConferencySystem.BL;
using ConferencySystem.BL.Services;
using ConferencySystem.DAL.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConferencySystemTests
{
    [TestClass]
    public class DbContextTests
    {
        [TestMethod]
        public void DbConnectionTest()
        {
            using (var dbContext = new DbContext())
            {
                Assert.IsNotNull(dbContext.Users.Any());
            }
        }

        private readonly AdminService _adminService = new AdminService();
        private int _personID = 2;

        [TestMethod]
        public void FindByID_User_NotNull()
        {
            MapperInitializer.Initialize();

            var user = _adminService.GetPerson(_personID);
            Assert.IsNotNull(user);
        }
    }
}
