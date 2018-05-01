using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ConferencySystem.BL.DTO;
using ConferencySystem.DAL.Data;
using ConferencySystem.DAL.Data.UserIdentity;
using DotVVM.Framework.Controls;
using DbContext = ConferencySystem.DAL.Data.DbContext;

namespace ConferencySystem.BL.Services
{
    public class AdminService
    {
        public void GetUsers(GridViewDataSet<AppUserDTO> usersDataSet)
        {
            using (var db = new DbContext())
            {
                var users = db.Users.Where(user => user.Roles.All(role => role.RoleId == 1)).ProjectTo<AppUserDTO>().ToList();
                usersDataSet.LoadFromQueryable(users.AsQueryable());
            }
        }


        public void DeleteUser(int id)
        {
            using (var db = new DbContext())
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }


        public OrganizationDTO GetOrganization(int organizationId)
        {
            using (var db = new DbContext())
            {
                var organization = Mapper.Map<Organization, OrganizationDTO>(db.Organization.SingleOrDefault(o => o.Id == organizationId));

                return organization;
            }
        }


        public AppUserDTO GetUser(int id)
        {
            using (var db = new DbContext())
            {
                return Mapper.Map<AppUser, AppUserDTO>(db.Users.SingleOrDefault(p => p.Id == id));
            }
        }

        public void SaveUser(AppUserDTO userData)
        {
            using (var db = new DbContext())
            {
                var user = db.Users.Find(userData.Id);

                Mapper.Map(userData, user);

                db.SaveChanges();
            }
        }


        public void SaveOrganization(OrganizationDTO organizationData)
        {
            using (var db = new DbContext())
            {
                var organization = db.Organization.Find(organizationData.Id);
                Mapper.Map(organizationData, organization);

                db.SaveChanges();
            }
        }
    }
}