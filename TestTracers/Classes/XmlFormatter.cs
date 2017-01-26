using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTracers.Interfaces;
using System.Xml.Linq;

namespace TestTracers.Classes
{
    public class XmlFormatter : ITraceResultFormatter
    {
        private string fileName;

        public XmlFormatter(string fileName)
        {
            this.fileName = fileName;
        }

        public void Format(TraceResult traceResult)
        {
            XDocument xDocument = new XDocument();
            XElement root = new XElement("root");
            
            foreach (TreeRoot treeRoot in traceResult.GetTraceTree())
            {
                XElement threadElement = new XElement("thread");
                threadElement.SetAttributeValue("id", treeRoot.threadNumber);
                threadElement.SetAttributeValue("timeForExecute", treeRoot.executeTime.TotalMilliseconds);

                TreeTraversal(threadElement, treeRoot.GetTraceTrees());

                root.Add(threadElement);
            }

            xDocument.Add(root);
            xDocument.Save(fileName);
        }

        private void TreeTraversal(XElement inheritElement, List<TraceSubTree> subTree)
        {
            foreach (TraceSubTree traceSubTree in subTree)
            {
                XElement methodElement = new XElement("method");

                WriteMethodInfOut(traceSubTree.traceNode, methodElement);

                inheritElement.Add(methodElement);

                List<TraceSubTree> localSubTree = traceSubTree.GetSubTree();

                if (localSubTree != null)
                {
                    TreeTraversal(methodElement, localSubTree);
                }
            }
        }

        private void WriteMethodInfOut(TraceNode traceNode, XElement methodElement)
        {
            methodElement.SetAttributeValue("methodName", traceNode.methodName);
            methodElement.SetAttributeValue("className", traceNode.className);
            methodElement.SetAttributeValue("parametrsCount", traceNode.parametrsCount);
            methodElement.SetAttributeValue("totalTime", traceNode.timeForExecute.TotalMilliseconds); 
        }
    }
}
