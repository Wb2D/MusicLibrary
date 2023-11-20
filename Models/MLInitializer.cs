using System.Data.Entity;

namespace MusicLibrary.Models
{
    /*
     * Класс MLInitializer позволяет при каждом новом запуске заполнять 
     * базу данных заново заданными начальными данными. Используя
     * метод db.nameField.Add(...), каждый такой объект добавляется в бд.
    */
    public class MLInitializer : DropCreateDatabaseAlways<MLContext>
    {
        protected override void Seed(MLContext context)
        {
            // инициализирую кортежи отношения Disk
            context.Disks.Add(new Disk
            {
                Name = "High Voltage",
                Date = new DateTime(1975, 2, 17),
                Available = true
            });
            context.Disks.Add(new Disk
            {
                Name = "T.N.T.",
                Date = new DateTime(1975, 12, 1),
                Available = true
            });
            context.Disks.Add(new Disk
            {
                Name = "Let There Be Rock",
                Date = new DateTime(1977, 3, 21),
                Available = true
            });
            context.Disks.Add(new Disk
            {
                Name = "Highway to Hell",
                Date = new DateTime(1979, 6, 27),
                Available = true
            });



            base.Seed(context);
        }
    }
}
