namespace FixesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workdetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkDetails",
                c => new
                    {
                        WorkDetailsId = c.Int(nullable: false, identity: true),
                        WorkerMobile = c.Int(nullable: false),
                        UserMobile = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WorkDetailsId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WorkDetails");
        }
    }
}
