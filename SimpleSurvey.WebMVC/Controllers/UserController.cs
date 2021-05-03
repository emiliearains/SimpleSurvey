using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimpleSurvey.Data;

namespace SimpleSurvey.WebMVC.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: User
        public ActionResult Index()
        {
            var userSurveys = db.UserSurveys.Include(u => u.Survey);
            return View(userSurveys.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSurvey userSurvey = db.UserSurveys.Find(id);
            if (userSurvey == null)
            {
                return HttpNotFound();
            }
            return View(userSurvey);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.SurveyId = new SelectList(db.Surveys, "SurveyId", "SurveyTitle");
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserSurveyId,UserId,SurveyId,DateCompleted")] UserSurvey userSurvey)
        {
            if (ModelState.IsValid)
            {
                db.UserSurveys.Add(userSurvey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SurveyId = new SelectList(db.Surveys, "SurveyId", "SurveyTitle", userSurvey.SurveyId);
            return View(userSurvey);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSurvey userSurvey = db.UserSurveys.Find(id);
            if (userSurvey == null)
            {
                return HttpNotFound();
            }
            ViewBag.SurveyId = new SelectList(db.Surveys, "SurveyId", "SurveyTitle", userSurvey.SurveyId);
            return View(userSurvey);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserSurveyId,UserId,SurveyId,DateCompleted")] UserSurvey userSurvey)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userSurvey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SurveyId = new SelectList(db.Surveys, "SurveyId", "SurveyTitle", userSurvey.SurveyId);
            return View(userSurvey);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSurvey userSurvey = db.UserSurveys.Find(id);
            if (userSurvey == null)
            {
                return HttpNotFound();
            }
            return View(userSurvey);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserSurvey userSurvey = db.UserSurveys.Find(id);
            db.UserSurveys.Remove(userSurvey);
            db.SaveChanges();
            return RedirectToAction("Index");
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
