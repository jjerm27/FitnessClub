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
        public ActionResult Index()
        {
            var trainings = db.Training.Include(t => t.Room).Include(t => t.Trainer);
            return View(trainings.ToList());
        }

        // GET: Trainings/Details/5
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

        // GET: Trainings/Create
        public ActionResult Create()
        {           

            ViewBag.RoomId = new SelectList(db.Room, "RoomId", "Name_Room");                       
            ViewBag.TrainerId = new SelectList(FindTrainers(), "Id", "Name");
            ViewBag.TimeOfTraining = new SelectList(GetTime());
            return View();
        }

        // POST: Trainings/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTraining,Training_Name,TrainerId,RoomId,TimeOfTraining")] Training training)
        {
            if (ModelState.IsValid)
            {
                db.Training.Add(training);
                //db.TrainingUsers.Add(new TrainingUsers { IdTraining = training.IdTraining, UserId = User.Identity.GetUserId() });
                db.SaveChanges();
                return RedirectToAction("Index","Номе");
            }

            ViewBag.RoomId = new SelectList(db.Room, "RoomId", "Name_Room", training.RoomId);
            ViewBag.TrainerId = new SelectList(FindTrainers(), "Id", "Name", training.TrainerId);
            ViewBag.TimeOfTraining = new SelectList(GetTime());
            return View(training);
        }

        // GET: Trainings/Edit/5
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
            return View(training);
        }

        // POST: Trainings/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTraining,Training_Name,TrainerId,RoomId,TimeStart,TimeStop")] Training training)
        {
            if (ModelState.IsValid)
            {
                db.Entry(training).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoomId = new SelectList(db.Room, "RoomId", "Name_Room", training.RoomId);
            ViewBag.TrainerId = new SelectList(FindTrainers(), "Id", "Name", training.TrainerId);
            return View(training);
        }

        // GET: Trainings/Delete/5
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

        // POST: Trainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Training training = db.Training.Find(id);
            db.Training.Remove(training);
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
