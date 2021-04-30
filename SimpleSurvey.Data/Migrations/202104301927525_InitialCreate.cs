namespace SimpleSurvey.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionChoice",
                c => new
                    {
                        QuestionChoiceId = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        QuestionChoiceText = c.String(nullable: false, maxLength: 200),
                        QuestionChoiceValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionChoiceId)
                .ForeignKey("dbo.Question", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        QuestionText = c.String(nullable: false, maxLength: 500),
                        QuestionType = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.SurveyQuestion",
                c => new
                    {
                        SurveyQuestionId = c.Int(nullable: false, identity: true),
                        SurveyId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SurveyQuestionId)
                .ForeignKey("dbo.Question", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.Survey", t => t.SurveyId, cascadeDelete: true)
                .Index(t => t.SurveyId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Survey",
                c => new
                    {
                        SurveyId = c.Int(nullable: false, identity: true),
                        SurveyTitle = c.String(nullable: false),
                        SurveyDescription = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SurveyId);
            
            CreateTable(
                "dbo.UserAnswer",
                c => new
                    {
                        UserAnswerId = c.Int(nullable: false, identity: true),
                        UserSurveyId = c.Int(nullable: false),
                        QuestionChoiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserAnswerId)
                .ForeignKey("dbo.QuestionChoice", t => t.QuestionChoiceId, cascadeDelete: true)
                .ForeignKey("dbo.UserSurvey", t => t.UserSurveyId, cascadeDelete: true)
                .Index(t => t.UserSurveyId)
                .Index(t => t.QuestionChoiceId);
            
            CreateTable(
                "dbo.UserSurvey",
                c => new
                    {
                        UserSurveyId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        SurveyId = c.Int(nullable: false),
                        DateCompleted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserSurveyId)
                .ForeignKey("dbo.Survey", t => t.SurveyId, cascadeDelete: true)
                .Index(t => t.SurveyId);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DisplayName = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                        Department = c.Int(),
                        JobTitle = c.Int(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.UserAnswer", "UserSurveyId", "dbo.UserSurvey");
            DropForeignKey("dbo.UserSurvey", "SurveyId", "dbo.Survey");
            DropForeignKey("dbo.UserAnswer", "QuestionChoiceId", "dbo.QuestionChoice");
            DropForeignKey("dbo.SurveyQuestion", "SurveyId", "dbo.Survey");
            DropForeignKey("dbo.SurveyQuestion", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.QuestionChoice", "QuestionId", "dbo.Question");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserSurvey", new[] { "SurveyId" });
            DropIndex("dbo.UserAnswer", new[] { "QuestionChoiceId" });
            DropIndex("dbo.UserAnswer", new[] { "UserSurveyId" });
            DropIndex("dbo.SurveyQuestion", new[] { "QuestionId" });
            DropIndex("dbo.SurveyQuestion", new[] { "SurveyId" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.QuestionChoice", new[] { "QuestionId" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.UserSurvey");
            DropTable("dbo.UserAnswer");
            DropTable("dbo.Survey");
            DropTable("dbo.SurveyQuestion");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Question");
            DropTable("dbo.QuestionChoice");
        }
    }
}
