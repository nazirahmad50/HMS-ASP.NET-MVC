namespace HMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPictureEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccomadationPackagePictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccomadationPackageID = c.Int(nullable: false),
                        PictureID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pictures", t => t.PictureID, cascadeDelete: true)
                .ForeignKey("dbo.AccomadationPackages", t => t.AccomadationPackageID, cascadeDelete: true)
                .Index(t => t.AccomadationPackageID)
                .Index(t => t.PictureID);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        URL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AccomadationsPictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccomadationsPictureID = c.Int(nullable: false),
                        PictureID = c.Int(nullable: false),
                        Accomadation_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pictures", t => t.PictureID, cascadeDelete: true)
                .ForeignKey("dbo.Accomadations", t => t.Accomadation_ID)
                .Index(t => t.PictureID)
                .Index(t => t.Accomadation_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccomadationsPictures", "Accomadation_ID", "dbo.Accomadations");
            DropForeignKey("dbo.AccomadationsPictures", "PictureID", "dbo.Pictures");
            DropForeignKey("dbo.AccomadationPackagePictures", "AccomadationPackageID", "dbo.AccomadationPackages");
            DropForeignKey("dbo.AccomadationPackagePictures", "PictureID", "dbo.Pictures");
            DropIndex("dbo.AccomadationsPictures", new[] { "Accomadation_ID" });
            DropIndex("dbo.AccomadationsPictures", new[] { "PictureID" });
            DropIndex("dbo.AccomadationPackagePictures", new[] { "PictureID" });
            DropIndex("dbo.AccomadationPackagePictures", new[] { "AccomadationPackageID" });
            DropTable("dbo.AccomadationsPictures");
            DropTable("dbo.Pictures");
            DropTable("dbo.AccomadationPackagePictures");
        }
    }
}
