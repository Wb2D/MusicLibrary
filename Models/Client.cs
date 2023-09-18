namespace MusicLibrary.Models
{
    public class Client
    {
        public int Id { get; set; } // первичный ключ
        public string? FirstName { get; set; } // имя
        public string? LastName { get; set; } // фамилия
        public string? Patronymic { get; set; } // отчество
        public string? Address { get; set; } // адрес
        public string? Passport { get; set; } // паспорт
    }
}