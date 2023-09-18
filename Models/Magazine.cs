namespace MusicLibrary.Models
{
    public class Magazine
    {
        public int Id { get; set; } // первичный ключ
        public int ClienId { get; set; } // клиент
        public int DiskId { get; set; } // диск
        public DateTime DateIssue { get; set; } // дата получения
        public DateTime DateReturn { get; set; } // дата возврата
    }
}
