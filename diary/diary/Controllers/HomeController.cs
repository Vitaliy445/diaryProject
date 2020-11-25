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
            return View(/*db.Animals.ToList()*/);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        /*
        [HttpGet]
        public IActionResult AddWorker()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddWorker(string _name, string _type, int _price, DateTime _date, int _age, string _gender, string _img)
        {
            db.Animals.AddRange(
             new Worker
             {
                 Name = _name,
                 Type = _type,
                 Price = _price,
                 BirthDay = _date,
                 Age = _age,
                 Gender = _gender,
                 Img = _img
             });
            db.SaveChanges();
            return View();
        }
        public IActionResult DeleteWorker(int? Id)
        {
            db.Animals.Remove(db.Animals.Find(Id));
            db.SaveChanges();
            return Redirect("/Home/Index");
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.AnimalId = id;
            return View();
        }

        [HttpPost]
        public IActionResult Update(Worker animal)
        {
            db.Animals.Update(animal);

            db.SaveChanges();

            return Redirect("/Home/Index");
        }
        */
    }
}

