using Microsoft.EntityFrameworkCore;
using MtChangeLog.Abstractions.Extensions;
using MtChangeLog.Abstractions.Repositories;
using MtChangeLog.Context.Realizations;
using MtChangeLog.Entities.Builders.Tables;
using MtChangeLog.Entities.Extensions.Tables;
using MtChangeLog.Entities.Tables;
using MtChangeLog.TransferObjects.Editable;
using MtChangeLog.TransferObjects.Views.Shorts;
using MtChangeLog.TransferObjects.Views.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Repositories.Realizations
{
    public class ProjectRevisionsRepository : IProjectRevisionsRepository
    {
        public ApplicationContext context;
        
        public ProjectRevisionsRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public IQueryable<ProjectRevisionShortView> GetShortEntities()
        {
            var result = this.context.ProjectRevisions
                .AsNoTracking()
                .Include(pr => pr.ProjectVersion.AnalogModule)
                .OrderBy(pr => pr.ProjectVersion.AnalogModule.Title)
                .ThenBy(pr => pr.ProjectVersion.Title)
                .ThenBy(pr => pr.ProjectVersion.Version)
                .ThenBy(pr => pr.Revision)
                .Select(pr => pr.ToShortView());
            return result;
        }

        public IQueryable<ProjectRevisionTableView> GetTableEntities()
        {
            var result = this.context.ProjectRevisions
                .AsNoTracking()
                .Include(pr => pr.ArmEdit)
                .Include(pr => pr.ProjectVersion.AnalogModule)
                .OrderByDescending(pr => pr.Date)
                .ThenByDescending(pr => pr.ArmEdit.Version)
                .Select(pr => pr.ToTableView());
            return result;
        }

        public ProjectRevisionEditable GetEntity(Guid guid)
        {
            var result = this.context.ProjectRevisions
                .Include(e => e.CommunicationModule)
                .Include(e => e.ArmEdit)
                .Include(e => e.Authors)
                .Include(e => e.RelayAlgorithms)
                .Include(e => e.ProjectVersion.AnalogModule)
                .Include(e => e.ProjectVersion.Platform)
                .Search(guid);
            if (result.ParentRevisionId != Guid.Empty)
            {
                this.context.ProjectRevisions
                    .Include(e => e.ProjectVersion.AnalogModule)
                    .Search(result.ParentRevisionId);
            }
            return result.ToEditable();
        }

        public ProjectRevisionEditable GetTemplate(Guid guid) 
        {
            var project = this.context.ProjectVersions
                .Include(e => e.AnalogModule)
                .Search(guid)
                .ToShortView();
            var lastRevision = this.context.ProjectRevisions
                .Include(e => e.CommunicationModule)
                .Include(e => e.Authors)
                .Include(e => e.RelayAlgorithms)
                .Where(e => e.ProjectVersionId == guid)
                .OrderByDescending(e => e.Revision)
                .FirstOrDefault();
            var armEdit = this.context.ArmEdits
                .OrderByDescending(e => e.Version)
                .FirstOrDefault()
                .ToShortView();
            var communications = lastRevision?.CommunicationModule.ToShortView();
            if (communications is null) 
            {
                communications = this.context.CommunicationModules
                    .OrderByDescending(e => e.Title)
                    .FirstOrDefault()
                    .ToShortView();
            }
            var revision = lastRevision is null ? "00" : (int.Parse(lastRevision.Revision) + 1).ToString("D2");
            var algorithms = lastRevision?.RelayAlgorithms
                .Select(e => e.ToShortView());
            var authors = lastRevision?.Authors
                .Select(e => e.ToShortView());
            var result = new ProjectRevisionEditable()
            {
                Id = Guid.Empty,
                ParentRevision = lastRevision?.ToShortView(),
                ProjectVersion = project,
                Revision = revision,
                ArmEdit = armEdit,
                CommunicationModule = communications,
                RelayAlgorithms = algorithms,
                Date = DateTime.Now,
                Authors = authors,
                Reason = "введите причину для новой редакции",
                Description = "укажите подробное описание для нового изменения"
            };
            return result;
        }

        public void AddEntity(ProjectRevisionEditable entity)
        {
            var dbParent = this.context.ProjectRevisions
                .SearchOrNull(entity.ParentRevision != null ? entity.ParentRevision.Id : Guid.Empty);
            var dbProjectVersion = this.context.ProjectVersions
                .Search(entity.ProjectVersion.Id);
            var dbArmEdit = this.context.ArmEdits
                .SearchOrDefault(entity.ArmEdit.Id);
            var dbAuthors = this.context.Authors
                .SearchManyOrDefault(entity.Authors.Select(e => e.Id));
            var dbModule = this.context.CommunicationModules
                .Search(entity.CommunicationModule.Id);
            var dbAlgorithms = this.context.RelayAlgorithms
                .SearchManyOrDefault(entity.RelayAlgorithms.Select(e => e.Id));
            var dbProjectRevision = ProjectRevisionBuilder.GetBuilder()
                .SetAttributes(entity)
                .SetParentRevision(dbParent)
                .SetProjectVersion(dbProjectVersion)
                .SetArmEdit(dbArmEdit)
                .SetAuthors(dbAuthors)
                .SetCommunication(dbModule)
                .SetAlgorithms(dbAlgorithms)
                .Build();
            if(this.context.ProjectRevisions.Include(e => e.ProjectVersion).IsContained(dbProjectRevision))
            {
                throw new ArgumentException($"Сущность \"{entity}\" уже содержится в БД");
            }
            this.context.ProjectRevisions.Add(dbProjectRevision);
            this.context.SaveChanges();
        }

        public void UpdateEntity(ProjectRevisionEditable entity)
        {
            var dbArmEdit = this.context.ArmEdits
                .SearchOrDefault(entity.ArmEdit.Id);
            var dbAuthors = this.context.Authors
                .SearchManyOrDefault(entity.Authors.Select(e => e.Id));
            var dbModule = this.context.CommunicationModules
                .Search(entity.CommunicationModule.Id);
            var dbParent = this.context.ProjectRevisions
               .SearchOrNull(entity.ParentRevision.Id);
            var dbAlgorithms = this.context.RelayAlgorithms
                .SearchManyOrDefault(entity.RelayAlgorithms.Select(e => e.Id));
            var dbProjectRevision = this.context.ProjectRevisions
                .Include(e => e.ProjectVersion)
                .Include(e => e.Authors)
                .Include(e => e.RelayAlgorithms)
                .Search(entity.Id)
                .GetBuilder()
                .SetAttributes(entity)
                .SetArmEdit(dbArmEdit)
                .SetCommunication(dbModule)
                .SetAuthors(dbAuthors)
                .SetAlgorithms(dbAlgorithms)
                .SetParentRevision(dbParent)
                .Build();
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid)
        {
            var dbRemovable = this.context.ProjectRevisions
                .Include(e => e.ProjectVersion)
                .Search(guid);
            throw new NotImplementedException($"Удаление редакций проекта \"{dbRemovable}\" не доступно, поддерживается только функционал по удалению версии проекта со всеми редакциями");
        }
    }
}
