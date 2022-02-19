using MtChangeLog.Entities.Builders.Tables;
using MtChangeLog.Entities.Tables;
using MtChangeLog.TransferObjects.Editable;
using MtChangeLog.TransferObjects.Views.Shorts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Extensions.Tables
{
    public static class AuthorExtensions
    {
        public static AuthorShortView ToShortView(this Author entity)
        {
            var result = new AuthorShortView()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };
            return result;
        }

        public static AuthorEditable ToEditable(this Author entity)
        {
            var result = new AuthorEditable()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Position = entity.Position
            };
            return result;
        }

        public static AuthorBuilder GetBuilder(this Author entity)
        {
            return new AuthorBuilder(entity);
        }
    }
}
