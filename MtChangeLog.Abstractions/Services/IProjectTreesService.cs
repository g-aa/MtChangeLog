using MtChangeLog.TransferObjects.Views.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Abstractions.Services
{
    public interface IProjectTreesService
    {
        /// <summary>
        /// получить перечень наименований проектов (БФПО)
        /// </summary>
        /// <returns></returns>
        IQueryable<string> GetProjectTitles();
        /// <summary>
        /// получить полное дерево проектов (БФПО) с конкретным наименованием
        /// </summary>
        /// <param name="title">project version title</param>
        /// <returns></returns>
        IQueryable<ProjectRevisionTreeView> GetTreeEntities(string title);
    }
}
