using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTracers.Classes
{
    public class TraceSubTree
    {
        public TraceNode traceNode { get; set; }
        private List<TraceSubTree> traceTrees = new List<TraceSubTree>();

        public TraceSubTree Add(TraceNode newTraceNode)
        {
            TraceSubTree localTraceTree = new TraceSubTree();

            traceTrees.Add(localTraceTree);
            traceTrees[traceTrees.Count - 1].traceNode = newTraceNode;

            return traceTrees[traceTrees.Count - 1];
        }

        public void ChangeTimeInNode(TimeSpan newTraceNode)
        {
            traceNode.timeForExecute = newTraceNode - traceNode.timeForExecute;

        }

        public List<TraceSubTree> GetSubTree()
        {
            return traceTrees;
        }
    }
}
