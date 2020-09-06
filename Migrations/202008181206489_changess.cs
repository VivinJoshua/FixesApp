namespace FixesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changess : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Works",
                c => new 
                    {
                        WorkId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        WorkerId = c.Int(nullable: false),
                        RequestStatus = c.Int(nullable: false),
                        WorkStatus = c.Int(nullable: false),
                        RequestDT = c.DateTime(nullable: false),
                        WorkdoneDT = c.DateTime(nullable: false),
                        Worker_WorkerId = c.Int(),
                        Workers_WorkerId = c.Int(),
                    })
                .PrimaryKey(t => t.WorkId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Workers", t => t.Worker_WorkerId)
                .ForeignKey("dbo.Workers", t => t.Workers_WorkerId)
                .Index(t => t.UserId)
                .Index(t => t.Worker_WorkerId)
                .Index(t => t.Workers_WorkerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Works", "Workers_WorkerId", "dbo.Workers");
            DropForeignKey("dbo.Works", "Worker_WorkerId", "dbo.Workers");
            DropForeignKey("dbo.Works", "UserId", "dbo.Users");
            DropIndex("dbo.Works", new[] { "Workers_WorkerId" });
            DropIndex("dbo.Works", new[] { "Worker_WorkerId" });
            DropIndex("dbo.Works", new[] { "UserId" });
            DropTable("dbo.Works");
        }
    }
}
