namespace MusicLibrary.Models
{
    /*
     * Данный класс отображает отношение Group со следующим набором атрибутов:
     * 1. Идентификатор;
     * 2. Название группы;
     * 3. Год основания.
    */
    public class Group
    {
        public int Id { get; set; } // первичный ключ
        public string? Name { get; set; } // название группы
        public int Year { get; set; } // год основания
    }
}
