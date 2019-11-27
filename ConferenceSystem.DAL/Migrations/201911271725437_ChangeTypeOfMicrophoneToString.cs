namespace ConferencySystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTypeOfMicrophoneToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lectures", "Microphone", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lectures", "Microphone", c => c.Boolean(nullable: false));
        }
    }
}
