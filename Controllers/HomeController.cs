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

        [HttpGet]
        public ActionResult Take(int id)
        {
            ViewBag.diskId = id;
            return View();
        }

        [HttpPost]
        public string Take(Client client) 
        {
            // выполнить действие после взятия диска
            mlContext.SaveChanges(); // сохранение в бд всех изменений
            return "succes";
        }
    }
}