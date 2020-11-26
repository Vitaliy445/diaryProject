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
        
    }
}

