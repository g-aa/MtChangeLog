using MtChangeLog.DataObjects.Entities.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Entities
{
    internal class DbArmEdit : ArmEditBase
    {
        #region Relationships
        public ICollection<DbProjectRevision> ProjectRevisions { get; set; }
        #endregion

        public DbArmEdit() : base() 
        {
            this.ProjectRevisions = new HashSet<DbProjectRevision>();
        }

        public DbArmEdit(ArmEditBase other) : base(other) 
        {

        }

        public void Update(ArmEditBase other) 
        {
            this.DIVG = other.DIVG;
            this.Date = other.Date;
            this.Description = other.Description;
            this.Version = other.Version;
        }

        public ArmEditBase GetBase() 
        {
            return new ArmEditBase(this);
        }
    }
}
