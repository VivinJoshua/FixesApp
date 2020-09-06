namespace FixesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkDetails", "WorkerName", c => c.String());
            AddColumn("dbo.WorkDetails", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkDetails", "UserName");
            DropColumn("dbo.WorkDetails", "WorkerName");
        }
    }
}
