using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Task1.Models;

namespace Task1.Controllers
{
    public class tblPersonsController : Controller
    {
        private dbTask1Entities db = new dbTask1Entities();

        // GET: tblPersons
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView(db.tblPersons.ToList());
            }
            return View(db.tblPersons.ToList());
        }

        // GET: tblPersons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPerson tblPerson = db.tblPersons.Find(id);
            DateTime saveNow = DateTime.Now;
            tblPerson.DATE = saveNow;
            if (tblPerson == null)
            {
                return HttpNotFound();
            }
            return PartialView(tblPerson);
        }

        // GET: tblPersons/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: tblPersons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NAME,PROFESSION,DATE")] tblPerson tblPerson)
        {
            DateTime saveUtcNow = DateTime.UtcNow;
            tblPerson.DATE = saveUtcNow;
            if (ModelState.IsValid)
            {
                db.tblPersons.Add(tblPerson);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblPerson);
        }

        // GET: tblPersons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPerson tblPerson = db.tblPersons.Find(id);
            if (tblPerson == null)
            {
                return HttpNotFound();
            }
            return PartialView(tblPerson);
        }

        // POST: tblPersons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NAME,PROFESSION,DATE")] tblPerson tblPerson)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblPerson).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblPerson);
        }

        // GET: tblPersons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPerson tblPerson = db.tblPersons.Find(id);
            if (tblPerson == null)
            {
                return HttpNotFound();
            }
            return PartialView(tblPerson);
        }

        // POST: tblPersons/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "ID,NAME,PROFESSION,DATE")] tblPerson tblPerson)
        {
            tblPerson person = db.tblPersons.Find(tblPerson.ID);
            db.tblPersons.Remove(person);
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
