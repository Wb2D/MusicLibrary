namespace MusicLibrary.Models
{
    /*
     * Отношение многие-ко-многим допустимо в ИЛМ, но требует преобразования 
     * в РМД. Поэтому связь ММ, возникаю между сущностями Disk и Song,
     * по правилу 8 необходимо реализовать через промежуточное отношение
     * SongDisk. Оно имеет первичный ключ, состоящий из первичных ключей
     * связываемых элементов.
     */
    public class SongDisk
    {
        public int SongId { get; set; } // песня
        public int DiskId { get; set; } // диск
        public int Id { get; set; }
        public SongDisk()
        {
            Id = SongId + DiskId * 10;
        }
    }
}
