namespace FixesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workdet : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WorkDetails", "RequestDT", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.WorkDetails", "WorkdoneDT", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkDetails", "WorkdoneDT", c => c.DateTime(nullable: false));
            AlterColumn("dbo.WorkDetails", "RequestDT", c => c.DateTime(nullable: false));
        }
    }
}
