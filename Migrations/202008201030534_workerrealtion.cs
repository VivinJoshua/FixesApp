namespace FixesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workerrealtion : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.WorkDetails", "WorkerId");
            AddForeignKey("dbo.WorkDetails", "WorkerId", "dbo.Workers", "WorkerId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkDetails", "WorkerId", "dbo.Workers");
            DropIndex("dbo.WorkDetails", new[] { "WorkerId" });
        }
    }
}
