using SimpleSurvey.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Net;
using SimpleSurvey.Data;
using SimpleSurvey.Models;

namespace SimpleSurvey.WebMVC.Controllers
{
    public class UserSurveyController : Controller
    {
        // GET: UserSurvey
        public ActionResult Index()
        {
            return View();
        }

        // GET: MyOpenSurveys
        public ActionResult MyOpenSurveys()
        {
            SurveyService surveyService = new SurveyService();
            var userId = User.Identity.GetUserId();

            var userSurveys = surveyService.GetOpenSurveysByUserId(userId);
            List<UserSurveyInfo> model = new List<UserSurveyInfo>();

            foreach (var userSurvey in userSurveys)
            {
                var survey = surveyService.GetSurveyById(userSurvey.SurveyId);
                model.Add(new UserSurveyInfo
                {
                    UserSurveyId = userSurvey.UserSurveyId,
                    SurveyId = survey.SurveyId,
                    SurveyDescription = survey.SurveyDescription,
                    SurveyTitle = survey.SurveyTitle
                });
            }

            return View(model);
        }

        // GET: TakeSurvey
        public ActionResult TakeSurvey(int userSurveyId)
        {
            QuestionService _questionService = new QuestionService();
            SurveyService _surveyService = new SurveyService();

            if (userSurveyId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSurvey userSurvey = _surveyService.GetUserSurveyById(userSurveyId);
            Survey survey = _surveyService.GetSurveyById(userSurvey.SurveyId);
            List<Question> questions = _questionService.GetQuestionsBySurveyId(survey.SurveyId);
            List<QuestionDetail> questionDetails = questions.Select(x => new QuestionDetail()
            {
                QuestionText = x.QuestionText,
                QuestionId = x.QuestionId,
                QuestionType = Enum.GetName(typeof(QuestionType), x.QuestionType),
                QuestionChoiceText = _questionService.GetQuestionChoicesByQuestionId(x.QuestionId)
                                                        .Select(y => new QuestionChoiceDetails()
                                                        {
                                                            QuestionChoiceText = y.QuestionChoiceText,
                                                            QuestionChoiceValue = y.QuestionChoiceValue,
                                                            QuestionChoiceId = y.QuestionChoiceId
                                                        }).ToList()
            }).ToList();

            SurveyDetail surveyDetail = new SurveyDetail()
            {
                SurveyTitle = survey.SurveyTitle,
                SurveyDescription = survey.SurveyDescription,
                StartDate = survey.StartDate,
                EndDate = survey.EndDate,
                SurveyQuestions = questionDetails,
                SurveyId = survey.SurveyId,
                UserSurveyId = userSurvey.UserSurveyId
            };

            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(surveyDetail);
            //return View();
        }

        [HttpPost]
        public ActionResult TakeSurvey(SurveyDetail surveyDetail)
        {
            SurveyService surveyService = new SurveyService();
            var userSurvey = surveyService.GetUserSurveyById(surveyDetail.UserSurveyId);
            foreach (var question in surveyDetail.SurveyQuestions)
            {
                surveyService.CreateSurveyAnswer(userSurvey.UserSurveyId, question.SelectedChoice);
                surveyService.CompleteUserSurvey(userSurvey.UserSurveyId);
            }
            return RedirectToAction("MyOpenSurveys");
        }
    }
}