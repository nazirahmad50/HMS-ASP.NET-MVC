namespace HMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyAccomadationIDInAccomadationpackage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccomadationPackages", "AccomadationType_ID", "dbo.AccomadationTypes");
            DropIndex("dbo.AccomadationPackages", new[] { "AccomadationType_ID" });
            RenameColumn(table: "dbo.AccomadationPackages", name: "AccomadationType_ID", newName: "AccomadationTypeID");
            AlterColumn("dbo.AccomadationPackages", "AccomadationTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.AccomadationPackages", "AccomadationTypeID");
            AddForeignKey("dbo.AccomadationPackages", "AccomadationTypeID", "dbo.AccomadationTypes", "ID", cascadeDelete: true);
            DropColumn("dbo.AccomadationPackages", "AccomadationID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccomadationPackages", "AccomadationID", c => c.Int(nullable: false));
            DropForeignKey("dbo.AccomadationPackages", "AccomadationTypeID", "dbo.AccomadationTypes");
            DropIndex("dbo.AccomadationPackages", new[] { "AccomadationTypeID" });
            AlterColumn("dbo.AccomadationPackages", "AccomadationTypeID", c => c.Int());
            RenameColumn(table: "dbo.AccomadationPackages", name: "AccomadationTypeID", newName: "AccomadationType_ID");
            CreateIndex("dbo.AccomadationPackages", "AccomadationType_ID");
            AddForeignKey("dbo.AccomadationPackages", "AccomadationType_ID", "dbo.AccomadationTypes", "ID");
        }
    }
}
