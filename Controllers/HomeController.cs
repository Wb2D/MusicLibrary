using Microsoft.AspNetCore.Mvc;
using MusicLibrary.Models;
using System.Data.Entity;
using System.Diagnostics;

namespace MusicLibrary.Controllers
{
    public class HomeController : Controller
    {
        MLContext mlContext = new MLContext(); // создается контекст данных
        public ActionResult Index()
        {
            ViewBag.Disks = mlContext.Disks;
            return View(); // возвращаю представление
        }

        public ActionResult GroupsView()
        {
            ViewBag.Groups = mlContext.Groups;
            return View("\\Views\\GroupView\\GroupsView.cshtml");
        }

        public ActionResult SongsView()
        {
            // включаю информацию о группе
            ViewBag.Songs = mlContext.Songs!.Join(
                mlContext.Groups!,
                song => song.GroupId,
                group => group.Id,
                (song, group) => new { Song = song, GroupName = group.Name }
                );

            return View("\\Views\\SongView\\SongsView.cshtml");
        }

        public ActionResult GenresView()
        {
            ViewBag.Genres = mlContext.Genres;
            return View("\\Views\\GenreView\\GenresView.cshtml");
        }

        public ActionResult JournalView()
        {
            ViewBag.Journal = mlContext.Magazines!
                .Join(mlContext.Clients!,
                journal => journal.ClientId,
                client => client.Id,
                (journal, client) => new 
                { 
                    Journal = journal, 
                    ClientFirstName = client.FirstName, 
                    ClientLastName = client.LastName 
                })
                .Join(mlContext.Disks!,
                journalClient => journalClient.Journal.DiskId,
                disk => disk.Id,
                (journalClient, disk) => new 
                { 
                    Journal = journalClient.Journal, 
                    ClientFirstName = journalClient.ClientFirstName, 
                    ClientLastName = journalClient.ClientLastName, 
                    DiskName = disk.Name 
                });

            return View("\\Views\\JournalView\\JournalView.cshtml");
        }

        public ActionResult ClientsView()
        {
            ViewBag.Clients = mlContext.Clients;
            return View("\\Views\\ClientView\\ClientsView.cshtml");
        }

        // метод для отображения информации о диске
        public ActionResult DiscView(int id)
        {
            ViewBag.Disks = mlContext.Disks!.Find(id);
            // список Id песен, связанных с текущим диском
            var songsId = mlContext.SongsDisks!
                .Where(sd => sd.DiskId == id)
                .Select(sd => sd.SongId)
                .ToList();
            // список песен по найденным Id
            ViewBag.Songs = mlContext.Songs!
                .Where(song => songsId.Contains(song.Id))
                .ToList();
            return View("\\Views\\DiskView\\DiskView.cshtml");
        }

        public ActionResult SongView(int id)
        {
            Song song = mlContext.Songs!.Find(id);
            ViewBag.Song = song;
            ViewBag.group = mlContext.Groups!
                .First(g => g.Id == song.GroupId)
                .Name;
            ViewBag.Genre = mlContext.Genres!
                .First(g => g.Id == song.GenreId)
                .Name;
            return View("\\Views\\SongView\\SongView.cshtml");
        }

        public ActionResult GroupView(int id)
        {
            ViewBag.Group = mlContext.Groups!.Find(id);
            return View("\\Views\\GroupView\\GroupView.cshtml");
        }

        public ActionResult GenreView(int id)
        {
            ViewBag.Genre = mlContext.Genres!.Find(id);
            return View("\\Views\\GenreView\\GenreView.cshtml");
        }

        /*
        [HttpGet]
        public ActionResult Take(int id)
        {
            ViewBag.diskId = id;
            return View("~/Views/Home/DiscView.cshtml");
        }

        [HttpPost]
        public string Take(client client) 
        {
            // выполнить действие после взятия диска
            mlContext.SaveChanges(); // сохранение в бд всех изменений
            return "succes";
        }
        */
    }
}