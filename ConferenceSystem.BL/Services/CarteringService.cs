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

                if(cartering != null && !cartering.User.Contains(currentUser))
                {
                    cartering.User.Add(currentUser);
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

                if (cartering != null && cartering.User.Contains(currentUser))
                {
                    cartering.User.Remove(currentUser);
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
                        Users = c.User
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

        public List<AppUserDTO> GetUsersOverview()
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
                }).Where(user => (user.Id != 2) && (user.Id != 2));

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
                    Users = c.User
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

        public List<CarteringUsers> GetCarteringSumary()
        {
            using (var db = new DbContext())
            {
                var query = db.Cartering.Select(c => new CarteringDTO()
                {
                    Id = c.Id,
                    Name = c.Name,
                    When = c.When,
                    Users = c.User
                        .Select(p => new AppUserDTO()
                        {
                            Id = p.Id,
                            FirstName = p.FirstName,
                            LastName = p.LastName
                        })
                }).OrderBy(c => c.When);

                List<CarteringUsers> carteringSumary = new List<CarteringUsers>();

                foreach (CarteringDTO cartering in query.ToList())
                {
                    carteringSumary.Add(new CarteringUsers(cartering.Name, cartering.Users));
                }

                return carteringSumary;
            }
        }
    }
}