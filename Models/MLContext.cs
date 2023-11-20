using System.Data.Entity;

namespace MusicLibrary.Models
{
    /*
     * Данный класс является контекстом данных. Он используется 
     * EntityFramework для доступа к БД на основе некоторой модели. 
    */
    public class MLContext : DbContext
    {
        public DbSet<client>? Clients { get; set; }
        public DbSet<Disk>? Disks { get; set; }
        public DbSet<Genre>? Genres { get; set; }
        public DbSet<Group>? Groups { get; set; }
        public DbSet<Magazine>? Magazines { get; set; }
        public DbSet<Song>? Songs { get; set; }
        public DbSet<SongDisk>? SongsDisks { get; set; }
    }
}
