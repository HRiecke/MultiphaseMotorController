using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FT245B
{
    public interface IStepMode
    {
        void Start();
        void Stop();
        int Sleep { get; set; }
        bool Inverse { set; }
        event FT245B.StepBase.LogHandler LogMsg;
    }
}
