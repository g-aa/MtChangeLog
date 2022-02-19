using Microsoft.EntityFrameworkCore;
using MtChangeLog.Abstractions.Extensions;
using MtChangeLog.Abstractions.Repositories;
using MtChangeLog.Context.Realizations;
using MtChangeLog.Entities.Builders.Tables;
using MtChangeLog.Entities.Extensions.Tables;
using MtChangeLog.Entities.Tables;
using MtChangeLog.TransferObjects.Editable;
using MtChangeLog.TransferObjects.Views.Shorts;
using MtChangeLog.TransferObjects.Views.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Repositories.Realizations
{
    public class CommunicationModulesRepository : ICommunicationModulesRepository
    {
        private readonly ApplicationContext context;

        public CommunicationModulesRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public IQueryable<CommunicationModuleShortView> GetShortEntities() 
        {
            var result = this.context.CommunicationModules
                .AsNoTracking()
                .Where(e => e.Protocols.Any())
                .OrderBy(e => e.Title)
                .Select(e => e.ToShortView());
            return result;
        }

        public IQueryable<CommunicationModuleTableView> GetTableEntities() 
        {
            var result = this.context.CommunicationModules
                .AsNoTracking()
                .Include(e => e.Protocols)
                .OrderBy(e=> e.Title)
                .Select(e => e.ToTableView());
            return result;
        }

        public CommunicationModuleEditable GetTemplate()
        {
            var protocols = this.context.Protocols
                .AsNoTracking()
                .Where(e => e.Default)
                .Select(e => e.ToShortView());
            var result = new CommunicationModuleEditable()
            {
                Id = Guid.Empty,
                Title = "АК",
                Description = "введите описание коммуникационного модуля",
                Protocols = protocols
            };
            return result;
        }

        public CommunicationModuleEditable GetEntity(Guid guid) 
        {
            var result =  this.context.CommunicationModules
                .AsNoTracking()
                .Include(e => e.Protocols)
                .Search(guid)
                .ToEditable();
            return result;
        }

        public void AddEntity(CommunicationModuleEditable entity) 
        {
            var dbProtocols = this.context.Protocols
                .SearchManyOrDefault(entity.Protocols.Select(e => e.Id));
            var dbModule = CommunicationModuleBuilder.GetBuilder()
                .SetAttributes(entity)
                .SetProtocols(dbProtocols)
                .Build();
            if (this.context.CommunicationModules.IsContained(dbModule)) 
            {
                throw new ArgumentException($"Сущность \"{entity}\" уже содержится в БД");
            }
            this.context.CommunicationModules.Add(dbModule);
            this.context.SaveChanges();
        }

        public void UpdateEntity(CommunicationModuleEditable entity) 
        {
            var dbModule = this.context.CommunicationModules
                .Include(e => e.Protocols)
                .Search(entity.Id);
            if (dbModule.Default)
            {
                throw new ArgumentException($"Сущность по умолчанию \"{entity}\" не может быть обновлена");
            }
            var dbProtocols = this.context.Protocols
                .SearchManyOrDefault(entity.Protocols.Select(e => e.Id));
            dbModule.GetBuilder()
                .SetAttributes(entity)
                .SetProtocols(dbProtocols)
                .Build();
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid) 
        {
            var dbRemovable = this.context.CommunicationModules
                .Include(e => e.ProjectRevisions)
                .Include(e => e.Protocols).ThenInclude(e => e.CommunicationModules)
                .AsSingleQuery()
                .Search(guid);
            if (dbRemovable.Default)
            {
                throw new ArgumentException($"Сущность по умолчанию \"{dbRemovable}\" не может быть удалена из БД");
            }
            if (dbRemovable.ProjectRevisions.Any()) 
            {
                throw new ArgumentException($"Сущность \"{dbRemovable}\" используемая в редакциях БФПО не может быть удалена из БД");

            }
            if (dbRemovable.Protocols.Any()) 
            {
                var defModule = this.context.CommunicationModules.First(e => e.Default);
                foreach (var dbProtocols in dbRemovable.Protocols)
                {
                    dbProtocols.CommunicationModules.Remove(dbRemovable);
                    if (!dbProtocols.CommunicationModules.Any()) 
                    {
                        dbProtocols.CommunicationModules.Add(defModule);
                    }
                }
            }
            this.context.CommunicationModules.Remove(dbRemovable);
            this.context.SaveChanges();
        }
    }
}
