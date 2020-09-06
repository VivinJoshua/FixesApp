namespace FixesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserWorkerTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Email = c.String(),
                        MobileNumber = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Location = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        WorkerId = c.Int(nullable: false, identity: true),
                        WorkerName = c.String(nullable: false),
                        MobileNumber = c.String(nullable: false),
                        ServiceId = c.Int(nullable: false),
                        Location = c.String(nullable: false),
                        CostPerHour = c.Int(nullable: false),
                        Servicess_ServicesId = c.Int(),
                    })
                .PrimaryKey(t => t.WorkerId)
                .ForeignKey("dbo.Services", t => t.Servicess_ServicesId)
                .Index(t => t.Servicess_ServicesId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workers", "Servicess_ServicesId", "dbo.Services");
            DropIndex("dbo.Workers", new[] { "Servicess_ServicesId" });
            DropTable("dbo.Workers");
            DropTable("dbo.Users");
        }
    }
}
