using MtChangeLog.Abstractions.Entities;
using MtChangeLog.TransferObjects.Editable;
using MtChangeLog.TransferObjects.Views.Shorts;
using MtChangeLog.TransferObjects.Views.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MtChangeLog.Entities.Tables
{
    public class ProjectVersion : IIdentifiable, IEqualityPredicate<ProjectVersion>
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Децимальный номер версии проекта обязательный параметр для заполнения")]
        [RegularExpression("^ДИВГ.[0-9]{5}-[0-9]{2}$", ErrorMessage = "Децимальный номер должен иметь следующий вид ДИВГ.xxxxx-xx, где x - число [0-9]", MatchTimeoutInMilliseconds = 1000)]
        public string DIVG { get; set; }

        [Required(ErrorMessage = "Префикс проекта, псевдоним аналогового модуля обязательный параметр для заполнения")]
        [RegularExpression("^БФПО(-[0-9]{3})?$", ErrorMessage = "Префикс проекта, псевдоним аналогового модуля должено иметь следующий вид БФПО-xxx, где x - [0-9]", MatchTimeoutInMilliseconds = 1000)]
        public string Prefix { get; set; }

        [StringLength(16, MinimumLength = 2, ErrorMessage = "Наименование проекта должно содержать не больше {1} и не менее {2} символов")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Версия БФПО обязательный параметр для заполнения")]
        [RegularExpression("^[0-9]{2}$", ErrorMessage = "Версия БФПО, может принимать значение в интервала 00-99", MatchTimeoutInMilliseconds = 1000)]
        public string Version { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(500, ErrorMessage = "Описание проекта должно содержать не больше 500 символов")]
        public string Description { get; set; }

        #region Relationships
        public Guid PlatformId { get; set; }
        public Platform Platform { get; set; }
        
        public Guid AnalogModuleId { get; set; }
        public AnalogModule AnalogModule { get; set; }

        public Guid ProjectStatusId { get; set; }
        public ProjectStatus ProjectStatus { get; set; } 

        public ICollection<ProjectRevision> ProjectRevisions { get; set; }
        #endregion

        public ProjectVersion()
        {
            this.Id = Guid.NewGuid();
            this.ProjectRevisions = new HashSet<ProjectRevision>();
        }

        public ProjectVersion(ProjectVersionEditable other) : this()
        {
            this.DIVG = other.DIVG;
            this.Title = other.Title;
            this.Version = other.Version;
            this.Description = other.Description;
        }

        public Func<ProjectVersion, bool> GetEqualityPredicate()
        {
            return (ProjectVersion e) => e.Id == this.Id 
            || e.DIVG == this.DIVG 
            || e.Prefix == this.Prefix && e.Title == this.Title && e.Version == this.Version;
        }

        public void Update(ProjectVersionEditable other, AnalogModule module, Platform platform, ProjectStatus status) 
        {
            // this.Id - не обновляется !!!
            this.DIVG = other.DIVG;
            this.Prefix = other.Prefix != string.Empty ? other.Prefix : module.Title.Replace("БМРЗ", "БФПО");
            this.Title = other.Title;
            this.Version = other.Version;
            this.Description = other.Description;
            this.AnalogModule = module;
            this.Platform = platform;
            this.ProjectStatus = status;
            // this.ProjectRevisions - не обновляется !!!
        }

        public ProjectVersionShortView ToShortView() 
        {
            var result = new ProjectVersionShortView()
            {
                Id = this.Id,
                Prefix = this.Prefix,
                Title = this.Title,
                Version = this.Version
            };
            return result;
        }

        public ProjectVersionTableView ToTableView()
        {
            var title = "представлению для таблицы";
            this.CheckAnalogModule(title);
            this.CheckPlatform(title);
            var result = new ProjectVersionTableView() 
            {
                Id = this.Id,
                DIVG = this.DIVG,
                Prefix = this.Prefix,
                Title = this.Title,
                Status = this.ProjectStatus.Title,
                Version = this.Version,
                Description = this.Description,
                Module = this.AnalogModule.Title,
                Platform = this.Platform.Title
            };
            return result;
        }

        public ProjectVersionEditable ToEditable()
        {
            var title = "представлению для редактирования";
            this.CheckAnalogModule(title);
            this.CheckPlatform(title);
            this.CheckProjectStatus(title);
            var result = new ProjectVersionEditable()
            {
                Id = this.Id,
                DIVG = this.DIVG,
                Prefix = this.Prefix,
                Title = this.Title,
                ProjectStatus = this.ProjectStatus.ToShortView(),
                Version = this.Version,
                Description = this.Description,
                AnalogModule = this.AnalogModule.ToShortView(),
                Platform = this.Platform.ToShortView()
            };
            return result;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.DIVG, this.Prefix, this.Title, this.Version);
        }
        
        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.DIVG)) 
            {
                return $"ID = {this.Id}";   
            }
            return $"{this.DIVG}, {this.Prefix}-{this.Title}_{this.Version}";
        }

        private void CheckAnalogModule(string title)
        {
            if (this.AnalogModule is null)
            {
                throw new ArgumentException($"Нельзя преобразовать сущность \"ProjectVersion\" к {title}, отсутствует аналоговый модуль проекта (БФПО)");
            }
        }

        private void CheckPlatform(string title)
        {
            if (this.Platform is null)
            {
                throw new ArgumentException($"Нельзя преобразовать сущность \"ProjectVersion\" к {title}, отсутствует программная платформа проекта (БФПО)");
            }
        }

        private void CheckProjectStatus(string title) 
        {
            if (this.ProjectStatus is null)
            {
                throw new ArgumentException($"Нельзя преобразовать сущность \"ProjectVersion\" к {title}, отсутствует статус проекта (БФПО)");
            }
        }
    }
}
