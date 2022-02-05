using MtChangeLog.Context.Realizations;
using MtChangeLog.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Context.Configurations.Default
{
    public static partial class DefaultConfiguration
    {
        private static void CreateDefaultEntities(ApplicationContext context)
        {
            var armEdit = new ArmEdit()
            {
                Date = DateTime.MinValue,
                DIVG = "ДИВГ.55101-00",
                Version = "v0.00.00.00",
                Description = "ArmEdit по умолчанию",
                Default = true
            };
            context.ArmEdits.Add(armEdit);

            var author = new Author()
            {
                FirstName = "автор",
                LastName = " не известный",
                Position = "по умолчанию",
                Default = true
            };
            context.Authors.Add(author);

            var protocol = new Protocol()
            {
                Title = "ModBus-MT",
                Description = "протокол по умолчанию применяемый в блоках БМРЗ",
                Default = true,
            };
            var com = new CommunicationModule()
            {
                Title = "АК",
                Description = "тип коммуникационного модуля по умолчанию",
                Default = true
            };
            protocol.CommunicationModules.Add(com);
            com.Protocols.Add(protocol);

            context.Protocols.Add(protocol);
            context.CommunicationModules.Add(com);

            var module = new AnalogModule()
            {
                DIVG = "ДИВГ.00000-00",
                Title = "БМРЗ-000",
                Current = "0A",
                Description = "аналоговый модуль по умолчанию",
                Default = true
            };
            var platform = new Platform()
            {
                Title = "БМРЗ-000",
                Description = "платформа по умолчанию",
                Default = true
            };
            module.Platforms.Add(platform);
            platform.AnalogModules.Add(module);

            context.AnalogModules.Add(module);
            context.Platforms.Add(platform);

            var status = new ProjectStatus()
            {
                Title = "внутренне",
                Description = "проект для внутреннего использования в НТЦ Механотроника",
                Default = true
            };
            context.ProjectStatuses.Add(status);

            var algorithm = new RelayAlgorithm()
            {
                Group = "МТ",
                ANSI = "-//-",
                Title = "Самодиагностика БМРЗ",
                LogicalNode = "-//-",
                Description = "алгоритм по умолчанию",
                Default = true
            };
            context.RelayAlgorithms.Add(algorithm);            
        }
    }
}
