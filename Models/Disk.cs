namespace MusicLibrary.Models
{
    public class Disk
    {
        public int Id { get; set; } // первичный ключ
        public string? Name { get; set; } // название диска
        public DateTime Date { get; set; } // дата поступления
        public bool Available { get; set; } // наличие
    }
}
