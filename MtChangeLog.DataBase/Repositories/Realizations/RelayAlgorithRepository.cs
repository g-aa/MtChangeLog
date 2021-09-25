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
    public class RelayAlgorithRepository : BaseRepository, IRelayAlgorithRepository
    {
        public RelayAlgorithRepository(ApplicationContext context) : base(context) 
        {
            
        }

        public IEnumerable<RelayAlgorithmBase> GetEntities()
        {
            return this.context.RelayAlgorithms.Select(alg => alg.GetBase());
        }

        public RelayAlgorithmBase GetEntity(Guid guid)
        {
            var dbAlgorithm = this.GetDbRelayAlgorithm(guid);
            return dbAlgorithm.GetBase();
        }

        public void AddEntity(RelayAlgorithmBase entity)
        {
            if (this.context.RelayAlgorithms.AsEnumerable().FirstOrDefault(alg => alg.Equals(entity)) != null) 
            {
                throw new ArgumentException($"Relay algorithm {entity.ANSI} {entity.Title} is contained in database");
            }
            var dbAlgorithm = new DbRelayAlgorithm(entity);
            this.context.RelayAlgorithms.Add(dbAlgorithm);
            this.context.SaveChanges();
        }

        public void UpdateEntity(RelayAlgorithmBase entity)
        {
            var dbAlgorithm = this.GetDbRelayAlgorithm(entity.Id);
            dbAlgorithm.Update(entity);
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid) 
        {
            var dbAlgorithm = this.GetDbRelayAlgorithm(guid);
            this.context.RelayAlgorithms.Remove(dbAlgorithm);
            this.context.SaveChanges();
        }
    }
}
