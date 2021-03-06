using MtChangeLog.TransferObjects.Views.Historical;
using MtChangeLog.TransferObjects.Views.Shorts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Abstractions.Services
{
    public interface IProjectHistoriesService
    {
        /// <summary>
        /// получить перечень проектов (БФПО) у которых имеютя редакции
        /// </summary>
        /// <returns></returns>
        IQueryable<ProjectVersionShortView> GetShortEntities();
        /// <summary>
        /// получить информацию о редакции проекта (БФПО)
        /// </summary>
        /// <param name="guid">project revision id</param>
        /// <returns></returns>
        ProjectRevisionHistoryView GetProjectRevisionHistory(Guid guid);
        /// <summary>
        /// получить полную историю конкретного проекта (БФПО)
        /// </summary>
        /// <param name="guid">project version id</param>
        /// <returns></returns>
        ProjectVersionHistoryView GetProjectVersionHistory(Guid guid);
    }
}
