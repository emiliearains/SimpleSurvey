using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimpleSurvey.Data;
using SimpleSurvey.Models;
using SimpleSurvey.Services;

namespace SimpleSurvey.WebMVC.Controllers
{
    public class SurveyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Survey
        public ActionResult Index()
        {
            return View(db.Surveys.ToList());
        }

        // GET: Survey/Details/5
        public ActionResult Details(int? id)
        {
            QuestionService _questionService = new QuestionService();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            List<Question> questions = _questionService.GetQuestionsBySurveyId(survey.SurveyId);
            List<QuestionDetail> questionDetails = questions.Select(x => new QuestionDetail()
            {
                QuestionText = x.QuestionText,
                QuestionType = Enum.GetName(typeof(QuestionType), x.QuestionType),
                QuestionChoiceText = _questionService.GetQuestionChoicesByQuestionId(x.QuestionId)
                                                        .Select(y => new QuestionChoiceDetails()
                                                        {
                                                            QuestionChoiceText = y.QuestionChoiceText,
                                                            QuestionChoiceValue = y.QuestionChoiceValue
                                                        }).ToList()
            }).ToList();

            SurveyDetail surveyDetail = new SurveyDetail()
            {
                SurveyTitle = survey.SurveyTitle,
                SurveyDescription = survey.SurveyDescription,
                StartDate = survey.StartDate,
                EndDate = survey.EndDate,
                SurveyQuestions = questionDetails,
                SurveyId = survey.SurveyId
            };

            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(surveyDetail);
        }

        // GET: Survey/Create
        public ActionResult Create()
        {
            QuestionService _questionService = new QuestionService();
            List<QuestionSelectList> questionList = _questionService.GetQuestions().Select(x => new QuestionSelectList()
            {
                QuestionId = x.QuestionId,
                QuestionText = x.QuestionText
            }).ToList();
            ViewBag.QuestionSelectList = questionList;

            return View();
        }

        // POST: Survey/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SurveyCreate survey)
        {
            QuestionService _questionService = new QuestionService();
            bool surveySaved = false;

            Survey newSurvey = new Survey()
            {
                SurveyTitle = survey.SurveyTitle,
                SurveyDescription = survey.SurveyDescription,
                StartDate = survey.StartDate,
                EndDate = survey.EndDate
            };

            if (ModelState.IsValid)
            {
                db.Surveys.Add(newSurvey);
                db.SaveChanges();
                surveySaved = true;
            }

            List<SurveyQuestion> surveyQuestions = new List<SurveyQuestion>();
            int i = 0;
            foreach (int questionId in survey.SelectedSurveyQuestions)
            {
                Question currQuestion = _questionService.GetQuestionById(questionId);
                surveyQuestions.Add(new SurveyQuestion()
                {
                    QuestionId = questionId,
                    SurveyId = newSurvey.SurveyId,
                    Order = i
                });

                i++;
            }

            if(surveyQuestions.Count > 0)
            {
                db.SurveyQuestions.AddRange(surveyQuestions);
                db.SaveChanges();
            }

            if (surveySaved)
            {
                return RedirectToAction("Index");
            }

            return View(survey);
        }

        // GET: Survey/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // POST: Survey/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SurveyId,SurveyTitle,SurveyDescription,StartDate,EndDate")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                db.Entry(survey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(survey);
        }

        // GET: Survey/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // POST: Survey/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Survey survey = db.Surveys.Find(id);
            db.Surveys.Remove(survey);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Survey
        public ActionResult AssignUsersToSurvey(int? surveyId)
        {
            UserService userService = new UserService();

            UserSurveyAssign model = new UserSurveyAssign();
            model.SurveyId = surveyId ?? 0;

            var userList = userService.GetUsers();
            ViewBag.UserList = userList.Select(x => new UserSelectList
            {
               UserId = x.Id,
               UserName = $"{x.LastName}, {x.FirstName}"

            }).OrderBy(x => x.UserName);

            //ViewBag.DepartmentList = 

            return View(model);
        }

        [HttpPost]
        public ActionResult AssignUsersToSurvey(UserSurveyAssign userSurveyAssign, string assignType)
        {
            UserService userService = new UserService();
            SurveyService surveyService = new SurveyService();

            List<ApplicationUser> selectedUsers = new List<ApplicationUser>();

            switch (assignType.ToUpper())
            {
                case "USER":
                    selectedUsers = userService.GetUserByIdRange(userSurveyAssign.UserIds);
                    break;
                case "DEPARTMENT":
                    selectedUsers = userService.GetUsersByDepartment(userSurveyAssign.DepartmentId);
                    break;
                case "JOBTITLE":
                    selectedUsers =userService.GetUsersByJobTitle(userSurveyAssign.JobTitleId);
                    break;
            }

            // Create UserSurvey records for each user in selectedUsers
            foreach (var user in selectedUsers)
            {
                surveyService.CreateUserSurvey(new UserSurveyCreate
                {
                    SurveyId = userSurveyAssign.SurveyId,
                    UserId = Guid.Parse(user.Id)
                });
            }

            return RedirectToAction("Index");
        }

        public ActionResult UserSurveyCompleted(int surveyId)
        {
            UserService userService = new UserService();
            SurveyService surveyService = new SurveyService();
            var completedUserSurveys = surveyService.GetCompletedUserSurveysBySurveyId(surveyId);
            List<UserSurveyCompleted> model = new List<UserSurveyCompleted>();
            foreach (var survey in completedUserSurveys)
            {
                var user = userService.GetUserById(survey.UserId.ToString());
                model.Add(new UserSurveyCompleted
                {
                    UserSurveyId = survey.UserSurveyId,
                    UserName = $"{user.FirstName} {user.LastName}",
                    DateCompleted = survey.DateCompleted ?? new DateTime()
                });
            }
            return View(model);

        }

        public ActionResult UserSurveyCompletedDetails(int userSurveyId)
        {
            QuestionService _questionService = new QuestionService();
            SurveyService _surveyService = new SurveyService();

            if (userSurveyId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSurvey userSurvey = _surveyService.GetUserSurveyById(userSurveyId);
            Survey survey = _surveyService.GetSurveyById(userSurvey.SurveyId);
            List<UserAnswer> userAnswers = _surveyService.GetUserAnswersByUserSurveyId(userSurveyId);
            //List<Question> questions = _questionService.GetQuestionsBySurveyId(survey.SurveyId);
            List<QuestionDetail> questionDetails = new List<QuestionDetail>();
            foreach (var answer in userAnswers)
            {
                var questionChoice = _questionService.GetQuestionChoiceById(answer.QuestionChoiceId);
                var question = _questionService.GetQuestionById(questionChoice.QuestionId);
                List<QuestionChoiceDetails> questionChoiceDetails = new List<QuestionChoiceDetails>();
                questionChoiceDetails.Add(new QuestionChoiceDetails
                {
                    QuestionChoiceText = questionChoice.QuestionChoiceText,
                    QuestionChoiceValue = questionChoice.QuestionChoiceValue,
                    QuestionChoiceId = questionChoice.QuestionChoiceId
                });

                
                questionDetails.Add(new QuestionDetail
                {
                    QuestionText = question.QuestionText,
                    QuestionId = question.QuestionId,
                    QuestionType = Enum.GetName(typeof(QuestionType), question.QuestionType),
                    QuestionChoiceText = questionChoiceDetails
                });
            }


            //List<QuestionDetail> questionDetails = userAnswers.Select(x => new QuestionDetail()
            //{
            //    QuestionText = x.QuestionChoice.Question.QuestionText,
            //    QuestionId = x.QuestionChoice.QuestionId,
            //    QuestionType = Enum.GetName(typeof(QuestionType), x.QuestionChoice.Question.QuestionType),
            //    QuestionChoiceText = new List<QuestionChoiceDetails>() { new QuestionChoiceDetails
            //    {
            //        QuestionChoiceText = x.QuestionChoice.QuestionChoiceText,
            //        QuestionChoiceValue = x.QuestionChoice.QuestionChoiceValue,
            //        QuestionChoiceId = x.QuestionChoiceId
            //    }}
                
            //    //_questionService.GetQuestionChoiceById()
            //    //                                        .Select(y => new QuestionChoiceDetails()
            //    //                                        {
            //    //                                            QuestionChoiceText = y.QuestionChoiceText,
            //    //                                            QuestionChoiceValue = y.QuestionChoiceValue,
            //    //                                            QuestionChoiceId = y.QuestionChoiceId
            //    //                                        }).ToList()
            //}).ToList();

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
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
