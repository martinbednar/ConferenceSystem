using ConferencySystem.DAL.Data.UserIdentity;
using Microsoft.AspNet.Identity;
using DbContext = ConferencySystem.DAL.Data.DbContext;

namespace ConferencySystem.BL.Services.UserManagment
{
    public class AppRoleManager : RoleManager<AppRole,int>
    {
        public AppRoleManager(DbContext dbContext) : base(new AppRoleStore(dbContext))
        {
        }
    }
}