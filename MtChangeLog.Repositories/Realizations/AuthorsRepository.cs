using Microsoft.EntityFrameworkCore;
using MtChangeLog.Abstractions.Extensions;
using MtChangeLog.Abstractions.Repositories;
using MtChangeLog.Context.Realizations;
using MtChangeLog.Entities.Builders.Tables;
using MtChangeLog.Entities.Extensions.Tables;
using MtChangeLog.Entities.Tables;
using MtChangeLog.TransferObjects.Editable;
using MtChangeLog.TransferObjects.Views.Shorts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Repositories.Realizations
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly ApplicationContext context;

        public AuthorsRepository(ApplicationContext context) 
        {
            this.context = context;
        }

        public IQueryable<AuthorShortView> GetShortEntities() 
        {
            var result = this.context.Authors
                .AsNoTracking()
                .OrderBy(e => e.LastName)
                .Select(e => e.ToShortView());
            return result;
        }

        public IQueryable<AuthorEditable> GetTableEntities() 
        {
            var result = this.context.Authors
                .AsNoTracking()
                .OrderBy(e => e.LastName)
                .Select(e => e.ToEditable());
            return result;
        }

        public AuthorEditable GetTemplate()
        {
            var result = new AuthorEditable() 
            {
                Id = Guid.Empty,
                FirstName = "имя",
                LastName = "фамилия",
                Position = "введите должность автора"
            };
            return result;
        }

        public AuthorEditable GetEntity(Guid guid) 
        {
            var result = this.context.Authors
                .AsNoTracking()
                .Search(guid)
                .ToEditable();
            return result;
        }

        public void AddEntity(AuthorEditable entity)
        {
            var dbAuthor = AuthorBuilder.GetBuilder()
                .SetAttributes(entity)
                .Build();
            if (this.context.Authors.IsContained(dbAuthor))
            {
                throw new ArgumentException($"Сущность \"{entity}\" уже содержится в БД");
            }
            this.context.Authors.Add(dbAuthor);
            this.context.SaveChanges();
        }

        public void UpdateEntity(AuthorEditable entity)
        {
            var dbAuthor = this.context.Authors
                .Search(entity.Id);
            if (dbAuthor.Default)
            {
                throw new ArgumentException($"Сущность по умолчанию \"{entity}\" не может быть обновлена");
            }
            dbAuthor.GetBuilder()
                .SetAttributes(entity)
                .Build();
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid)
        {
            throw new NotImplementedException("функционал по удалению автора проекта на данный момент не доступен");
        }
    }
}
