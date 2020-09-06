namespace FixesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workdetail : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WorkDetails", "WorkerMobile", c => c.String());
            AlterColumn("dbo.WorkDetails", "UserMobile", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkDetails", "UserMobile", c => c.Int(nullable: false));
            AlterColumn("dbo.WorkDetails", "WorkerMobile", c => c.Int(nullable: false));
        }
    }
}
