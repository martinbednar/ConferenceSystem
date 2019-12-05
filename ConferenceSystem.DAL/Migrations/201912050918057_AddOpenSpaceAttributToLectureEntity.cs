namespace ConferencySystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOpenSpaceAttributToLectureEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lectures", "OpenSpace", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lectures", "OpenSpace");
        }
    }
}
