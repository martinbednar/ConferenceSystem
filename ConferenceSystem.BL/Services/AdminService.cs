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
        public void GetPersons(GridViewDataSet<AppUserDTO> peopleDataSet)
        {
            using (var db = new DbContext())
            {
                var people = db.Users.Where(user => user.Roles.All(role => role.RoleId == 1)).ProjectTo<AppUserDTO>().ToList();
                peopleDataSet.LoadFromQueryable(people.AsQueryable());
            }
        }


        public void DeletePerson(int id)
        {
            using (var db = new DbContext())
            {
                var person = db.Users.Find(id);
                db.Users.Remove(person);
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


        public AppUserDTO GetPerson(int id)
        {
            using (var db = new DbContext())
            {
                return Mapper.Map<AppUser, AppUserDTO>(db.Users.SingleOrDefault(p => p.Id == id));
            }
        }

        public void SavePerson(AppUserDTO personData)
        {
            using (var db = new DbContext())
            {
                var person = db.Users.Find(personData.Id);

                Mapper.Map(personData, person);

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