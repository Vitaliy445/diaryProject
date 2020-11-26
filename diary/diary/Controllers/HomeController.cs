using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using diary.Models;

namespace diary.Controllers
{
    public class HomeController : Controller
    {
        WorkContext db;

        public HomeController(WorkContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            SendSalary(1);
            return View(db.workers.ToList());
        }
        
        [HttpGet]
        public IActionResult AddWorker()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddWorker(string _FirstName, string _MiddleName, string _LastName, string _Email, int _Position_Id, int _Departament_id, float _HourlyPayment, string _Status, float _Money)
        {
            _Money = 0;
            db.workers.AddRange(
             new Worker
             {
                 FirstName = _FirstName,
                 MiddlName = _MiddleName,
                 LastName = _LastName,
                 Email = _Email,
                 Position_Id = _Position_Id,
                 Departament_Id = _Departament_id,
                 HourlyPayment = _HourlyPayment,
                 Status = _Status,
                 Money = _Money 
             }) ;
            db.SaveChanges();
            return View();
        }
        /*
        public IActionResult DeleteWorker(int? Id)
        {
            db.Workers.Remove(db.Workers.Find(Id));
            db.SaveChanges();
            return Redirect("/Home/Index");
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.WorkerId = id;
            return View();
        }

        [HttpPost]
        public IActionResult Update(Worker worker)
        {
            db.Workers.Update(worker);

            db.SaveChanges();

            return Redirect("/Home/Index");
        }
        */


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
            worker.Money = salary;
            db.workers.Update(worker);
            db.SaveChanges();
        }
        [HttpGet]
        public IActionResult Report()
        {
            return View(db.workers);
        }
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
            return View();
        }
        public IActionResult Report2()
        {
            return View();
        }
    }
}

