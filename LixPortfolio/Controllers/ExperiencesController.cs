using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LixPortfolio.Models;

namespace LixPortfolio.Controllers
{
    [Authorize]
    public class ExperiencesController : Controller
    {
        private  StarLabs_PortfolioEntities db = new StarLabs_PortfolioEntities();
        
        // GET: Experiences
        public ActionResult Index()
        {
           UserData ud = new UserData(UserData.LoggedEmail);
            return View(ud.vmodel.Experiences);
        }

        // GET: Experiences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experience experience = db.Experiences.Find(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            return View(experience);
        }

        // GET: Experiences/Create
        public ActionResult Create()
        {
            ViewBag.PersonID = new SelectList(db.Personals, "ID", "Name");
            ViewBag.WorkType = new SelectList(db.WorkTypes, "ID", "WorkType1");
            return View();
        }

        // POST: Experiences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PersonID,WorkType,WorkPlace,Description,StartDate,EndDate")] Experience experience)
        {
            if (ModelState.IsValid)
            { experience.PersonID = UserData.PortfolioID;
                db.Experiences.Add(experience);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonID = new SelectList(db.Personals, "ID", "Name", experience.PersonID);
            ViewBag.WorkType = new SelectList(db.WorkTypes, "ID", "WorkType1", experience.WorkType);
            return View(experience);
        }

        // GET: Experiences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experience experience = db.Experiences.Find(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonID = new SelectList(db.Personals, "ID", "Name", experience.PersonID);
            ViewBag.WorkType = new SelectList(db.WorkTypes, "ID", "WorkType1", experience.WorkType);
            return View(experience);
        }

        // POST: Experiences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PersonID,WorkType,WorkPlace,Description,StartDate,EndDate")] Experience experience)
        {
            if (ModelState.IsValid)
            {
                experience.PersonID = UserData.PortfolioID;
                db.Entry(experience).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonID = new SelectList(db.Personals, "ID", "Name", experience.PersonID);
            ViewBag.WorkType = new SelectList(db.WorkTypes, "ID", "WorkType1", experience.WorkType);
            return View(experience);
        }

        // GET: Experiences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experience experience = db.Experiences.Find(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            return View(experience);
        }

        // POST: Experiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Experience experience = db.Experiences.Find(id);
            db.Experiences.Remove(experience);
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
