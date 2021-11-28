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
    public class ArmEditsRepositor : BaseRepository, IArmEditsRepository
    {
        public ArmEditsRepositor(ApplicationContext context) : base(context) 
        {
            
        }

        public IEnumerable<ArmEditShortView> GetShortEntities() 
        {
            var result = this.context.ArmEdits.OrderBy(e => e.Version).Select(e => e.ToShortView());
            return result;
        }

        public IEnumerable<ArmEditEditable> GetTableEntities() 
        {
            var result = this.context.ArmEdits.OrderBy(e => e.Version).Select(e => e.ToEditable());
            return result;
        }

        public ArmEditEditable GetTemplate() 
        {
            var template = new ArmEditEditable()
            {
                Id = Guid.Empty,
                Date = DateTime.Now,
                DIVG = "ДИВГ.55101-00",
                Version = "v0.00.00.00",
                Description = "введите описание для ArmEdit"
            };
            return template;
        }

        public ArmEditEditable GetEntity(Guid guid) 
        {
            var dbArmEdit = this.GetDbArmEdit(guid);
            return dbArmEdit.ToEditable();
        }

        public void AddEntity(ArmEditEditable entity)
        {
            var dbArmEdit = new DbArmEdit(entity);
            if (this.context.ArmEdits.FirstOrDefault(e => e.Equals(dbArmEdit)) != null)
            {
                throw new ArgumentException($"ArmEdit {entity.DIVG} {entity.Version} is contained in database");
            }
            this.context.ArmEdits.Add(dbArmEdit);
            this.context.SaveChanges();
        }

        public void UpdateEntity(ArmEditEditable entity)
        {
            DbArmEdit dbArmEdit = this.GetDbArmEdit(entity.Id);
            if (dbArmEdit.Default)
            {
                throw new ArgumentException($"default entity {entity} can not by update");
            }
            dbArmEdit.Update(entity);
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid)
        {
            throw new NotImplementedException("функционал не поддерживается");
            //DbArmEdit dbArmEdit = this.GetDbArmEdit(guid);
            //this.context.ArmEdits.Remove(dbArmEdit);
            //this.context.SaveChanges();
        }
    }
}
