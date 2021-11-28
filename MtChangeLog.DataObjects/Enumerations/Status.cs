using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Enumerations
{
    public enum Status
    {
        Actual,
        Deprecated,
        Technological,
        Test
    }
}
