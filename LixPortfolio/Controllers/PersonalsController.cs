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
    public class PersonalsController : Controller
    {
        private  StarLabs_PortfolioEntities db = new StarLabs_PortfolioEntities();



        // GET: Personals
      
        public ActionResult Index()
        { UserData ud = new UserData(UserData.LoggedEmail);
            return View(ud.vmodel.Personal);
        }

        // GET: Personals/Details/5
       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personal personal = db.Personals.Find(id);
            if (personal == null)
            {
                return HttpNotFound();
            }
            return View(personal);
        }

        // GET: Personals/Create
       
        public ActionResult Create()
        {
            return View();
        }

        // POST: Personals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Surname,DateOfBirth,Hometown,Description,Photo,Email")] Personal personal)
        {
            UserData ud = new UserData(UserData.LoggedEmail);
            if (ud.vmodel.Personal == null)
            { 
            if (ModelState.IsValid)
            {       personal.Email =User.Identity.Name;
                    db.Personals.Add(personal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            }

            return View(personal);
        }

        // GET: Personals/Edit/5
      
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personal personal = db.Personals.Find(id);
            if (personal == null)
            {
                return HttpNotFound();
            }
            return View(personal);
        }

        // POST: Personals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Surname,DateOfBirth,Hometown,Description,Photo,Email")] Personal personal)
        {
            if (ModelState.IsValid)
            {
                personal.Email = User.Identity.Name;
                db.Entry(personal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personal);
        }

        // GET: Personals/Delete/5
       
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personal personal = db.Personals.Find(id);
            if (personal == null)
            {
                return HttpNotFound();
            }
            UserData ud = new UserData(UserData.LoggedEmail);
            
            return View(ud.vmodel);
        }

        // POST: Personals/Delete/5
      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Personal personal = db.Personals.Find(id);
            UserData ud = new UserData(UserData.LoggedEmail);
            foreach (var item in ud.vmodel.Hobbies)
            {
                db.Hobbies.Remove(db.Hobbies.Find(item.ID));
            }
            foreach (var item in ud.vmodel.AcademicLife)
            {
                db.AcademicLives.Remove(db.AcademicLives.Find(item.ID));
            }
            foreach (var item in ud.vmodel.Skills)
            {
                db.Skills.Remove(db.Skills.Find(item.ID));
            }
            foreach (var item in ud.vmodel.Experiences)
            {
                db.Experiences.Remove(db.Experiences.Find(item.ID));
            }
            foreach (var item in ud.vmodel.Contacts)
            {
                db.Contacts.Remove(db.Contacts.Find(item.ID));
            }

            db.Personals.Remove(personal);
            ud.RemoveData();
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
