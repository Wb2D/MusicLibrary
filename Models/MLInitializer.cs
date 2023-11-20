using System.Data.Entity;
using Newtonsoft.Json;

namespace MusicLibrary.Models
{
    /*
     * Класс MLInitializer позволяет при каждом новом запуске заполнять 
     * базу данных заново заданными начальными данными. Используя
     * метод db.nameField.Add(...), каждый такой объект добавляется в бд.
    */
    public class MLInitializer : DropCreateDatabaseAlways<MLContext>
    {
        protected override void Seed(MLContext context)
        {
            List<Disk>? disks = ReadData<Disk>("Data\\disks.json");
            context.Disks!.AddRange(disks);

            List<Song>? songs = ReadData<Song>("Data\\songs.json");
            context.Songs!.AddRange(songs);

            List<SongDisk> songdisk = ReadData<SongDisk>("Data\\songsdisks.json");
            context.SongsDisks!.AddRange(songdisk);

            List<Group> groups = ReadData<Group>("Data\\groups.json");
            context.Groups!.AddRange(groups);

            List<Genre> genres = ReadData<Genre>("Data\\genres.json");
            context.Genres!.AddRange(genres);

            List<Client> clients = ReadData<Client>("Data\\clients.json");
            context.Clients!.AddRange(clients);

            List<Magazine> journal = ReadData<Magazine>("Data\\journal.json");
            context.Magazines!.AddRange(journal);

            base.Seed(context);
        }

        private static List<T> ReadData<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                // файл не существует
                throw new FileNotFoundException($"File not found: {filePath}");
            }

            string jsonData = File.ReadAllText(filePath);

            if (string.IsNullOrWhiteSpace(jsonData))
            {
                // файл пуст или содержит некорректные данные
                throw new InvalidOperationException($"File is empty or contains invalid data: {filePath}");
            }

            List<T>? data = JsonConvert.DeserializeObject<List<T>>(jsonData);

            return data ?? new List<T>();
        }
    }
}
