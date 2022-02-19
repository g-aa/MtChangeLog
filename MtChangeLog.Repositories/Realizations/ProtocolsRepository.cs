using Microsoft.EntityFrameworkCore;
using MtChangeLog.Abstractions.Extensions;
using MtChangeLog.Abstractions.Repositories;
using MtChangeLog.Context.Realizations;
using MtChangeLog.Entities.Builders.Tables;
using MtChangeLog.Entities.Extensions.Tables;
using MtChangeLog.Entities.Tables;
using MtChangeLog.TransferObjects.Editable;
using MtChangeLog.TransferObjects.Views.Shorts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Repositories.Realizations
{
    public class ProtocolsRepository : IProtocolsRepository
    {
        private readonly ApplicationContext context;

        public ProtocolsRepository(ApplicationContext context)  
        {
            this.context = context;
        }

        public IQueryable<ProtocolShortView> GetShortEntities()
        {
            var result = this.context.Protocols
                .AsNoTracking()
                .OrderBy(p => p.Title)
                .Select(p => p.ToShortView());
            return result;
        }

        public IQueryable<ProtocolEditable> GetTableEntities()
        {
            var result = this.context.Protocols
                .AsNoTracking()
                .OrderBy(p => p.Title)
                .Select(p => p.ToEditable());
            return result;
        }

        public ProtocolEditable GetTemplate()
        {
            var communications = this.context.CommunicationModules
                .AsNoTracking()
                .Where(e => e.Default)
                .Select(e => e.ToShortView());
            var result = new ProtocolEditable()
            {
                Id = Guid.Empty,
                Title = "ModBus-MT",
                Description = "введите описание для протокола информационного обмена",
                CommunicationModules = communications
            };
            return result;
        }

        public ProtocolEditable GetEntity(Guid guid)
        {
            var result = this.context.Protocols
                .AsNoTracking()
                .Include(e => e.CommunicationModules)
                .Search(guid)
                .ToEditable();
            return result;
        }

        public void AddEntity(ProtocolEditable entity)
        {
            var dbModules = this.context.CommunicationModules
                .SearchManyOrDefault(entity.CommunicationModules.Select(e => e.Id));
            var dbProtocol = ProtocolBuilder.GetBuilder()
                .SetAttributes(entity)
                .SetModules(dbModules)
                .Build();
            if (this.context.Protocols.IsContained(dbProtocol)) 
            {
                throw new ArgumentException($"Сущность \"{entity}\" уже содержится в БД");
            }
            this.context.Protocols.Add(dbProtocol);
            this.context.SaveChanges();
        }

        public void UpdateEntity(ProtocolEditable entity)
        {
            var dbProtocol = this.context.Protocols
                .Search(entity.Id);
            if (dbProtocol.Default)
            {
                throw new ArgumentException($"Сущность по умолчанию \"{entity}\" не может быть обновлена");
            }
            var dbModules = this.context.CommunicationModules
                .SearchManyOrDefault(entity.CommunicationModules.Select(e => e.Id));
            dbProtocol.GetBuilder()
                .SetAttributes(entity)
                .SetModules(dbModules)
                .Build();
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid)
        {
            var dbRemovable = this.context.Protocols
                .Include(e => e.CommunicationModules).ThenInclude(e => e.Protocols)
                .AsSingleQuery()
                .Search(guid);
            if (dbRemovable.Default)
            {
                throw new ArgumentException($"Сущность по умолчанию \"{dbRemovable}\" не может быть удалена из БД");
            }
            if (dbRemovable.CommunicationModules.Any()) 
            {
                var defProtocol = this.context.Protocols.First(e => e.Default);
                foreach (var dbModule in dbRemovable.CommunicationModules)
                {
                    dbModule.Protocols.Remove(dbRemovable);
                    if (!dbModule.Protocols.Any())
                    {
                        dbModule.Protocols.Add(defProtocol);
                    }
                }
            }
            this.context.Protocols.Remove(dbRemovable);
            this.context.SaveChanges();
        }
    }
}
