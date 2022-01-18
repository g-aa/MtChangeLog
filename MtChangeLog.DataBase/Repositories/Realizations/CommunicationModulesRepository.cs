using Microsoft.EntityFrameworkCore;
using MtChangeLog.DataBase.Contexts;
using MtChangeLog.DataBase.Entities.Tables;
using MtChangeLog.DataBase.Repositories.Interfaces;
using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views.Shorts;
using MtChangeLog.DataObjects.Entities.Views.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Repositories.Realizations
{
    public class CommunicationModulesRepository : BaseRepository, ICommunicationModulesRepository
    {
        public CommunicationModulesRepository(ApplicationContext context) : base(context) 
        {
            
        }

        public IQueryable<CommunicationModuleShortView> GetShortEntities() 
        {
            var result = this.context.CommunicationModules
                .Where(e => e.Protocols.Any())
                .OrderBy(e => e.Title)
                .Select(e => e.ToShortView());
            return result;
        }

        public IQueryable<CommunicationModuleTableView> GetTableEntities() 
        {
            var result = this.context.CommunicationModules
                .Include(e => e.Protocols)
                .OrderBy(e=> e.Title)
                .Select(e => e.ToTableView());
            return result;
        }

        public CommunicationModuleEditable GetTemplate()
        {
            var protocols = this.context.Protocols.Where(e => e.Default)?.Select(e => e.ToShortView());
            var template = new CommunicationModuleEditable()
            {
                Id = Guid.Empty,
                Title = "АК",
                Description = "введите описание коммуникационного модуля",
                Protocols = protocols
            };
            return template;
        }

        public CommunicationModuleEditable GetEntity(Guid guid) 
        {
            var dbModule =  this.GetDbCommunication(guid);
            return dbModule.ToEditable();
        }

        public void AddEntity(CommunicationModuleEditable entity) 
        {
            var dbCommunication = new DbCommunicationModule(entity)
            {
                Protocols = this.GetDbProtocolsOrDefault(entity.Protocols.Select(e => e.Id))
            };
            if (this.context.CommunicationModules.FirstOrDefault(e => e.Equals(dbCommunication)) != null) 
            {
                throw new ArgumentException($"CommunicationModule {entity} is contained in database");
            }
            this.context.CommunicationModules.Add(dbCommunication);
            this.context.SaveChanges();
        }

        public void UpdateEntity(CommunicationModuleEditable entity) 
        {
            var dbCommunication = this.GetDbCommunication(entity.Id);
            dbCommunication.Update(entity, this.GetDbProtocolsOrDefault(entity.Protocols.Select(e => e.Id)));
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid) 
        {
            throw new NotImplementedException("функционал по удалению коммуникационных модулей на данный момент не поддерживается");
        }
    }
}
