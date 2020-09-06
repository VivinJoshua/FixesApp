namespace FixesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wor : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WorkDetails", "ServiceName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkDetails", "ServiceName", c => c.Int(nullable: false));
        }
    }
}
