using Microsoft.EntityFrameworkCore;
using MtChangeLog.DataBase.Entities.Tables;
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

            var protocol = new DbProtocol() 
            {
                Title = "ModBus-MT",
                Description = "протокол по умолчанию применяемый в блоках БМРЗ",
                Default = true,
            };
            var com = new DbCommunicationModule()
            {
                Title = "АК",
                Description = "тип коммуникационного модуля по умолчанию",
                Default = true
            };
            protocol.CommunicationModules.Add(com);
            com.Protocols.Add(protocol);

            this.Protocols.Add(protocol);
            this.CommunicationModules.Add(com);

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

            var status = new DbProjectStatus() 
            {
                Title = "внутренне",
                Description = "проект для внутреннего использования в НТЦ Механотроника",
                Default = true
            };
            this.ProjectStatuses.Add(status);

            this.Database.ExecuteSqlRaw(
                @"CREATE VIEW LastProjectsRevision AS
                SELECT  pr.Id AS Id,
		                am.Title AS AnalogModule,
		                pv.Title AS Title,
		                pv.Version AS Version,
                        Max(pr.Revision) AS Revision,
					    p.Title AS Platform,
		                arm.Version AS ArmEdit,
                        pr.Date 
                FROM ProjectRevision pr
                JOIN ArmEdit arm
                ON arm.Id = pr.ArmEditId
                JOIN ProjectVersion pv
                ON pv.Id = pr.ProjectVersionId
                JOIN AnalogModule am
                ON am.Id = pv.AnalogModuleId
                JOIN Platform p
			    ON pv.PlatformId = p.Id
			    GROUP BY pr.ProjectVersionId");

            this.SaveChanges();
        }
    }
}
