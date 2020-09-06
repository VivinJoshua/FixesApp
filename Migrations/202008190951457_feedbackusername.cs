namespace FixesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feedbackusername : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedbacks", "UserName", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Feedbacks", "UserName");
        }
    }
}
