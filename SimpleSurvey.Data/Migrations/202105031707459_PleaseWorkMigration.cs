namespace SimpleSurvey.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PleaseWorkMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSurvey", "UserId", c => c.Guid(nullable: false));
            DropColumn("dbo.UserSurvey", "OwnerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserSurvey", "OwnerId", c => c.Guid(nullable: false));
            DropColumn("dbo.UserSurvey", "UserId");
        }
    }
}
