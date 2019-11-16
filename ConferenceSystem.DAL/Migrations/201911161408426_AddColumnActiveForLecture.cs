namespace ConferencySystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnActiveForLecture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lectures", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lectures", "Active");
        }
    }
}
