using SimpleSurvey.Data;
using SimpleSurvey.Models;
using SimpleSurvey.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleSurvey.WebMVC.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Question
        public ActionResult Index()
        {
            var service = new QuestionService();
            var questions = service.GetQuestions();
            var model = questions.Select(
                e =>
                    new QuestionListItem
                    {
                        QuestionId = e.QuestionId,
                        QuestionText = e.QuestionText,
                    }
                    );
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(QuestionCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = new QuestionService();

            if (service.CreateQuestion(model))
            {
                ViewBag.SaveResult = "Question is created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Question could not be created.");
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var svc = new QuestionService();
            var question = svc.GetQuestionById(id);
            var questionDetails = svc.GetQuestionChoicesByQuestionId(id).Select(x => new QuestionChoiceDetails()
            {
                QuestionChoiceText = x.QuestionChoiceText,
                QuestionChoiceValue = x.QuestionChoiceValue
            }).ToList();
            var model = new QuestionDetail
            {
                QuestionText = question.QuestionText,
                QuestionType = Enum.GetName(typeof(QuestionType), question.QuestionType),
                QuestionChoiceText = questionDetails


            };
            return View(model);
        }
            
        public ActionResult Edit(int id)
        {
            var svc = new QuestionService();
            var detail = svc.GetQuestionById(id);
            var model =
                new QuestionEdit
                {
                    // QuestionText = detail.QuestionText,
                    // QuestionType = detail.QuestionType,
                    IsActive = detail.IsActive
                };
            return View(model);
        }








        // Helper method
        
            

    }
}