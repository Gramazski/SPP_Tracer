using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TestTracers.Classes
{
    public class TraceResult
    {
        private List<TreeRoot> traceTree = new List<TreeRoot>();

        public TraceSubTree AddToTree(TraceNode value, bool newThreadFlag)
        {
            TreeRoot localTreeRoot = new TreeRoot();

            localTreeRoot = FindInTreeByThreadNumber(Thread.CurrentThread.ManagedThreadId);

            if (localTreeRoot != null)
            {
                return localTreeRoot.Add(Thread.CurrentThread.ManagedThreadId, value);
            }
            else
            {
                traceTree.Add(new TreeRoot());
                return traceTree[traceTree.Count - 1].Add(Thread.CurrentThread.ManagedThreadId, value);
            }
        }

        private TreeRoot FindInTreeByThreadNumber(int threadNumber)
        {
            foreach(TreeRoot treeRoot in traceTree)
            {
                if (treeRoot.threadNumber == threadNumber)
                {
                    return treeRoot;
                }
            }

            return null;
        }

        public List<TreeRoot> GetTraceTree()
        {
            return traceTree;
        }
    }
}
