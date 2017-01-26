using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTracers.Classes;

namespace TestTracers.Interfaces
{
    public interface ITracer
    {
        // метод вызывается в начале замеряемого метода
        void StartTrace();

        // метод вызывается в конце замеряемого метода
        void StopTrace();

        // возвращает объект с результатами измерений
        TraceResult GetTraceResult();
    }
}
