﻿using MtChangeLog.DataObjects.Entities.Views.Shorts;
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
        StatisticsView GetShortStatistics();
        /// <summary>
        /// получить перечень наименований проектов
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetProjectTitles();
        /// <summary>
        /// получить перечень версий проектов у которые имеют редакции
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProjectVersionShortView> GetShortEntities();
        /// <summary>
        /// получить краткое представление версии проекта
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        ProjectVersionShortView GetShortEntity(Guid guid);
        /// <summary>
        /// получить перечень последних редакций проектов (БФПО)
        /// </summary>
        /// <returns></returns>
        IEnumerable<LastProjectRevisionView> GetLastProjectRevisions();
        /// <summary>
        /// получить полное дерево проектов (БФПО) с конкретным наименованием
        /// </summary>
        /// <param name="projectTitle">project version title</param>
        /// <returns></returns>
        IEnumerable<ProjectRevisionTreeView> GetTreeEntities(string projectTitle);
        /// <summary>
        /// получить полную историю конкретного проекта (БФПО)
        /// </summary>
        /// <param name="guid">project version id</param>
        /// <returns></returns>
        ProjectVersionHistoryView GetProjectVersionHistory(Guid guid);
        /// <summary>
        /// получить последние изменения в проекте (БФПО)
        /// </summary>
        /// <param name="guid">project revision id</param>
        /// <returns></returns>
        ProjectRevisionHistoryView GetProjectRevisionHistory(Guid guid);
        /// <summary>
        /// получить выборку в количестве n-штук последних измененных проектов
        /// </summary>
        /// <param name="n">количество элементов в выборке</param>
        /// <returns></returns>
        IEnumerable<ProjectRevisionHistoryShortView> GetNLastModifiedProjects(ushort count);
        /// <summary>
        /// получить n-часто редактируемых проектов
        /// </summary>
        /// <param name="n">количество элементов в выборке</param>
        /// <returns></returns>
        IEnumerable<ProjectRevisionHistoryShortView> GetNMostChangingProjects(ushort count);
    }
}
