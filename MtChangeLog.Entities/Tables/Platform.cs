using MtChangeLog.Abstractions.Entities;
using MtChangeLog.TransferObjects.Editable;
using MtChangeLog.TransferObjects.Views.Shorts;
using MtChangeLog.TransferObjects.Views.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace MtChangeLog.Entities.Tables
{
    public class Platform : IDefaultable, IIdentifiable, IEqualityPredicate<Platform>
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Наименование платформы БМРЗ параметр обязательный для заполнения")]
        [RegularExpression("^БМРЗ-[0-9 A-Z А-Я]{2,5}$", ErrorMessage = "Наименование платформы должено иметь следующий вид БМРЗ-xxxxx, где x - [0-9 A-Z А-Я]", MatchTimeoutInMilliseconds = 1000)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(500, ErrorMessage = "Описание платформы должно содержать не больше 500 символов")]
        public string Description { get; set; }
        public bool Default { get; set; }

        #region Relationships
        public ICollection<AnalogModule> AnalogModules { get; set; }
        public ICollection<ProjectVersion> Projects { get; set; }
        #endregion

        public Platform()
        {
            this.Id = Guid.NewGuid();
            this.AnalogModules = new HashSet<AnalogModule>();
            this.Projects = new HashSet<ProjectVersion>();
            this.Default = false;
        }

        public Platform(PlatformEditable other) : this() 
        {
            this.Title = other.Title;
            this.Description = other.Description;
        }

        public Func<Platform, bool> GetEqualityPredicate()
        {
            return (Platform e) => e.Id == this.Id || e.Title == this.Title;
        }

        public void Update(PlatformEditable other, ICollection<AnalogModule> modules) 
        {
            if (this.Default)
            {
                throw new ArgumentException($"Сущность по умолчанию \"{this}\" не может быть обновлена");
            }
            var prohibModules = this.AnalogModules.Except(modules).Where(e => e.Projects.Intersect(this.Projects).Any()).Select(e => e.Title);
            if (prohibModules.Any()) 
            {
                throw new ArgumentException($"Следующие аналоговые модули: \"{string.Join(",", prohibModules)}\" используются в проектах (БФПО) и не могут быть исключены из состава программных платформ \"{this}\"");
            }
            // this.Id - не обновляется !!!
            this.Title = other.Title;
            this.Description = other.Description;
            this.AnalogModules = modules;
            // this.Projects - не обновляется !!!
        }

        public PlatformShortView ToShortView() 
        {
            var result = new PlatformShortView()
            {
                Id = this.Id,
                Title = this.Title
            };
            return result;
        }

        public PlatformTableView ToTableView()
        {
            var result = new PlatformTableView()
            {
                Id = this.Id,
                Title = this.Title,
                Description = this.Description
            };
            return result;
        }

        public PlatformEditable ToEditable()
        {
            if (this.AnalogModules is null)
            {
                throw new ArgumentException($"Нельзя преобразовать сущность \"Platform\" к представлению для редактирования, отсутствует перечень аналоговых модулей используемых в БМРЗ");
            }
            var result = new PlatformEditable() 
            { 
                Id = this.Id, 
                Title = this.Title, 
                Description = this.Description, 
                AnalogModules = this.AnalogModules.Select(module => module.ToShortView()) 
            };
            return result;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Title);
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.Title))
            {
                return $"ID = {this.Title}";
            }
            return this.Title;
        }
    }
}
