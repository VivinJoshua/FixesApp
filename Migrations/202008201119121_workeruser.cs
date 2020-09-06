namespace FixesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workeruser : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.WorkDetails", "UserId");
            AddForeignKey("dbo.WorkDetails", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkDetails", "UserId", "dbo.Users");
            DropIndex("dbo.WorkDetails", new[] { "UserId" });
        }
    }
}
