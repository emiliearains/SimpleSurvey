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
                ctx.SaveChanges();
                PopulateQuestionChoices(entity.QuestionId);
            }
            return true;
        }
        // GET: All active questions
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

        // GET: QuestionById
        public Question GetQuestionById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Questions
                        .Single(e => e.QuestionId == id);
                return entity;
            }
        }

        public List<QuestionChoice> GetQuestionChoicesByQuestionId(int questionId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .QuestionChoices
                        .Where(x => x.QuestionId == questionId)
                        .ToList();
                return entity;
            }
        }

        public QuestionChoice GetQuestionChoiceById(int questionChoiceId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .QuestionChoices
                        .Single(x => x.QuestionChoiceId == questionChoiceId);
                        
                        return entity;
            }
        }

        private bool PopulateQuestionChoices(int questionId)
        {
            List<QuestionChoice> questionChoices = new List<QuestionChoice>();

            questionChoices.Add(new QuestionChoice() 
            { 
                QuestionChoiceText = "Strongly Agree", 
                QuestionChoiceValue = 5, 
                QuestionId = questionId 
            });

            questionChoices.Add(new QuestionChoice() 
            { 
                QuestionChoiceText = "Agree", 
                QuestionChoiceValue = 4, 
                QuestionId = questionId 
            });

            questionChoices.Add(new QuestionChoice() 
            { 
                QuestionChoiceText = "Neither Agree nor Disagree", 
                QuestionChoiceValue = 3, 
                QuestionId = questionId 
            });

            questionChoices.Add(new QuestionChoice() 
            { 
                QuestionChoiceText = "Disagree", 
                QuestionChoiceValue = 2, 
                QuestionId = questionId 
            });

            questionChoices.Add(new QuestionChoice() 
            { 
                QuestionChoiceText = "Strongly Disagree", 
                QuestionChoiceValue = 1, 
                QuestionId = questionId
            });

            using (var ctx = new ApplicationDbContext())
            {
                ctx.QuestionChoices.AddRange(questionChoices);
                return ctx.SaveChanges() == 1;
            }
        }

        public List<Question> GetQuestionsBySurveyId(int surveyId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = 
                    ctx.Questions
                        .Where(x => ctx.SurveyQuestions
                        .Where(y => y.SurveyId == surveyId)
                        .Select(z => z.QuestionId)
                        .Contains(x.QuestionId)).ToList();

                return entity;
            }
        }
    }
}
