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
    }
}
