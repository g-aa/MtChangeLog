﻿using Microsoft.EntityFrameworkCore;
using MtChangeLog.DataBase.Contexts;
using MtChangeLog.DataBase.Entities;
using MtChangeLog.DataBase.Repositories.Interfaces;
using MtChangeLog.DataObjects.Entities.Base;
using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Repositories.Realizations
{
    public class ProjectRevisionsRepository: BaseRepository, IProjectRevisionsRepository
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
                .Select(pr => pr.GetTableView());
        }

        public IEnumerable<ProjectRevisionShortView> GetShortEntities() 
        {
            return this.context.ProjectRevisions
                .Include(pr => pr.ProjectVersion)
                .ThenInclude(pr => pr.AnalogModule)
                .Select(pr => pr.GetShortView());
        }

        public ProjectRevisionEditable GetEntity(Guid guid)
        {
            throw new NotImplementedException();
        }

        public ProjectRevisionEditable GetByProjectVersionId(Guid guid) 
        {
            var project = this.GetDbProjectVersion(guid);
            var lastRevision = project.ProjectRevisions?.OrderBy(pr => pr.Revision).LastOrDefault();
            var armEdit = this.context.ArmEdits.OrderBy(arm => arm.Version).LastOrDefault();
            var communications = lastRevision is null ? this.context.Communications.OrderBy(c => c.Protocols).LastOrDefault() : lastRevision.Communication;
            var revision = lastRevision is null ? "00" : (int.Parse(lastRevision.Revision) + 1).ToString("D2");
            var algorithms = lastRevision?.RelayAlgorithms.Select(ra => ra.GetBase());
            var authors = lastRevision?.Authors.Select(a => a.GetBase());

            var result = new ProjectRevisionEditable()
            {
                Id = Guid.Empty,
                ParentRevision = lastRevision?.GetShortView(),
                ProjectVersion = project.GetView(),
                Revision = revision,
                ArmEdit = armEdit.GetBase(),
                Communication = communications.GetBase(),
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
            if (this.context.ProjectRevisions.AsParallel().AsEnumerable().FirstOrDefault(pr => pr.Equals(entity)) != null) 
            {
                throw new ArgumentException($"The revision {entity} is contained in the database");
            }
            var dbProjectRevision = new DbProjectRevision(entity)
            {
                ParentRevision = entity.ParentRevision != null ? this.GetDbProjectRevision(entity.ParentRevision.Id) : null,
                ProjectVersion = this.GetDbProjectVersion(entity.ProjectVersion.Id),
                ArmEdit = this.GetDbArmEdit(entity.ArmEdit.Id),
                Authors = this.GetDbAuthors(entity.Authors.Select(a => a.Id)),
                Communication = this.GetDbCommunication(entity.Communication.Id),
                RelayAlgorithms = this.GetDbRelayAlgorithms(entity.RelayAlgorithms.Select(ra => ra.Id)),
            };
            this.context.ProjectRevisions.Add(dbProjectRevision);
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid)
        {
            throw new NotImplementedException();
        }

        public void UpdateEntity(ProjectRevisionEditable entity)
        {
            throw new NotImplementedException();
        }
    }
}
