using ConferencySystem.DAL.Data.UserIdentity;
using Microsoft.AspNet.Identity;
using DbContext = ConferencySystem.DAL.Data.DbContext;

namespace ConferencySystem.BL.Services.UserManagment
{
    public class AppUserManager : UserManager<AppUser, int>
    {
        public AppUserManager(DbContext dbContext) : base(new AppUserStore(dbContext))
        {
        }

        public AppUserManager(IUserStore<AppUser, int> store)
            : base(store)
        {
        }
    }
}