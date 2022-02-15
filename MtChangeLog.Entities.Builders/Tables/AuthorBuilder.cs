using MtChangeLog.Entities.Tables;
using MtChangeLog.TransferObjects.Editable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Builders.Tables
{
    public class AuthorBuilder
    {
        private readonly Author entity;

        private string firstname;
        private string lastname;
        private string position;

        public AuthorBuilder(Author entity) 
        {
            this.entity = entity;
        }

        public AuthorBuilder SetAttributes(AuthorEditable editable)
        {
            this.firstname = editable?.FirstName;
            this.lastname = editable?.LastName;
            this.position = editable?.Position;
            return this;
        }

        public Author Build()
        {
            // атрибуты:
            // this.entity.Id - не обновляется!
            this.entity.FirstName = this.firstname;
            this.entity.LastName = this.lastname;
            this.entity.Position = this.position;
            // реляционные связи:
            // this.entity.ProjectRevisions - не обновляется!
            return this.entity;
        }

        public static AuthorBuilder GetBuilder()
        {
            return new AuthorBuilder(new Author());
        }
    }
}
