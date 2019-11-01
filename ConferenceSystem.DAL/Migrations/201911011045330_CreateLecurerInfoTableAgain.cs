namespace ConferencySystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateLecurerInfoTableAgain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LecturerInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "LecturerInfo_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "LecturerInfo_Id");
            AddForeignKey("dbo.AspNetUsers", "LecturerInfo_Id", "dbo.LecturerInfoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "LecturerInfo_Id", "dbo.LecturerInfoes");
            DropIndex("dbo.AspNetUsers", new[] { "LecturerInfo_Id" });
            DropColumn("dbo.AspNetUsers", "LecturerInfo_Id");
            DropTable("dbo.LecturerInfoes");
        }
    }
}
