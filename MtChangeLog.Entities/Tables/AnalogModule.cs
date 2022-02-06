using MtChangeLog.Abstractions.Entities;
using MtChangeLog.TransferObjects.Editable;
using MtChangeLog.TransferObjects.Views.Shorts;
using MtChangeLog.TransferObjects.Views.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Tables
{
    public class AnalogModule : IDefaultable, IIdentifiable, IEqualityPredicate<AnalogModule>
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Децимальный номер аналогового модуля обязательный параметр для заполнения")]
        [RegularExpression("^ДИВГ.[0-9]{5}-[0-9]{2}$", ErrorMessage = "Децимальный номер аналогового модуля должен иметь следующий вид ДИВГ.xxxxx-xx, где x - число [0-9]", MatchTimeoutInMilliseconds = 1000)]
        public string DIVG { get; set; }

        [Required(ErrorMessage = "Наименование аналогово модуля обязательный параметр для заполнения")]
        [RegularExpression("^БМРЗ-[0-9]{3}$", ErrorMessage = "Наименование аналогово модуля должено иметь следующий вид БМРЗ-xxx, где x - число [0-9]", MatchTimeoutInMilliseconds = 1000)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Номинальный ток аналогового модуля обязательный параметр для заполнения")]
        [RegularExpression("^[0-9]{1}A$", ErrorMessage = "Номинальный ток аналогового модуля должен принимать значение от [0-9]A", MatchTimeoutInMilliseconds = 1000)]
        public string Current { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(500, ErrorMessage = "Описание аналогового модуля должно содержать не больше 500 символов")]
        public string Description { get; set; }

        public bool Default { get; set; }

        #region Relationships
        public ICollection<ProjectVersion> Projects { get; set; }
        public ICollection<Platform> Platforms { get; set; }
        #endregion

        public AnalogModule()
        {
            this.Id = Guid.NewGuid();
            this.Projects = new HashSet<ProjectVersion>();
            this.Platforms = new HashSet<Platform>();
            this.Default = false;
        }

        public AnalogModule(AnalogModuleEditable other) : this()
        {
            this.DIVG = other.DIVG;
            this.Title = other.Title;
            this.Current = other.Current;
            this.Description = other.Description;
        }

        public Func<AnalogModule, bool> GetEqualityPredicate()
        {
            //return (AnalogModule e) => e.Id == this.Id || e.DIVG == this.DIVG || e.Title == this.Title; - пока нет данных по ДИВГ-ам
            return (AnalogModule e) => e.Id == this.Id || e.Title == this.Title;
        }

        public void Update(AnalogModuleEditable other, ICollection<Platform> platforms)
        {
            if (this.Default)
            {
                throw new ArgumentException($"Сущность по умолчанию \"{this}\" не может быть обновлена");
            }
            var prohibPlatforms = this.Platforms.Except(platforms).Where(e => e.Projects.Intersect(this.Projects).Any()).Select(e => e.Title);
            if (prohibPlatforms.Any())
            {
                throw new ArgumentException($"Следующие платформы: \"{string.Join(", ", prohibPlatforms)}\" используются в проектах (БФПО) и не могут быть исключены из состава аналогового модуля \"{this}\"");
            }
            // this.Id = - не обновляется !!!
            this.DIVG = other.DIVG;
            this.Title = other.Title;
            this.Current = other.Current;
            this.Description = other.Description;
            this.Platforms = platforms;
            // this.ProjectVersion - не обновляется !!!
        }

        public AnalogModuleShortView ToShortView()
        {
            var result = new AnalogModuleShortView()
            {
                Id = this.Id,
                Title = this.Title
            };
            return result;
        }

        public AnalogModuleTableView ToTableView()
        {
            var result = new AnalogModuleTableView()
            {
                Id = this.Id,
                Title = this.Title,
                Current = this.Current,
                DIVG = this.DIVG,
                Description = this.Description
            };
            return result;
        }

        public AnalogModuleEditable ToEditable()
        {
            if (this.Platforms is null) 
            {
                throw new ArgumentException($"Нельзя преобразовать сущность \"AnalogModule\" к представлению для таблицы, отсутствует перечень программных платформ БМРЗ");
            }
            var result = new AnalogModuleEditable()
            {
                Id = this.Id,
                Title = this.Title,
                DIVG = this.DIVG,
                Current = this.Current,
                Description = this.Description,
                Platforms = this.Platforms.Select(platforms => platforms.ToShortView())
            };
            return result;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.DIVG, this.Title, this.Current);
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.Title))
            {
                return $"ID = {this.Id}";
            }
            return $"{this.Title}, номинальный ток: {this.Current}";
        }
    }
}
