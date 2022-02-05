using MtChangeLog.TransferObjects.Editable;
using MtChangeLog.TransferObjects.Views.Shorts;
using MtChangeLog.TransferObjects.Views.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Abstractions.Repositories
{
    public interface IStatisticsRepository
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
        /// <param name="n">количество элементов в выборке</param>
        /// <returns></returns>
        IQueryable<ProjectRevisionHistoryShortView> GetNLastModifiedProjects(ushort count);
        /// <summary>
        /// получить n-часто редактируемых проектов
        /// </summary>
        /// <param name="n">количество элементов в выборке</param>
        /// <returns></returns>
        IQueryable<ProjectRevisionHistoryShortView> GetNMostChangingProjects(ushort count);
    }
}
