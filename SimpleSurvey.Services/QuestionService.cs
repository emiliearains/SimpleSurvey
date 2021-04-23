using SimpleSurvey.Data;
using SimpleSurvey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSurvey.Services
{
    public class QuestionService
    {
        

        public QuestionService()
        {
           
        }

        public bool CreateQuestion(QuestionCreate model)
        {
            var entity =
                new Question()
                {
                    QuestionText = model.QuestionText,
                    QuestionType = model.QuestionType,
                    IsActive = model.IsActive
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Questions.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<Question> GetQuestions()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Questions
                        .Where(e => e.IsActive == true);
                        
                return query.ToArray();
            }
        }

        public QuestionDetail GetQuestionById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Questions
                        .Single(e => e.QuestionId == id);
                return
                    new QuestionDetail
                    {
                        QuestionText = entity.QuestionText,
                        QuestionType = entity.QuestionType
                    };
            }
        }
    }
}
