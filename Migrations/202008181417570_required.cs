namespace FixesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class required : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Address", c => c.String());
            AlterColumn("dbo.Users", "Location", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Location", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Address", c => c.String(nullable: false));
        }
    }
}
