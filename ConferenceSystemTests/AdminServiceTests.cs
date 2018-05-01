using ConferencySystem.BL;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using DotVVM.Framework.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConferencySystemTests
{
    [TestClass]
    public class AdminServiceTests
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
            AdminService adminService = new AdminService();

            for (int userId = 1; userId <= 3; userId++)
            {
                var user = adminService.GetUser(userId);
                Assert.IsNotNull(user);
            }
        }

        [TestMethod]
        public void GetAllUsers_GetUsers_NotNull()
        {
            InitializeMapper();
            AdminService adminService = new AdminService();

            GridViewDataSet<AppUserDTO> users = new GridViewDataSet<AppUserDTO>();

            adminService.GetUsers(users);
            Assert.IsNotNull(users);
        }

        [TestMethod]
        public void GetAllUsers_GetUsers_CountNotZero()
        {
            InitializeMapper();
            AdminService adminService = new AdminService();

            GridViewDataSet<AppUserDTO> users = new GridViewDataSet<AppUserDTO>();

            adminService.GetUsers(users);
            Assert.IsTrue(users.Items.Count > 0);
        }

        [TestMethod]
        public void DeleteUser_DeleteUser_NotFound()
        {
            InitializeMapper();
            AdminService adminService = new AdminService();
            RegisterService registerService = new RegisterService();

            AppUserDTO newUser = new AppUserDTO()
            {
                Email = "testUser",
                PasswordHash = "AAvuhHR+UhsEgslJ1FDgtBpfipMZ4IYqY62Bst5BwU2MYbP4SPld7bQgG6ZmENQC7A=="
            };

            int newUserId = registerService.AddUser(newUser);

            adminService.DeleteUser(newUserId);
            Assert.IsNull(adminService.GetUser(newUserId));
        }

        [TestMethod]
        public void DeleteOrganization_DeleteOrganization_NotFound()
        {
            InitializeMapper();
            AdminService adminService = new AdminService();
            RegisterService registerService = new RegisterService();

            AppUserDTO newUser = new AppUserDTO()
            {
                Email = "testUser",
                PasswordHash = "AAvuhHR+UhsEgslJ1FDgtBpfipMZ4IYqY62Bst5BwU2MYbP4SPld7bQgG6ZmENQC7A=="
            };

            OrganizationDTO newOrganization = new OrganizationDTO()
            {
                IN = 1234567,
                Name = "testOrganization"
            };

            int newUserId = registerService.AddOrganization(newOrganization, newUser);

            int newOrganizationId = registerService.GetOrganizationId(newOrganization.IN);

            adminService.DeleteUser(newUserId);
            adminService.DeleteOrganization(newOrganizationId);
            Assert.IsNull(adminService.GetOrganization(newOrganizationId));
        }

        [TestMethod]
        public void GetDefaultOrganization_GetOrganization_NotNull()
        {
            InitializeMapper();
            AdminService adminService = new AdminService();

            var organization = adminService.GetOrganization(1);
            Assert.IsNotNull(organization);
        }

        [TestMethod]
        public void SaveChangesInUserData_SaveUser_NotNull()
        {
            InitializeMapper();
            AdminService adminService = new AdminService();
            RegisterService registerService = new RegisterService();

            AppUserDTO newUser = new AppUserDTO()
            {
                Email = "testUser",
                PasswordHash = "AAvuhHR+UhsEgslJ1FDgtBpfipMZ4IYqY62Bst5BwU2MYbP4SPld7bQgG6ZmENQC7A=="
            };

            int newUserId = registerService.AddUser(newUser);

            newUser.Id = newUserId;
            newUser.Email = "test2User";

            adminService.SaveUser(newUser);
            AppUserDTO foundUser = adminService.GetUser(newUserId);
            Assert.IsNotNull(foundUser);
            Assert.AreEqual(foundUser.Id, newUserId);
            adminService.DeleteUser(newUserId);
        }

        [TestMethod]
        public void SaveChangesInOrganizationData_SaveOrganizations_NotNull()
        {
            InitializeMapper();
            AdminService adminService = new AdminService();
            RegisterService registerService = new RegisterService();

            OrganizationDTO newOrganization = new OrganizationDTO()
            {
                Name = "testOrganization",
                IN = 1234567
            };

            AppUserDTO newUser = new AppUserDTO()
            {
                Email = "testUser",
                PasswordHash = "AAvuhHR+UhsEgslJ1FDgtBpfipMZ4IYqY62Bst5BwU2MYbP4SPld7bQgG6ZmENQC7A=="
            };

            int newUserId = registerService.AddOrganization(newOrganization, newUser);

            int newOrganizationId = registerService.GetOrganizationId(newOrganization.IN);

            newOrganization.Id = newOrganizationId;
            newOrganization.Name = "test2Organization";

            adminService.SaveOrganization(newOrganization);
            OrganizationDTO foundOrganization = adminService.GetOrganization(newOrganizationId);
            Assert.IsNotNull(foundOrganization);
            Assert.AreEqual(foundOrganization.Id, newOrganizationId);
            adminService.DeleteUser(newUserId);
            adminService.DeleteOrganization(newOrganizationId);
        }
    }
}