namespace MusicLibrary.Models
{
    public class Genre
    {
        public int Id { get; set; } // первичный ключ
        public string? Name { get; set; } // название жанра
        public string? Description { get; set; } // описание жанра
    }
}
