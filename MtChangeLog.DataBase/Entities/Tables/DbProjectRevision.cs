﻿using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views.Shorts;
using MtChangeLog.DataObjects.Entities.Views.Statistics;
using MtChangeLog.DataObjects.Entities.Views.Tables;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace MtChangeLog.DataBase.Entities.Tables
{
    internal class DbProjectRevision : IEquatable<DbProjectRevision>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Revision { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }

        #region Relationships
        public Guid ProjectVersionId { get; set; }
        public DbProjectVersion ProjectVersion { get; set; }

        public Guid ParentRevisionId { get; set; }
        public DbProjectRevision ParentRevision { get; set; }

        public Guid ArmEditId { get; set; }
        public DbArmEdit ArmEdit { get; set; }

        public Guid CommunicationModuleId { get; set; }
        public DbCommunicationModule CommunicationModule { get; set; }

        public ICollection<DbAuthor> Authors { get; set; }
        public ICollection<DbRelayAlgorithm> RelayAlgorithms { get; set; }
        #endregion

        public DbProjectRevision()
        {
            this.Id = Guid.NewGuid();
            this.Date = DateTime.Now;
            this.Authors = new HashSet<DbAuthor>();
            this.RelayAlgorithms = new HashSet<DbRelayAlgorithm>();
        }

        public DbProjectRevision(ProjectRevisionEditable other) : this() 
        {
            this.Date = other.Date;
            this.Revision = other.Revision;
            this.Reason = other.Reason;
            this.Description = other.Description;
        }

        public void Update(ProjectRevisionEditable other, DbArmEdit armEdit, DbCommunicationModule communication, ICollection<DbAuthor> authors, ICollection<DbRelayAlgorithm> algorithms, DbProjectRevision parent) 
        {
            // this.Id - не обновляется !!!
            // this.Revision - не обновляется !!!
            this.Date = other.Date;
            this.Reason = other.Reason;
            this.Description = other.Description;
            this.ArmEdit = armEdit;
            this.CommunicationModule = communication;
            this.Authors = authors;
            this.RelayAlgorithms = algorithms;
            // this.ProjectVersion - не обновляется !!!
            this.ParentRevision = parent;
        }

        public ProjectRevisionShortView ToShortView() 
        {
            var result = new ProjectRevisionShortView()
            {
                Id = this.Id,
                Module = this.ProjectVersion?.AnalogModule?.Title ?? "БМРЗ-000",
                Title = this.ProjectVersion?.Title ?? "",
                Version = this.ProjectVersion?.Version ?? "v0.00.00.00",
                Revision = this.Revision
            };
            return result;
        }

        public ProjectRevisionTableView ToTableView() 
        {
            var result = new ProjectRevisionTableView()
            {
                Id = this.Id,
                Module = this.ProjectVersion?.AnalogModule?.Title ?? "БМРЗ-000",
                Title = this.ProjectVersion?.Title ?? "",
                Version = this.ProjectVersion?.Version ?? "",
                Revision = this.Revision,
                Date = this.Date,
                ArmEdit = this.ArmEdit?.Version ?? "v0.00.00.00",
                Reason = this.Reason
            };
            return result;
        }

        public ProjectRevisionEditable ToEditable()
        {
            var result = new ProjectRevisionEditable()
            {
                Id = this.Id,
                Date = this.Date,
                Revision = this.Revision,
                Reason = this.Reason,
                Description = this.Description,
                ParentRevision = this.ParentRevision?.ToShortView(),
                ProjectVersion = this.ProjectVersion?.ToShortView(),
                ArmEdit = this.ArmEdit?.ToShortView(),
                CommunicationModule = this.CommunicationModule?.ToShortView(),
                Authors = this.Authors.Select(author => author.ToShortView()),
                RelayAlgorithms = this.RelayAlgorithms.Select(alg => alg.ToShortView()),
            };
            return result;
        }

        public ProjectRevisionTreeView ToTreeView() 
        {
            var result = new ProjectRevisionTreeView()
            {
                Id = this.Id,
                ParentId = this.ParentRevisionId,
                Module = this.ProjectVersion?.AnalogModule?.Title ?? "БМРЗ-000",
                Title = this.ProjectVersion?.Title,
                Version = this.ProjectVersion?.Version,
                Revision = this.Revision,
                Date = this.Date.ToString("yyyy-MM-dd"),
                ArmEdit = this.ArmEdit?.Version ?? "v0.00.00.00",
                Platform = this.ProjectVersion?.Platform?.Title ?? "БМРЗ-000"
            };
            return result;
        }

        public ProjectRevisionHistoryView ToHistoryView()
        {
            var result = new ProjectRevisionHistoryView()
            {
                ArmEdit = this.ArmEdit?.Version ?? "v0.00.00.00",
                Authors = this.Authors.Select(a => $"{a?.FirstName} {a?.LastName}"),
                RelayAlgorithms = this.RelayAlgorithms.Select(ra => ra.Title),
                Communication = string.Join(", ", this.CommunicationModule?.Protocols.OrderBy(e => e.Title).Select(e => e.Title)),
                Date = this.Date,
                Description = this.Description,
                Platform = this.ProjectVersion?.Platform?.Title ?? "БМРЗ-000",
                Reason = this.Reason,
                Title = $"{this.ProjectVersion?.AnalogModule?.Title}-{this.ProjectVersion?.Title}-{this.ProjectVersion?.Version}_{this.Revision}"
            };
            return result;
        }

        public ProjectRevisionHistoryShortView ToHistoryShortView() 
        {
            var result = new ProjectRevisionHistoryShortView()
            {
                Id = this.Id,
                Date = this.Date,
                Title = $"{this.ProjectVersion?.AnalogModule?.Title}-{this.ProjectVersion?.Title}-{this.ProjectVersion?.Version}_{this.Revision}",
                Platform = this.ProjectVersion?.Platform?.Title ?? "БМРЗ-000"
            };
            return result;
        }

        public bool Equals([AllowNull] DbProjectRevision other)
        {
            return this.Id == other.Id || this.Date == other.Date && this.Revision == other.Revision && this.Reason == other.Reason && this.ProjectVersion.Equals(other.ProjectVersion);
        }
        
        public override bool Equals(object obj)
        {
            return this.Equals(obj as DbProjectRevision);
        }

        public override int GetHashCode()
        {
            // при определении уникальности картежа нужно учитывать и версию проекта к которой он привязан !!!
            // ПС чисто теоретически даты и время компиляции должны отличасться, но так происходит не всегда
            return HashCode.Combine(this.Date, this.Revision, this.Reason, this.ProjectVersionId);
        }

        public override string ToString()
        {
            return $"редакция: {this.Revision}, дата изменения: {this.Date}, причина: {this.Reason}";
        }
    }
}
