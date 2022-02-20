using MtChangeLog.TransferObjects.Editable;
using MtChangeLog.TransferObjects.Views.Historical;
using MtChangeLog.TransferObjects.Views.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Abstractions.Services
{
    public interface IStatisticsService
    {
        /// <summary>
        /// получить актуальный ArmEdit
        /// </summary>
        /// <returns></returns>
        ArmEditEditable GetActualArmEdit(); 
        /// <summary>
        /// получить актуальную статистику по имеющимся проектам в БД
        /// </summary>
        /// <returns></returns>
        StatisticsView GetStatistics();
        /// <summary>
        /// получить перечень последних редакций проектов (БФПО)
        /// </summary>
        /// <returns></returns>
        IQueryable<LastProjectRevisionView> GetLastProjectRevisions();
        /// <summary>
        /// получить выборку в количестве n-штук последних измененных проектов
        /// </summary>
        /// <param name="count">количество элементов в выборке</param>
        /// <returns></returns>
        IQueryable<ProjectRevisionHistoryShortView> GetNLastModifiedProjects(ushort count);
        /// <summary>
        /// получить n-часто редактируемых проектов
        /// </summary>
        /// <param name="count">количество элементов в выборке</param>
        /// <returns></returns>
        IQueryable<ProjectRevisionHistoryShortView> GetNMostChangingProjects(ushort count);
        /// <summary>
        /// получить краткую статистику по вкладам авторов в проекты (БФПО)
        /// </summary>
        /// <returns></returns>
        IQueryable<AuthorContributionView> GetAuthorContributions();
        /// <summary>
        /// получить статистику по вкладам авторов в отдельные проекты (БФПО) 
        /// </summary>
        /// <returns></returns>
        IQueryable<AuthorProjectContributionView> GetAuthorProjectContributions();
    }
}
