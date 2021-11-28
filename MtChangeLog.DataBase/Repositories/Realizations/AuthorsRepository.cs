using MtChangeLog.DataBase.Contexts;
using MtChangeLog.DataBase.Entities;
using MtChangeLog.DataBase.Repositories.Interfaces;

using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views.Shorts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Repositories.Realizations
{
    public class AuthorsRepository : BaseRepository, IAuthorsRepository
    {
        public AuthorsRepository(ApplicationContext context) : base(context) 
        {
            
        }

        public IEnumerable<AuthorShortView> GetShortEntities() 
        {
            var result = this.context.Authors.OrderBy(e => e.LastName).Select(e => e.ToShortView());
            return result;
        }

        public IEnumerable<AuthorEditable> GetTableEntities() 
        {
            var result = this.context.Authors.OrderBy(e => e.LastName).Select(e => e.ToEditable());
            return result;
        }

        public AuthorEditable GetTemplate()
        {
            var template = new AuthorEditable() 
            {
                Id = Guid.Empty,
                FirstName = "имя",
                LastName = "фамилия",
                Position = "введите должность автора"
            };
            return template;
        }

        public AuthorEditable GetEntity(Guid guid) 
        {
            var dbAuthor = this.GetDbAuthor(guid);
            return dbAuthor.ToEditable();
        }

        public void AddEntity(AuthorEditable entity)
        {
            var dbAuthor = new DbAuthor(entity);
            if (this.context.Authors.FirstOrDefault(e => e.Equals(dbAuthor)) != null)
            {
                throw new ArgumentException($"Author {entity} is contained in database");
            }
            this.context.Authors.Add(dbAuthor);
            this.context.SaveChanges();
        }

        public void UpdateEntity(AuthorEditable entity)
        {
            var dbAuthor = this.GetDbAuthor(entity.Id);
            if (dbAuthor.Default)
            {
                throw new ArgumentException($"Default entity {entity} can not by update");
            }
            dbAuthor.Update(entity);
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid)
        {
            throw new NotImplementedException("функционал не поддерживается");
            //DbAuthor dbAuthor = this.GetDbAuthor(guid);
            //this.context.Authors.Remove(dbAuthor);
            //this.context.SaveChanges();
        }
    }
}
