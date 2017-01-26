using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTracers.Interfaces;

namespace TestTracers.Classes
{
    public class ConsoleFormatter : ITraceResultFormatter
    {
        public void Format(TraceResult traceResult)
        {
            foreach(TreeRoot treeRoot in traceResult.GetTraceTree())
            {
                Console.WriteLine("Thread id - " + treeRoot.threadNumber);
                TreeTraversal(" ", treeRoot.GetTraceTrees());
            }
        }

        private void TreeTraversal(string indent, List<TraceSubTree> subTree)
        {
            foreach (TraceSubTree traceSubTree in subTree)
            {
                WriteMethodInfOut(traceSubTree.traceNode, indent);

                List<TraceSubTree> localSubTree = traceSubTree.GetSubTree();

                if (localSubTree != null)
                {
                    TreeTraversal(indent + "  ", localSubTree);
                }
            }
        }

        private void WriteMethodInfOut(TraceNode traceNode, string indent)
        {
            Console.WriteLine(indent + "Method - " + traceNode.methodName + ", class - " + traceNode.className 
                + ", parametrs count - " + Convert.ToString(traceNode.parametrsCount) + ", time for execute - " 
                + traceNode.timeForExecute.TotalMilliseconds);
        }
    }
}
