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
    public class ProjectStatusRepository : BaseRepository, IProjectStatusRepository
    {
        public ProjectStatusRepository(ApplicationContext context) : base(context) 
        {
            
        }

        public IQueryable<ProjectStatusShortView> GetShortEntities()
        {
            var result = this.context.ProjectStatuses
                .OrderBy(ps => ps.Title)
                .Select(ps => ps.ToShortView());
            return result;
        }

        public IQueryable<ProjectStatusEditable> GetTableEntities()
        {
            var result = this.context.ProjectStatuses
                .OrderBy(ps => ps.Title)
                .Select(ps => ps.ToEditable());
            return result;
        }

        public ProjectStatusEditable GetTemplate()
        {
            var template = new ProjectStatusEditable() 
            {
                Id = Guid.Empty,
                Title = "статус",
                Description = "введите описание для статуса проекта"
            };
            return template;
        }

        public ProjectStatusEditable GetEntity(Guid guid)
        {
            var dbProjectStatus = this.GetDbProjectStatus(guid);
            return dbProjectStatus.ToEditable();
        }

        public void AddEntity(ProjectStatusEditable entity)
        {
            var dbProjectStatus = new DbProjectStatus(entity);
            if (this.context.ProjectStatuses.FirstOrDefault(ps => ps.Equals(dbProjectStatus)) != null) 
            {
                throw new ArgumentException($"The project status {entity} is contained in the database");   
            }
            this.context.ProjectStatuses.Add(dbProjectStatus);
            this.context.SaveChanges();
        }

        public void UpdateEntity(ProjectStatusEditable entity)
        {
            var dbProjectStatus = this.GetDbProjectStatus(entity.Id);
            dbProjectStatus.Update(entity);
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid)
        {
            throw new NotImplementedException("функционал по удалению статусов проектов (БФПО) на данный момент не доступен");
        }
    }
}
