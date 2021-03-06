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
    public class ChaptersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Chapters
        public ActionResult Index(int? id)
        {
            var chapters = db.Chapters;

            MarkDownChaptersContentToHtml(chapters);

            if (id != null)
            {
                ViewBag.CreativeId = id;
                return View(chapters.Where(x => x.Creative.Id == id));
            }
            else
            {
                return View(chapters);
            }
        }

        // GET: Chapters/Details/5
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chapter chapter = db.Chapters.Find(id);
            MarkDownChaptersContentHtml(chapter);
            if (chapter== null)
            {
                return HttpNotFound();
            }
            return View(chapter);
        }

        // GET: Chapters/Create
        public ActionResult Create(int? creativeId)
        {
            ViewBag.CreativeId = creativeId;

            return View();
        }

        // POST: Chapters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,Tags")] Chapter chapter, int? creativeId)
        {
            if (ModelState.IsValid)
            {
                var creative = db.Creatives.Find(creativeId);
                creative.Chapters.Add(chapter);
                db.SaveChanges();
                return RedirectToAction("Index", routeValues: new { id = creativeId });
            }

            return View(chapter);
        }
        // GET: Chapters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chapter chapter = db.Chapters.Find(id);
            if (chapter == null)
            {
                return HttpNotFound();
            }
            return View(chapter);
        }

        // POST: Chapters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,Tags")] Chapter chapter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chapter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chapter);
        }

        // GET: Chapters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Chapter chapter = db.Chapters.Find(id);
            if (chapter == null)
            {
                return HttpNotFound();
            }
            return View(chapter);
        }

        // POST: Chapters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chapter chapter = db.Chapters.Find(id);
            db.Chapters.Remove(chapter);
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

        private void MarkDownChaptersContentToHtml(IEnumerable<Chapter> chapters)
        {
            foreach (var chapter in chapters)
            {
                chapter.Content = CommonMark.CommonMarkConverter.Convert(chapter.Content); 
            }
        }

        private void MarkDownChaptersContentHtml(Chapter chapter)
        {
           
                chapter.Content = CommonMark.CommonMarkConverter.Convert(chapter.Content);
        }
    }
}
