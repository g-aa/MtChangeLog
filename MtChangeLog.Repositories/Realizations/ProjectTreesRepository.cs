﻿using Microsoft.EntityFrameworkCore;
using MtChangeLog.Abstractions.Repositories;
using MtChangeLog.Context.Realizations;
using MtChangeLog.TransferObjects.Views.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Repositories.Realizations
{
    public class ProjectTreesRepository : IProjectTreesRepository
    {
        private readonly ApplicationContext context;
        
        public ProjectTreesRepository(ApplicationContext context) 
        {
            this.context = context;
        }
        
        public IQueryable<string> GetProjectTitles()
        {
            var result = this.context.ProjectVersions
                .AsNoTracking()
                .Select(e => e.Title)
                .Distinct()
                .OrderBy(e => e);
            return result;
        }

        public IQueryable<ProjectRevisionTreeView> GetTreeEntities(string title)
        {
            var result = this.context.ProjectRevisions
                .AsNoTracking()
                .Include(pr => pr.ArmEdit)
                .Include(pr => pr.ProjectVersion.AnalogModule)
                .Include(pr => pr.ProjectVersion.Platform)
                .Where(pr => pr.ProjectVersion.Title == title)
                .Select(pr => pr.ToTreeView());
            return result;
        }
    }
}