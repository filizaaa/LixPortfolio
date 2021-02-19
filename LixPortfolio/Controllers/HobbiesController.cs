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
    public class HobbiesController : Controller
    {
        private  StarLabs_PortfolioEntities db = new StarLabs_PortfolioEntities();



        // GET: Hobbies
       
        public ActionResult Index()
        {
            UserData ud = new UserData(UserData.LoggedEmail);
            return View(ud.vmodel.Hobbies);
        }

        // GET: Hobbies/Details/5
       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hobby hobby = db.Hobbies.Find(id);
            if (hobby == null)
            {
                return HttpNotFound();
            }
            return View(hobby);
        }

        // GET: Hobbies/Create
        public ActionResult Create()
        {
            ViewBag.PersonID = new SelectList(db.Personals, "ID", "Name");
            return View();
        }

        // POST: Hobbies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult Create([Bind(Include = "ID,PersonID,Hoby")] Hobby hobby)
        {
            if (ModelState.IsValid)
            {
                hobby.PersonID= UserData.PortfolioID;
                db.Hobbies.Add(hobby);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonID = new SelectList(db.Personals, "ID", "Name", hobby.PersonID);
            return View(hobby);
        }

        // GET: Hobbies/Edit/5
      
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hobby hobby = db.Hobbies.Find(id);
            if (hobby == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonID = new SelectList(db.Personals, "ID", "Name", hobby.PersonID);
            return View(hobby);
        }

        // POST: Hobbies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public ActionResult Edit([Bind(Include = "ID,PersonID,Hoby")] Hobby hobby)
        {
            if (ModelState.IsValid)
            {
                hobby.PersonID = UserData.PortfolioID;
                db.Entry(hobby).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonID = new SelectList(db.Personals, "ID", "Name", hobby.PersonID);
            return View(hobby);
        }

        // GET: Hobbies/Delete/5
    
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hobby hobby = db.Hobbies.Find(id);
            if (hobby == null)
            {
                return HttpNotFound();
            }
            return View(hobby);
        }

        // POST: Hobbies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hobby hobby = db.Hobbies.Find(id);
            db.Hobbies.Remove(hobby);
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
