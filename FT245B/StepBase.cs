using FTD2XX_NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FT245B
{
    /// <summary>
    /// This shute be a base clas for all step modes.
    /// This class initilise teh communicont to USB interface.
    /// </summary>
    public abstract class StepBase: IDisposable,IStepMode
    {
        Thread thread;
        // Volatile is used as hint to the compiler that this data
        // member will be accessed by multiple threads.
        private volatile bool _shouldStop;
        // Flag: Has Dispose already been called?
        bool disposed = false;

        FTDI.FT_STATUS ftStatus;
        FTDI myFtdiDevice = null;
        // Define a delegate named LogHandler, which will encapsulate
        // any method that takes a string as the parameter and returns no value
        public delegate void LogHandler(string message);

        // Define an Event based on the above Delegate
        public event LogHandler LogMsg;
        private bool inverse;
        private bool isCW = true;
        //set up the SynchronizationContext
        SynchronizationContext context;

        public StepBase()
        {
            context = SynchronizationContext.Current;
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
                DisposeThread();
                myFtdiDevice.Close();
                myFtdiDevice = null;
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }


        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
        }
        /// <summary>
        /// Get the step mode name.
        /// </summary>
        public string StepName { get { return "StepBase Clase"; } }
        
        private void Write(int item)
        {
            uint writtenLength = 0;
            byte[] step = { Convert.ToByte(item) };
            if (myFtdiDevice == null) return;
            ftStatus = myFtdiDevice.Write(step, step.Length, ref writtenLength);

            if (ftStatus == FTDI.FT_STATUS.FT_OK)
                Log("Seted Write item = " + item);
            else
                Log("Not set Write=" + ftStatus.ToString());
        }

        protected void Do(int item)
        {
            try
            {
                int stepValue = Math.Abs(15 - (int)item);
                Write(stepValue);
            }
            catch (InvalidOperationException oex)
            {
                MessageBox.Show(oex.Message);
            }
        }


        protected void Log(string text)
        {
            if (LogMsg != null)
            {
                try
                {
                    context.Send(new SendOrPostCallback(delegate(object state)
                    {
                        LogMsg(text);

                    }), null);
                }
                catch (InvalidOperationException oex)
                {
                    MessageBox.Show(oex.Message);
                }                
            }
        }

        protected void InitBase()
        {
            UInt32 ftdiDeviceCount = 0;
            ftStatus = FTDI.FT_STATUS.FT_OK;

            // Create new instance of the FTDI device class
            if (myFtdiDevice != null && myFtdiDevice.IsOpen == true)
            {
                myFtdiDevice.Close();
                myFtdiDevice = null;
            }
            myFtdiDevice = new FTDI();

            // Determine the number of FTDI devices connected to the machine
            ftStatus = myFtdiDevice.GetNumberOfDevices(ref ftdiDeviceCount);
                        
            // Check status
            if (ftStatus == FTDI.FT_STATUS.FT_OK)
            {
                Log("Number of FTDI devices: " + ftdiDeviceCount.ToString());
                // If no devices available, return
                if (ftdiDeviceCount > 0)
                {
                    // Allocate storage for device info list
                    FTDI.FT_DEVICE_INFO_NODE[] ftdiDeviceList = new FTDI.FT_DEVICE_INFO_NODE[ftdiDeviceCount];

                    // Populate our device list
                    ftStatus = myFtdiDevice.GetDeviceList(ftdiDeviceList);

                    if (ftStatus == FTDI.FT_STATUS.FT_OK)
                    {
                        for (UInt32 i = 0; i < ftdiDeviceCount; i++)
                        {
                            Log("Device Index: " + i.ToString());
                            Log("Flags: " + String.Format("{0:x}", ftdiDeviceList[i].Flags));
                            Log("Type: " + ftdiDeviceList[i].Type.ToString());
                            Log("ID: " + String.Format("{0:x}", ftdiDeviceList[i].ID));
                            Log("Location ID: " + String.Format("{0:x}", ftdiDeviceList[i].LocId));
                            Log("Serial Number: " + ftdiDeviceList[i].SerialNumber.ToString());
                            Log("Description: " + ftdiDeviceList[i].Description.ToString());
                            Log("");
                        }
                    }

                    ftStatus = myFtdiDevice.OpenByIndex(0);
                    if (ftStatus == FTDI.FT_STATUS.FT_OK)
                    {
                        Log("Set OpenByIndex");
                    }
                    else
                    {
                        Log("Not set OpenByIndex=" + ftStatus.ToString());
                    }

                    ftStatus = myFtdiDevice.SetBitMode(0xFF, FTD2XX_NET.FTDI.FT_BIT_MODES.FT_BIT_MODE_ASYNC_BITBANG);
                    if (ftStatus == FTDI.FT_STATUS.FT_OK)
                        Log("Set bit mode async  bitbang");
                    else
                        Log("Not set bit mode async  bitbang error=" + ftStatus.ToString());


                    ftStatus = myFtdiDevice.SetBaudRate(9600);
                    if (ftStatus == FTDI.FT_STATUS.FT_OK)
                    {
                        Log("Set SetBaudRate");
                    }
                    else
                    {
                        Log("Not set SetBaudRate=" + ftStatus.ToString());
                    }

                    DisposeThread();
                    thread = new Thread(new ThreadStart(OneRound));
                    thread.Start();

                    // Wait for a key press
                    Log("Press any key to continue.");
                }
                else
                    // Wait for a key press
                    Log("Failed to get number of devices (error " + ftStatus.ToString() + ")");
            }
            else
            {
                // Wait for a key press
                Log("Failed to get number of devices (error " + ftStatus.ToString() + ")");
            }
        }

        private void OneRound()
        {
            while (_shouldStop == false)
            {
                int[] array;
                if (isCW == true)
                {
                    array = RoundCM;
                }
                else
                {
                    array = RoundCCM;
                }

                foreach (int item in array)
                {
                    if (inverse == true)
                        Do(item ^ 15);
                    else
                        Do(item);
                    Thread.Sleep(Sleep);
                }
            }
        }

        protected abstract int[] RoundCM {get;}
        protected abstract int[] RoundCCM { get; }       

        private void DisposeThread()
        {
            if (thread != null)
            {
                _shouldStop = true;
                bool isThreadExit = thread.Join(2*Sleep);
                if (isThreadExit == false)
                    thread.Abort();

                thread = null;
            }
        }

        #region IStepMode

        public int Sleep { get; set; }

        public void Start()
        {
            InitBase();
        }

        public void Stop()
        {
            DisposeThread();
        }

        public bool Inverse
        {
            protected get { return inverse; }
            set { inverse=value; }
        }

        /// <summary>
        /// Get or Set here state of clockwise (true) or counterclockwise (CCW)
        /// </summary>
        public bool IsCW
        {
            get { return isCW; }
            set { isCW = value; }
        }

        #endregion
    }
}
