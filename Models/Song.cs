namespace MusicLibrary.Models
{
    public class Song
    {
        public int Id { get; set; } // первичный ключ
        public string? Name { get; set; } // название песни
        public int GroupId { get; set;} // группа исполнитель
        public int Year { get; set; } // год релиза
        public int GenreId { get; set; } // жанр
    }
}
