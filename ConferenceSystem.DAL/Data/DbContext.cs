using System.Data.Entity;
using ConferencySystem.DAL.Data.UserIdentity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ConferencySystem.DAL.Data
{
    public class DbContext : IdentityDbContext<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        // Your context has been configured to use a 'EntityModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ConferencySystem.EntityModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'EntityModel' 
        // connection string in the application configuration file.
        public DbContext()
            : base("name=DbContext")
        {
            //Database.SetInitializer(new DbInitializer());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Organization> Organization { get; set; }

        public virtual DbSet<Cartering> Cartering { get; set; }

        public virtual DbSet<Workshop> Workshop { get; set; }

        public virtual DbSet<WorkshopsBlock> WorkshopsBlock { get; set; }

        public virtual DbSet<Invoice> Invoice { get; set; }

        public virtual DbSet<Text> Text { get; set; }

        public virtual DbSet<Constant> Constant { get; set; }

        public virtual DbSet<LecturerInfo> LecturerInfo { get; set; }
    }
}