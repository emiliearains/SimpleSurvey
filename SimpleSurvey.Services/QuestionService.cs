﻿using SimpleSurvey.Data;
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
                        .Questions;
                        
                return query.ToArray();
            }
        }
    }
}
