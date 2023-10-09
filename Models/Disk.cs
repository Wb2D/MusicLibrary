namespace MusicLibrary.Models
{
    /*
    * Данный класс отображает отношение Disk со следующим набором атрибутов:
    * 1. Идентификатор;
    * 2. Название диска;
    * 3. Дата постулпние;
    * 4. Наличие.
    */
    public class Disk
    {
        public int Id { get; set; } // первичный ключ
        public string? Name { get; set; } // название диска
        public DateTime Date { get; set; } // дата поступления
        public bool Available { get; set; } // наличие
    }
}
