using Microsoft.EntityFrameworkCore;
using MtChangeLog.Abstractions.Extensions;
using MtChangeLog.Abstractions.Services;
using MtChangeLog.Context.Realizations;
using MtChangeLog.Entities.Extensions.Tables;
using MtChangeLog.Entities.Extensions.Views;
using MtChangeLog.TransferObjects.Editable;
using MtChangeLog.TransferObjects.Views.Shorts;
using MtChangeLog.TransferObjects.Views.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Services.Realizations
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationContext context;
        
        public StatisticsService(ApplicationContext context)
        {
            this.context = context;
        }

        public ArmEditEditable GetActualArmEdit() 
        {
            var result = this.context.ArmEdits
                .AsNoTracking()
                .OrderByDescending(e => e.Version)
                .First()
                .ToEditable();
            return result;
        }

        public StatisticsView GetStatistics() 
        {
            ushort count = 10;
            var distributions = this.context.ProjectStatuses
                .AsNoTracking()
                .Include(e => e.ProjectVersions)
                .OrderByDescending(e => e.ProjectVersions.Count)
                .ToDictionary(k => k.Title, v => v.ProjectVersions.Count);
            var sArmEdit = this.context.ArmEdits
                .AsNoTracking()
                .OrderByDescending(e => e.Version)
                .First().Version;
            var lastModifiedProjects = this.GetNLastModifiedProjects(count).ToArray();
            var contributions = this.GetAuthorContributions().ToArray();
            var result = new StatisticsView()
            {
                Date = DateTime.Now,
                ArmEdit = sArmEdit,
                ProjectCount = distributions.Sum(e => e.Value),
                ProjectDistributions = distributions,
                AuthorContributions = contributions,
                LastModifiedProjects = lastModifiedProjects
            };
            return result;
        }

        public IQueryable<LastProjectRevisionView> GetLastProjectRevisions() 
        {
            var result = this.context.LastProjectRevisions
                .AsNoTracking()
                .OrderBy(e => e.Title)
                .ThenBy(e => e.Prefix)
                .ThenBy(e => e.Version)
                .Select(e => e.ToView());
            return result;
        }

        public IQueryable<ProjectRevisionHistoryShortView> GetNLastModifiedProjects(ushort count) 
        {
            var result = this.context.LastProjectRevisions
                .AsNoTracking()
                .OrderByDescending(e => e.Date)
                .Take(count)
                .Select(e => e.ToHistoryShortView());
            return result;
        }

        public IQueryable<ProjectRevisionHistoryShortView> GetNMostChangingProjects(ushort count) 
        {
            var result = this.context.LastProjectRevisions
                .AsNoTracking()
                .OrderByDescending(e => e.Revision)
                .ThenByDescending(e => e.Date)
                .Take(count)
                .Select(e => e.ToHistoryShortView());
            return result;
        }

        public IQueryable<AuthorContributionView> GetAuthorContributions()
        {
            var result = this.context.AuthorContributions
                .Select(e => e.ToView());
            return result;
        }

        public IQueryable<AuthorProjectContributionView> GetAuthorProjectContributions()
        {
            var result = this.context.AuthorProjectContributions
                .Select(e => e.ToView());
            return result;
        }
    }
}
