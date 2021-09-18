using MtChangeLog.DataBase.Contexts;
using MtChangeLog.DataBase.Entities;
using MtChangeLog.DataBase.Repositories.Interfaces;
using MtChangeLog.DataObjects.Entities.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Repositories.Realizations
{
    public class AuthorRepository : BaseRepository, IAuthorRepository
    {
        public AuthorRepository(ApplicationContext context) : base(context) 
        {
            
        }

        public IEnumerable<AuthorBase> GetEntities()
        {
            return this.context.Authors.OrderBy(author => author.LastName).Select(author => author.GetBase());
        }

        public AuthorBase GetEntity(Guid guid) 
        {
            var dbAuthor = this.GetDbAuthor(guid);
            return dbAuthor.GetBase();
        }

        public void AddEntity(AuthorBase entity)
        {
            if (this.context.Authors.AsEnumerable().FirstOrDefault(a => a.Equals(entity)) != null)
            {
                throw new ArgumentException($"Author {entity.FirstName} {entity.LastName} is contained in database");
            }
            var dbArmEdit = new DbAuthor(entity);

            this.context.Authors.Add(dbArmEdit);
            this.context.SaveChanges();
        }

        public void UpdateEntity(AuthorBase entity)
        {
            DbAuthor dbAuthor = this.GetDbAuthor(entity.Id);
            dbAuthor.Update(entity);
            // dbAuthor.ProjectRevisions - не должно обновляеться !!!
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid)
        {
            DbAuthor dbAuthor = this.GetDbAuthor(guid);
            this.context.Authors.Remove(dbAuthor);
            this.context.SaveChanges();
        }
    }
}
