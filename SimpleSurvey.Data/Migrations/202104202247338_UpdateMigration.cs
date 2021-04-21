namespace SimpleSurvey.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserSurvey", "UserId", "dbo.ApplicationUser");
            DropIndex("dbo.UserSurvey", new[] { "UserId" });
            AddColumn("dbo.UserSurvey", "OwnerId", c => c.Guid(nullable: false));
            DropColumn("dbo.UserSurvey", "UserId");
            DropColumn("dbo.ApplicationUser", "BirthDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationUser", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserSurvey", "UserId", c => c.String(maxLength: 128));
            DropColumn("dbo.UserSurvey", "OwnerId");
            CreateIndex("dbo.UserSurvey", "UserId");
            AddForeignKey("dbo.UserSurvey", "UserId", "dbo.ApplicationUser", "Id");
        }
    }
}
