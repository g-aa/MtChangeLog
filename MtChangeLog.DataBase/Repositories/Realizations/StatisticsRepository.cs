using Microsoft.EntityFrameworkCore;
using MtChangeLog.DataBase.Contexts;
using MtChangeLog.DataBase.Entities;
using MtChangeLog.DataBase.Repositories.Interfaces;
using MtChangeLog.DataObjects.Entities.Views.Statistics;
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

        public StatisticsView GetShortStatistics() 
        {
            ushort count = 10;
            var distributions = this.context.ProjectStatuses
                .Include(e => e.ProjectVersions)
                .OrderByDescending(e => e.ProjectVersions.Count)
                .ToDictionary(k => k.Title, v => v.ProjectVersions.Count);
            var result = new StatisticsView()
            {
                Date = DateTime.Now,
                ArmEdit = this.context.ArmEdits.OrderByDescending(e => e.Version).FirstOrDefault()?.Version,
                ProjectCount = distributions.Sum(e => e.Value),
                ProjectDistributions = distributions,
                LastModifiedProjects = this.GetNLastModifiedProjects(count),
                MostChangingProjects = this.GetNMostChangingProjects(count)
            };
            return result;
        }

        public ProjectHistoryView GetProjectRevisionHistory(Guid guid)
        {
            var result = this.GetDbProjectRevision(guid);
            return result.ToHistoryView();
        }

        public IEnumerable<string> GetProjectTitles() 
        {
            var result = this.context.ProjectVersions.Select(pv => pv.Title).Distinct().OrderBy(s => s);
            return result;
        }

        public IEnumerable<ProjectHistoryView> GetProjectVersionHistory(Guid guid) 
        {
            var result = new List<ProjectHistoryView>();
            var query = this.context.ProjectRevisions
                .Include(pr => pr.ArmEdit)
                .Include(pr => pr.Authors)
                .Include(pr => pr.CommunicationModule.Protocols)
                .Include(pr => pr.ProjectVersion.Platform)
                .Include(pr => pr.ProjectVersion.AnalogModule)
                .Include(pr => pr.RelayAlgorithms);

            var entity = query.Where(pr => pr.ProjectVersion.Id == guid)
                .OrderByDescending(pr => pr.Revision)
                .FirstOrDefault();
            if (entity != null)
            {
                do
                {
                    result.Add(entity.ToHistoryView());
                } while ((entity = query.FirstOrDefault(pr => pr.Id == entity.ParentRevisionId)) != null);
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

        public IEnumerable<ProjectHistoryShortView> GetNLastModifiedProjects(ushort count) 
        {
            var result = this.context.LastProjectRevisions
                .OrderByDescending(e => e.Date)
                .Take(count)
                .Select(e => e.ToHistoryShortView());
            return result;
        }

        public IEnumerable<ProjectHistoryShortView> GetNMostChangingProjects(ushort count) 
        {
            var result = this.context.LastProjectRevisions
                .OrderByDescending(e => e.Revision).ThenByDescending(e => e.Date)
                .Take(count)
                .Select(e => e.ToHistoryShortView());
            return result;
        }
    }
}
