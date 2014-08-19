using FTD2XX_NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FT245B
{
    public abstract class StepBase: IDisposable,IStepMode
    {
        Thread thread;

        FTDI.FT_STATUS ftStatus;
        FTDI myFtdiDevice = null;
        // Define a delegate named LogHandler, which will encapsulate
        // any method that takes a string as the parameter and returns no value
        public delegate void LogHandler(string message);

        // Define an Event based on the above Delegate
        public event LogHandler LogMsg;
        private int sleep;
        private bool inverse;

        private void Write(int item)
        {
            uint writtenLength = 0;
            byte[] step = { Convert.ToByte(item) };
            if (myFtdiDevice == null) return;
            ftStatus = myFtdiDevice.Write(step, step.Length, ref writtenLength);

            //if (checkBox2.Checked == true)
              //  return;

            if (ftStatus == FTDI.FT_STATUS.FT_OK)
                Log("Seted Write item = " + item);
            else
                Log("Not set Write=" + ftStatus.ToString());
        }

        protected void Do(int item)
        {
            try
            {
         //       context.Send(new SendOrPostCallback(delegate(object state)
                //{
                    int bla = Math.Abs(15 - (int)item);
                    Write(bla);

                 //   if (checkBox2.Checked == true)
                   //     return;
               //     this.userControl11.writeLED((int)item);
             //       this.Refresh();

           //     }), null);
            }
            catch (InvalidOperationException oex)
            {
                MessageBox.Show(oex.Message);
            }
        }


        protected void Log(string text)
        {
            if (LogMsg != null)
                LogMsg(text);
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
            if (inverse == true)
            {
                OneRoundCM();
            }
            else {
                OneRoundCCM();
            }
        }

        protected abstract void OneRoundCM();
        protected abstract void OneRoundCCM();

        private void DisposeThread()
        {
            if (thread != null)
            {
                thread.Abort();
                thread = null;
            }
        }

        public void Dispose()
        {
            DisposeThread();
        }

        public void Start()
        {
            InitBase();
        }

        public void Stop()
        {
            DisposeThread();
        }

        public int Sleep { get; set; }

        public bool Inverse
        {
            protected get { return inverse; }
            set { inverse=value; }
        }        
    }
}
