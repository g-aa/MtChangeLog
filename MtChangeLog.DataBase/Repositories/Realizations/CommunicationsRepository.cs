using MtChangeLog.DataBase.Contexts;
using MtChangeLog.DataBase.Entities;
using MtChangeLog.DataBase.Repositories.Interfaces;

using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views.Shorts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Repositories.Realizations
{
    public class CommunicationsRepository : BaseRepository, ICommunicationsRepository
    {
        public CommunicationsRepository(ApplicationContext context) : base(context) 
        {
            
        }

        public IEnumerable<CommunicationShortView> GetShortEntities() 
        {
            var result = this.context.Communications.Select(e => e.ToShortView());
            return result;
        }

        public IEnumerable<CommunicationEditable> GetTableEntities() 
        {
            var result = this.context.Communications.Select(e => e.ToEditable());
            return result;
        }

        public CommunicationEditable GetTemplate()
        {
            var template = new CommunicationEditable() 
            {
                Id = Guid.Empty,
                Protocols = "modbus-mt",
                Description = "введите описание протокола"
            };
            return template;
        }

        public CommunicationEditable GetEntity(Guid guid) 
        {
            var dbCommunication =  this.GetDbCommunication(guid);
            return dbCommunication.ToEditable();
        }

        public void AddEntity(CommunicationEditable entity) 
        {
            var dbCommunication = new DbCommunication(entity);
            if (this.context.Communications.FirstOrDefault(e => e.Equals(dbCommunication)) != null) 
            {
                throw new ArgumentException($"ArmEdit {entity} is contained in database");
            }
            this.context.Communications.Add(dbCommunication);
            this.context.SaveChanges();
        }

        public void UpdateEntity(CommunicationEditable entity) 
        {
            var dbCommunication = this.GetDbCommunication(entity.Id);
            if (dbCommunication.Default)
            {
                throw new ArgumentException($"Default entity {entity} can not by update");
            }
            dbCommunication.Update(entity);
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid) 
        {
            throw new NotImplementedException("функционал не поддерживается");
            //DbCommunication dbCommunication = this.GetDbCommunication(guid);
            //this.context.Communications.Remove(dbCommunication);
            //this.context.SaveChanges();
        }
    }
}
