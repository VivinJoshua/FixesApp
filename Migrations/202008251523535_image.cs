namespace FixesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class image : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "userImage", c => c.Binary());
            AddColumn("dbo.Workers", "workerImage", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Workers", "workerImage");
            DropColumn("dbo.Users", "userImage");
        }
    }
}
