using Microsoft.EntityFrameworkCore;
using MtChangeLog.DataBase.Contexts;
using MtChangeLog.DataBase.Entities.Tables;
using MtChangeLog.DataBase.Repositories.Interfaces;
using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views.Shorts;
using MtChangeLog.DataObjects.Entities.Views.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Repositories.Realizations
{
    public class ProjectRevisionsRepository : BaseRepository, IProjectRevisionsRepository
    {
        public ProjectRevisionsRepository(ApplicationContext context) : base(context)
        {

        }

        public IEnumerable<ProjectRevisionTableView> GetTableEntities()
        {
            return this.context.ProjectRevisions
                .Include(pr => pr.ArmEdit)
                .Include(pr => pr.ProjectVersion).ThenInclude(pv => pv.AnalogModule)
                .OrderBy(pr => pr.Date).ThenBy(pr => pr.ArmEdit.Version)
                .Select(pr => pr.ToTableView());
        }

        public IEnumerable<ProjectRevisionShortView> GetShortEntities()
        {
            return this.context.ProjectRevisions
                .Include(pr => pr.ProjectVersion)
                .ThenInclude(pr => pr.AnalogModule)
                .Select(pr => pr.ToShortView());
        }

        public ProjectRevisionEditable GetEntity(Guid guid)
        {
            var dbProjectRevision = this.GetDbProjectRevision(guid);
            return dbProjectRevision.ToEditable();
        }

        public ProjectRevisionEditable GetTemplate(Guid guid) 
        {
            var project = this.GetDbProjectVersion(guid);
            var lastRevision = project.ProjectRevisions?.OrderBy(pr => pr.Revision).LastOrDefault();
            var armEdit = this.context.ArmEdits.OrderBy(arm => arm.Version).LastOrDefault();
            var communications = lastRevision is null ? this.context.Communications.OrderBy(c => c.Protocols).LastOrDefault() : lastRevision.Communication;
            var revision = lastRevision is null ? "00" : (int.Parse(lastRevision.Revision) + 1).ToString("D2");
            var algorithms = lastRevision?.RelayAlgorithms.Select(ra => ra.ToShortView());
            var authors = lastRevision?.Authors.Select(a => a.ToShortView());

            var result = new ProjectRevisionEditable()
            {
                Id = Guid.Empty,
                ParentRevision = lastRevision?.ToShortView(),
                ProjectVersion = project.ToShortView(),
                Revision = revision,
                ArmEdit = armEdit.ToShortView(),
                Communication = communications.ToShortView(),
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
            var dbProjectRevision = new DbProjectRevision(entity)
            {
                ParentRevision = entity.ParentRevision != null ? this.GetDbProjectRevision(entity.ParentRevision.Id) : null,
                ProjectVersion = this.GetDbProjectVersion(entity.ProjectVersion.Id),
                ArmEdit = this.GetDbArmEdit(entity.ArmEdit.Id),
                Authors = this.GetDbAuthorsOrDefault(entity.Authors.Select(a => a.Id)),
                Communication = this.GetDbCommunication(entity.Communication.Id),
                RelayAlgorithms = this.GetDbRelayAlgorithms(entity.RelayAlgorithms.Select(ra => ra.Id)),
            };
            if (this.context.ProjectRevisions.Include(pr=>pr.ProjectVersion).AsParallel().FirstOrDefault(pr => pr.Equals(dbProjectRevision)) != null) 
            {
                throw new ArgumentException($"The revision {entity} is contained in the database");
            }
            this.context.ProjectRevisions.Add(dbProjectRevision);
            this.context.SaveChanges();
        }

        public void UpdateEntity(ProjectRevisionEditable entity)
        {
            var dbProjectRevision = this.GetDbProjectRevision(entity.Id);
            dbProjectRevision.Update(entity, 
                this.GetDbArmEdit(entity.ArmEdit.Id), 
                this.GetDbCommunication(entity.Communication.Id), 
                this.GetDbAuthorsOrDefault(entity.Authors.Select(a => a.Id)), 
                this.GetDbRelayAlgorithms(entity.RelayAlgorithms.Select(ra => ra.Id)));
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid)
        {
            throw new NotImplementedException("функционал не поддерживается");
        }
    }
}
