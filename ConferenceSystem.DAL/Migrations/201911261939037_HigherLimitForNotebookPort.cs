namespace ConferencySystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HigherLimitForNotebookPort : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lectures", "NotebookPort", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lectures", "NotebookPort", c => c.String(maxLength: 15));
        }
    }
}
