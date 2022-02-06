using Microsoft.EntityFrameworkCore;
using MtChangeLog.Abstractions.Extensions;
using MtChangeLog.Abstractions.Repositories;
using MtChangeLog.Context.Realizations;
using MtChangeLog.Entities.Tables;
using MtChangeLog.TransferObjects.Editable;
using MtChangeLog.TransferObjects.Views.Shorts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Repositories.Realizations
{
    public class ArmEditsRepositor : IArmEditsRepository
    {
        private readonly ApplicationContext context;

        public ArmEditsRepositor(ApplicationContext context)
        {
            this.context = context;
        }

        public IQueryable<ArmEditShortView> GetShortEntities() 
        {
            var result = this.context.ArmEdits
                .AsNoTracking()
                .OrderBy(e => e.Version)
                .Select(e => e.ToShortView());
            return result;
        }

        public IQueryable<ArmEditEditable> GetTableEntities() 
        {
            var result = this.context.ArmEdits
                .AsNoTracking()
                .OrderBy(e => e.Version)
                .Select(e => e.ToEditable());
            return result;
        }

        public ArmEditEditable GetTemplate() 
        {
            var result = new ArmEditEditable()
            {
                Id = Guid.Empty,
                Date = DateTime.Now,
                DIVG = "ДИВГ.55101-00",
                Version = "v0.00.00.00",
                Description = "введите описание для ArmEdit"
            };
            return result;
        }

        public ArmEditEditable GetEntity(Guid guid) 
        {
            var result = this.context.ArmEdits
                .AsNoTracking()
                .Search(guid)
                .ToEditable();
            return result;
        }

        public void AddEntity(ArmEditEditable entity)
        {
            var dbArmEdit = new ArmEdit(entity);
            if(this.context.ArmEdits.IsContained(dbArmEdit))
            {
                throw new ArgumentException($"Сущность \"{entity}\" уже содержится в БД");
            }
            this.context.ArmEdits.Add(dbArmEdit);
            this.context.SaveChanges();
        }

        public void UpdateEntity(ArmEditEditable entity)
        {
            var dbArmEdit = this.context.ArmEdits
                .Search(entity.Id);
            dbArmEdit.Update(entity);
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid)
        {
            //throw new NotImplementedException("функционал по удалению ArmEdit на данный момент не доступен");
            var dbArmEdit = this.context.ArmEdits
                .Include(e => e.ProjectRevisions)
                .Search(guid);
            if (dbArmEdit.Default) 
            {
                throw new ArgumentException($"Сущность по умолчанию \"{dbArmEdit}\" нельзя удалить из БД");
            }
            if (dbArmEdit.ProjectRevisions.Any()) 
            {
                throw new ArgumentException($"Сущность \"{dbArmEdit}\" используется в редакциях БФПО и неможет быть удалена из БД");    
            }
            this.context.ArmEdits.Remove(dbArmEdit);
            this.context.SaveChanges();
        }
    }
}
