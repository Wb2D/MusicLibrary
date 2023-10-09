namespace MusicLibrary.Models
{
    /*
     * Данный класс отображает отношение Client со следующим набором атрибутов:
     * 1. Идентификатор;
     * 2. Фамилия;
     * 3. Имя;
     * 4. Отчество;
     * 5. Адрес проживания;
     * 6. Паспорт.
     */
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