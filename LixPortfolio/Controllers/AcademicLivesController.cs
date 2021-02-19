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
    public class AcademicLivesController : Controller
    {
        private  StarLabs_PortfolioEntities db = new StarLabs_PortfolioEntities();

     
        // GET: AcademicLives
        public ActionResult Index()
        {
           UserData ud = new UserData(UserData.LoggedEmail);
            return View(ud.vmodel.AcademicLife);
        }

        // GET: AcademicLives/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicLife academicLife = db.AcademicLives.Find(id);
            if (academicLife == null)
            {
                return HttpNotFound();
            }
            return View(academicLife);
        }

        // GET: AcademicLives/Create
        public ActionResult Create()
        {
            ViewBag.AcademicType = new SelectList(db.AcademicTypes, "ID", "AcademyType");
            ViewBag.PersonID = new SelectList(db.Personals, "ID", "Name");
            return View();
        }

        // POST: AcademicLives/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PersonID,AcademicType,Academy,StartDate,EndDate,Description")] AcademicLife academicLife)
        {
            if (ModelState.IsValid)
            {
               academicLife.PersonID = UserData.PortfolioID;
                db.AcademicLives.Add(academicLife);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AcademicType = new SelectList(db.AcademicTypes, "ID", "AcademyType", academicLife.AcademicType);
            ViewBag.PersonID = new SelectList(db.Personals, "ID", "Name", academicLife.PersonID);
            return View(academicLife);
        }

        // GET: AcademicLives/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicLife academicLife = db.AcademicLives.Find(id);
            if (academicLife == null)
            {
                return HttpNotFound();
            }
            ViewBag.AcademicType = new SelectList(db.AcademicTypes, "ID", "AcademyType", academicLife.AcademicType);
            ViewBag.PersonID = new SelectList(db.Personals, "ID", "Name", academicLife.PersonID);
            return View(academicLife);
        }

        // POST: AcademicLives/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PersonID,AcademicType,Academy,StartDate,EndDate,Description")] AcademicLife academicLife)
        {
            if (ModelState.IsValid)
            {
                academicLife.PersonID = UserData.PortfolioID;
                db.Entry(academicLife).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AcademicType = new SelectList(db.AcademicTypes, "ID", "AcademyType", academicLife.AcademicType);
            ViewBag.PersonID = new SelectList(db.Personals, "ID", "Name", academicLife.PersonID);
            return View(academicLife);
        }

        // GET: AcademicLives/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicLife academicLife = db.AcademicLives.Find(id);
            if (academicLife == null)
            {
                return HttpNotFound();
            }
            return View(academicLife);
        }

        // POST: AcademicLives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AcademicLife academicLife = db.AcademicLives.Find(id);
            db.AcademicLives.Remove(academicLife);
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
