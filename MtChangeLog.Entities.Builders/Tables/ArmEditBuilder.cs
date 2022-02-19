using MtChangeLog.Entities.Tables;
using MtChangeLog.TransferObjects.Editable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Builders.Tables
{
    public class ArmEditBuilder
    {
        private readonly ArmEdit entity;

        private string divg;
        private string version;
        private DateTime? date;
        private string description;

        public ArmEditBuilder(ArmEdit entity) 
        {
            this.entity = entity;
        }

        public ArmEditBuilder SetAttributes(ArmEditEditable editable)
        {
            this.divg = editable?.DIVG;
            this.version = editable?.Version;
            this.date = editable?.Date;
            this.description = editable?.Description;
            return this;
        }

        public ArmEdit Build()
        {
            // атрибуты:
            // this.entity.Id - не обновляется!
            this.entity.DIVG = this.divg;
            this.entity.Version = this.version;
            this.entity.Date = date != null ? date.Value : DateTime.Now;
            this.entity.Description = description;
            // реляционные связи:
            // this.entity.ProjectRevisions - не обновляется!
            return this.entity;
        }

        public static ArmEditBuilder GetBuilder()
        {
            return new ArmEditBuilder(new ArmEdit());
        }
    }
}
