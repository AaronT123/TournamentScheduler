using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TournamentScheduler.Models;

namespace TournamentScheduler.Controllers
{
    public class RRFixtureController : Controller
    {
        private TournamentSchedulerContext db = new TournamentSchedulerContext();

        //// GET: RRFixture
        //public ActionResult Index()
        //{
        //    var rRFixtures = db.RRFixtures.Include(r => r.Tournament);
        //    return View(rRFixtures.ToList());
        //}

        //// GET: RRFixture/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RRFixture rRFixture = db.RRFixtures.Find(id);
        //    if (rRFixture == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(rRFixture);
        //}

        //// GET: RRFixture/Create
        //public ActionResult Create()
        //{
        //    ViewBag.TournamentID = new SelectList(db.Tournaments, "TournamentID", "TournamentName");
        //    return View();
        //}

        //// POST: RRFixture/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "RRFixtureID,RoundNumber,Team1Name,Team2Name,Team1Score,Team2Score,TournamentID")] RRFixture rRFixture)
        //{
        //    RRFixture NewFixture = db.RRFixtures.Find(rRFixture.RRFixtureID);
        //    NewFixture.Team1Score = rRFixture.Team1Score;
        //    NewFixture.Team2Score = rRFixture.Team2Score;

        //    if (ModelState.IsValid)
        //    {
                
        //        db.RRFixtures.Add(NewFixture);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.TournamentID = new SelectList(db.Tournaments, "TournamentID", "TournamentName", rRFixture.TournamentID);
        //    return View(rRFixture);
        //}

        // GET: RRFixture/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RRFixture rRFixture = db.RRFixtures.Find(id);
            if (rRFixture == null)
            {
                return HttpNotFound();
            }
            Tournament MyTournament = db.Tournaments.Find(rRFixture.TournamentID);
            string currentUserID = User.Identity.GetUserId();
            if (MyTournament.TournamentOwnerID == currentUserID)
            {
                ViewBag.TournamentID = new SelectList(db.Tournaments, "TournamentID", "TournamentName", rRFixture.TournamentID);
                return View(rRFixture);
            }
            else
            {
                return HttpNotFound();
            }
        }

        // POST: RRFixture/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RRFixtureID,RoundNumber,Team1Name,Team2Name,Team1Score,Team2Score,TournamentID")] RRFixture rRFixture)
        {

            RRFixture NewFixture = db.RRFixtures.Find(rRFixture.RRFixtureID);
            NewFixture.Team1Score = rRFixture.Team1Score;
            NewFixture.Team2Score = rRFixture.Team2Score;
            if (ModelState.IsValid)
            {
                db.Entry(NewFixture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("RoundRobin", "Tournament", new { id = NewFixture.TournamentID });
            }
            ViewBag.TournamentID = new SelectList(db.Tournaments, "TournamentID", "TournamentName", rRFixture.TournamentID);
            return View(rRFixture);
        }

        //// GET: RRFixture/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RRFixture rRFixture = db.RRFixtures.Find(id);
        //    if (rRFixture == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(rRFixture);
        //}

        //// POST: RRFixture/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    RRFixture rRFixture = db.RRFixtures.Find(id);
        //    db.RRFixtures.Remove(rRFixture);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
