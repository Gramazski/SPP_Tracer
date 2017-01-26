using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTracers.Classes;
using System.Threading;

namespace ForTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread myThread = new Thread(Sleep3); 

            myThread.Start();
            Tracer.Instance.StartTrace();

            Sleep4();
            Thread.Sleep(500);
            Tracer.Instance.StopTrace();

            ThreadCreator();

            myThread.Join();

            XmlFormatter xmlFormatter = new XmlFormatter("ThreadsTree.xml");
            xmlFormatter.Format(Tracer.Instance.GetTraceResult());

            ConsoleFormatter consoleFormatter = new ConsoleFormatter();
            consoleFormatter.Format(Tracer.Instance.GetTraceResult());

            Console.ReadLine();
        }

        static void Sleep1()
        {
            Tracer.Instance.StartTrace();
            Thread.Sleep(100);
            Tracer.Instance.StopTrace();
            Tracer.Instance.StartTrace();
            Thread.Sleep(100);
            Tracer.Instance.StopTrace();
        }

        static void Sleep2()
        {
            Tracer.Instance.StartTrace();
            Thread.Sleep(200);

            Tracer.Instance.StopTrace();
        }

        static void Sleep3()
        {
            Thread myThread = new Thread(Sleep1);
            myThread.Start();

            Tracer.Instance.StartTrace();
            Thread.Sleep(300);
            Sleep2();
            Tracer.Instance.StopTrace();

            myThread.Join();
        }

        static void Sleep4()
        {
            Tracer.Instance.StartTrace();
            for (int i = 0; i < 10; i++)
            {
                Sleep5();
            }
            Thread.Sleep(400);
            Tracer.Instance.StopTrace();
        }

        static void Sleep5()
        {
            Tracer.Instance.StartTrace();
            Thread.Sleep(100);
            Tracer.Instance.StopTrace();
        }

        static void Sleep6()
        {
            Tracer.Instance.StartTrace();
            Thread.Sleep(100);
            Tracer.Instance.StopTrace();
        }

        static void ThreadCreator()
        {
            Tracer.Instance.StartTrace();
            List<Thread> threadList = new List<Thread>();
            for (int i = 0; i< 5; i++)
            {
                threadList.Add(new Thread(Sleep6));
                threadList[i].Start();
            }

            foreach(Thread currentThread in threadList)
            {
                currentThread.Join();
            }

            Tracer.Instance.StopTrace();
        }
    }
}
