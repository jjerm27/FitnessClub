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
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        RoleManager<IdentityRole> roleManager;
        ApplicationUserManager userManager;

        // GET: Admin
        public ActionResult Index()
        {
            var usersWithRoles = (from user in db.Users
                                  select new
                                  {
                                      user.Id,
                                      user.UserName,
                                      user.Name,
                                      user.Email,
                                      user.Photo,
                                      user.Sex,
                                      user.Specialize,
                                      user.Status,
                                      user.WorkingTimeId,
                                      user.PhoneNumber,
                                      user.Adress,
                                      user.BirthDay,
                                      user.Date_Of_Create,
                                      user.Filial_Id,
                                      user.IsActive,
                                      user.EmailConfirmed,
                                      user.PhoneNumberConfirmed,
                                      user.TwoFactorEnabled,
                                      user.LockoutEndDateUtc,
                                      user.LockoutEnabled,
                                      user.AccessFailedCount,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in db.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new UsersWidthRole()

                                  {
                                      Id = p.Id,
                                      UserName = p.UserName,
                                      Name = p.Name,
                                      Email = p.Email,
                                      EmailConfirmed = p.EmailConfirmed,
                                      Photo = p.Photo,
                                      Sex = p.Sex,
                                      Specialize = p.Specialize,
                                      Status = p.Status,
                                      WorkingTimeName = db.WorkingTimes.Find(p.WorkingTimeId).NameOfChema,
                                      PhoneNumber = p.PhoneNumber,
                                      PhoneNumberConfirmed = p.PhoneNumberConfirmed,
                                      Adress = p.Adress,
                                      BirthDay = p.BirthDay,
                                      Date_Of_Create = p.Date_Of_Create,
                                      Filial_Id = p.Filial_Id,
                                      IsActive = p.IsActive,
                                      TwoFactorEnabled = p.TwoFactorEnabled,
                                      LockoutEndDateUtc = p.LockoutEndDateUtc,
                                      LockoutEnabled = p.LockoutEnabled,
                                      AccessFailedCount = p.AccessFailedCount,

                                      Role = string.Join(" , ", p.RoleNames)
                                  });

         
            return View(usersWithRoles.ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(string id)
        {
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);

            

            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            else
            {                          
                var RoleNames = userManager.GetRoles(id);

                ViewBag.Roles = string.Join(" , ",RoleNames);               

                string work = db.WorkingTimes.Where(w => w.WorkingTimeId == applicationUser.WorkingTimeId).FirstOrDefault().From.ToShortTimeString() + " : "+ db.WorkingTimes.Where(w => w.WorkingTimeId == applicationUser.WorkingTimeId).FirstOrDefault().To.ToShortTimeString();

                ViewBag.WorkTimes = work;
            }

            return View(applicationUser);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            ViewBag.Roles = new SelectList(roleManager.Roles, "Id", "Name");
            ViewBag.WorkShema = new SelectList(db.WorkingTimes, "WorkingTimeId", "NameOfChema");
            return View();
        }

        // POST: Admin/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Sex,Specialize,Status,WorkingTimeId,Filial_Id,IsActive,BirthDay,Adress,Photo,Date_Of_Create,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser, string Role)
        {
            
            if (ModelState.IsValid)
            {
                userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
                roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                var addRole = roleManager.FindById(Role);
                string password = "Qwerty";                
                var result = userManager.Create(applicationUser, password);
                if (result.Succeeded)
                {
                    userManager.AddToRole(applicationUser.Id, addRole.Name);
                    userManager.AddToRole(applicationUser.Id, "dont_change");
                    userManager.AddToRole(applicationUser.Id, "user");
                }

                //    db.Users.Add(applicationUser);               
                //db.SaveChanges();
               
                return RedirectToAction("Index");
            }

            
            ViewBag.Roles = new SelectList(roleManager.Roles, "Id", "Name");
            ViewBag.WorkShema = new SelectList(db.WorkingTimes, "WorkingTimeId", "NameOfChema");


            return View(applicationUser);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(string id)
        {
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var dont_change = roleManager.FindByName("dont_change");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
           
            else
            {
                var RoleNames = (from userRole in applicationUser.Roles
                                 join role in db.Roles on userRole.RoleId
                                 equals role.Id
                                 select role).ToList();

                var RoleNamesStr = userManager.GetRoles(id);

                ViewBag.RolesNow = new SelectList(RoleNames, "Id", "Name", dont_change.Id);             

                ViewBag.Roles = string.Join(" , ", RoleNamesStr);
            }


           
            ViewBag.AllRoles = new SelectList(roleManager.Roles, "Id", "Name", dont_change.Id);

            ViewBag.WorkShema = new SelectList(db.WorkingTimes, "WorkingTimeId", "NameOfChema");

            return View(applicationUser);
        }

        // POST: Admin/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Sex,Specialize,Status,WorkingTimeId,Filial_Id,IsActive,BirthDay,Adress,Photo,Date_Of_Create,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser, string AddRole, string DelRole, string WorkSchema)
        {
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var dont_change = roleManager.FindByName("dont_change");
            var addRole = roleManager.FindById(AddRole);
            var delRole = roleManager.FindById(DelRole);

            if (ModelState.IsValid)
            {                            
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                if (AddRole != "" && AddRole != null && AddRole != dont_change.Id)
                    userManager.AddToRole(applicationUser.Id, addRole.Name);
                if (DelRole != "" && DelRole != null && DelRole != dont_change.Id)
                    userManager.RemoveFromRole(applicationUser.Id, delRole.Name);
                return RedirectToAction("Index");
            }
            var RoleNames = (from userRole in applicationUser.Roles
                             join role in db.Roles on userRole.RoleId
                             equals role.Id
                             select role);
            ViewBag.RolesNow = new SelectList(RoleNames.ToList(), "Id", "Name", dont_change.Id);
 
            ViewBag.AllRoles = new SelectList(roleManager.Roles, "Id", "Name", dont_change.Id);

            ViewBag.WorkShema = new SelectList(db.WorkingTimes, "WorkingTimeId", "NameOfChema");

            return View(applicationUser);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(string id)
        {
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            else
            {                
                var RoleNames = userManager.GetRoles(id);

                ViewBag.RolesNow = string.Join(" , ", RoleNames);
            }

            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            ViewBag.Roles = roleManager.Roles.ToList();

            return View(applicationUser);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
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
