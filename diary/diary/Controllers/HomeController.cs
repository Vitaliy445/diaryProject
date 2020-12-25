using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using diary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace diary.Controllers
{
    public class HomeController : Controller
    {
        WorkContext db;

        public HomeController(WorkContext context)
        {
            db = context;
        }
        public IActionResult Start()
        {
            return View();
        }
 
        [Authorize(Roles = "admin, moderator, user")]
        public IActionResult Index()
        {
            ViewBag.Departments = db.Departments;
            ViewBag.Positions = db.Positions;
            return View(db.workers.ToList());
        }

        [Authorize(Roles = "admin, moderator")]
        [HttpGet]
        public IActionResult AddWorker()
        {
            ViewBag.Departments = db.Departments;
            ViewBag.Positions = db.Positions;
            return View();
        }
        [Authorize(Roles = "admin, moderator")]
        [HttpPost]
        public async Task<IActionResult> AddWorker(RegisterModel registerModel)
        {
            ViewBag.Departments = db.Departments;
            ViewBag.Positions = db.Positions;
            if (ModelState.IsValid)
            {
                Worker user = await db.workers.FirstOrDefaultAsync(u => u.Email == registerModel.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    user = new Worker {
                        FirstName = registerModel.FirstName,
                        MiddlName = registerModel.MiddlName,
                        LastName = registerModel.LastName,
                        Email = registerModel.Email,
                        Position_Id = registerModel.Position_Id,
                        Departament_Id = registerModel.Departament_Id,
                        HourlyPayment = registerModel.HourlyPayment,
                        Status = registerModel.Status,
                        Password = registerModel.Password
                    };
                    Role userRole = await db.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                    if (userRole != null)
                        user.Role = userRole;

                    db.workers.Add(user);
                    await db.SaveChangesAsync();


                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View();
        }
        [Authorize(Roles = "admin, moderator")]
        public IActionResult DeleteWorker(int? Id)
        {
            db.workers.Remove(db.workers.Find(Id));
            db.SaveChanges();
            return Redirect("/Home/Index");
        }

        [Authorize(Roles = "admin, moderator")]
        [HttpGet]
        public IActionResult Update(int? Id)
        {
            if (Id == null) return RedirectToAction("Index");
            ViewBag.WorkerId = Id;
            ViewBag.Departments = db.Departments;
            ViewBag.Positions = db.Positions;
            ViewBag.Role = db.Roles;
            return View(db.workers.ToList());
        }
        [Authorize(Roles = "admin, moderator")]
        [HttpPost]
        public IActionResult Update(Worker worker)
        {
            db.workers.Update(worker);

            db.SaveChanges();

            return Redirect("/Home/Index");
        }
        


        public void SendSalary(int id_user)
        {
            int month = DateTime.Now.Month - 1;
            if (month <= 0)
                month = 12;
            double salary = 0;
            Worker worker = new Worker();
            foreach (var w in db.workers)
            {
                if (id_user == w.Id)
                {
                    worker = w;
                    break;
                }
            }
            foreach (var e in db.Events)
            {
                if (e.Date.Month == month)
                {
                    foreach (var ew in db.Events_Workers)
                    {
                        if (e.Id == ew.Id)
                        {
                            if (id_user == ew.Worker_Id)
                            {
                                salary += ew.Hours * worker.HourlyPayment;
                            }
                        }
                    }
                }
            }
            worker.Money += salary;
            db.workers.Update(worker);
            db.SaveChanges();
        }
        [Authorize(Roles = "admin, moderator")]
        [HttpGet]
        public IActionResult Report()
        {
            return View(db.workers);
        }
        [Authorize(Roles = "admin, moderator")]
        [HttpPost]
        public IActionResult Report(DateTime date1, DateTime date2, int id_user)
        {
            double h = 0;
            double rez = 0;
            Worker worker = new Worker();
            foreach (var w in db.workers)
            {
                if (id_user == w.Id)
                {
                    worker = w;
                    break;
                }
            }
            foreach (var e in db.Events)
            {
                if (e.Date >= date1 && e.Date <= date2)
                {
                    foreach (var ew in db.Events_Workers)
                    {
                        if (ew.Event_Id == e.Id && ew.Worker_Id == id_user)
                        {
                            h += ew.Hours;
                            rez += ew.Hours * worker.HourlyPayment; 
                        }
                    }
                }
            }
            ViewBag.rez = rez;
            ViewBag.h = h;
            return View(db.workers);
        }
        [Authorize(Roles = "admin, moderator")]
        [HttpGet]
        public IActionResult AddEvent()
        {
            return View();
        }
        [Authorize(Roles = "admin, moderator")]
        [HttpPost]
        public IActionResult AddEvent(Event e)
        {
            db.Events.Add(e);
            db.SaveChanges();
            int rez = (from s in db.Events
                       orderby s.Id
                       select s).LastOrDefault().Id;

            return Redirect($"/Home/AddEventWorkers/{rez}");
        }
        [Authorize(Roles = "admin, moderator")]
        [HttpGet]
        public IActionResult AddEventWorkers(int? id)
        {
            ViewBag.id = id;
            ViewBag.event_workers = db.Events_Workers;
            return View(db.workers);
        }
        [Authorize(Roles = "admin, moderator")]
        [HttpPost]
        public IActionResult AddEventWorkers(int event_id,int id_user,int hours)
        {
            EventWorkers worker = new EventWorkers() { Event_Id = event_id, Worker_Id = id_user, Hours = hours };
            db.Events_Workers.Add(worker);
            db.SaveChanges();
            ViewBag.event_workers = db.Events_Workers;


            return Redirect($"/Home/AddEventWorkers/{event_id}");
        }
        public IActionResult Events()
        {
            return View(db.Events.ToList());
        }
        [Authorize(Roles = "admin")]
        public IActionResult RemoveEvent(int? id)
        {
            var _event = db.Events.Find((id));
            if (_event != null)
            {
                db.Events.Remove(_event);
                db.SaveChanges();
            }
            db.SaveChanges();
            return Redirect($"/Home/Events");
        }
        [Authorize(Roles = "admin, moderator")]
        public IActionResult RemoveEventWorker(int? id)
        {
            var worker = db.Events_Workers.Find((id));
            int event_id=0;
            if (worker != null)
            {
                event_id = worker.Event_Id;
                db.Events_Workers.Remove(worker);
                db.SaveChanges();
            }
            db.SaveChanges();
            return Redirect($"/Home/AddEventWorkers/{event_id}");
        }

    }
}