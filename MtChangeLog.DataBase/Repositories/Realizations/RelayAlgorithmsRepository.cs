using MtChangeLog.DataBase.Contexts;
using MtChangeLog.DataBase.Entities.Tables;
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
    public class RelayAlgorithmsRepository : BaseRepository, IRelayAlgorithmsRepository
    {
        public RelayAlgorithmsRepository(ApplicationContext context) : base(context) 
        {
            
        }

        public IEnumerable<RelayAlgorithmShortView> GetShortEntities() 
        {
            var result = this.context.RelayAlgorithms.OrderBy(e => e.ANSI).Select(e => e.ToShortView());
            return result;
        }
        
        public IEnumerable<RelayAlgorithmEditable> GetTableEntities() 
        {
            var result = this.context.RelayAlgorithms.OrderBy(e => e.ANSI).Select(e => e.ToEditable());
            return result;
        }
        
        public RelayAlgorithmEditable GetTemplate() 
        {
            var template = new RelayAlgorithmEditable()
            {
                Id = Guid.Empty,
                Title = "наименование алгоритма РЗА",
                ANSI = "код ANSI",
                LogicalNode = "логический узел в МЭК-61850",
                Description = "шаблон функции релейной защиты"
            };
            return template;
        }

        public RelayAlgorithmEditable GetEntity(Guid guid)
        {
            var dbAlgorithm = this.GetDbRelayAlgorithm(guid);
            return dbAlgorithm.ToEditable();
        }

        public void AddEntity(RelayAlgorithmEditable entity)
        {
            var dbAlgorithm = new DbRelayAlgorithm(entity);
            if (this.context.RelayAlgorithms.FirstOrDefault(e => e.Equals(dbAlgorithm)) != null) 
            {
                throw new ArgumentException($"Relay algorithm {entity} is contained in database");
            }
            this.context.RelayAlgorithms.Add(dbAlgorithm);
            this.context.SaveChanges();
        }

        public void UpdateEntity(RelayAlgorithmEditable entity)
        {
            var dbAlgorithm = this.GetDbRelayAlgorithm(entity.Id);
            dbAlgorithm.Update(entity);
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid) 
        {
            throw new NotImplementedException("функционал не поддерживается");
            //var dbAlgorithm = this.GetDbRelayAlgorithm(guid);
            //this.context.RelayAlgorithms.Remove(dbAlgorithm);
            //this.context.SaveChanges();
        }
    }
}
