using System.Collections.Generic;
using System.Linq;
using ConferencySystem.BL.DTO;
using ConferencySystem.DAL.Data;
using ConferencySystem.DAL.Data.UserIdentity;
using DotVVM.Framework.Controls;

namespace ConferencySystem.BL.Services
{
    public class CarteringService
    {
        public AppUser GetUser(int idUser)
        {
            using (var db = new DbContext())
            {
                return db.Users.Find(idUser);
            }
        }

        public void AddOrChangeBoarder(int idCartering, int idUser)
        {
            using (var db = new DbContext())
            {
                var cartering = db.Cartering.Find(idCartering);

                var currentUser = db.Users.Find(idUser);

                if(cartering != null && !cartering.People.Contains(currentUser))
                {
                    cartering.People.Add(currentUser);
                    db.SaveChanges();
                }
            }
        }

        public void DeleteBoarder(int idCartering, int idUser)
        {
            using (var db = new DbContext())
            {
                var cartering = db.Cartering.Find(idCartering);

                var currentUser = db.Users.Find(idUser);

                if (cartering != null && cartering.People.Contains(currentUser))
                {
                    cartering.People.Remove(currentUser);
                    db.SaveChanges();
                }
            }
        }

        public CarteringDTO[] GetCarterings()
        {
            using (var db = new DbContext())
            {
                return db.Cartering
                    .Select(c => new CarteringDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        When = c.When,
                        Category = c.Category,
                        People = c.People
                        .Select(p => new AppUserDTO()
                        {
                            Id = p.Id,
                            FirstName = p.FirstName,
                            LastName = p.LastName
                        })
                    })
                    .OrderBy(c => c.When)
                    .ToArray();
            }
        }

        public void SaveChanges(bool isOnlyCategory, int idCartering, bool isCategoryChecked, int idChoice, int lastidChoice, int idUser)
        {
            if (isCategoryChecked)
            {
                if (isOnlyCategory)
                {
                    AddOrChangeBoarder(idCartering, idUser);
                }
                else
                {
                    if ((lastidChoice != 0) && (idChoice != lastidChoice))
                    {
                        //byla porvedena změna, je třeba smazat nejprve původní záznam
                        DeleteBoarder(lastidChoice, idUser);
                    }
                    AddOrChangeBoarder(idChoice, idUser);
                }
            }
            else
            {
                if (isOnlyCategory)
                {
                    DeleteBoarder(idCartering, idUser);
                }
                else
                {
                    if (lastidChoice != 0)
                    {
                        DeleteBoarder(lastidChoice, idUser);
                    }
                }
            }
        }

        public List<AppUserDTO> GetPeopleOverview()
        {
            using (var db = new DbContext())
            {
                var query = db.Users.Select(p => new AppUserDTO()
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Cartering = p.Cartering
                        .Select(c => new CarteringDTO
                        {
                            Name = c.Name,
                            Id = c.Id
                        })
                });

                return query.ToList();
            }
        }

        public void GetCarteringOverview(GridViewDataSet<CarteringDTO> carteringDataSet)
        {
            using (var db = new DbContext())
            {
                var query = db.Cartering.Select(c => new CarteringDTO()
                {
                    Name = c.Name,
                    When = c.When,
                    People = c.People
                        .Select(p => new AppUserDTO()
                        {
                            Id = p.Id,
                            FirstName = p.FirstName,
                            LastName = p.LastName
                        })
                });

                carteringDataSet.LoadFromQueryable(query);
            }
        }

        public List<CarteringPeople> GetCarteringSumary()
        {
            using (var db = new DbContext())
            {
                var query = db.Cartering.Select(c => new CarteringDTO()
                {
                    Id = c.Id,
                    Name = c.Name,
                    When = c.When,
                    People = c.People
                        .Select(p => new AppUserDTO()
                        {
                            Id = p.Id,
                            FirstName = p.FirstName,
                            LastName = p.LastName
                        })
                }).OrderBy(c => c.When);

                List<CarteringPeople> carteringSumary = new List<CarteringPeople>();

                foreach (CarteringDTO cartering in query.ToList())
                {
                    carteringSumary.Add(new CarteringPeople(cartering.Name, cartering.People));
                }

                return carteringSumary;
            }
        }
    }
}