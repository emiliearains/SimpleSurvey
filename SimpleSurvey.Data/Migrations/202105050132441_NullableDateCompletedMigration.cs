namespace SimpleSurvey.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableDateCompletedMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserSurvey", "DateCompleted", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserSurvey", "DateCompleted", c => c.DateTime(nullable: false));
        }
    }
}
