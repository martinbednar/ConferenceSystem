using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services.UserManagment;
using ConferencySystem.DAL.Data;
using ConferencySystem.DAL.Data.UserIdentity;
using Microsoft.AspNet.Identity;
using DbContext = ConferencySystem.DAL.Data.DbContext;

namespace ConferencySystem.BL.Services
{
    public class RegisterService
    {
        public int AddUser (AppUserDTO userData)
        {
            AppUser user = Mapper.Map<AppUserDTO, AppUser>(userData);

            using (AppUserManager userManager = new AppUserManager(new DbContext()))
            {
                userManager.UserValidator =
                    new UserValidator<AppUser, int>(userManager) {AllowOnlyAlphanumericUserNames = false};
                
                userManager.Create(user, userData.PasswordHash);
                
                AppUser currentUser = userManager.FindByName(user.UserName);

                userManager.AddToRole(currentUser.Id, "user");

                return currentUser.Id;
            }
        }

        public int AddOrganization(OrganizationDTO organizationData, AppUserDTO userData)
        {
            using (var db = new DbContext())
            {
                Organization organization = Mapper.Map<OrganizationDTO, Organization>(organizationData);
                db.Organization.Add(organization);
                db.SaveChanges();

                int addedUserId = AddUser(userData);

                AppUser addedUser = db.Users.Find(addedUserId);

                organization.Users.Add(addedUser);

                addedUser.VariableSymbol = 2019000 + addedUserId;

                db.SaveChanges();

                return addedUser.Id;
            }
        }

        public int GetOrganizationId (long IN)
        {
            using (var db = new DbContext())
            {
                OrganizationDTO organization = Mapper.Map<Organization, OrganizationDTO>(db.Organization.SingleOrDefault(p => p.IN == IN));

                if (organization == null)
                {
                    return -1;
                }
                else
                {
                    return organization.Id;
                }
            }
        }

        public int UpdateOrganization(int id, AppUserDTO userData)
        {
            using (var db = new DbContext())
            {
                Organization organization = db.Organization.Find(id);

                int addedUserId = AddUser(userData);

                AppUser addedUser = db.Users.Find(addedUserId);

                if (organization != null) organization.Users.Add(addedUser);

                db.SaveChanges();

                AppUser user = db.Users.Find(addedUser.Id);

                user.VariableSymbol = 2019000 + user.Id;

                db.SaveChanges();

                return user.Id;
            }
        }

        public AppUserDTO GetUser(int id)
        {
            using (var db = new DbContext())
            {
                return Mapper.Map<AppUser, AppUserDTO>(db.Users.SingleOrDefault(p => p.Id == id));
            }
        }

        public List<AppUserDTO> GetUserEmails()
        {
            using (var db = new DbContext())
            {
                return db.Users.Select(p => new AppUserDTO()
                {
                    Id = p.Id,
                    Email = p.Email,
                    RegisterTimestamp = p.RegisterTimestamp
                }).ToList();
            }
        }
    }
}