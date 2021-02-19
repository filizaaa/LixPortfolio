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
    public class ContactsController : Controller
    {
        private StarLabs_PortfolioEntities db = new StarLabs_PortfolioEntities();

      
        // GET: Contacts
        public ActionResult Index()
        {
             UserData ud = new UserData(UserData.LoggedEmail);
            return View(ud.vmodel.Contacts);
        }

        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            ViewBag.ContactType = new SelectList(db.ContactTypes, "ID", "ContactName");
            ViewBag.PersonID = new SelectList(db.Personals, "ID", "Name");
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PersonID,ContactType,Contact1")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.PersonID = UserData.PortfolioID;
                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContactType = new SelectList(db.ContactTypes, "ID", "ContactName", contact.ContactType);
            ViewBag.PersonID = new SelectList(db.Personals, "ID", "Name", contact.PersonID);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactType = new SelectList(db.ContactTypes, "ID", "ContactName", contact.ContactType);
            ViewBag.PersonID = new SelectList(db.Personals, "ID", "Name", contact.PersonID);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PersonID,ContactType,Contact1")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.PersonID = UserData.PortfolioID;
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContactType = new SelectList(db.ContactTypes, "ID", "ContactName", contact.ContactType);
            ViewBag.PersonID = new SelectList(db.Personals, "ID", "Name", contact.PersonID);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
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
