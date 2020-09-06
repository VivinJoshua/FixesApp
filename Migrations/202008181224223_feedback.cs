namespace FixesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feedback : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Works");
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        FeedbackId = c.Int(nullable: false, identity: true),
                        WorkerId = c.Int(nullable: false),
                        WorkId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Comments = c.String(nullable: false),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FeedbackId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Workers", t => t.WorkerId, cascadeDelete: true)
                .Index(t => t.WorkerId)
                .Index(t => t.UserId);
            
            AlterColumn("dbo.Works", "WorkId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Works", "WorkId");
            CreateIndex("dbo.Works", "WorkId");
            AddForeignKey("dbo.Works", "WorkId", "dbo.Feedbacks", "FeedbackId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Works", "WorkId", "dbo.Feedbacks");
            DropForeignKey("dbo.Feedbacks", "WorkerId", "dbo.Workers");
            DropForeignKey("dbo.Feedbacks", "UserId", "dbo.Users");
            DropIndex("dbo.Works", new[] { "WorkId" });
            DropIndex("dbo.Feedbacks", new[] { "UserId" });
            DropIndex("dbo.Feedbacks", new[] { "WorkerId" });
            DropPrimaryKey("dbo.Works");
            AlterColumn("dbo.Works", "WorkId", c => c.Int(nullable: false, identity: true));
            DropTable("dbo.Feedbacks");
            AddPrimaryKey("dbo.Works", "WorkId");
        }
    }
}
