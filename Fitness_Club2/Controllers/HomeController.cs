using Fitness_Club2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fitness_Club2.Controllers
{
    [AllowAnonymous]   
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));


            #region
            {

                //// создаем роли
                var role1 = new IdentityRole { Name = "admin" };
                var role2 = new IdentityRole { Name = "user" };
                var role3 = new IdentityRole { Name = "manager" };
                var role4 = new IdentityRole { Name = "trainer" };
                var role5 = new IdentityRole { Name = "dont_change" };

                //// добавляем роли в бд
                //roleManager.Create(role1);
                //roleManager.Create(role2);
                //roleManager.Create(role3);
                //roleManager.Create(role4);
                //roleManager.Create(role5);

                //Создаем филиал
                //var fil = new Filial { FilialName = "Житомир", FilialAdress = "Победы, 10" };
                //db.Filial.Add(fil);
                //db.SaveChanges();

                //Создаем комнату
                //var room = new Room { FilialId = 1, Name_Room = "Спортзал №1" };
                //db.Room.Add(room);
                //db.SaveChanges();

                //Создаем схемы расписания
                //WorkingTime admins = new WorkingTime { NameOfChema = "admin", WorkingPeriodMinutes = 240, From = new DateTime(2019, 1, 1, 9, 0, 0), To = new DateTime(2019, 1, 1, 18, 0, 0), RelaxPeriodMinutes = 60 };
                //db.WorkingTimes.Add(admins);
                //WorkingTime managers = new WorkingTime { NameOfChema = "manager", WorkingPeriodMinutes = 240, From = new DateTime(2019, 1, 1, 10, 0, 0), To = new DateTime(2019, 1, 1, 19, 0, 0), RelaxPeriodMinutes = 60 };
                //db.WorkingTimes.Add(managers);
                //WorkingTime trainers = new WorkingTime { NameOfChema = "trainer", WorkingPeriodMinutes = 60, From = new DateTime(2019, 1, 1, 10, 0, 0), To = new DateTime(2019, 1, 1, 20, 0, 0), RelaxPeriodMinutes = 30 };
                //db.WorkingTimes.Add(trainers);
                //WorkingTime users = new WorkingTime { NameOfChema = "user", WorkingPeriodMinutes = 0, From = new DateTime(1991, 1, 1, 10, 0, 0), To = new DateTime(2019, 1, 1, 0, 0, 0), RelaxPeriodMinutes = 30 };
                //db.WorkingTimes.Add(users);
                //db.SaveChangesAsync();

                // создаем пользователей
                //var admin = new ApplicationUser { Email = "admin@mail.ru", UserName = "admin@mail.ru", Name = "Иванов Иван Иванович", Adress = "B.Berdichevskaya, 1", BirthDay = new DateTime(1989, 2, 22), Date_Of_Create = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), Filial_Id = 1, IsActive = "1", PhoneNumber = "+380500990653", Sex = "male", WorkingTimeId = 4, Photo = "~/Content/Photos/img1.jpg" };
                //string password = "Qwerty";
                //var result = userManager.Create(admin, password);
                //if (result.Succeeded)
                //{
                //    userManager.AddToRole(admin.Id, role1.Name);
                //    userManager.AddToRole(admin.Id, role2.Name);
                //    userManager.AddToRole(admin.Id, role5.Name);
                //}

                //var user = new ApplicationUser { Email = "user1@mail.ru", UserName = "user1@mail.ru", Name = "Егоров Егор Иванович", Adress = "B.Berdichevskaya, 10", BirthDay = new DateTime(1989, 4, 22), Date_Of_Create = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), Filial_Id = 1, IsActive = "1", PhoneNumber = "+380500770615", WorkingTimeId = 7, Sex = "male", Photo = "~/Content/Photos/img2.jpg" };

                //result = userManager.Create(user, password);
                //if (result.Succeeded)
                //{
                //    userManager.AddToRole(user.Id, role2.Name);
                //    userManager.AddToRole(user.Id, role5.Name);
                //}


                //user = new ApplicationUser { Email = "user2@mail.ru", UserName = "user2@mail.ru", Name = "Васильева Альбина Петровна", Adress = "B.Berdichevskaya, 22", BirthDay = new DateTime(1980, 4, 22), Date_Of_Create = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), Filial_Id = 1, IsActive = "1", PhoneNumber = "+380990322524", WorkingTimeId = 7, Sex = "female", Photo = "~/Content/Photos/img3.jpg" };
                //result = userManager.Create(user, password);
                //if (result.Succeeded)
                //{
                //    userManager.AddToRole(user.Id, role2.Name);
                //    userManager.AddToRole(user.Id, role5.Name);
                //}

                //user = new ApplicationUser { Email = "manager1@mail.ru", UserName = "manager1@mail.ru", Name = "Кустов Артем Степанович", Adress = "B.Berdichevskaya, 48", BirthDay = new DateTime(1989, 8, 22), Date_Of_Create = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), Filial_Id = 1, IsActive = "1", PhoneNumber = "+380992362524", Sex = "male", Photo = "~/Content/Photos/img4.jpg", WorkingTimeId = 5 };

                //result = userManager.Create(user, password);
                //if (result.Succeeded)
                //{
                //    userManager.AddToRole(user.Id, role3.Name);
                //    userManager.AddToRole(user.Id, role5.Name);
                //}

                //user = new ApplicationUser { Email = "trainer1@mail.ru", UserName = "trainer1@mail.ru", Name = "Портнов Василий Петрович", Adress = "B.Berdichevskaya, 66", BirthDay = new DateTime(1991, 8, 22), Date_Of_Create = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), Filial_Id = 1, IsActive = "1", PhoneNumber = "+380504887879", Sex = "male", Photo = "~/Content/Photos/img5.jpg", WorkingTimeId = 6, Specialize = "индивидуальные тренировки", Status = "Главный тренер" };

                //result = userManager.Create(user, password);
                //if (result.Succeeded)
                //{
                //    userManager.AddToRole(user.Id, role4.Name);
                //    userManager.AddToRole(user.Id, role5.Name);
                //}
            }
            #endregion

            var currentUser = User.Identity.GetUserId();
            if(currentUser!=null)
                if (userManager.IsInRole(currentUser, "manager"))
                    return RedirectToAction("Manager");


            return View();
        }

        [Authorize(Roles ="manager")]
        public ActionResult Manager()
        {
            var dateOfTraining = DateTime.Now.ToShortDateString();
            var todayTrainings = db.Training.Where(t => t.dateOfTraining == dateOfTraining).ToList();

            return View(todayTrainings);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}