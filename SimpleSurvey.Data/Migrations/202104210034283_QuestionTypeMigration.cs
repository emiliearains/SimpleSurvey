namespace SimpleSurvey.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionTypeMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Question", "QuestionType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Question", "QuestionType", c => c.String());
        }
    }
}
