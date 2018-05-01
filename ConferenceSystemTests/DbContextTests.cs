using System.Linq;
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
    }
}
