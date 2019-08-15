namespace HMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initlalEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accomadations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AccomadationPackageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccomadationPackages", t => t.AccomadationPackageID, cascadeDelete: true)
                .Index(t => t.AccomadationPackageID);
            
            CreateTable(
                "dbo.AccomadationPackages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NoOfRoom = c.Int(nullable: false),
                        FeePerNight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Name = c.String(),
                        AccomadationID = c.Int(nullable: false),
                        AccomadationType_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccomadationTypes", t => t.AccomadationType_ID)
                .Index(t => t.AccomadationType_ID);
            
            CreateTable(
                "dbo.AccomadationTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FromDate = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                        AccomadationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accomadations", t => t.AccomadationID, cascadeDelete: true)
                .Index(t => t.AccomadationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "AccomadationID", "dbo.Accomadations");
            DropForeignKey("dbo.Accomadations", "AccomadationPackageID", "dbo.AccomadationPackages");
            DropForeignKey("dbo.AccomadationPackages", "AccomadationType_ID", "dbo.AccomadationTypes");
            DropIndex("dbo.Bookings", new[] { "AccomadationID" });
            DropIndex("dbo.AccomadationPackages", new[] { "AccomadationType_ID" });
            DropIndex("dbo.Accomadations", new[] { "AccomadationPackageID" });
            DropTable("dbo.Bookings");
            DropTable("dbo.AccomadationTypes");
            DropTable("dbo.AccomadationPackages");
            DropTable("dbo.Accomadations");
        }
    }
}
