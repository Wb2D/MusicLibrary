namespace MusicLibrary.Models
{
    public class Group
    {
        public int Id { get; set; } // первичный ключ
        public string? Name { get; set; } // название группы
        public int Year { get; set; } // год основания
    }
}
