using MtChangeLog.Entities.Tables;
using MtChangeLog.TransferObjects.Editable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Builders.Tables
{
    public class RelayAlgorithmBuilder
    {
        private readonly RelayAlgorithm entity;

        private string group;
        private string title;
        private string ansi;
        private string logicalnode;
        private string description;

        public RelayAlgorithmBuilder(RelayAlgorithm entity) 
        {
            this.entity = entity;
        }

        public RelayAlgorithmBuilder SetAttributes(RelayAlgorithmEditable editable)
        {
            this.group = editable?.Group;
            this.title = editable?.Title;
            this.ansi = editable?.ANSI;
            this.logicalnode = editable?.LogicalNode;
            this.description = editable?.Description;
            return this;
        }

        public RelayAlgorithm Build()
        {
            // атрибуты:
            // this.entity.Id - не обновляется!
            this.entity.Group = this.group;
            this.entity.Title = this.title;
            this.entity.ANSI = this.ansi;
            this.entity.LogicalNode = this.logicalnode;
            this.entity.Description = this.description;
            // this.entity.ProjectRevisions - не обновляется!
            return this.entity;
        }

        public static RelayAlgorithmBuilder GetBuilder() 
        {
            return new RelayAlgorithmBuilder(new RelayAlgorithm());
        }
    }
}
