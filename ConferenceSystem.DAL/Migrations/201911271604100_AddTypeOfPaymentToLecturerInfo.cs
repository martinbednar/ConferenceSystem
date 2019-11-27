namespace ConferencySystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTypeOfPaymentToLecturerInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LecturerInfoes", "Fee", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LecturerInfoes", "Fee");
        }
    }
}
