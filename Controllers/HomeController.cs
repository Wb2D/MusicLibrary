using Microsoft.AspNetCore.Mvc;
using MusicLibrary.Models;
using System.Diagnostics;

namespace MusicLibrary.Controllers
{
    public class HomeController : Controller
    {
        MLContext mlContext = new MLContext(); // создается контекст данных
        public ActionResult Index()
        {
            IEnumerable<Disk>? disks = mlContext.Disks; // получение из бд всех объектов Disk
            ViewBag.Disks = disks; // Disks передаются в динамическое свойство Disks в ViewBag
            
            return View(); // возвращаю представление
        }

        // метод для отображения информации о диске
        public ActionResult DiscView(int id)
        {
            Disk disc = mlContext.Disks.Find(id);
            ViewBag.Disks = disc;
            return View();
        }

        [HttpGet]
        public ActionResult Take(int id)
        {
            ViewBag.diskId = id;
            return View();
        }

        [HttpPost]
        public string Take(client client) 
        {
            // выполнить действие после взятия диска
            mlContext.SaveChanges(); // сохранение в бд всех изменений
            return "succes";
        }
    }
}