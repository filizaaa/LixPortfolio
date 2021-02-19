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
    public class SkillsController : Controller
    {
        private  StarLabs_PortfolioEntities db = new StarLabs_PortfolioEntities();
      
            // GET: Skills
          
            public ActionResult Index()
        {
            UserData ud = new UserData(UserData.LoggedEmail);
           
            return View(ud.vmodel.Skills);
        }

        // GET: Skills/Details/5
       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        // GET: Skills/Create
      
        public ActionResult Create()
        {
            ViewBag.PersonID = new SelectList(db.Personals, "ID", "Name");
            ViewBag.SkillType = new SelectList(db.SkillTypes, "ID", "SkillType1");
            return View();
        }

        // POST: Skills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PersonID,SkillType,Skill1,Skill_Description")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                skill.PersonID = UserData.PortfolioID;
                skill.Skill_Description = skill.Skill1;
                db.Skills.Add(skill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonID = new SelectList(db.Personals, "ID", "Name", skill.PersonID);
            ViewBag.SkillType = new SelectList(db.SkillTypes, "ID", "SkillType1", skill.SkillType);
            return View(skill);
        }

        // GET: Skills/Edit/5
     
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonID = new SelectList(db.Personals, "ID", "Name", skill.PersonID);
            ViewBag.SkillType = new SelectList(db.SkillTypes, "ID", "SkillType1", skill.SkillType);
            return View(skill);
        }

        // POST: Skills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PersonID,SkillType,Skill1,Skill_Description")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                skill.PersonID = UserData.PortfolioID;
                db.Entry(skill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonID = new SelectList(db.Personals, "ID", "Name", skill.PersonID);
            ViewBag.SkillType = new SelectList(db.SkillTypes, "ID", "SkillType1", skill.SkillType);
            return View(skill);
        }

        // GET: Skills/Delete/5
      
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        // POST: Skills/Delete/5
       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Skill skill = db.Skills.Find(id);
            db.Skills.Remove(skill);
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
