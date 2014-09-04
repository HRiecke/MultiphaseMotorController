using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FT245B
{
    public class SingleStepping : StepBase, IDisposable, IStepMode
    {
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
        public string StepName { get { return "Single Stepping?"; } }
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
                return new int[] { 1, 2, 4, 8 }; ;
            }
        }
        protected override int[] RoundCCM
        {
            get
            {
                return new int[] { 8, 4, 2, 1 }; ;
            }
        }
    }
}
