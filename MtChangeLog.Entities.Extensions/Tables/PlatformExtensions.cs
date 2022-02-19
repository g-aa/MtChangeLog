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
    public static class PlatformExtensions
    {
        public static PlatformShortView ToShortView(this Platform entity)
        {
            var result = new PlatformShortView()
            {
                Id = entity.Id,
                Title = entity.Title
            };
            return result;
        }

        public static PlatformTableView ToTableView(this Platform entity)
        {
            var result = new PlatformTableView()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description
            };
            return result;
        }

        public static PlatformEditable ToEditable(this Platform entity)
        {
            var result = new PlatformEditable()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                AnalogModules = entity.AnalogModules.Select(module => module.ToShortView())
            };
            return result;
        }

        public static PlatformBuilder GetBuilder(this Platform entity)
        {
            return new PlatformBuilder(entity);
        }
    }
}
