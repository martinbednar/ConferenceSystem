using ConferencySystem.BL;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConferencySystemTests
{
    [TestClass]
    public class RegisterServiceTests
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
        public void NewUserRegistered_AddUser_NotNull()
        {
            InitializeMapper();
            RegisterService registerService = new RegisterService();
            AdminService adminService = new AdminService();

            AppUserDTO newUser = new AppUserDTO()
            {
                Email = "testUser",
                PasswordHash = "AAvuhHR+UhsEgslJ1FDgtBpfipMZ4IYqY62Bst5BwU2MYbP4SPld7bQgG6ZmENQC7A=="
            };

            int newUserId = registerService.AddUser(newUser);
            Assert.IsNotNull(registerService.GetUser(newUserId));
            adminService.DeleteUser(newUserId);
        }

        [TestMethod]
        public void NewUserRegistered_AddUser_IsTrue()
        {
            InitializeMapper();
            RegisterService registerService = new RegisterService();
            AdminService adminService = new AdminService();

            AppUserDTO newUser = new AppUserDTO()
            {
                Email = "testUser",
                PasswordHash = "AAvuhHR+UhsEgslJ1FDgtBpfipMZ4IYqY62Bst5BwU2MYbP4SPld7bQgG6ZmENQC7A=="
            };

            int newUserId = registerService.AddUser(newUser);
            Assert.IsTrue(registerService.GetUser(newUserId).Id == newUserId);
            Assert.IsTrue(registerService.GetUser(newUserId).Email == newUser.Email);
            adminService.DeleteUser(newUserId);
        }

        [TestMethod]
        public void NewOrganizationAdded_AddOrganization_NotNull()
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

            Assert.IsNotNull(adminService.GetOrganization(newOrganizationId));

            adminService.DeleteUser(newUserId);
            adminService.DeleteOrganization(newOrganizationId);
        }

        [TestMethod]
        public void NewOrganizationAdded_AddOrganization_IsTrue()
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

            Assert.IsTrue(adminService.GetOrganization(newOrganizationId).Id == newOrganizationId);
            Assert.IsTrue(adminService.GetOrganization(newOrganizationId).Name == newOrganization.Name);

            adminService.DeleteUser(newUserId);
            adminService.DeleteOrganization(newOrganizationId);
        }

        [TestMethod]
        public void GetDefaultOrganizationId_GetOrganizationId_NotNull()
        {
            InitializeMapper();
            RegisterService registerService = new RegisterService();

            Assert.IsNotNull(registerService.GetOrganizationId(0));
        }

        [TestMethod]
        public void GetDefaultOrganizationId_GetOrganizationId_IsTrue()
        {
            InitializeMapper();
            RegisterService registerService = new RegisterService();

            Assert.IsTrue(registerService.GetOrganizationId(0) == 1);
        }

        [TestMethod]
        public void UserAddedToOrganization_UpdateOrganization_IsTrue()
        {
            InitializeMapper();
            RegisterService registerService = new RegisterService();
            AdminService adminService = new AdminService();

            AppUserDTO newUser = new AppUserDTO()
            {
                Email = "testUser",
                PasswordHash = "AAvuhHR+UhsEgslJ1FDgtBpfipMZ4IYqY62Bst5BwU2MYbP4SPld7bQgG6ZmENQC7A=="
            };

            int newUserId = registerService.UpdateOrganization(1, newUser);

            Assert.IsTrue(adminService.GetUser(newUserId).Organization.Id == 1);
            adminService.DeleteUser(newUserId);
        }

        [TestMethod]
        public void GetUser_GetUser_NotNull()
        {
            InitializeMapper();
            RegisterService registerService = new RegisterService();

            for (int userId = 1; userId <= 3; userId++)
            {
                var user = registerService.GetUser(userId);
                Assert.IsNotNull(user);
            }
        }

        [TestMethod]
        public void GetUserEmails_GetUserEmails_NotNull()
        {
            InitializeMapper();
            RegisterService registerService = new RegisterService();

            Assert.IsNotNull(registerService.GetUserEmails());
        }

        [TestMethod]
        public void GetUserEmails_GetUserEmails_CountIsBiggerThan2()
        {
            InitializeMapper();
            RegisterService registerService = new RegisterService();

            Assert.IsNotNull(registerService.GetUserEmails().Count > 2);
        }
    }
}
