using ConferencySystem.DAL.Data.UserIdentity;
using Microsoft.AspNet.Identity.EntityFramework;
using DbContext = ConferencySystem.DAL.Data.DbContext;

namespace ConferencySystem.BL.Services.UserManagment
{
    public class AppUserStore : UserStore<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public AppUserStore(DbContext dbContext) : base(dbContext)
        {
        }
    }
}