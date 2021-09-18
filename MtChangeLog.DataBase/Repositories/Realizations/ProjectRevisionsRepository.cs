using MtChangeLog.DataBase.Contexts;
using MtChangeLog.DataBase.Entities;
using MtChangeLog.DataBase.Repositories.Interfaces;
using MtChangeLog.DataObjects.Entities.Base;
using MtChangeLog.DataObjects.Entities.Editable;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Repositories.Realizations
{
    public class ProjectRevisionsRepository: BaseRepository, IProjectRevisionsRepository
    {
        public ProjectRevisionsRepository(ApplicationContext context) : base(context) 
        {
            
        }

        public void AddEntity(ProjectRevisionEditable entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(Guid guid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProjectRevisionBase> GetEntities()
        {
            throw new NotImplementedException();
        }

        public ProjectRevisionEditable GetEntity(Guid guid)
        {
            throw new NotImplementedException();
        }

        public void UpdateEntity(ProjectRevisionEditable entity)
        {
            throw new NotImplementedException();
        }
    }
}
