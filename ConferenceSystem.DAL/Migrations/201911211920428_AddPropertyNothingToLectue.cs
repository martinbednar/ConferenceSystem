namespace ConferencySystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropertyNothingToLectue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lectures", "Nothing", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lectures", "Nothing");
        }
    }
}
