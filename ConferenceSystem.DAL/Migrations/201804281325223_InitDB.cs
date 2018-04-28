namespace ConferencySystem.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carterings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        When = c.DateTime(nullable: false),
                        Category = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegisterTimestamp = c.DateTime(nullable: false),
                        FirstName = c.String(maxLength: 200),
                        LastName = c.String(maxLength: 200),
                        BirthDate = c.DateTime(),
                        BirthPlace = c.String(maxLength: 200),
                        TitleBefore = c.String(maxLength: 30),
                        TitleAfter = c.String(maxLength: 30),
                        Position = c.String(maxLength: 200),
                        Agreement = c.Boolean(nullable: false),
                        PaidDate = c.DateTime(),
                        VariableSymbol = c.Int(nullable: false),
                        NoteUser = c.String(maxLength: 1000),
                        NoteAdmin = c.String(maxLength: 1000),
                        WantInvoice = c.Boolean(nullable: false),
                        IsAlternate = c.Boolean(nullable: false),
                        InvoiceNumber = c.String(maxLength: 30),
                        WasEmailWorkshopSent = c.Boolean(nullable: false),
                        WasEmailCarteringSent = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Organization_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.Organization_Id)
                .Index(t => t.FirstName)
                .Index(t => t.LastName)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Organization_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IN = c.Long(nullable: false),
                        VATID = c.String(),
                        Name = c.String(maxLength: 300),
                        BillStreet = c.String(maxLength: 300),
                        Town = c.String(maxLength: 300),
                        PostalCode = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Workshops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 500),
                        Presenter = c.String(maxLength: 255),
                        Room = c.String(maxLength: 31),
                        Capacity = c.Int(nullable: false),
                        WorkshopsBlock_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkshopsBlocks", t => t.WorkshopsBlock_Id)
                .Index(t => t.WorkshopsBlock_Id);
            
            CreateTable(
                "dbo.WorkshopsBlocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AppUserCarterings",
                c => new
                    {
                        AppUser_Id = c.Int(nullable: false),
                        Cartering_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AppUser_Id, t.Cartering_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Carterings", t => t.Cartering_Id, cascadeDelete: true)
                .Index(t => t.AppUser_Id)
                .Index(t => t.Cartering_Id);
            
            CreateTable(
                "dbo.WorkshopAppUsers",
                c => new
                    {
                        Workshop_Id = c.Int(nullable: false),
                        AppUser_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Workshop_Id, t.AppUser_Id })
                .ForeignKey("dbo.Workshops", t => t.Workshop_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id, cascadeDelete: true)
                .Index(t => t.Workshop_Id)
                .Index(t => t.AppUser_Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Workshops", "WorkshopsBlock_Id", "dbo.WorkshopsBlocks");
            DropForeignKey("dbo.WorkshopAppUsers", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.WorkshopAppUsers", "Workshop_Id", "dbo.Workshops");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Organization_Id", "dbo.Organizations");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AppUserCarterings", "Cartering_Id", "dbo.Carterings");
            DropForeignKey("dbo.AppUserCarterings", "AppUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.WorkshopAppUsers", new[] { "AppUser_Id" });
            DropIndex("dbo.WorkshopAppUsers", new[] { "Workshop_Id" });
            DropIndex("dbo.AppUserCarterings", new[] { "Cartering_Id" });
            DropIndex("dbo.AppUserCarterings", new[] { "AppUser_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Workshops", new[] { "WorkshopsBlock_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Organizations", new[] { "Name" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Organization_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "LastName" });
            DropIndex("dbo.AspNetUsers", new[] { "FirstName" });
            DropTable("dbo.WorkshopAppUsers");
            DropTable("dbo.AppUserCarterings");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.WorkshopsBlocks");
            DropTable("dbo.Workshops");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Organizations");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Carterings");
        }
    }
}
