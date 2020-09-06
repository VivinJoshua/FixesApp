namespace FixesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkDetails", "cost", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkDetails", "cost");
        }
    }
}
