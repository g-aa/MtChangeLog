using MtChangeLog.Entities.Builders.Tables;
using MtChangeLog.Entities.Tables;
using MtChangeLog.TransferObjects.Editable;
using MtChangeLog.TransferObjects.Views.Historical;
using MtChangeLog.TransferObjects.Views.Shorts;
using MtChangeLog.TransferObjects.Views.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Extensions.Tables
{
    public static class ProjectRevisionExtensions
    {
        public static ProjectRevisionShortView ToShortView(this ProjectRevision entity)
        {
            var result = new ProjectRevisionShortView()
            {
                Id = entity.Id,
                Prefix = entity.ProjectVersion.Prefix,
                Title = entity.ProjectVersion.Title,
                Version = entity.ProjectVersion.Version,
                Revision = entity.Revision
            };
            return result;
        }

        public static ProjectRevisionTableView ToTableView(this ProjectRevision entity)
        {
            var result = new ProjectRevisionTableView()
            {
                Id = entity.Id,
                Prefix = entity.ProjectVersion.Prefix,
                Title = entity.ProjectVersion.Title,
                Version = entity.ProjectVersion.Version,
                Revision = entity.Revision,
                Date = entity.Date,
                ArmEdit = entity.ArmEdit.Version,
                Reason = entity.Reason
            };
            return result;
        }

        public static ProjectRevisionTreeView ToTreeView(this ProjectRevision entity)
        {
            var result = new ProjectRevisionTreeView()
            {
                Id = entity.Id,
                ParentId = entity.ParentRevisionId,
                Prefix = entity.ProjectVersion.Prefix,
                Title = entity.ProjectVersion.Title,
                Version = entity.ProjectVersion.Version,
                Revision = entity.Revision,
                Date = entity.Date.ToString("yyyy-MM-dd"),
                ArmEdit = entity.ArmEdit.Version,
                Platform = entity.ProjectVersion.Platform.Title
            };
            return result;
        }

        public static ProjectRevisionHistoryShortView ToHistoryShortView(this ProjectRevision entity)
        {
            var result = new ProjectRevisionHistoryShortView()
            {
                Id = entity.Id,
                Date = entity.Date,
                Title = $"{entity.ProjectVersion.Prefix}-{entity.ProjectVersion.Title}-{entity.ProjectVersion.Version}_{entity.Revision}",
                Platform = entity.ProjectVersion.Platform.Title
            };
            return result;
        }

        public static ProjectRevisionHistoryView ToHistoryView(this ProjectRevision entity)
        {
            var result = new ProjectRevisionHistoryView()
            {
                Id = entity.Id,
                ArmEdit = entity.ArmEdit.Version,
                Authors = entity.Authors.Select(a => $"{a.FirstName} {a.LastName}"),
                RelayAlgorithms = entity.RelayAlgorithms.Select(ra => ra.Title),
                Communication = string.Join(", ", entity.CommunicationModule.Protocols.OrderBy(e => e.Title).Select(e => e.Title)),
                Date = entity.Date,
                Description = entity.Description,
                Platform = entity.ProjectVersion.Platform.Title,
                Reason = entity.Reason,
                Title = $"{entity.ProjectVersion.Prefix}-{entity.ProjectVersion.Title}-{entity.ProjectVersion.Version}_{entity.Revision}"
            };
            return result;
        }

        public static ProjectRevisionEditable ToEditable(this ProjectRevision entity)
        {
            var result = new ProjectRevisionEditable()
            {
                Id = entity.Id,
                Date = entity.Date,
                Revision = entity.Revision,
                Reason = entity.Reason,
                Description = entity.Description,
                ParentRevision = entity.ParentRevision?.ToShortView(),
                ProjectVersion = entity.ProjectVersion.ToShortView(),
                ArmEdit = entity.ArmEdit.ToShortView(),
                CommunicationModule = entity.CommunicationModule.ToShortView(),
                Authors = entity.Authors.Select(author => author.ToShortView()),
                RelayAlgorithms = entity.RelayAlgorithms.Select(alg => alg.ToShortView()),
            };
            return result;
        }

        public static ProjectRevisionBuilder GetBuilder(this ProjectRevision entity)
        {
            return new ProjectRevisionBuilder(entity);
        }
    }
}
