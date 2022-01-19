using MtChangeLog.DataBase.Contexts;
using MtChangeLog.DataBase.Entities.Tables;
using MtChangeLog.DataBase.Repositories.Interfaces;
using MtChangeLog.DataBase.Repositories.Realizations.Base;
using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views.Shorts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Repositories.Realizations
{
    public class ProtocolsRepository : BaseRepository, IProtocolsRepository
    {
        public ProtocolsRepository(ApplicationContext context) : base(context) 
        {
            
        }

        public IQueryable<ProtocolShortView> GetShortEntities()
        {
            var result = this.context.Protocols
                .OrderBy(p => p.Title)
                .Select(p => p.ToShortView());
            return result;
        }

        public IQueryable<ProtocolEditable> GetTableEntities()
        {
            var result = this.context.Protocols
                .OrderBy(p => p.Title)
                .Select(p => p.ToEditable());
            return result;
        }

        public ProtocolEditable GetTemplate()
        {
            var communications = this.context.CommunicationModules.Where(e => e.Default).Select(e => e.ToShortView());
            var template = new ProtocolEditable()
            {
                Id = Guid.Empty,
                Title = "ModBus-MT",
                CommunicationModules = communications,
                Description = "введите описание для протокола информационного обмена"
            };
            return template;
        }

        public ProtocolEditable GetEntity(Guid guid)
        {
            var dbProtocol = this.GetDbProtocol(guid);
            return dbProtocol.ToEditable();
        }

        public void AddEntity(ProtocolEditable entity)
        {
            var dbProtocol = new DbProtocol(entity) 
            {
                CommunicationModules = this.GetDbCommunicationModulesOrDefault(entity.CommunicationModules.Select(e => e.Id))
            };
            if (this.SearchInDataBase(dbProtocol) != null) 
            {
                throw new ArgumentException($"The protocol {entity} is contained in the database");
            }
            this.context.Protocols.Add(dbProtocol);
            this.context.SaveChanges();
        }

        public void UpdateEntity(ProtocolEditable entity)
        {
            var dbProtocol = this.GetDbProtocol(entity.Id);
            var dbCommunications = this.GetDbCommunicationModulesOrDefault(entity.CommunicationModules.Select(e => e.Id));
            dbProtocol.Update(entity, dbCommunications);
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid)
        {
            throw new NotImplementedException("функционал по удалению протоколов инф. обмена на данный момент не доступен");
        }
    }
}
