using MtChangeLog.Entities.Builders.Tables;
using MtChangeLog.Entities.Tables;
using MtChangeLog.TransferObjects.Editable;
using MtChangeLog.TransferObjects.Views.Shorts;
using MtChangeLog.TransferObjects.Views.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Extensions.Tables
{
    public static class CommunicationModuleExtensions
    {
        public static CommunicationModuleShortView ToShortView(this CommunicationModule entity)
        {
            var result = new CommunicationModuleShortView()
            {
                Id = entity.Id,
                Title = entity.Title,
            };
            return result;
        }

        public static CommunicationModuleTableView ToTableView(this CommunicationModule entity)
        {
            var result = new CommunicationModuleTableView()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Protocols = entity.Protocols.Any() ? string.Join(", ", entity.Protocols.OrderBy(e => e.Title).Select(e => e.Title)) : ""
            };
            return result;
        }

        public static CommunicationModuleEditable ToEditable(this CommunicationModule entity)
        {
            var result = new CommunicationModuleEditable()
            {
                Id = entity.Id,
                Title = entity.Title,
                Protocols = entity.Protocols.OrderBy(e => e.Title).Select(e => e.ToShortView()),
                Description = entity.Description
            };
            return result;
        }

        public static CommunicationModuleBuilder GetBuilder(this CommunicationModule entity)
        {
            return new CommunicationModuleBuilder(entity);
        }
    }
}
