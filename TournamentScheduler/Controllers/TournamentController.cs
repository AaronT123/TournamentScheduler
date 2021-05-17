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
                if (tournament.TournamentStarted == false)
                {
                    //Get team names associated to the tournament
                    //Generate round robin fixtures
                    //Add fixtures to fixture table

                    //Full List of Team Names
                    List<string> TeamNameList = new List<string>();

                    foreach (var myTeam in tournament.Teams)
                    {
                        string AddThis = myTeam.TeamName;
                        TeamNameList.Add(myTeam.TeamName);
                    }

                    //Length of list
                    int HowManyTeams = TeamNameList.Count();

                    //Find if # of teams is even
                    bool TeamIsEven;
                    if (HowManyTeams % 2 == 0)
                    {
                        TeamIsEven = true;
                    }
                    else
                    {
                        TeamIsEven = false;
                    }

                    //midway point of list
                    int mid = HowManyTeams / 2;

                    //The two lists needed to generate fixtures
                    List<string> RRList1 = TeamNameList.Take(mid).ToList();
                    List<string> RRList2 = TeamNameList.Skip(mid).ToList();

                    //Fill in list so they are the same length, null will be a bye
                    if (TeamIsEven == false)
                    {
                        RRList1.Add(null);
                    }

                    //If odd, mid would be 2, if even it would be 3 in this example
                    int LastListIndex;
                    if (TeamIsEven == true)
                    {
                        //mid is 3, index needs to go to 2
                        LastListIndex = mid - 1;
                    }
                    else
                    {
                        //mid is 2, index can =
                        LastListIndex = mid;
                    }

                    //Reverse list to make fixture generating easier
                    //(First team will play the last in the first game)
                    RRList2.Reverse();

                    int count = 1;
                    int count2 = 0;
                    var MyFixture = new RRFixture();
                    MyFixture.TournamentID = tournament.TournamentID;

                    int newHowManyTeams;
                    if (HowManyTeams % 2 == 1)
                    {
                        newHowManyTeams = HowManyTeams + 1;
                    }
                    else
                    {
                        newHowManyTeams = HowManyTeams;
                    }

                    //Rounds
                    while (count < newHowManyTeams)
                    {
                        //Set round number
                        MyFixture.RoundNumber = count;
                        //Escape Case
                        count += 1;
                        

                        //Matches per round
                        while (count2 <= LastListIndex)
                        {
                            //Add the matching index in each list with each other
                            MyFixture.Team1Name = RRList1[count2];
                            MyFixture.Team2Name = RRList2[count2];

                            //add the fixture with the tournamentid, roundnumber, team1name and team2name

                            db.RRFixtures.Add(MyFixture);
                            db.SaveChanges();

                            //Escape case
                            count2 += 1;
                        }
                        //Reset Match loop counter
                        count2 = 0;

                        // After each round alter the lists so that every team plays each other
                        // For round robin, keep the first team in the same position and rotate
                        // the rest around till they have all played each other

                        //LastListIndex = 2

                        //-----------------------ROUND 1------------------------
                        //RRList1 = [DMU, Cabbage Family, *NULL*]
                        //RRList2 = [Man City, Liverpool, Man U]

                        RRList1.Insert(1, RRList2[0]);

                        //RRList1 = [DMU, Man City, Cabbage Family, *NULL*]
                        //RRList2 = [Man City, Liverpool, Man U]

                        RRList2.RemoveAt(0);

                        //RRList1 = [DMU, Man City, Cabbage Family, *NULL*]
                        //RRList2 = [Liverpool, Man U]

                        RRList2.Add(RRList1[LastListIndex + 1]);

                        //RRList1 = [DMU, Man City, Cabbage Family, *NULL*]
                        //RRList2 = [Liverpool, Man U, *NULL*]

                        RRList1.RemoveAt(LastListIndex + 1);

                        //RRList1 = [DMU, Man City, Cabbage Family]
                        //RRList2 = [Liverpool, Man U, *NULL*]

                        //-----------------------ROUND 2------------------------
                        //RRList1 = [DMU, Man City, Cabbage Family]
                        //RRList2 = [Liverpool, Man U, *NULL*]

                        //-----------------------ROUND 3------------------------
                        //RRList1 = [DMU, Liverpool, Man City]
                        //RRList2 = [Man U, *NULL*, Cabbage Family]

                        //-----------------------ROUND 4------------------------
                        //RRList1 = [DMU, Man U, Liverpool]
                        //RRList2 = [*NULL*, Cabbage Family, Man City]

                        //-----------------------ROUND 5------------------------
                        //RRList1 = [DMU, *NULL*, Man U]
                        //RRList2 = [Cabbage Family, Man City, Liverpool]

                    }
                    tournament.TournamentStarted = true;
                    db.SaveChanges();
                }
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
