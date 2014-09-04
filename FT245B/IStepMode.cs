using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FT245B
{
    /// <summary>
    /// base interface for step mode implentaiones.
    /// </summary>
    public interface IStepMode
    {
        /// <summary>
        /// Get the step mode name.
        /// </summary>
        string StepName{get;}
        /// <summary>
        /// Start motor stepping.
        /// </summary>
        void Start();
        /// <summary>
        /// Stopt motor stepping.
        /// </summary>
        void Stop();
        /// <summary>
        /// Get or Set hier interneal sleep time.
        /// ~ control with this motor spead 
        /// </summary>
        int Sleep { get; set; }
        /// <summary>
        /// Set hear CW or CCW.
        /// </summary>
        bool Inverse { set; }
        /// <summary>
        /// Event hndler for internal log informations.
        /// </summary>
        event FT245B.StepBase.LogHandler LogMsg;
        /// <summary>
        /// Get or Set here state of clockwise (true) or counterclockwise (CCW) 
        /// </summary>
        bool IsCW { get; set; }
    }
}
