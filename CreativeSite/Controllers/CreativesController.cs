﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CreativeSite.Models;

namespace CreativeSite.Controllers
{
    public class CreativesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Creatives
        public ActionResult Index()
        {
            return View(db.Creatives.ToList());
        }

        // GET: Creatives/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Creative creative = db.Creatives.Find(id);
            if (creative == null)
            {
                return HttpNotFound();
            }
            return View(creative);
        }

        // GET: Creatives/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Creatives/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title")] Creative creative)
        {
            if (ModelState.IsValid)
            {
                db.Creatives.Add(creative);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(creative);
        }

        // GET: Creatives/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Creative creative = db.Creatives.Find(id);
            if (creative == null)
            {
                return HttpNotFound();
            }
            return View(creative);
        }

        // POST: Creatives/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title")] Creative creative)
        {
            if (ModelState.IsValid)
            {
                db.Entry(creative).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(creative);
        }

        // GET: Creatives/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Creative creative = db.Creatives.Find(id);
            if (creative == null)
            {
                return HttpNotFound();
            }
            return View(creative);
        }

        // POST: Creatives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Creative creative = db.Creatives.Find(id);
            db.Creatives.Remove(creative);
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
