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
    public static class AnalogModuleExtensions
    {
        public static AnalogModuleShortView ToShortView(this AnalogModule entity) 
        {
            var result = new AnalogModuleShortView()
            {
                Id = entity.Id,
                Title = entity.Title
            };
            return result;
        }

        public static AnalogModuleTableView ToTableView(this AnalogModule entity)
        {
            var result = new AnalogModuleTableView()
            {
                Id = entity.Id,
                Title = entity.Title,
                Current = entity.Current,
                DIVG = entity.DIVG,
                Description = entity.Description
            };
            return result;
        }

        public static AnalogModuleEditable ToEditable(this AnalogModule entity)
        {
            var result = new AnalogModuleEditable()
            {
                Id = entity.Id,
                Title = entity.Title,
                DIVG = entity.DIVG,
                Current = entity.Current,
                Description = entity.Description,
                Platforms = entity.Platforms.Select(platforms => platforms.ToShortView())
            };
            return result;
        }

        public static AnalogModuleBuilder GetBuilder(this AnalogModule entity)
        {
            return new AnalogModuleBuilder(entity);
        }
    }
}
