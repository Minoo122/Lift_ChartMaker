using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Lift_ChartMaker.Data;
using Lift_ChartMaker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Lift_ChartMaker.Calculate.Calculate;

namespace Lift_ChartMaker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;
        public HomeController(ApplicationDbContext _db)
        {
            db = _db;
        }
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            IList<Exercise> exercise = db.Exercise.Include(c => c.Scores).ToList();
            return View(exercise);
        }
        public IActionResult GetData(int x)
        {
            IList<Exercise> exercise = db.Exercise.Include(c => c.Scores).ToList();
            Exercise ex = exercise.Where(p => p.Id == x).FirstOrDefault();
            List<Scores> s = ex.Scores;
            List<double> volumes = CountVolume(s);
            for (int i = 0; i < s.Count; i++)
            {
                s[i].Weight = volumes[i]; // zamianka wagi na objętość żeby wykres narysować
            }
            var query = db.Exercise.Include(c => c.Scores).ToList().Where(p => p.Id == x);
            

            return Json(query);
        }
        public IActionResult Delete(int Id)
        {
            Scores sc = db.Scores.Where(s => s.Id == Id).FirstOrDefault();
            db.Scores.Remove(sc);
            db.SaveChanges();
            TempData["deleted"] = string.Format("Pomyślnie usunięto");
            return RedirectToAction("Index");
        }
        public IActionResult Graph(int Id)
        {
            IList<Exercise> exercise = db.Exercise.Include(c => c.Scores).ToList();
            Exercise ex = exercise.Where(p => p.Id == Id).FirstOrDefault();

            double maks = OneRepMax(ex.Scores);
            TempData["max"] = string.Format(maks.ToString("F2"));
            return View(ex);
        }
        public IActionResult Create(int id)
        {
            Exercise ex = db.Exercise.Where(p => p.Id==id).FirstOrDefault();
            return View(ex);
        }
        [HttpPost]
        public IActionResult Create(float weight, int reps, int Id)
        {
            Exercise ex = db.Exercise.Where(p => p.Id == Id).FirstOrDefault(); // żeby wiadomosc wyswietlic
            Scores s = new Scores { ExerciseId = Id, Reps = reps, Weight = weight };
            db.Scores.Add(s);
            db.SaveChanges();
            TempData["message"] = string.Format("Zapisano dla {0}", ex.Name);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
