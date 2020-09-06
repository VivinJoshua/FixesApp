namespace FixesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workerr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkDetails", "ServiceName", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkDetails", "ServiceName");
        }
    }
}
