using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTracers.Interfaces;
using System.Reflection;
using System.Diagnostics;
using System.Threading;

namespace TestTracers.Classes
{
    public class Tracer : ITracer
    {
        private TraceResult traceResult;
        private static Tracer instance = null;
        private Dictionary<int, Stack<TraceSubTree>> traceStacks = new Dictionary<int, Stack<TraceSubTree>>();
        private static readonly Object syncObj = new Object();

        private Tracer()
        {
            traceResult = new TraceResult();
        }

        public static Tracer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncObj)
                    {
                        if (instance == null)
                        {
                            instance = new Tracer();
                        }
                    }
                }
                return instance;
            }
        }

        public void StartTrace()
        {
            lock (syncObj)
            {
                AddToTraceTree(InitialTraceNode());
            }
            
        }

        public void StopTrace()
        {
            lock (syncObj)
            {
                AddTotalTimeToTraceTree(DateTime.Now.TimeOfDay);
            }  
        }

        private TraceNode GetMethodInfo()
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(3).GetMethod();

            TraceNode traceNode = new TraceNode();

            traceNode.className = methodBase.DeclaringType.ToString();
            traceNode.methodName = methodBase.Name;
            traceNode.parametrsCount = Convert.ToString(methodBase.GetParameters().Length);
            traceNode.timeForExecute = new TimeSpan();

            return traceNode;
        }

        private void AddToTraceTree(TraceNode value)
        {
            int currentThreadId = Thread.CurrentThread.ManagedThreadId;
            Stack<TraceSubTree> traceStack;

            if (traceStacks.ContainsKey(currentThreadId))
            {
                traceStack = traceStacks[currentThreadId];
                if (traceStack.Count != 0)
                {
                    traceStack.Push(traceStack.Peek().Add(value));
                }
                else
                {
                    traceStack.Push(traceResult.AddToTree(value, false));
                }
            }
            else
            {
                traceStack = new Stack<TraceSubTree>();
                traceStacks.Add(currentThreadId, traceStack);
                traceStack.Push(traceResult.AddToTree(value, true));
            }
        }

        private void AddTotalTimeToTraceTree(TimeSpan value)
        {
            traceStacks[Thread.CurrentThread.ManagedThreadId].Pop().ChangeTimeInNode(value);
        }

        private TraceNode InitialTraceNode()
        {
            TimeSpan startTime = new TimeSpan();
            startTime = DateTime.Now.TimeOfDay;
            TraceNode traceNode = new TraceNode();

            traceNode = GetMethodInfo();
            traceNode.timeForExecute = startTime;

            return traceNode;
        }

        public TraceResult GetTraceResult()
        {
            return traceResult;
        }
    }
}
