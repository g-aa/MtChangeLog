using MtChangeLog.Abstractions.Entities;
using MtChangeLog.TransferObjects.Editable;
using MtChangeLog.TransferObjects.Views.Shorts;
using MtChangeLog.TransferObjects.Views.Statistics;
using MtChangeLog.TransferObjects.Views.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace MtChangeLog.Entities.Tables
{
    public class ProjectRevision : IIdentifiable, IEqualityPredicate<ProjectRevision>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Редакция БФПО обязательный параметр для заполнения")]
        [RegularExpression("^[0-9]{2}$", ErrorMessage = "Редакция БФПО, может принимать значение в интервала 00-99", MatchTimeoutInMilliseconds = 1000)]
        public string Revision { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(500, ErrorMessage = "Причина выпуска новой редакции должна содержать не больше 500 символов")]
        public string Reason { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(5000, ErrorMessage = "Описание редакции должно содержать не больше 5000 символов")]
        public string Description { get; set; }

        #region Relationships
        public Guid ProjectVersionId { get; set; }
        public ProjectVersion ProjectVersion { get; set; }

        public Guid ParentRevisionId { get; set; }
        public ProjectRevision ParentRevision { get; set; }

        public Guid ArmEditId { get; set; }
        public ArmEdit ArmEdit { get; set; }

        public Guid CommunicationModuleId { get; set; }
        public CommunicationModule CommunicationModule { get; set; }

        public ICollection<Author> Authors { get; set; }
        public ICollection<RelayAlgorithm> RelayAlgorithms { get; set; }
        #endregion

        public ProjectRevision()
        {
            this.Id = Guid.NewGuid();
            this.Date = DateTime.Now;
            this.Authors = new HashSet<Author>();
            this.RelayAlgorithms = new HashSet<RelayAlgorithm>();
        }

        public ProjectRevision(ProjectRevisionEditable other) : this()
        {
            this.Date = other.Date;
            this.Revision = other.Revision;
            this.Reason = other.Reason;
            this.Description = other.Description;
        }

        public Func<ProjectRevision, bool> GetEqualityPredicate()
        {
            return (ProjectRevision e) => e.Id == this.Id
            || (e.ProjectVersionId == this.ProjectVersionId || e.ProjectVersion.DIVG == this.ProjectVersion.DIVG) && e.Revision == this.Revision;
        }

        public void Update(ProjectRevisionEditable other, ArmEdit armEdit, CommunicationModule communication, ICollection<Author> authors, ICollection<RelayAlgorithm> algorithms, ProjectRevision parent) 
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
            var title = "краткому представлению";
            this.CheckProjectVersion(title);
            this.CheckAnalogModule(title);
            var result = new ProjectRevisionShortView()
            {
                Id = this.Id,
                Prefix = this.ProjectVersion.Prefix,
                Title = this.ProjectVersion.Title,
                Version = this.ProjectVersion.Version,
                Revision = this.Revision
            };
            return result;
        }

        public ProjectRevisionTableView ToTableView() 
        {
            var title = "представлению для таблицы";
            this.CheckProjectVersion(title);
            this.CheckAnalogModule(title);
            this.CheckArmEdit(title);
            var result = new ProjectRevisionTableView()
            {
                Id = this.Id,
                Prefix = this.ProjectVersion.Prefix,
                Title = this.ProjectVersion.Title,
                Version = this.ProjectVersion.Version,
                Revision = this.Revision,
                Date = this.Date,
                ArmEdit = this.ArmEdit.Version,
                Reason = this.Reason
            };
            return result;
        }

        public ProjectRevisionEditable ToEditable()
        {
            var title = "представлению для редактирования";
            this.CheckProjectVersion(title);
            this.CheckAnalogModule(title);
            this.CheckArmEdit(title);
            this.CheckCommunicationModule(title);
            var result = new ProjectRevisionEditable()
            {
                Id = this.Id,
                Date = this.Date,
                Revision = this.Revision,
                Reason = this.Reason,
                Description = this.Description,
                ParentRevision = this.ParentRevision?.ToShortView(),
                ProjectVersion = this.ProjectVersion.ToShortView(),
                ArmEdit = this.ArmEdit.ToShortView(),
                CommunicationModule = this.CommunicationModule.ToShortView(),
                Authors = this.Authors.Select(author => author.ToShortView()),
                RelayAlgorithms = this.RelayAlgorithms.Select(alg => alg.ToShortView()),
            };
            return result;
        }

        public ProjectRevisionTreeView ToTreeView() 
        {
            var title = "представлению для дерева";
            this.CheckProjectVersion(title);
            this.CheckAnalogModule(title);
            this.CheckArmEdit(title);
            this.CheckPlatform(title);
            var result = new ProjectRevisionTreeView()
            {
                Id = this.Id,
                ParentId = this.ParentRevisionId,
                Prefix = this.ProjectVersion.Prefix,
                Title = this.ProjectVersion.Title,
                Version = this.ProjectVersion.Version,
                Revision = this.Revision,
                Date = this.Date.ToString("yyyy-MM-dd"),
                ArmEdit = this.ArmEdit.Version,
                Platform = this.ProjectVersion.Platform.Title
            };
            return result;
        }

        public ProjectRevisionHistoryShortView ToHistoryShortView()
        {
            var title = "краткому представлению для истории";
            this.CheckProjectVersion(title);
            this.CheckAnalogModule(title);
            this.CheckPlatform(title);
            var result = new ProjectRevisionHistoryShortView()
            {
                Id = this.Id,
                Date = this.Date,
                Title = $"{this.ProjectVersion.Prefix}-{this.ProjectVersion.Title}-{this.ProjectVersion.Version}_{this.Revision}",
                Platform = this.ProjectVersion.Platform.Title
            };
            return result;
        }

        public ProjectRevisionHistoryView ToHistoryView()
        {
            var title = "полному представлению для истории";
            this.CheckProjectVersion(title);
            this.CheckArmEdit(title);
            this.CheckCommunicationModule(title);
            this.CheckPlatform(title);
            this.CheckAnalogModule(title);
            var result = new ProjectRevisionHistoryView()
            {
                Id = this.Id,
                ArmEdit = this.ArmEdit.Version,
                Authors = this.Authors.Select(a => $"{a.FirstName} {a.LastName}"),
                RelayAlgorithms = this.RelayAlgorithms.Select(ra => ra.Title),
                Communication = string.Join(", ", this.CommunicationModule.Protocols.OrderBy(e => e.Title).Select(e => e.Title)),
                Date = this.Date,
                Description = this.Description,
                Platform = this.ProjectVersion.Platform.Title,
                Reason = this.Reason,
                Title = $"{this.ProjectVersion.Prefix}-{this.ProjectVersion.Title}-{this.ProjectVersion.Version}_{this.Revision}"
            };
            return result;
        }

        public override int GetHashCode()
        {
            // при определении уникальности картежа нужно учитывать и версию проекта к которой он привязан !!!
            // ПС чисто теоретически даты и время компиляции должны отличасться, но так происходит не всегда
            return HashCode.Combine(this.ProjectVersionId, this.Date, this.Revision);
        }

        public override string ToString()
        {
            return $"редакция: {this.Revision}, дата изменения: {this.Date}, причина: {this.Reason}";
        }

        private void CheckAnalogModule(string title)
        {
            if (this.ProjectVersion.AnalogModule is null)
            {
                throw new ArgumentException($"Нельзя преобразовать сущность \"ProjectRevision\" к {title}, отсутствует аналоговый модуль проекта (БФПО)");
            }
        }

        private void CheckPlatform(string title)
        {
            if (this.ProjectVersion.Platform is null)
            {
                throw new ArgumentException($"Нельзя преобразовать сущность \"ProjectRevision\" к {title}, отсутствует программная платформа проекта (БФПО)");
            }
        }

        private void CheckProjectVersion(string title)
        {
            if (this.ProjectVersion is null)
            {
                throw new ArgumentException($"Нельзя преобразовать сущность \"ProjectRevision\" к {title}, отсутствует проект (БФПО)");
            }
        }

        private void CheckArmEdit(string title) 
        {
            if (this.ArmEdit is null)
            {
                throw new ArgumentException($"Нельзя преобразовать сущность \"ProjectRevision\" к {title}, отсутствует ArmEdit");
            }
        }

        private void CheckCommunicationModule(string title) 
        {
            if (this.ArmEdit is null)
            {
                throw new ArgumentException($"Нельзя преобразовать сущность \"ProjectRevision\" к {title}, отсутствует коммуникационный модуль");
            }
        }
    }
}
