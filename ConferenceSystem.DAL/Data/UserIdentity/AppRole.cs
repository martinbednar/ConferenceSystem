using Microsoft.AspNet.Identity.EntityFramework;

namespace ConferencySystem.DAL.Data.UserIdentity
{
    public class AppRole : IdentityRole<int, AppUserRole>
    {
        public AppRole() { }
        public AppRole(string name) { Name = name; }
    }
}