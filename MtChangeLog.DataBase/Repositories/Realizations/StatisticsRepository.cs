using Microsoft.EntityFrameworkCore;
using MtChangeLog.DataBase.Contexts;
using MtChangeLog.DataBase.Entities;
using MtChangeLog.DataBase.Repositories.Interfaces;
using MtChangeLog.DataObjects.Entities.Views.Statistics;
using MtChangeLog.DataObjects.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Repositories.Realizations
{
    public class StatisticsRepository : BaseRepository, IStatisticsRepository
    {
        public StatisticsRepository(ApplicationContext context) : base(context) 
        {
            
        }

        public StatisticsShortView GetShortStatistics() 
        {
            ushort count = 10;
            var result = new StatisticsShortView()
            {
                Date = DateTime.Now,
                ArmEdit = this.context.ArmEdits.OrderByDescending(e => e.Version).FirstOrDefault()?.Version,
                ProjectCount = this.context.ProjectVersions.Count(),
                ActualProjectCount = this.context.ProjectVersions.Where(e => e.Status == Status.Actual.ToString()).Count(),
                TestProjectCount = this.context.ProjectVersions.Where(e => e.Status == Status.Test.ToString()).Count(),
                DeprecatedProjectCount = this.context.ProjectVersions.Where(e => e.Status == Status.Deprecated.ToString()).Count(),
                LastModifiedProjects = this.GetNLastModifiedProjects(count),
                MostChangingProjects = this.GetNMostChangingProjects(count)
            };
            return result;
        }

        public IEnumerable<string> GetProjectTitles() 
        {
            var result = this.context.ProjectVersions.Select(pv => pv.Title).Distinct().OrderBy(s => s);
            return result;
        }

        public IEnumerable<ProjectHistoryView> GetProjectVersionHistory(Guid guid) 
        {
            // требуется оптимизировать логику !!!
            var result = new List<ProjectHistoryView>();
            var entity = this.context.ProjectRevisions
                .Include(pr => pr.ArmEdit)
                .Include(pr => pr.Authors)
                .Include(pr => pr.Communication)
                .Include(pr => pr.ProjectVersion.Platform)
                .Include(pr => pr.ProjectVersion.AnalogModule)
                .Include(pr => pr.RelayAlgorithms)
                .Where(pr => pr.ProjectVersion.Id == guid)
                .OrderByDescending(pr => pr.Revision)
                .FirstOrDefault();
            if (entity is not null)
            {
                result.Add(entity.ToHistoryView());
                while (entity.ParentRevisionId != Guid.Empty)
                {
                    entity = this.context.ProjectRevisions
                        .Include(pr => pr.ArmEdit)
                        .Include(pr => pr.Authors)
                        .Include(pr => pr.Communication)
                        .Include(pr => pr.ProjectVersion.Platform)
                        .Include(pr => pr.ProjectVersion.AnalogModule)
                        .Include(pr => pr.RelayAlgorithms)
                        .FirstOrDefault(pr => pr.Id == entity.ParentRevisionId);
                    result.Add(entity.ToHistoryView());
                }
            }
            return result;
        }

        public IEnumerable<ProjectRevisionTreeView> GetTreeEntities(string projectTitle) 
        {
            return this.context.ProjectRevisions
                .Include(pr => pr.ArmEdit)
                .Include(pr => pr.ProjectVersion).ThenInclude(pv => pv.AnalogModule)
                .Include(pr => pr.ProjectVersion).ThenInclude(pv => pv.Platform)
                .Where(pr => pr.ProjectVersion.Title == projectTitle)
                .Select(pr => pr.ToTreeView());
        }

        public ProjectHistoryView GetProjectRevisionHistory(Guid guid)
        {
            var result = this.GetDbProjectRevision(guid).ToHistoryView();
            return result;
        }

        public IEnumerable<ProjectHistoryShortView> GetNLastModifiedProjects(ushort count) 
        {
            var result = this.GetLastProjectsRevisionForHistory()
                .Include(pr => pr.ProjectVersion.AnalogModule)
                .Include(pr => pr.ProjectVersion.Platform)
                .OrderByDescending(pr => pr.Date)
                .Take(count)
                .Select(pr => pr.ToHistoryShortView());
            return result;
        }

        public IEnumerable<ProjectHistoryShortView> GetNMostChangingProjects(ushort count) 
        {
            var result = this.GetLastProjectsRevisionForHistory()
                .Include(pr => pr.ProjectVersion.AnalogModule)
                .Include(pr => pr.ProjectVersion.Platform)
                .OrderByDescending(pr => pr.Revision).ThenByDescending(pr => pr.Date)
                .Take(count)
                .Select(pr => pr.ToHistoryShortView());
            return result;
        }

        private IQueryable<DbProjectRevision> GetLastProjectsRevisionForHistory() 
        {
            return this.context.ProjectRevisions
                .FromSqlRaw(@"SELECT    Id,
                                        ProjectVersionId,
                                        ParentRevisionId,
                                        ArmEditId,
                                        CommunicationId,
                                        Date,
                                        Max(Revision) AS Revision,
                                        Reason,
                                        Description
                              FROM ProjectRevisions 
                              GROUP BY ProjectVersionId")
                .Include(pr => pr.ProjectVersion.AnalogModule)
                .Include(pr => pr.ProjectVersion.Platform);
        }
    }
}
