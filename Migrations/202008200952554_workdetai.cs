namespace FixesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workdetai : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkDetails", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.WorkDetails", "WorkerId", c => c.Int(nullable: false));
            AddColumn("dbo.WorkDetails", "RequestStatus", c => c.Int(nullable: false));
            AddColumn("dbo.WorkDetails", "WorkStatus", c => c.Int(nullable: false));
            AddColumn("dbo.WorkDetails", "RequestDT", c => c.DateTime(nullable: false));
            AddColumn("dbo.WorkDetails", "WorkdoneDT", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkDetails", "WorkdoneDT");
            DropColumn("dbo.WorkDetails", "RequestDT");
            DropColumn("dbo.WorkDetails", "WorkStatus");
            DropColumn("dbo.WorkDetails", "RequestStatus");
            DropColumn("dbo.WorkDetails", "WorkerId");
            DropColumn("dbo.WorkDetails", "UserId");
        }
    }
}
