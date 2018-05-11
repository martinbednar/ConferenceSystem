using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services.UserManagment;
using ConferencySystem.DAL.Data;
using ConferencySystem.DAL.Data.UserIdentity;
using Microsoft.AspNet.Identity;

namespace ConferencySystem.BL.Services
{
    public class LoginService
    {
        public AppUserDTO GetUser(string email)
        {
            using (var db = new DbContext())
            {
                var user = db.Users.Select(p => new AppUserDTO()
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email,
                    PasswordHash = p.PasswordHash
                })
                .SingleOrDefault(p => p.Email == email);

                return user;
            }
        }

        public List<AppUserDTO> GetAllUsers()
        {
            using (var db = new DbContext())
            {
                return db.Users.Select(a => new AppUserDTO()
                {
                    Id = a.Id,
                    Email = a.Email,
                }).ToList();
            }
        }

        public void EmailSent(int userId)
        {
            using (var db = new DbContext())
            {
                var user = db.Users.Find(userId);
                user.WasEmailWorkshopSent = true;

                db.SaveChanges();
            }
        }

        public ClaimsIdentity Login(string email, string password)
        {
            using (var userManager = new AppUserManager(new DbContext()))
            {
                AppUser user = userManager.Find(email, password);

                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                userIdentity.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));
                userIdentity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName + " " + user.LastName ));
                return userIdentity;
            }
        }
    }
}