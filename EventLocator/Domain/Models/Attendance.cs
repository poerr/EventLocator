using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLocator.Domain.Models
{
    public enum Attendance
    {
        UNDER_1000 = 1,
        FROM_1000_TO_5000 = 2,
        FROM_5000_TO_10000 = 3,
        OVER_10000 = 4
    }
}
