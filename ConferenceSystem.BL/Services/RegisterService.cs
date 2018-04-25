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
        public int AddPerson (AppUserDTO personData)
        {
            AppUser person = Mapper.Map<AppUserDTO, AppUser>(personData);

            using (AppUserManager userManager = new AppUserManager(new DbContext()))
            {
                userManager.UserValidator =
                    new UserValidator<AppUser, int>(userManager) {AllowOnlyAlphanumericUserNames = false};
                
                userManager.Create(person, personData.PasswordHash);
                
                AppUser currentUser = userManager.FindByName(person.UserName);

                userManager.AddToRole(currentUser.Id, "user");

                return currentUser.Id;
            }
        }

        public int AddOrganization(OrganizationDTO organizationData, AppUserDTO personData)
        {
            using (var db = new DbContext())
            {
                Organization organization = Mapper.Map<OrganizationDTO, Organization>(organizationData);
                db.Organization.Add(organization);
                db.SaveChanges();

                int addedPersonId = AddPerson(personData);

                AppUser addedPerson = db.Users.Find(addedPersonId);

                organization.People.Add(addedPerson);

                addedPerson.VariableSymbol = 2018000 + addedPersonId;

                db.SaveChanges();

                return addedPerson.Id;
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

        public int UpdateOrganization(int id, AppUserDTO personData)
        {
            using (var db = new DbContext())
            {
                Organization organization = db.Organization.Find(id);

                int addedPersonId = AddPerson(personData);

                AppUser addedPerson = db.Users.Find(addedPersonId);

                if (organization != null) organization.People.Add(addedPerson);

                db.SaveChanges();

                AppUser person = db.Users.Find(addedPerson.Id);

                person.VariableSymbol = 2018000 + person.Id;

                db.SaveChanges();

                return person.Id;
            }
        }

        public AppUserDTO GetPerson(int id)
        {
            using (var db = new DbContext())
            {
                return Mapper.Map<AppUser, AppUserDTO>(db.Users.SingleOrDefault(p => p.Id == id));
            }
        }

        public List<AppUserDTO> GetPersonEmails()
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