namespace FixesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feeda : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkDetails", "Feedbackstatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkDetails", "Feedbackstatus");
        }
    }
}
