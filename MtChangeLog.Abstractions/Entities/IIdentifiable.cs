using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Abstractions.Entities
{
    public interface IIdentifiable
    {
        Guid Id { get; set; }
    }
}
