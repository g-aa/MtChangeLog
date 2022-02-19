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
    public static class ProjectStatusExtensions
    {
        public static ProjectStatusShortView ToShortView(this ProjectStatus entity)
        {
            var result = new ProjectStatusShortView()
            {
                Id = entity.Id,
                Title = entity.Title
            };
            return result;
        }

        public static ProjectStatusEditable ToEditable(this ProjectStatus entity)
        {
            var result = new ProjectStatusEditable()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description
            };
            return result;
        }

        public static ProjectStatusBuilder GetBuilder(this ProjectStatus entity)
        {
            return new ProjectStatusBuilder(entity);
        }
    }
}
