namespace ConferencySystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldsToPrticipantUer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "InfoFrom", c => c.String(maxLength: 1000));
            AddColumn("dbo.AspNetUsers", "WantGet", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "WantGet");
            DropColumn("dbo.AspNetUsers", "InfoFrom");
        }
    }
}
