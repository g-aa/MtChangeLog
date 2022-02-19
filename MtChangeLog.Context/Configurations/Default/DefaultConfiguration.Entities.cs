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
                Id = Guid.Parse("3E4DF70F-63EC-4101-8119-762B32464A27"),
                Date = DateTime.MinValue,
                DIVG = "ДИВГ.55101-00",
                Version = "v0.00.00.00",
                Description = "ArmEdit по умолчанию",
                Default = true
            };
            context.ArmEdits.Add(armEdit);

            var author = new Author()
            {
                Id = Guid.Parse("3A90CF3A-B9E3-43F7-ABFD-0E4483A9FE55"),
                FirstName = "автор",
                LastName = " не известный",
                Position = "по умолчанию",
                Default = true
            };
            context.Authors.Add(author);

            var protocol = new Protocol()
            {
                Id = Guid.Parse("275EAE7E-797D-4EA0-B8DF-915E155FD117"),
                Title = "ModBus-MT",
                Description = "протокол по умолчанию применяемый в блоках БМРЗ",
                Default = true,
            };
            var communicationModule = new CommunicationModule()
            {
                Id = Guid.Parse("B7A2DA8E-2494-4C6C-BFB9-5CCA01E7EB1C"),
                Title = "АК",
                Description = "тип коммуникационного модуля по умолчанию",
                Default = true
            };
            protocol.CommunicationModules.Add(communicationModule);
            communicationModule.Protocols.Add(protocol);

            context.Protocols.Add(protocol);
            context.CommunicationModules.Add(communicationModule);

            var analogModule = new AnalogModule()
            {
                Id = Guid.Parse("3A90CF3A-B9E3-43F7-ABFD-0E4483A9FE55"),
                DIVG = "ДИВГ.00000-00",
                Title = "БМРЗ-000",
                Current = "0A",
                Description = "аналоговый модуль по умолчанию",
                Default = true
            };
            var platform = new Platform()
            {
                Id = Guid.Parse("2405C011-D0F1-4FBF-9D1C-32E814DE7087"),
                Title = "БМРЗ-000",
                Description = "платформа по умолчанию",
                Default = true
            };
            analogModule.Platforms.Add(platform);
            platform.AnalogModules.Add(analogModule);

            context.AnalogModules.Add(analogModule);
            context.Platforms.Add(platform);

            var projectStatus = new ProjectStatus()
            {
                Id = Guid.Parse("6C19D2AD-B68F-4F30-A3C8-5E89263B5067"),
                Title = "внутренне",
                Description = "проект для внутреннего использования в НТЦ Механотроника",
                Default = true
            };
            context.ProjectStatuses.Add(projectStatus);

            var relayAlgorithms = new RelayAlgorithm[]
            {
                new RelayAlgorithm()
                {
                    Id = Guid.Parse("D2D7B8D8-6AEF-4D1C-A56D-99117B7040D6"),
                    Group = "МТ",
                    ANSI = "-//-",
                    Title = "Самодиагностика БМРЗ",
                    LogicalNode = "-//-",
                    Description = "алгоритм по умолчанию",
                    Default = true
                },
                new RelayAlgorithm()
                {
                    Id = Guid.Parse("5CFC833D-EFCA-40AA-8652-E7CC9B51610F"),
                    Group = "МТ",
                    ANSI = "-//-",
                    Title = "Вызов",
                    LogicalNode = "-//-",
                    Description = "алгоритм по умолчанию, вызывная сигнализация",
                    Default = true
                },
                new RelayAlgorithm()
                {
                    Id = Guid.Parse("6417F76B-F1C7-4BFF-A550-5A0863F84B06"),
                    Group = "МТ",
                    ANSI = "-//-",
                    Title = "Осциллограф",
                    LogicalNode = "-//-",
                    Description = "алгоритм по умолчанию",
                    Default = true
                },
                new RelayAlgorithm()
                {
                    Id = Guid.Parse("3942C6E4-7BE6-4B45-9897-20C655D5FE22"),
                    Group = "МТ",
                    ANSI = "-//-",
                    Title = "Векторная диаграмма",
                    LogicalNode = "-//-",
                    Description = "алгоритм по умолчанию",
                    Default = true
                },
                new RelayAlgorithm()
                {
                    Id = Guid.Parse("71416188-61BE-42FC-9142-4DC5D26CDFE4"),
                    Group = "МТ",
                    ANSI = "-//-",
                    Title = "Диагностика по пос. сост.",
                    LogicalNode = "-//-",
                    Description = "алгоритм по умолчанию, алгоритм диагностики аналогового модуля по постоянной составляющей сигнала",
                    Default = true
                },
                new RelayAlgorithm()
                {
                    Id = Guid.Parse("31BCC150-731F-4109-81D0-42F837D2929D"),
                    Group = "МТ",
                    ANSI = "-//-",
                    Title = "Фазировка",
                    LogicalNode = "-//-",
                    Description = "алгоритм по умолчанию, алгоритм диагностики аналогового модуля по постоянной составляющей сигнала",
                    Default = true
                },
                new RelayAlgorithm()
                {
                    Id = Guid.Parse("E655F180-FAC1-47CC-A4E6-A72AA3C3D754"),
                    Group = "МТ",
                    ANSI = "-//-",
                    Title = "Пгр. смены уставок",
                    LogicalNode = "-//-",
                    Description = "алгоритм по умолчанию, алгоритм смены программы уставок",
                    Default = true
                }
            };
            context.RelayAlgorithms.AddRange(relayAlgorithms);            
        }
    }
}
