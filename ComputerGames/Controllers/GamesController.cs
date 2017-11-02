using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ComputerGames.Models;

namespace ComputerGames.Controllers
{
    public class GamesController : Controller
    {
        private ComputerGamesEntities db = new ComputerGamesEntities();

        // GET: Games
        public ActionResult Index(string gameGenre, string searchString)
        {
            // Game Genre & Game Title Search
            var genreList = new List<string>();

            var genreQuery = from q in db.CompGames
                             orderby q.Genre
                             select q.Genre;

            genreList.AddRange(genreQuery.Distinct());
            ViewBag.gameGenre = new SelectList(genreList);

            var gameSearch = from g in db.CompGames
                             select g;

            if (!String.IsNullOrEmpty(searchString))
            {
                gameSearch = gameSearch.Where(s => s.GameTitle.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(gameGenre))
            {
                gameSearch = gameSearch.Where(x => x.Genre == gameGenre);
            }

            return View(gameSearch);
        }

        public ActionResult Likes(int id)
        {
            CompGame compgame = db.CompGames.Find(id);

            int? userLikes = compgame.Likes;
            compgame.Likes = userLikes + 1;

            if (ModelState.IsValid)
            {
                db.Entry(compgame).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Dislikes(int id)
        {
            CompGame compgame = db.CompGames.Find(id);

            int? userDislikes = compgame.Dislikes;
            compgame.Dislikes = userDislikes + 1;

            if (ModelState.IsValid)
            {
                db.Entry(compgame).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        
        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompGame compGame = db.CompGames.Find(id);
            if (compGame == null)
            {
                return HttpNotFound();
            }
            return View(compGame);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GameTitle,Platform,Genre,AgeRating,ReleaseDate,Price,Picture,Preview,Likes,Dislikes")] CompGame compGame)
        {
            //if (compGame.Picture == null)
            //{
            //    compGame.Picture = "https://upload.wikimedia.org/wikipedia/commons/a/ac/No_image_available.svg";
            //}

            if (ModelState.IsValid)
            {
                db.CompGames.Add(compGame);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(compGame);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompGame compGame = db.CompGames.Find(id);
            if (compGame == null)
            {
                return HttpNotFound();
            }
            return View(compGame);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GameTitle,Platform,Genre,AgeRating,ReleaseDate,Price,Picture,Preview,Likes,Dislikes")] CompGame compGame)
        {
            //if (compGame.Picture == null)
            //{
            //    compGame.Picture = "https://upload.wikimedia.org/wikipedia/commons/a/ac/No_image_available.svg";
            //}

            if (ModelState.IsValid)
            {
                db.Entry(compGame).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(compGame);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompGame compGame = db.CompGames.Find(id);
            if (compGame == null)
            {
                return HttpNotFound();
            }
            return View(compGame);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Find Game
            CompGame compGame = db.CompGames.Find(id);

            //Delete from Database
            db.CompGames.Remove(compGame);
            db.SaveChanges();

            //Return to Index page
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
