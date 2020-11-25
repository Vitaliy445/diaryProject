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
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(db.Workers.ToList());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        [HttpGet]
        public IActionResult AddWorker()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddWorker(string _FirstName, string _MiddlName, string _LastName, string _Email, int _Position_Id, double _HourlyPayment, string _Status)
        {
            db.Workers.AddRange(
             new Worker
             {
                 FirstName = _FirstName,
                 MiddlName = _MiddlName,
                 LastName = _LastName,
                 Email = _Email,
                 Position_Id = _Position_Id,
                 HourlyPayment = _HourlyPayment,
                 Status = _Status
             });
            db.SaveChanges();
            return View();
        }
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
        
    }
}

