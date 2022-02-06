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
    public class ProjectStatusRepository : IProjectStatusRepository
    {
        private readonly ApplicationContext context;

        public ProjectStatusRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public IQueryable<ProjectStatusShortView> GetShortEntities()
        {
            var result = this.context.ProjectStatuses
                .AsNoTracking()
                .OrderBy(e => e.Title)
                .Select(e => e.ToShortView());
            return result;
        }

        public IQueryable<ProjectStatusEditable> GetTableEntities()
        {
            var result = this.context.ProjectStatuses
                .AsNoTracking()
                .OrderBy(e => e.Title)
                .Select(e => e.ToEditable());
            return result;
        }

        public ProjectStatusEditable GetTemplate()
        {
            var result = new ProjectStatusEditable() 
            {
                Id = Guid.Empty,
                Title = "статус",
                Description = "введите описание для статуса проекта"
            };
            return result;
        }

        public ProjectStatusEditable GetEntity(Guid guid)
        {
            var result = this.context.ProjectStatuses
                .AsNoTracking()
                .Search(guid)
                .ToEditable();
            return result;
        }

        public void AddEntity(ProjectStatusEditable entity)
        {
            var dbProjectStatus = new ProjectStatus(entity);
            if (this.context.ProjectStatuses.IsContained(dbProjectStatus)) 
            {
                throw new ArgumentException($"Сущность \"{entity}\" уже содержится в БД");   
            }
            this.context.ProjectStatuses.Add(dbProjectStatus);
            this.context.SaveChanges();
        }

        public void UpdateEntity(ProjectStatusEditable entity)
        {
            var dbProjectStatus = this.context.ProjectStatuses
                .Search(entity.Id);
            dbProjectStatus.Update(entity);
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid)
        {
            throw new NotImplementedException("функционал по удалению статусов проектов (БФПО) на данный момент не доступен");
        }
    }
}
