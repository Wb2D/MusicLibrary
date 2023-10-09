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
            context.Disks.Add(new Disk { Name = "High Voltage", 
                Date = new DateTime(2023, 10, 5),  Available = true});

            base.Seed(context);
        }
    }
}
