using MtChangeLog.DataObjects.Entities.Views.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Repositories.Interfaces
{
    public interface IStatisticsRepository
    {
        StatisticsShortView GetShortStatistics();
        /// <summary>
        /// получить перечень наименований проектов
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetProjectTitles();
        /// <summary>
        /// получить полную историю конкретного проекта (БФПО)
        /// </summary>
        /// <param name="guid">project version id</param>
        /// <returns></returns>
        IEnumerable<ProjectHistoryView> GetProjectHistories(Guid guid);
        /// <summary>
        /// получить полное дерево проектов (БФПО) с конкретным наименованием
        /// </summary>
        /// <param name="projectTitle">project version title</param>
        /// <returns></returns>
        IEnumerable<ProjectRevisionTreeView> GetTreeEntities(string projectTitle);
    }
}
