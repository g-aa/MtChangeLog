﻿using MtChangeLog.DataObjects.Entities.Base;
using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Repositories.Interfaces
{
    public interface IProjectRevisionsRepository : ICRUD<ProjectRevisionEditable>
    {
        IEnumerable<ProjectRevisionShortView> GetShortEntities();
        IEnumerable<ProjectRevisionTableView> GetTableEntities();
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
        ProjectRevisionEditable GetByProjectVersionId(Guid guid);
    }
}
