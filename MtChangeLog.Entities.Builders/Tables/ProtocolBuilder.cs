using MtChangeLog.Entities.Tables;
using MtChangeLog.TransferObjects.Editable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Builders.Tables
{
    public class ProtocolBuilder
    {
        private readonly Protocol entity;

        private string title;
        private string description;
        private IQueryable<CommunicationModule> modules;

        public ProtocolBuilder(Protocol entity) 
        {
            this.entity = entity;
        }

        public ProtocolBuilder SetAttributes(ProtocolEditable editable) 
        {
            this.title = editable?.Title;
            this.description = editable?.Description;
            return this;
        }

        public ProtocolBuilder SetModules(IQueryable<CommunicationModule> modules) 
        {
            this.modules = modules;
            return this;
        }

        public Protocol Build() 
        {
            // атрибуты:
            // this.entity.Id - не обновляется!
            this.entity.Title = this.title;
            this.entity.Description = this.description;
            // реляционные связи:
            this.entity.CommunicationModules = this.modules.ToHashSet();
            return this.entity;
        }

        public static ProtocolBuilder GetBuilder() 
        {
            return new ProtocolBuilder(new Protocol());
        }
    }
}
