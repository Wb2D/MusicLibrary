namespace MusicLibrary.Models
{
    /*
     * Данный класс отображает отношение Genre со следующим набором атрибутов:
     * 1. Идентификатор;
     * 2. Название жанра;
     * 3. Описание жанра.
    */
    public class Genre
    {
        public int Id { get; set; } // первичный ключ
        public string? Name { get; set; } // название жанра
        public string? Description { get; set; } // описание жанра
    }
}
