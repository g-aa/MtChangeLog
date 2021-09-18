using MtChangeLog.DataObjects.Entities.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Entities
{
    internal class DbAuthor : AuthorBase
    {
        #region Relationships
        public ICollection<DbProjectRevision> ProjectRevisions { get; set; }
        #endregion

        public DbAuthor() : base() 
        {
            this.ProjectRevisions = new HashSet<DbProjectRevision>();
        }

        public AuthorBase GetBase() 
        {
            return new AuthorBase(this);
        }
    }
}
