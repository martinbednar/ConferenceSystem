namespace ConferencySystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLecturerInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LecturerInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Introduce = c.String(maxLength: 4000),
                        Photo = c.Binary(),
                        PhotoLink = c.String(maxLength: 4000),
                        IN = c.Long(nullable: false),
                        Street = c.String(maxLength: 300),
                        Town = c.String(maxLength: 300),
                        PostalCode = c.String(maxLength: 10),
                        AccountNumber = c.String(maxLength: 50),
                        Accomodation = c.String(maxLength: 200),
                        RoomMate = c.String(maxLength: 400),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LecturerInfoes", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.LecturerInfoes", new[] { "Id" });
            DropTable("dbo.LecturerInfoes");
        }
    }
}
