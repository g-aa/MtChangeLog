using MtChangeLog.Entities.Tables;
using MtChangeLog.TransferObjects.Editable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Builders.Tables
{
    public class ProjectRevisionBuilder
    {
        private readonly ProjectRevision entity;

        private DateTime? date;
        private string revision;
        private string reason;
        private string description;
        private ProjectVersion project;
        private ProjectRevision parent;
        private ArmEdit armedit;
        private CommunicationModule module;
        private IQueryable<Author> authors;
        private IQueryable<RelayAlgorithm> algorithms;

        public ProjectRevisionBuilder(ProjectRevision entity) 
        {
            this.entity = entity;
        }

        public ProjectRevisionBuilder SetAttributes(ProjectRevisionEditable editable)
        {
            this.date = editable?.Date;
            this.revision = editable?.Revision;
            this.reason = editable?.Reason;
            this.description = editable?.Description;
            return this;
        }

        public ProjectRevisionBuilder SetProjectVersion(ProjectVersion project) 
        {
            this.project = project;
            return this;
        }

        public ProjectRevisionBuilder SetParentRevision(ProjectRevision parent) 
        {
            this.parent = parent;
            return this;
        }
        
        public ProjectRevisionBuilder SetArmEdit(ArmEdit armedit)
        {
            this.armedit = armedit;
            return this;
        }

        public ProjectRevisionBuilder SetCommunication(CommunicationModule module)
        {
            this.module = module;
            return this;
        }

        public ProjectRevisionBuilder SetAuthors(IQueryable<Author> authors)
        {
            this.authors = authors;
            return this;
        }

        public ProjectRevisionBuilder SetAlgorithms(IQueryable<RelayAlgorithm> algorithms)
        {
            this.algorithms = algorithms;
            return this;
        }

        public ProjectRevision Build()
        {
            // атрибуты:
            // this.entity.Id - не обновляется!
            this.entity.Date = this.date != null ? this.date.Value : DateTime.Now;
            // - не обновляется!
            if (string.IsNullOrEmpty(this.entity.Revision)) 
            {
                this.entity.Revision = this.revision;     
            }
            this.entity.Reason = this.reason;
            this.entity.Description = description;
            // реляционные связи:
            // -не обновляется!
            if (this.entity.ProjectVersion is null) 
            {
                this.entity.ProjectVersion = this.project;    
            }
            this.entity.ParentRevision = this.parent;
            this.entity.ArmEdit = this.armedit;
            this.entity.CommunicationModule = this.module;
            this.entity.Authors = this.authors.ToHashSet();
            this.entity.RelayAlgorithms = this.algorithms.ToHashSet();
            return this.entity;
        }

        public static ProjectRevisionBuilder GetBuilder()
        {
            return new ProjectRevisionBuilder(new ProjectRevision());
        }
    }
}
