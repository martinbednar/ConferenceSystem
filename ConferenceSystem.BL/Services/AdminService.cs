using System.Collections.Generic;
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

                int sequencenumber = 1;

                foreach (var userDTO in users)
                {
                    userDTO.SequenceNumber = sequencenumber;
                    sequencenumber++;
                }

                usersDataSet.LoadFromQueryable(users.AsQueryable());
            }
        }

        public void GetLecturers(GridViewDataSet<AppUserDTO> usersDataSet)
        {
            using (var db = new DbContext())
            {
                var users = db.Users.Where(user => user.Roles.All(role => role.RoleId == 4)).ProjectTo<AppUserDTO>().ToList().OrderBy(u => u.RegisterTimestamp);

                int sequencenumber = 1;

                foreach (var userDTO in users)
                {
                    userDTO.SequenceNumber = sequencenumber;
                    sequencenumber++;
                }

                usersDataSet.LoadFromQueryable(users.AsQueryable());
            }
        }

            public void GetLecturersNEW(GridViewDataSet<AppUserDTO> usersDataSet)
        {
            using (var db = new DbContext())
            {
                var query = db.Users.Select(p => new AppUserDTO()
                {
                    Id = p.Id,
                    RegisterTimestamp = p.RegisterTimestamp,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    BirthDate = p.BirthDate,
                    BirthPlace = p.BirthPlace,
                    TitleBefore = p.TitleBefore,
                    TitleAfter = p.TitleAfter,
                    Email = p.Email,
                    PhoneNumber = p.PhoneNumber,
                    Position = p.Position,
                    Agreement = p.Agreement,
                    PaidDate = p.PaidDate,
                    VariableSymbol = p.VariableSymbol,
                    InfoFrom = p.InfoFrom,
                    WantGet = p.WantGet,
                    NoteAdmin = p.NoteAdmin,
                    NoteUser = p.NoteUser,
                    IsAlternate = p.IsAlternate,
                    InvoiceNumber = p.InvoiceNumber,
                    Organization = new OrganizationDTO()
                    {
                        Id = p.Organization.Id,
                        IN = p.Organization.IN,
                        VATID = p.Organization.VATID,
                        Name = p.Organization.Name,
                        BillStreet = p.Organization.BillStreet,
                        Town = p.Organization.Town,
                        PostalCode = p.Organization.PostalCode
                    }
                }).Where(user => user.Roles.All(role => role.RoleId == 4));

                List<AppUserDTO> usersCompletInfo = new List<AppUserDTO>();

                foreach (AppUserDTO user in query.ToList())
                {
                    usersCompletInfo.Add(Mapper.Map<AppUserDTO, AppUserDTO>(user));
                }

                usersDataSet.LoadFromQueryable(usersCompletInfo.AsQueryable());
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


        public void DeleteOrganization(int id)
        {
            using (var db = new DbContext())
            {
                var organization = db.Organization.Find(id);
                db.Organization.Remove(organization);
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

        public InvoiceDTO GetInvoice(int userId)
        {
            using (var db = new DbContext())
            {
                return Mapper.Map<Invoice, InvoiceDTO>(db.Invoice.SingleOrDefault(i => i.UserId == userId));
            }
        }
    }
}