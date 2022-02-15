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
    public static class RelayAlgorithmExtensions
    {
        public static RelayAlgorithmShortView ToShortView(this RelayAlgorithm entity)
        {
            var result = new RelayAlgorithmShortView()
            {
                Id = entity.Id,
                Title = entity.Title
            };
            return result;
        }

        public static RelayAlgorithmEditable ToEditable(this RelayAlgorithm entity)
        {
            var result = new RelayAlgorithmEditable()
            {
                Id = entity.Id,
                Group = entity.Group,
                Title = entity.Title,
                ANSI = entity.ANSI,
                LogicalNode = entity.LogicalNode,
                Description = entity.Description
            };
            return result;
        }

        public static RelayAlgorithmBuilder GetBuilder(this RelayAlgorithm entity)
        {
            return new RelayAlgorithmBuilder(entity);
        }
    }
}
