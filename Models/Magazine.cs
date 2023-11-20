namespace MusicLibrary.Models
{
    /*
     * Данный класс отображает отношение Magazine со следующим набором атрибутов:
     * 1. Идентификатор
     * 2. Идентификатор клиента
     * 3. Идентификатор диска
     * 4. Дата получения диска
     * 5. Дата врзврата диска
     * Данное отношение содержит кортежи, хранящие информацию о выдаче клиентам дисков.
    */
    public class Magazine
    {
        public int Id { get; set; } // первичный ключ
        public int ClientId { get; set; } // клиент
        public int DiskId { get; set; } // диск
        public DateTime DateIssue { get; set; } // дата получения
        public DateTime DateReturn { get; set; } // дата возврата
    }
}
