namespace FixesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Works", "Worker_WorkerId", "dbo.Workers");
            DropForeignKey("dbo.Works", "Workers_WorkerId", "dbo.Workers");
            DropForeignKey("dbo.WorkDetails", "WorkerId", "dbo.Workers");
            DropIndex("dbo.Works", new[] { "Worker_WorkerId" });
            DropIndex("dbo.Works", new[] { "Workers_WorkerId" });
            DropIndex("dbo.WorkDetails", new[] { "WorkerId" });
            DropColumn("dbo.Works", "WorkerId");
            RenameColumn(table: "dbo.Works", name: "Workers_WorkerId", newName: "WorkerId");
            AddColumn("dbo.WorkDetails", "Worker_WorkerId", c => c.Int());
            AddColumn("dbo.WorkDetails", "Worker_WorkerId1", c => c.Int());
            AlterColumn("dbo.Works", "WorkerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Works", "WorkerId");
            CreateIndex("dbo.WorkDetails", "Worker_WorkerId");
            CreateIndex("dbo.WorkDetails", "Worker_WorkerId1");
            AddForeignKey("dbo.WorkDetails", "Worker_WorkerId1", "dbo.Workers", "WorkerId");
            AddForeignKey("dbo.Works", "WorkerId", "dbo.Workers", "WorkerId", cascadeDelete: true);
            AddForeignKey("dbo.WorkDetails", "Worker_WorkerId", "dbo.Workers", "WorkerId");
            DropColumn("dbo.Works", "Worker_WorkerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Works", "Worker_WorkerId", c => c.Int());
            DropForeignKey("dbo.WorkDetails", "Worker_WorkerId", "dbo.Workers");
            DropForeignKey("dbo.Works", "WorkerId", "dbo.Workers");
            DropForeignKey("dbo.WorkDetails", "Worker_WorkerId1", "dbo.Workers");
            DropIndex("dbo.WorkDetails", new[] { "Worker_WorkerId1" });
            DropIndex("dbo.WorkDetails", new[] { "Worker_WorkerId" });
            DropIndex("dbo.Works", new[] { "WorkerId" });
            AlterColumn("dbo.Works", "WorkerId", c => c.Int());
            DropColumn("dbo.WorkDetails", "Worker_WorkerId1");
            DropColumn("dbo.WorkDetails", "Worker_WorkerId");
            RenameColumn(table: "dbo.Works", name: "WorkerId", newName: "Workers_WorkerId");
            AddColumn("dbo.Works", "WorkerId", c => c.Int(nullable: false));
            CreateIndex("dbo.WorkDetails", "WorkerId");
            CreateIndex("dbo.Works", "Workers_WorkerId");
            CreateIndex("dbo.Works", "Worker_WorkerId");
            AddForeignKey("dbo.WorkDetails", "WorkerId", "dbo.Workers", "WorkerId", cascadeDelete: true);
            AddForeignKey("dbo.Works", "Workers_WorkerId", "dbo.Workers", "WorkerId");
            AddForeignKey("dbo.Works", "Worker_WorkerId", "dbo.Workers", "WorkerId");
        }
    }
}
