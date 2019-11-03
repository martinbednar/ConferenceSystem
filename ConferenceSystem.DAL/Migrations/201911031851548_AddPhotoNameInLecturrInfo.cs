namespace ConferencySystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhotoNameInLecturrInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LecturerInfoes", "PhotoName", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LecturerInfoes", "PhotoName");
        }
    }
}
