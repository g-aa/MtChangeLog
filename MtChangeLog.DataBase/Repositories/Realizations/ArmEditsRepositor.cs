﻿using MtChangeLog.DataBase.Contexts;
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
    public class ArmEditsRepositor : BaseRepository, IArmEditsRepository
    {
        public ArmEditsRepositor(ApplicationContext context) : base(context) 
        {
            
        }

        public IQueryable<ArmEditShortView> GetShortEntities() 
        {
            var result = this.context.ArmEdits.OrderBy(e => e.Version).Select(e => e.ToShortView());
            return result;
        }

        public IQueryable<ArmEditEditable> GetTableEntities() 
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
            if(this.SearchInDataBase(dbArmEdit) != null)
            {
                throw new ArgumentException($"ArmEdit {entity} is contained in database");
            }
            this.context.ArmEdits.Add(dbArmEdit);
            this.context.SaveChanges();
        }

        public void UpdateEntity(ArmEditEditable entity)
        {
            DbArmEdit dbArmEdit = this.GetDbArmEdit(entity.Id);
            dbArmEdit.Update(entity);
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid)
        {
            throw new NotImplementedException("функционал по удалению ArmEdit на данный момент не доступен");
        }
    }
}
