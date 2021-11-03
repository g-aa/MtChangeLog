﻿using MtChangeLog.DataObjects.Entities.Base;
using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Entities
{
    internal class DbProjectRevision : ProjectRevisionBase
    {
        #region Relationships
        public Guid ProjectVersionId { get; set; }
        public DbProjectVersion ProjectVersion { get; set; }

        public Guid ParentRevisionId { get; set; }
        public DbProjectRevision ParentRevision { get; set; }

        public Guid ArmEditId { get; set; }
        public DbArmEdit ArmEdit { get; set; }

        public Guid CommunicationId { get; set; }
        public DbCommunication Communication { get; set; }

        public ICollection<DbAuthor> Authors { get; set; }
        public ICollection<DbRelayAlgorithm> RelayAlgorithms { get; set; }
        #endregion

        public DbProjectRevision() : base() 
        {
            this.Authors = new HashSet<DbAuthor>();
            this.RelayAlgorithms = new HashSet<DbRelayAlgorithm>();
        }

        public DbProjectRevision(ProjectRevisionBase other) : base(other)
        {

        }

        public ProjectRevisionBase GetBase() 
        {
            return new ProjectRevisionBase(this);
        }

        public ProjectRevisionEditable GetEditable() 
        {
            return new ProjectRevisionEditable(this)
            {
                ParentRevision = this.ParentRevision.GetShortView(),
                ProjectVersion = this.ProjectVersion.GetView(),
                Communication = this.Communication.GetBase(),
                Authors = this.Authors.Select(author => author.GetBase()),
                ArmEdit = this.ArmEdit.GetBase(),
                RelayAlgorithms = this.RelayAlgorithms.Select(alg => alg.GetBase()),
            };
        }

        public ProjectRevisionShortView GetShortView() 
        {
            return new ProjectRevisionShortView()
            {
                Id = this.Id,
                Module = this.ProjectVersion.AnalogModule.Title,
                Title = this.ProjectVersion.Title,
                Version = this.ProjectVersion.Version,
                Revision = this.Revision
            };
        }

        public ProjectRevisionTableView GetTableView() 
        {
            return new ProjectRevisionTableView()
            {
                Id = this.Id,
                Module = this.ProjectVersion.AnalogModule.Title,
                Title = this.ProjectVersion.Title,
                Version = this.ProjectVersion.Version,
                Revision = this.Revision,
                Date = this.Date,
                ArmEdit = this.ArmEdit.Version,
                Reason = this.Reason
            };
        }
    }
}
