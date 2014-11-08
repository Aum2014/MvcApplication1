using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication2.Models;

namespace MvcApplication2.Controllers
{
    public class MoviesController : Controller
    {
        private MoviesDBContext db = new MoviesDBContext();

        //
        // GET: /Movies/
        public ActionResult Index(string movieGenre, string searchString)
        {
            var GenreLst = new List<string>();

            var GenreQry = from d in db.Movies
                           orderby d.Genre
                           select d.Genre;

            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.movieGenre = new SelectList(GenreLst);

            var movies = from m in db.Movies
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            return View(movies);
        }

        //
        // GET: /Movies/Details/5
        

        public ActionResult Details(int id = 0)
        {
            Movies movies = db.Movies.Find(id);
            if (movies == null)
            {
                return HttpNotFound();
            }
            return View(movies);
        }

        //
        // GET: /Movies/Create
         [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Movies/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movies movies)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movies);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movies);
        }

        //
        // GET: /Movies/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Movies movies = db.Movies.Find(id);
            if (movies == null)
            {
                return HttpNotFound();
            }
            return View(movies);
        }

        //
        // POST: /Movies/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Movies movies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movies);
        }

        //
        // GET: /Movies/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Movies movies = db.Movies.Find(id);
            if (movies == null)
            {
                return HttpNotFound();
            }
            return View(movies);
        }
        
        //
        // POST: /Movies/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movies movies = db.Movies.Find(id);
            db.Movies.Remove(movies);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}