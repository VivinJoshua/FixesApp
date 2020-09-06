namespace FixesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class servi : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Workers", "Servicess_ServicesId", "dbo.Services");
            DropPrimaryKey("dbo.Services");
            AlterColumn("dbo.Services", "ServicesId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Services", "ServicesId");
            AddForeignKey("dbo.Workers", "Servicess_ServicesId", "dbo.Services", "ServicesId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workers", "Servicess_ServicesId", "dbo.Services");
            DropPrimaryKey("dbo.Services");
            AlterColumn("dbo.Services", "ServicesId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Services", "ServicesId");
            AddForeignKey("dbo.Workers", "Servicess_ServicesId", "dbo.Services", "ServicesId");
        }
    }
}
