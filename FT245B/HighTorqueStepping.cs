using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FT245B
{
    public class HighTorqueStepping : StepBase, IDisposable, IStepMode
    {
        int[] roundCM = new int[] { 1, 3, 2, 6, 4, 12, 8, 9 };
        int[] roundCCM = new int[] { 9, 8, 12, 4, 6, 2, 3, 1 };
        int[] roundNCM = new int[] { 12, 12, 13, 9, 11, 3, 7, 6 };
        int[] roundNCCM = new int[] { 6, 7, 3, 11, 9, 13, 12, 14 };

        public void Start()
        {
            Log("Start HighTorqueStepping");
            InitBase();
        }

        public void Stop()
        {
            base.Stop();
        }

        protected override void OneRoundCM()
        {
            while (true)
            {
                if(Inverse == true)
                {
                    foreach (int item in roundCM)
                    {
                        Do(item);
                        Thread.Sleep(Sleep);
                    }
                }
                else {
                    foreach (int item in roundNCM)
                    {
                        Do(item);
                        Thread.Sleep(Sleep);
                    }
                }
            }
        }

 
        protected override void OneRoundCCM()
        {
            while (true)
            {
                if (Inverse == true)
                {
                    foreach (int item in roundCCM)
                    {
                        Do(item);
                        Thread.Sleep(Sleep);
                    }
                }
                else
                {
                    foreach (int item in roundNCCM)
                    {
                        Do(item);
                        Thread.Sleep(Sleep);
                    }
                }             
            }
        }
    }
}
