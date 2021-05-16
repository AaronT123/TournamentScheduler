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
    public class TournamentController : Controller
    {
        private TournamentSchedulerContext db = new TournamentSchedulerContext();

        // GET: Tournament
        [Authorize]
        public ActionResult Index()
        {
            string currentUserID = User.Identity.GetUserId();
            var tournaments = db.Tournaments.Where(e => e.TournamentOwnerID == currentUserID);
            return View(tournaments.ToList());
        }

        // GET: Tournament/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournament tournament = db.Tournaments.Find(id);
            if (tournament == null)
            {
                return HttpNotFound();
            }
            string currentUserID = User.Identity.GetUserId();
            if (tournament.TournamentOwnerID == currentUserID)
            {
                return View(tournament);
            }
            else
            {
                return HttpNotFound();
            }
        }

        // GET: Tournament/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tournament/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TournamentID,TournamentName,TournamentOwnerID,StartDate")] Tournament tournament)
        {
            if (ModelState.IsValid)
            {
                tournament.TournamentOwnerID = User.Identity.GetUserId();
                db.Tournaments.Add(tournament);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tournament);
        }


        //GET: Round Robin
        [Authorize]
        public ActionResult RoundRobin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournament tournament = db.Tournaments.Find(id);
            if (tournament == null)
            {
                return HttpNotFound();
            }
            string currentUserID = User.Identity.GetUserId();
            if (tournament.TournamentOwnerID == currentUserID)
            {
                return View(tournament);
            }
            else
            {
                return HttpNotFound();
            }
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
