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
    public class TeamController : Controller
    {
        private TournamentSchedulerContext db = new TournamentSchedulerContext();

        //// GET: Team
        //public ActionResult Index()
        //{
        //    return View(db.Teams.ToList());
        //}

        //// GET: Team/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Team team = db.Teams.Find(id);
        //    if (team == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(team);
        //}

        // GET: Team/Create
        [Authorize]
        public ActionResult Create(int? TeamTournamentID)
        {
            Tournament MyTournament = db.Tournaments.Find(TeamTournamentID);

            string TournamentUserID = MyTournament.TournamentOwnerID;
            string currentUserID = User.Identity.GetUserId();
            if (TournamentUserID == currentUserID)
            {
                Team team = new Team();
                team.TournamentID = (int)TeamTournamentID;
                return View(team);
            }
            else
            {
                return HttpNotFound();
            }
        }

        // POST: Team/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamID,TeamName")] Team team, int TeamTournamentID)
        {
            if (ModelState.IsValid)
            {
                team.TournamentID = TeamTournamentID;
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Details", "Tournament", new { id = TeamTournamentID } );
            }

            return View(team);
        }

        //// GET: Team/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Team team = db.Teams.Find(id);
        //    if (team == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(team);
        //}

        //// POST: Team/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "TeamID,TeamName")] Team team)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        db.Entry(team).State = EntityState.Modified;
        //        // Set a default for testing
        //        team.TournamentID = 1;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(team);
        //}

        // GET: Team/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            int TeamTournamentID = team.TournamentID;
            Tournament tournament = db.Tournaments.Find(TeamTournamentID);
            string TournamentUserID = tournament.TournamentOwnerID;
            string currentUserID = User.Identity.GetUserId();
            
            if (team == null)
            {
                return HttpNotFound();
            }
            if (TournamentUserID == currentUserID)
            {
                return View(team);
            }
            else
            {
                return HttpNotFound();
            }
        }

        // POST: Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Teams.Find(id);
            int TeamTournamentID = team.TournamentID;
            db.Teams.Remove(team);
            db.SaveChanges();
            return RedirectToAction("Details", "Tournament", new { id = TeamTournamentID });
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
