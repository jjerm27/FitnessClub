using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fitness_Club2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Fitness_Club2.Controllers
{   
    public class TrainingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        ApplicationUserManager userManager; RoleManager<IdentityRole> roleManager;

        // GET: Trainings
        [Authorize(Roles = "manager,admin")]
        public ActionResult Index()
        {
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));

            var trainings = db.Training.Include(t => t.Room).Include(t => t.Trainer);

            var currentUser = User.Identity.GetUserId();
            if (currentUser != null)
            {
                if (userManager.IsInRole(currentUser, "user"))
                    return RedirectToAction("UserTrainings","Home");
            }

            return View(trainings.ToList());
        }

        // GET: Trainings/Details/5
        [Authorize(Roles = "user, admin, manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training training = db.Training.Find(id);
            if (training == null)
            {
                return HttpNotFound();
            }
            return View(training);
        }

        private List<ApplicationUser> FindTrainers()
        {
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            List<ApplicationUser> trainers = new List<ApplicationUser>();
            var userList = db.Users.ToList();
            foreach (var item in userList)
            {
                if (userManager.IsInRole(item.Id, "trainer")==true)
                    trainers.Add(item);
            }
            return trainers;
        }

        private List<string> GetTime()
        {
            List<string> time = new List<string>();
            string t = "";
            var workTrainer = db.WorkingTimes.Where(n => n.NameOfChema == "trainer").FirstOrDefault();
            for (DateTime i = workTrainer.From; i < workTrainer.To; )
            {
                t = i.ToShortTimeString();
                i = i.AddMinutes(workTrainer.WorkingPeriodMinutes);
                time.Add(t + " - " + i.ToShortTimeString());
                i = i.AddMinutes(workTrainer.RelaxPeriodMinutes);
            }

            return time;
        }

        private List<string> GetDate()
        {
            List<string> date = new List<string>();
            DateTime day = DateTime.Now;
            DateTime to = day.AddDays(5);
            for (DateTime i = day; i < to;)
            {               
                date.Add(i.ToShortDateString());
                i = i.AddDays(1);
            }
                
            return date;
        }

        // GET: Trainings/Create
        [Authorize(Roles = "user, admin, manager")]
        public ActionResult Create()
        {           

            ViewBag.RoomId = new SelectList(db.Room, "RoomId", "Name_Room");                       
            ViewBag.TrainerId = new SelectList(FindTrainers(), "Id", "Name");
            ViewBag.TimeOfTraining = new SelectList(GetTime());
            ViewBag.DateOfTraining = new SelectList(GetDate());
            return View();
        }

        // POST: Trainings/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "user, admin, manager")]
        public ActionResult Create([Bind(Include = "IdTraining,Training_Name,TrainerId,RoomId,TimeOfTraining, dateOfTraining")] Training training)
        {
            
            TrainingUsers user = null;
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            string currantUId = User.Identity.GetUserId();
            DateTime date = Convert.ToDateTime(training.dateOfTraining);
            

            if (ModelState.IsValid)
            {
                var existTrain = db.Training.Where(t => t.TimeOfTraining == training.TimeOfTraining && t.dateOfTraining== training.dateOfTraining).FirstOrDefault();

                
                if (existTrain != null)
                {
                    user = db.TrainingUsers.Where(t => t.IdTraining == existTrain.IdTraining).Where(u => u.UserId == currantUId).FirstOrDefault();
                    if (user != null)
                        return RedirectToAction("AlreadyExist");                                      
                    db.TrainingUsers.Add(new TrainingUsers { IdTraining = existTrain.IdTraining, UserId = currantUId });
                    db.SaveChanges();
                }
                else
                {
                    //training.date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    db.Training.Add(training);                   
                    db.TrainingUsers.Add(new TrainingUsers { IdTraining = training.IdTraining, UserId = currantUId });
                    db.SaveChanges();
                }

                if (userManager.IsInRole(currantUId, "user"))
                    return RedirectToAction("UserTrainings", "Home");
                else
                    return RedirectToAction("Index");
            }

            ViewBag.RoomId = new SelectList(db.Room, "RoomId", "Name_Room", training.RoomId);
            ViewBag.TrainerId = new SelectList(FindTrainers(), "Id", "Name", training.TrainerId);
            ViewBag.TimeOfTraining = new SelectList(GetTime());
            ViewBag.DateOfTraining = new SelectList(GetDate());
                                                         
            return View(training);
        }

        [Authorize(Roles = "user, admin, manager")]
        public ActionResult AlreadyExist()
        {
            return View();
        }

        // GET: Trainings/Edit/5
        [Authorize(Roles = "manager, admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training training = db.Training.Find(id);
            if (training == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomId = new SelectList(db.Room, "RoomId", "Name_Room", training.RoomId);
            ViewBag.TrainerId = new SelectList(FindTrainers(), "Id", "Name", training.TrainerId);
            ViewBag.TimeOfTraining = new SelectList(GetTime());
            ViewBag.DateOfTraining = new SelectList(GetDate());
            return View(training);
        }

        // POST: Trainings/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager, admin")]
        public ActionResult Edit([Bind(Include = "IdTraining,Training_Name,TrainerId,RoomId,TimeOfTraining,dateOfTraining")] Training training)
        {
            //string[] s = training.dateOfTraining.Split('.');           
            //DateTime date = new DateTime(Int32.Parse(s[2]), Int32.Parse(s[1]), Int32.Parse(s[0]));

            DateTime date2 = Convert.ToDateTime(training.dateOfTraining);

            if (ModelState.IsValid)
            {
                //training.date = date;

                //Training tr = db.Training.Where(t => t.IdTraining == training.IdTraining).FirstOrDefault();
                //if(tr!=null)
                //    tr.dateOfTraining = date2;


                db.Entry(training).State = EntityState.Modified;
                db.SaveChanges();


                
                //var tr = db.Training.Find(training.IdTraining);
                //tr.date = date;

                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoomId = new SelectList(db.Room, "RoomId", "Name_Room", training.RoomId);
            ViewBag.TrainerId = new SelectList(FindTrainers(), "Id", "Name", training.TrainerId);
            ViewBag.TimeOfTraining = new SelectList(GetTime());
            ViewBag.DateOfTraining = new SelectList(GetDate());
            return View(training);
        }

        //GET: Trainings/Delete/5
        [Authorize(Roles = "user, admin, manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Training training = db.Training.Find(id);
            if (training == null)
            {
                return HttpNotFound();
            }
            return View(training);
        }

        //POST: Trainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "user, admin, manager")]
        public ActionResult DeleteConfirmed(int id)
        {

            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));

            Training training = db.Training.Find(id);
            db.Training.Remove(training);
            db.SaveChanges();

            var currentUser = User.Identity.GetUserId();
            if (currentUser != null)
            {
                if (userManager.IsInRole(currentUser, "user"))
                    return RedirectToAction("UserTrainings", "Home");
            }
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
