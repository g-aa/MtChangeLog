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
    public static class ProtocolExtensions
    {
        public static ProtocolShortView ToShortView(this Protocol entity)
        {
            var result = new ProtocolShortView()
            {
                Id = entity.Id,
                Title = entity.Title
            };
            return result;
        }

        public static ProtocolEditable ToEditable(this Protocol entity)
        {
            var result = new ProtocolEditable()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                CommunicationModules = entity.CommunicationModules.OrderBy(e => e.Title).Select(e => e.ToShortView())
            };
            return result;
        }

        public static ProtocolBuilder GetBuilder(this Protocol entity) 
        {
            return new ProtocolBuilder(entity);
        }
    }
}
