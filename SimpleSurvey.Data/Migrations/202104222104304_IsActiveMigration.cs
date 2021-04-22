namespace SimpleSurvey.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsActiveMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Question", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Question", "IsActive");
        }
    }
}
