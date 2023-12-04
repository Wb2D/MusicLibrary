using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [HttpGet]
        public ActionResult AddDisk()
        {
            //ViewBag.Songs = new SelectList(mlContext.Songs, "Id", "Name");
            ViewBag.Songs = mlContext.Songs;

            return View("\\Views\\DiskView\\DiskAddView.cshtml");
        }

        [HttpGet]
        public ActionResult AddGroup()
        {
            return View("\\Views\\GroupView\\GroupAddView.cshtml");
        }

        [HttpGet]
        public ActionResult AddSong()
        {
            ViewBag.Groups = mlContext.Groups;
            ViewBag.Genres = mlContext.Genres;

            return View("\\Views\\SongView\\SongAddView.cshtml");
        }

        [HttpGet]
        public ActionResult AddGenre()
        {
            return View("\\Views\\GenreView\\GenreAddView.cshtml");
        }

        [HttpGet]
        public ActionResult AddJournal()
        {
            ViewBag.Clients = mlContext.Clients;
            ViewBag.Disks = mlContext.Disks!.Where(d => d.Available == true).ToList();

            return View("\\Views\\JournalView\\JournalAddView.cshtml");
        }

        [HttpGet]
        public ActionResult AddClient()
        {
            return View("\\Views\\ClientView\\ClientAddView.cshtml");
        }

        [HttpPost]
        public ActionResult AddDisk(Disk disk, List<int> SelectedSongs)
        {
            // добавляю информацию о диске в контекст данных
            mlContext!.Disks!.Add(disk);
            mlContext.SaveChanges();
            // если песни выбраны, добавляю связи между диском и песнями
            if (SelectedSongs != null && SelectedSongs.Any())
            {
                foreach (var songId in SelectedSongs)
                {
                    // создаю объект SongDisk и добавляю его в контекст данных
                    var songDisk = new SongDisk { DiskId = disk.Id, SongId = songId };
                    mlContext.SongsDisks!.Add(songDisk);
                }
                mlContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddGroup(Group group, string returnUrl)
        {
            mlContext.Groups!.Add(group);
            mlContext.SaveChanges();

            return Redirect(returnUrl);
        }

        [HttpPost]
        public ActionResult AddSong(Song song, string returnUrl)
        {
            mlContext.Songs!.Add(song);
            mlContext.SaveChanges();

            return Redirect(returnUrl);
        }

        [HttpPost]
        public ActionResult AddGenre(Genre genre, string returnUrl)
        {
            mlContext.Genres!.Add(genre);
            mlContext.SaveChanges();

            return Redirect(returnUrl);
        }

        [HttpPost]
        public ActionResult AddJournal(Magazine journal, string returnUrl)
        {
            mlContext.Magazines!.Add(journal);
            int takenDisks = journal.DiskId;
            var takenDisk = mlContext.Disks!.FirstOrDefault(d => d.Id == takenDisks);
            takenDisk!.Available = false;
            mlContext.SaveChanges();

            return Redirect(returnUrl);
        }

        [HttpPost]
        public ActionResult AddClient(Client client, string returnUrl)
        {
            mlContext.Clients!.Add(client);
            mlContext.SaveChanges();

            return Redirect(returnUrl);
        }

        [HttpGet]
        public ActionResult DeleteDisk(int id)
        {
            Disk disk = mlContext.Disks!.FirstOrDefault(d => d.Id == id) ?? 
                throw new InvalidOperationException($"Disk with id {id} not found");

            return View("\\Views\\DiskView\\DeleteDiskView.cshtml");
        }

        [HttpGet]
        public ActionResult DeleteGroup(int id)
        {
            Group group = mlContext.Groups!.FirstOrDefault(g => g.Id == id) ??
                throw new InvalidOperationException($"Group with id {id} not found");

            return View("\\Views\\GroupView\\DeleteGroupView.cshtml");
        }

        [HttpGet]
        public ActionResult DeleteSong(int id)
        {
            Song song = mlContext.Songs!.FirstOrDefault(s => s.Id == id) ??
                throw new InvalidOperationException($"Song with id {id} not found");

            return View("\\Views\\SongView\\DeleteSongView.cshtml");
        }

        [HttpPost, ActionName("DeleteDisk")]
        public ActionResult DeleteDiskConfirmed(int id)
        {
            Disk disk = mlContext.Disks!.FirstOrDefault(d => d.Id == id) ?? 
                throw new InvalidOperationException($"Disk with id {id} not found");
            mlContext.Disks!.Remove(disk);
            mlContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("DeleteGroup")]
        public ActionResult DeleteGroupConfirmed(int id, string returnUrl)
        {
            Group group = mlContext.Groups!.FirstOrDefault(g => g.Id == id) ??
                throw new InvalidOperationException($"Group with id {id} not found");
            mlContext.Groups!.Remove(group);
            mlContext.SaveChanges();

            return Redirect(returnUrl);
        }

        [HttpPost, ActionName("DeleteSong")]
        public ActionResult DeleteSongConfirmed(int id, string returnUrl)
        {
            Song song = mlContext.Songs!.FirstOrDefault(s => s.Id == id) ??
                throw new InvalidOperationException($"Song with id {id} not found");
            mlContext.Songs!.Remove(song);
            mlContext.SaveChanges();

            return Redirect(returnUrl);
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