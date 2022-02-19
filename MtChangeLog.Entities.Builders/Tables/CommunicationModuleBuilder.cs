using MtChangeLog.Entities.Tables;
using MtChangeLog.TransferObjects.Editable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Builders.Tables
{
    public class CommunicationModuleBuilder
    {
        private readonly CommunicationModule entity;

        private string title;
        private string description;
        private IQueryable<Protocol> protocols;

        public CommunicationModuleBuilder(CommunicationModule entity) 
        {
            this.entity = entity;
        }

        public CommunicationModuleBuilder SetAttributes(CommunicationModuleEditable editable)
        {
            this.title = editable?.Title;
            this.description = editable?.Description;
            return this;
        }

        public CommunicationModuleBuilder SetProtocols(IQueryable<Protocol> protocols)
        {
            this.protocols = protocols;
            return this;
        }

        public CommunicationModule Build()
        {
            // атрибуты:
            // this.entity.Id - не обновляется!
            this.entity.Title = this.title;
            this.entity.Description = this.description;
            // реляционные связи:
            this.entity.Protocols = this.protocols.ToHashSet();
            return this.entity;
        }

        public static CommunicationModuleBuilder GetBuilder() 
        {
            return new CommunicationModuleBuilder(new CommunicationModule());
        }
    }
}
