namespace FixesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workde : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WorkDetails", "WorkdoneDT", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkDetails", "WorkdoneDT", c => c.DateTime());
        }
    }
}
