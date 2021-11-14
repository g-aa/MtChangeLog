using MtChangeLog.DataBase.Contexts;
using MtChangeLog.DataBase.Entities;
using MtChangeLog.DataBase.Repositories.Interfaces;
using MtChangeLog.DataObjects.Entities.Base;

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

        public IEnumerable<CommunicationBase> GetEntities() 
        {
            return this.context.Communications.Select(com => com.GetBase());
        }

        public CommunicationBase GetEntity(Guid guid) 
        {
            var dbCommunication =  this.GetDbCommunication(guid);
            return dbCommunication.GetBase();
        }

        public void AddEntity(CommunicationBase entity) 
        {
            if (this.context.Communications.AsEnumerable().FirstOrDefault(com => com.Equals(entity)) != null) 
            {
                throw new ArgumentException($"ArmEdit {entity.Protocols} is contained in database");
            }
            var dbCommunication = new DbCommunication(entity);

            this.context.Communications.Add(dbCommunication);
            this.context.SaveChanges();
        }

        public void UpdateEntity(CommunicationBase entity) 
        {
            DbCommunication dbCommunication = this.GetDbCommunication(entity.Id);
            dbCommunication.Update(entity);
            // dbCommunication.ProjectRevisions - не должно обновляться !!!
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
