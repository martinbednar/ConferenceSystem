using System.Collections.Generic;
using System.Linq;
using ConferencySystem.BL.DTO;
using ConferencySystem.DAL.Data;
using DotVVM.Framework.Controls;

namespace ConferencySystem.BL.Services
{
    public class ExportService
    {
        public void GetAllUsersData (GridViewDataSet<UserCompletInfo> usersDataSet)
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
                    },
                    Cartering = p.Cartering.Select(c => new CarteringDTO()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        When = c.When,
                        Category = c.Category,
                    }).OrderBy(c => c.When),
                    Workshops = p.Workshops.Select(w => new WorkshopDTO()
                    {
                        Id = w.Id,
                        Name = w.Name,
                        Presenter = w.Presenter,
                        WorkshopsBlock = new WorkshopsBlockDTO()
                        {
                            Id = w.WorkshopsBlock.Id,
                            Name = w.WorkshopsBlock.Name,
                            Start = w.WorkshopsBlock.Start,
                            End = w.WorkshopsBlock.End
                        }
                    }).OrderBy(w => w.WorkshopsBlock.Start),
                    Roles = p.Roles.Select(r => new AppUserRoleDTO
                        {
                            UserId = r.UserId,
                            RoleId = r.RoleId,
                        })
                }).Where(user => user.Roles.All(role => (role.RoleId == 1) || (role.RoleId == 6)));

                List<UserCompletInfo> usersCompletInfo = new List<UserCompletInfo>();

                foreach (AppUserDTO user in query.ToList())
                {
                    usersCompletInfo.Add(new UserCompletInfo(user));
                }

                usersDataSet.LoadFromQueryable(usersCompletInfo.AsQueryable());
            }
        }
    

        public void GetAllVisitorsData(GridViewDataSet<UserCompletInfo> usersDataSet)
        {
            using (var db = new DbContext())
            {
                var query = db.Users.Select(p => new AppUserDTO()
                {
                    Id = p.Id,
                    RegisterTimestamp = p.RegisterTimestamp,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    //BirthDate = p.BirthDate,
                    //BirthPlace = p.BirthPlace,
                    TitleBefore = p.TitleBefore,
                    TitleAfter = p.TitleAfter,
                    Email = p.Email,
                    //PhoneNumber = p.PhoneNumber,
                    //Position = p.Position,
                    Agreement = p.Agreement,
                    //PaidDate = p.PaidDate,
                    //VariableSymbol = p.VariableSymbol,
                    InfoFrom = p.InfoFrom,
                    //WantGet = p.WantGet,
                    NoteAdmin = p.NoteAdmin,
                    NoteUser = p.NoteUser,
                    //IsAlternate = p.IsAlternate,
                    //InvoiceNumber = p.InvoiceNumber,
                    /*Organization = new OrganizationDTO()
                    {
                        Id = p.Organization.Id,
                        IN = p.Organization.IN,
                        VATID = p.Organization.VATID,
                        Name = p.Organization.Name,
                        BillStreet = p.Organization.BillStreet,
                        Town = p.Organization.Town,
                        PostalCode = p.Organization.PostalCode
                    },*/
                    Cartering = p.Cartering.Select(c => new CarteringDTO()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        When = c.When,
                        Category = c.Category,
                    }).OrderBy(c => c.When),
                    Workshops = p.Workshops.Select(w => new WorkshopDTO()
                    {
                        Id = w.Id,
                        Name = w.Name,
                        Presenter = w.Presenter,
                        WorkshopsBlock = new WorkshopsBlockDTO()
                        {
                            Id = w.WorkshopsBlock.Id,
                            Name = w.WorkshopsBlock.Name,
                            Start = w.WorkshopsBlock.Start,
                            End = w.WorkshopsBlock.End
                        }
                    }).OrderBy(w => w.WorkshopsBlock.Start),
                    Roles = p.Roles.Select(r => new AppUserRoleDTO
                    {
                        UserId = r.UserId,
                        RoleId = r.RoleId,
                    })
                }).Where(user => user.Roles.All(role => role.RoleId == 5));

                List<UserCompletInfo> usersCompletInfo = new List<UserCompletInfo>();

                foreach (AppUserDTO user in query.ToList())
                {
                    usersCompletInfo.Add(new UserCompletInfo(user));
                }

                usersDataSet.LoadFromQueryable(usersCompletInfo.AsQueryable());
            }
        }


        public void GetAllLecturersData(GridViewDataSet<UserCompletInfo> usersDataSet)
        {
            using (var db = new DbContext())
            {
                var query = db.Users.Select(p => new AppUserDTO()
                {
                    Id = p.Id,
                    RegisterTimestamp = p.RegisterTimestamp,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    //BirthDate = p.BirthDate,
                    //BirthPlace = p.BirthPlace,
                    TitleBefore = p.TitleBefore,
                    TitleAfter = p.TitleAfter,
                    Email = p.Email,
                    PhoneNumber = p.PhoneNumber,
                    //Position = p.Position,
                    Agreement = p.Agreement,
                    //PaidDate = p.PaidDate,
                    //VariableSymbol = p.VariableSymbol,
                    //InfoFrom = p.InfoFrom,
                    //WantGet = p.WantGet,
                    NoteAdmin = p.NoteAdmin,
                    NoteUser = p.NoteUser,
                    //IsAlternate = p.IsAlternate,
                    //InvoiceNumber = p.InvoiceNumber,
                    /*Organization = new OrganizationDTO()
                    {
                        Id = p.Organization.Id,
                        IN = p.Organization.IN,
                        VATID = p.Organization.VATID,
                        Name = p.Organization.Name,
                        BillStreet = p.Organization.BillStreet,
                        Town = p.Organization.Town,
                        PostalCode = p.Organization.PostalCode
                    },*/
                    LecturerInfo = new LecturerInfoDTO()
                    {
                        Id = p.LecturerInfo.Id,
                        Introduce = p.LecturerInfo.Introduce,
                        Fee = p.LecturerInfo.Fee,
                        Street = p.LecturerInfo.Street,
                        Town = p.LecturerInfo.Town,
                        PostalCode = p.LecturerInfo.PostalCode,
                        AccountNumber = p.LecturerInfo.AccountNumber,
                        IN = p.LecturerInfo.IN,
                        Accomodation = p.LecturerInfo.Accomodation,
                        RoomMate = p.LecturerInfo.RoomMate
                    },
                    Cartering = p.Cartering.Select(c => new CarteringDTO()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        When = c.When,
                        Category = c.Category,
                    }).OrderBy(c => c.When),
                    Workshops = p.Workshops.Select(w => new WorkshopDTO()
                    {
                        Id = w.Id,
                        Name = w.Name,
                        Presenter = w.Presenter,
                        WorkshopsBlock = new WorkshopsBlockDTO()
                        {
                            Id = w.WorkshopsBlock.Id,
                            Name = w.WorkshopsBlock.Name,
                            Start = w.WorkshopsBlock.Start,
                            End = w.WorkshopsBlock.End
                        }
                    }).OrderBy(w => w.WorkshopsBlock.Start),
                    Roles = p.Roles.Select(r => new AppUserRoleDTO
                    {
                        UserId = r.UserId,
                        RoleId = r.RoleId,
                    })
                }).Where(user => user.Roles.All(role => role.RoleId == 4));

                List<UserCompletInfo> usersCompletInfo = new List<UserCompletInfo>();

                foreach (AppUserDTO user in query.ToList())
                {
                    usersCompletInfo.Add(new UserCompletInfo(user));
                }

                usersDataSet.LoadFromQueryable(usersCompletInfo.AsQueryable());
            }
        }
    }
}