namespace FixesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Workers", "Servicess_ServicesId", "dbo.Services");
            DropIndex("dbo.Workers", new[] { "Servicess_ServicesId" });
            RenameColumn(table: "dbo.Workers", name: "Servicess_ServicesId", newName: "ServicesId");
            AlterColumn("dbo.Workers", "ServicesId", c => c.Int(nullable: false));
            CreateIndex("dbo.Workers", "ServicesId");
            AddForeignKey("dbo.Workers", "ServicesId", "dbo.Services", "ServicesId", cascadeDelete: true);
            DropColumn("dbo.Workers", "ServiceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Workers", "ServiceId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Workers", "ServicesId", "dbo.Services");
            DropIndex("dbo.Workers", new[] { "ServicesId" });
            AlterColumn("dbo.Workers", "ServicesId", c => c.Int());
            RenameColumn(table: "dbo.Workers", name: "ServicesId", newName: "Servicess_ServicesId");
            CreateIndex("dbo.Workers", "Servicess_ServicesId");
            AddForeignKey("dbo.Workers", "Servicess_ServicesId", "dbo.Services", "ServicesId");
        }
    }
}
