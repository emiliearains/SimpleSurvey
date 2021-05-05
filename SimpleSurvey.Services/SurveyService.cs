using SimpleSurvey.Data;
using SimpleSurvey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSurvey.Services
{
    public class SurveyService
    {
        public bool CreateSurvey(SurveyCreate model)
        {
            var entity =
                new Survey()
                {
                    SurveyTitle = model.SurveyTitle,
                    SurveyDescription = model.SurveyDescription,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Surveys.Add(entity);
                ctx.SaveChanges();
            }
            return true;
        }
        public IEnumerable<Survey> GetSurveys()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Surveys;
                return query.ToArray();
            }
        }
        public Survey GetSurveyById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Surveys
                        .Single(e => e.SurveyId == id);
                return entity;
            }
        }

        public List<UserSurvey> GetOpenSurveysByUserId(string id)
        {
            var userId = Guid.Parse(id);
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserSurveys
                        .Where(e => e.UserId == userId && e.DateCompleted == null).ToList();
                return entity;
            }
        }

        public bool CreateUserSurvey(UserSurveyCreate model)
        {
            var entity =
                new UserSurvey()
                {
                    SurveyId = model.SurveyId,
                    UserId = model.UserId
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.UserSurveys.Add(entity);
                ctx.SaveChanges();
            }
            return true;
        }

        public bool CreateSurveyAnswer(int userSurveyId, int questionChoiceId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                ctx.UserAnswers.Add(new UserAnswer
                {
                    UserSurveyId = userSurveyId,
                    QuestionChoiceId = questionChoiceId
                });
                ctx.SaveChanges();
            }
            return true;
        }
        public bool CompleteUserSurvey(int userSurveyId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var userSurvey = ctx.UserSurveys
                    .Single(x => x.UserSurveyId == userSurveyId);
                userSurvey.DateCompleted = DateTime.Now;
                ctx.SaveChanges();
            }
            return true;

        }

        public UserSurvey GetUserSurveyById(int userSurveyId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.UserSurveys
                    .Single(x => x.UserSurveyId == userSurveyId);
                return entity;
            }
        }
        public UserSurvey GetUserSurvey(string userId, int surveyId)
        {
            Guid userIdGuid = Guid.Parse(userId);
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.UserSurveys
                    .Single(x => x.UserId == userIdGuid && x.SurveyId == surveyId);
                return entity;
            }
        }
    }
}
