using Microsoft.EntityFrameworkCore;
using MtChangeLog.Abstractions.Extensions;
using MtChangeLog.Abstractions.Repositories;
using MtChangeLog.Context.Realizations;
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
                .SearchManyOrDefault(entity.Protocols.Select(e => e.Id))
                .ToHashSet();
            var dbCommunication = new CommunicationModule(entity)
            {
                Protocols = dbProtocols
            };
            if (this.context.CommunicationModules.IsContained(dbCommunication)) 
            {
                throw new ArgumentException($"Сущность \"{entity}\" уже содержится в БД");
            }
            this.context.CommunicationModules.Add(dbCommunication);
            this.context.SaveChanges();
        }

        public void UpdateEntity(CommunicationModuleEditable entity) 
        {
            var dbCommunication = this.context.CommunicationModules
                .Include(e => e.Protocols)
                .Search(entity.Id);
            var dbProtocols = this.context.Protocols
                .SearchManyOrDefault(entity.Protocols.Select(e => e.Id))
                .ToHashSet();
            dbCommunication.Update(entity, dbProtocols);
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid) 
        {
            throw new NotImplementedException("функционал по удалению коммуникационных модулей на данный момент не поддерживается");
        }
    }
}
