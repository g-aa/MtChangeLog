using Microsoft.EntityFrameworkCore;
using MtChangeLog.Abstractions.Extensions;
using MtChangeLog.Abstractions.Services;
using MtChangeLog.Context.Realizations;
using MtChangeLog.Entities.Tables;
using MtChangeLog.TransferObjects.Views.Shorts;
using MtChangeLog.TransferObjects.Views.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Services.Realizations
{
    public class ProjectHistoriesService : IProjectHistoriesService
    {
        public readonly ApplicationContext context;

        public ProjectHistoriesService(ApplicationContext context) 
        {
            this.context = context;
        }

        public IQueryable<ProjectVersionShortView> GetShortEntities()
        {
            var result = this.context.LastProjectRevisions
                .OrderBy(e => e.AnalogModule)
                .ThenBy(e => e.Title)
                .ThenBy(e => e.Version)
                .Select(e => e.ToProjectVersionShortView());
            return result;
        }

        public ProjectRevisionHistoryView GetProjectRevisionHistory(Guid guid)
        {
            var result = this.GetProjectRevisionQuery()
                .Search(guid)
                .ToHistoryView();
            return result;
        }

        public ProjectVersionHistoryView GetProjectVersionHistory(Guid guid)
        {
            var result = new ProjectVersionHistoryView();
            var query = this.GetProjectRevisionQuery();
            var entity = query.Where(pr => pr.ProjectVersion.Id == guid)
                .OrderByDescending(pr => pr.Revision)
                .FirstOrDefault();
            if (entity != null)
            {
                result.Title = entity.ProjectVersion.ToShortView().ToString();
                do
                {
                    result.History.Add(entity.ToHistoryView());
                } while ((entity = query.FirstOrDefault(pr => pr.Id == entity.ParentRevisionId)) != null);
            }
            return result;
        }

        private IQueryable<ProjectRevision> GetProjectRevisionQuery()
        {
            var query = this.context.ProjectRevisions
                .AsNoTracking()
                .Include(e => e.ArmEdit)
                .Include(e => e.Authors)
                .Include(e => e.CommunicationModule.Protocols)
                .Include(e => e.ProjectVersion.AnalogModule)
                .Include(e => e.ProjectVersion.Platform)
                .Include(e => e.RelayAlgorithms);
            return query;
        }
    }
}
