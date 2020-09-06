namespace FixesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fee : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Feedbacks", "Comments", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Feedbacks", "Comments", c => c.String(nullable: false));
        }
    }
}
