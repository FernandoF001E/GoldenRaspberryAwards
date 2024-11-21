using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Data
{
    public class IntervalResponse
    {
        public required List<IntervalItem> Min { get; set; }
        public required List<IntervalItem> Max { get; set; }
    }
}
