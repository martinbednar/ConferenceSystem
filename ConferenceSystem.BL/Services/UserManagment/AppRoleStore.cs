using ConferencySystem.DAL.Data.UserIdentity;
using Microsoft.AspNet.Identity.EntityFramework;
using DbContext = ConferencySystem.DAL.Data.DbContext;

namespace ConferencySystem.BL.Services.UserManagment
{
    public class AppRoleStore : RoleStore<AppRole, int, AppUserRole>
    {
        public AppRoleStore(DbContext dbContext) : base(dbContext)
        {
        }
    }
}