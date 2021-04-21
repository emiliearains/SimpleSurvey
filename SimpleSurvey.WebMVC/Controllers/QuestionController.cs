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
        public ActionResult CreateQuestion(QuestionCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = new QuestionService();

            if (service.CreateQuestion(model))
            {
                TempData["SaveResult"] = "Your question was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Question could not be created.");
            return View(model);
        }
    }
}