namespace ConferencySystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSelfReferencingInLectureInfo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LecturerInfoes", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.LecturerInfoes", new[] { "Id" });
            DropPrimaryKey("dbo.LecturerInfoes");
            AddColumn("dbo.AspNetUsers", "LecturerInfo_Id", c => c.Int());
            AlterColumn("dbo.LecturerInfoes", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.LecturerInfoes", "Id");
            CreateIndex("dbo.AspNetUsers", "LecturerInfo_Id");
            AddForeignKey("dbo.AspNetUsers", "LecturerInfo_Id", "dbo.LecturerInfoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "LecturerInfo_Id", "dbo.LecturerInfoes");
            DropIndex("dbo.AspNetUsers", new[] { "LecturerInfo_Id" });
            DropPrimaryKey("dbo.LecturerInfoes");
            AlterColumn("dbo.LecturerInfoes", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "LecturerInfo_Id");
            AddPrimaryKey("dbo.LecturerInfoes", "Id");
            CreateIndex("dbo.LecturerInfoes", "Id");
            AddForeignKey("dbo.LecturerInfoes", "Id", "dbo.AspNetUsers", "Id");
        }
    }
}
