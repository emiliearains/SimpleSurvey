namespace SimpleSurvey.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HandsOffMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplicationUser", "Department", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplicationUser", "Department", c => c.Int());
        }
    }
}
