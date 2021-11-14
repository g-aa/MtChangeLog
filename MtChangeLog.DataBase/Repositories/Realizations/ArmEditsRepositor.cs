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
    public class ArmEditsRepositor : BaseRepository, IArmEditsRepository
    {
        public ArmEditsRepositor(ApplicationContext context) : base(context) 
        {
            
        }

        public IEnumerable<ArmEditBase> GetEntities() 
        {
            return this.context.ArmEdits.OrderBy(arm => arm.Version).Select(arm => arm.GetBase());
        }

        public ArmEditBase GetEntity(Guid guid) 
        {
            var dbArmEdit = this.GetDbArmEdit(guid);
            return dbArmEdit.GetBase();
        }

        public void AddEntity(ArmEditBase entity)
        {
            if (this.context.ArmEdits.AsEnumerable().FirstOrDefault(arm => arm.Equals(entity)) != null)
            {
                throw new ArgumentException($"ArmEdit {entity.DIVG} {entity.Version} is contained in database");
            }
            var dbArmEdit = new DbArmEdit(entity);
            this.context.ArmEdits.Add(dbArmEdit);
            this.context.SaveChanges();
        }

        public void UpdateEntity(ArmEditBase entity)
        {
            DbArmEdit dbArmEdit = this.GetDbArmEdit(entity.Id);
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
