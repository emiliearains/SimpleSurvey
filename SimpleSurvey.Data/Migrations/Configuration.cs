namespace SimpleSurvey.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SimpleSurvey.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SimpleSurvey.Data.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            List<Question> seededQuestions = new List<Question>();

            seededQuestions.Add(new Question() { QuestionText = "I know what is expected of me at work.", QuestionType = 1, IsActive = true });
            seededQuestions.Add(new Question() { QuestionText = "I have the materials and equipment I need to do my work right,", QuestionType = 1, IsActive = true });
            seededQuestions.Add(new Question() { QuestionText = "At work, I have the opportunity to do what I do best every day.", QuestionType = 1, IsActive = true });
            seededQuestions.Add(new Question() { QuestionText = "In the last seven days, I have received recognition or praise for doing good work.", QuestionType = 1, IsActive = true });
            seededQuestions.Add(new Question() { QuestionText = "My supervisor, or someone at work, seems to care about me as a person.", QuestionType = 1, IsActive = true });
            seededQuestions.Add(new Question() { QuestionText = "There is someone at work who encourages my development.", QuestionType = 1, IsActive = true });
            seededQuestions.Add(new Question() { QuestionText = "At work, my opinions seem to count.", QuestionType = 1, IsActive = true });
            seededQuestions.Add(new Question() { QuestionText = "The mission or purpose of my company makes me feel my job is important.", QuestionType = 1, IsActive = true });
            seededQuestions.Add(new Question() { QuestionText = "My associates or fellow employees are committed to doing quality work.", QuestionType = 1, IsActive = true });
            seededQuestions.Add(new Question() { QuestionText = "I have a best friend at work.", QuestionType = 1, IsActive = true });
            seededQuestions.Add(new Question() { QuestionText = "In the last six months, someone has talked to me about my progress,", QuestionType = 1, IsActive = true });
            seededQuestions.Add(new Question() { QuestionText = "This last year, I have had opportunities at work to learn and grow.", QuestionType = 1, IsActive = true });


            context.Questions.AddRange(seededQuestions);

            base.Seed(context);

        }
    }
}
