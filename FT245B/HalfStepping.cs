using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FT245B
{
    public class HalfStepping : StepBase, IDisposable, IStepMode
    {
        //int[] roundNCM = new int[] { 14, 12, 13, 9, 11, 3, 7, 6 };
        //int[] roundNCCM = new int[] { 6, 7, 3, 11, 9, 13, 12, 14 };

        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Protected implementation of Dispose pattern.
        protected new virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
                base.Dispose();
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }


        // Public implementation of Dispose pattern callable by consumers.
        public new void Dispose()
        {
            Dispose(true);
        }
        /// <summary>
        /// Get the step mode name.
        /// </summary>
        public new string StepName { get { return "Half Stepping"; } }
        /// <summary>
        /// 
        /// </summary>
        public new void Start()
        {
            Log("Start " + StepName);
            InitBase();
        }

        public new void Stop()
        {
            base.Stop();
        }

        protected override int[] RoundCM
        {
            get
            {
                return new int[] { 1, 3, 2, 6, 4, 12, 8, 9 }; 
            }
        }
        protected override int[] RoundCCM
        {
            get
            {
                return new int[] { 9, 8, 12, 4, 6, 2, 3, 1 }; ;
            }
        }
    }
}
