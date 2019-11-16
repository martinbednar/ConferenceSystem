namespace ConferencySystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLectureEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lectures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(maxLength: 50),
                        Name = c.String(maxLength: 200),
                        Annotation = c.String(maxLength: 4000),
                        Presentation = c.Binary(),
                        PresentationName = c.String(maxLength: 200),
                        PresentationLink = c.String(maxLength: 4000),
                        Microphone = c.Boolean(nullable: false),
                        Flipchart = c.Boolean(nullable: false),
                        PlaceRequirements = c.String(maxLength: 4000),
                        Goal = c.String(maxLength: 400),
                        Given = c.String(maxLength: 400),
                        Chairs = c.Boolean(nullable: false),
                        Carpet = c.Boolean(nullable: false),
                        Tables = c.Boolean(nullable: false),
                        Notebook = c.Boolean(nullable: false),
                        Dataprojector = c.Boolean(nullable: false),
                        NotebookPort = c.String(maxLength: 15),
                        Speakers = c.Boolean(nullable: false),
                        WorklistsCopies = c.Boolean(nullable: false),
                        Worklist = c.Binary(),
                        WorklistName = c.String(maxLength: 200),
                        WorklistLink = c.String(maxLength: 4000),
                        EquipmentRequirements = c.String(maxLength: 4000),
                        LecturerInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LecturerInfoes", t => t.LecturerInfo_Id)
                .Index(t => t.LecturerInfo_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lectures", "LecturerInfo_Id", "dbo.LecturerInfoes");
            DropIndex("dbo.Lectures", new[] { "LecturerInfo_Id" });
            DropTable("dbo.Lectures");
        }
    }
}
