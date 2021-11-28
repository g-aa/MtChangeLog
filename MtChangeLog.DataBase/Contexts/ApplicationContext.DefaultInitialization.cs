using MtChangeLog.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Contexts
{
    public partial class ApplicationContext
    {
        private void Initialize()
        {
            var armEdit = new DbArmEdit()
            {
                Date = DateTime.MinValue,
                DIVG = "ДИВГ.55101-00",
                Version = "v0.00.00.00",
                Description = "ArmEdit по умолчанию",
                Default = true
            };
            this.ArmEdits.Add(armEdit);

            var author = new DbAuthor()
            {
                FirstName = "автор",
                LastName = " не известный",
                Position = "по умолчанию",
                Default = true
            };
            this.Authors.Add(author);

            var com = new DbCommunication()
            {
                Protocols = "Modbus-Mt",
                Description = "тип коммуникационного модуля по умолчанию",
                Default = true
            };
            this.Communications.Add(com);

            var module = new DbAnalogModule()
            {
                DIVG = "ДИВГ.00000-00",
                Title = "БМРЗ-000",
                Current = "0A",
                Description = "аналоговый модуль по умолчанию",
                Default = true
            };
            var platform = new DbPlatform()
            {
                Title = "БМРЗ-000",
                Description = "платформа по умолчанию",
                Default = true
            };
            module.Platforms.Add(platform);
            platform.AnalogModules.Add(module);

            this.AnalogModules.Add(module);
            this.Platforms.Add(platform);

            this.SaveChanges();
        }
    }
}
