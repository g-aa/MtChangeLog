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
        IEnumerable<ProjectHistoryView> GetProjectVersionHistory(Guid guid);
        /// <summary>
        /// получить полное дерево проектов (БФПО) с конкретным наименованием
        /// </summary>
        /// <param name="projectTitle">project version title</param>
        /// <returns></returns>
        IEnumerable<ProjectRevisionTreeView> GetTreeEntities(string projectTitle);
        /// <summary>
        /// получить последние изменения в проекте (БФПО)
        /// </summary>
        /// <param name="guid">project revision id</param>
        /// <returns></returns>
        ProjectHistoryView GetProjectRevisionHistory(Guid guid);
        /// <summary>
        /// получить выборку в количестве n-штук последних измененных проектов
        /// </summary>
        /// <param name="n">количество элементов в выборке</param>
        /// <returns></returns>
        IEnumerable<ProjectHistoryShortView> GetNLastModifiedProjects(ushort count);
        /// <summary>
        /// получить n-часто редактируемых проектов
        /// </summary>
        /// <param name="n">количество элементов в выборке</param>
        /// <returns></returns>
        IEnumerable<ProjectHistoryShortView> GetNMostChangingProjects(ushort count);
    }
}
