using System;
using System.Collections.Generic;
using System.Linq;
using ConferencySystem.BL.DTO;
using ConferencySystem.DAL.Data;
using ConferencySystem.DAL.Data.UserIdentity;
using DotVVM.Framework.Controls;

namespace ConferencySystem.BL.Services
{
    public class WorkshopService
    {
        public AppUserDTO GetUser(int idUser)
        {
            using (var db = new DbContext())
            {
                return db.Users.Select(p => new AppUserDTO()
                {
                    Id = p.Id,
                    RegisterTimestamp = new DateTime(1990, 1, 1, 1, 0, 0, 0)
                }).FirstOrDefault(p => p.Id == idUser);
            }
        }

        public List<WorkshopsBlockDTO> GetWorkshopsBlocks(int currentUserId)
        {
            using (var db = new DbContext())
            {
                return db.WorkshopsBlock.Select(w => new WorkshopsBlockDTO()
                {
                    Id = w.Id,
                    Name = w.Name,
                    Start = w.Start,
                    End = w.End,
                    Workshops = w.Workshops.Select(s => new WorkshopDTO()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Capacity = s.Capacity,
                        Presenter = s.Presenter,
                        Room = s.Room,
                        Users = s.Users.Select(p => new AppUserDTO()
                        {
                            Id = p.Id,
                            RegisterTimestamp = new DateTime(1990, 1, 1, 1, 0, 0, 0)
                        })
                    })
                }).OrderBy(w => w.Start).ToList();
            }
        }

        private static readonly Object RegisterWorkshopLock = new Object();

        public bool RegisterWorkshop(int userId, int workshopId)
        {
            lock (RegisterWorkshopLock)
            {
                using (var db = new DbContext())
                {
                    Workshop workshop = db.Workshop.Find(workshopId);

                    AppUser currentUser = db.Users.Find(userId);

                    if (workshop != null && workshop.Capacity < 1)
                    {
                        return false;
                    }
                    else
                    {
                        if (workshop != null && !workshop.Users.Contains(currentUser))
                        {
                            workshop.Users.Add(currentUser);
                            workshop.Capacity = workshop.Capacity - 1;
                            db.SaveChanges();

                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }

        }

        public void UnregisterWorkshop(int userId, int workshopId)
        {
            using (var db = new DbContext())
            {
                Workshop workshop = db.Workshop.Find(workshopId);

                AppUser currentUser = db.Users.Find(userId);

                if (workshop != null && workshop.Users.Contains(currentUser))
                {
                    workshop.Users.Remove(currentUser);
                    workshop.Capacity = workshop.Capacity + 1;
                    db.SaveChanges();
                }
            }
        }

        public void GetUsersOverview(GridViewDataSet<UserWorkshops> usersDataSet)
        {
            using (var db = new DbContext())
            {
                var query = db.Users.Select(p => new AppUserDTO()
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Workshops = p.Workshops
                        .Select(c => new WorkshopDTO
                        {
                            Name = c.Name,
                            Id = c.Id,
                            WorkshopsBlock = new WorkshopsBlockDTO()
                            {
                                Id = c.WorkshopsBlock.Id
                            }
                        })
                });

                List<UserWorkshops> usersWorkshops = new List<UserWorkshops>();

                foreach (AppUserDTO user in query.ToList())
                {
                    usersWorkshops.Add(new UserWorkshops()
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Block1 = (user.Workshops.Where(w => w.WorkshopsBlock.Id == 1).Count() == 0) ? "" : user.Workshops.Where(w => w.WorkshopsBlock.Id == 1).First().Name,
                        Lecture = (user.Workshops.Where(w => w.WorkshopsBlock.Id == 4).Count() == 0) ? "" : user.Workshops.Where(w => w.WorkshopsBlock.Id == 4).First().Name,
                        Block2 = (user.Workshops.Where(w => w.WorkshopsBlock.Id == 2).Count() == 0) ? "" : user.Workshops.Where(w => w.WorkshopsBlock.Id == 2).First().Name,
                        Block3 = (user.Workshops.Where(w => w.WorkshopsBlock.Id == 3).Count() == 0) ? "" : user.Workshops.Where(w => w.WorkshopsBlock.Id == 3).First().Name
                    });
                }

                usersDataSet.LoadFromQueryable(usersWorkshops.AsQueryable());
            }
        }

        public List<WorkshopsBlockDTO> GetWorkshopSumary()
        {
            using (var db = new DbContext())
            {
                return db.WorkshopsBlock.Select(c => new WorkshopsBlockDTO()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Start = c.Start,
                    End = c.End,
                    Workshops = c.Workshops
                        .Select(w => new WorkshopDTO
                        {
                            Id = w.Id,
                            Name = w.Name,
                            Presenter = w.Presenter,
                            Capacity = w.Capacity,
                            Users = w.Users
                                .Select(p => new AppUserDTO()
                                {
                                    Id = p.Id,
                                    FirstName = p.FirstName,
                                    LastName = p.LastName,
                                    TitleBefore = p.TitleBefore,
                                    TitleAfter = p.TitleAfter
                                }),
                            NumberOfRegistered = w.Users.Count
                        })
                }).OrderBy(c => c.Start).ToList();
            }
        }
    }
}