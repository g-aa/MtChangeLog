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
    public static class ArmEditExtensions
    {
        public static ArmEditShortView ToShortView(this ArmEdit entity)
        {
            var result = new ArmEditShortView()
            {
                Id = entity.Id,
                Version = entity.Version
            };
            return result;
        }

        public static ArmEditEditable ToEditable(this ArmEdit entity)
        {
            var result = new ArmEditEditable()
            {
                Id = entity.Id,
                Date = entity.Date,
                DIVG = entity.DIVG,
                Version = entity.Version,
                Description = entity.Description
            };
            return result;
        }

        public static ArmEditBuilder GetBuilder(this ArmEdit entity)
        {
            return new ArmEditBuilder(entity);
        }
    }
}
