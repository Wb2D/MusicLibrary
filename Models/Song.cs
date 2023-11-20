namespace MusicLibrary.Models
{
    /*
     * Данный класс отображает отношение Song со следующим набором атрибутов:
     * 1. Идентификатор
     * 2. Название песни
     * 3. Группа, исполнившая песню
     * 4. Год релиза песни
     * 5. Идентификатор жанра
    */
    public class Song
    {
        public int Id { get; set; } // первичный ключ
        public string? Name { get; set; } // название песни
        public int GroupId { get; set;} // группа
        public string? Duration { get; set; } // длительность
        public int GenreId { get; set; } // жанр
    }
}
