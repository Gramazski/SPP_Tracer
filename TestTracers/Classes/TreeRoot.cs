using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTracers.Classes
{
    public class TreeRoot
    {
        public int threadNumber { get; private set; }
        public TimeSpan executeTime { get { return GetTotalExeciteTime(); } }
        private readonly List<TraceSubTree> traceTrees = new List<TraceSubTree>();

        public TraceSubTree Add(int thread, TraceNode method)
        {
            TraceSubTree localTraceTree = new TraceSubTree();

            threadNumber = thread;
            traceTrees.Add(localTraceTree);
            traceTrees[traceTrees.Count - 1].traceNode = method;

            return traceTrees[traceTrees.Count - 1];
        }

        public List<TraceSubTree> GetTraceTrees()
        {
            return traceTrees;
        }

        private TimeSpan GetTotalExeciteTime()
        {
            TimeSpan totalTime = new TimeSpan(0);

            foreach(TraceSubTree subTree in traceTrees)
            {
                totalTime += subTree.traceNode.timeForExecute;
            }

            return totalTime;
        }

    }
}
