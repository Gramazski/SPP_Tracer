using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTracers.Classes
{
    public class TraceNode
    {
        public string methodName { get; set; }
        public string className { get; set; }
        public TimeSpan timeForExecute { get; set; }
        public string parametrsCount { get; set; }
    }
}
