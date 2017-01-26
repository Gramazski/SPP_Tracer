using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTracers.Classes;

namespace TestTracers.Interfaces
{
    interface ITraceResultFormatter
    {
        void Format(TraceResult traceResult);
    }
}
