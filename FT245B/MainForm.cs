using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FTD2XX_NET;
using System.Threading;

namespace FT245B
{
    public partial class MainForm : Form
    {
        FTDI.FT_STATUS ftStatus;
        FTDI myFtdiDevice = null;
        
        Thread thread;
        IStepMode stepMode;
        
        //set up the SynchronizationContext
        SynchronizationContext context;

        public MainForm()
        {
            InitializeComponent();
            context = SynchronizationContext.Current;
        }

        private void OneRound()
        {
            while (true)
            {
                if (checkBox1.Checked == true)
                    Do(14);
                else
                    Do(1);
                Thread.Sleep((int)numericUpDown1.Value);

                if (checkBox1.Checked == true)
                    Do(12);
                else
                    Do(3);
                Thread.Sleep((int)numericUpDown1.Value);

                if (checkBox1.Checked == true)
                    Do(13);
                else
                    Do(2);
                Thread.Sleep((int)numericUpDown1.Value);

                if (checkBox1.Checked == true)
                    Do(9);
                else
                    Do(6);
                Thread.Sleep((int)numericUpDown1.Value);

                if (checkBox1.Checked == true)
                    Do(11);
                else
                    Do(4);
                Thread.Sleep((int)numericUpDown1.Value);

                if (checkBox1.Checked == true)
                    Do(3);
                else
                    Do(12);
                Thread.Sleep((int)numericUpDown1.Value);

                if (checkBox1.Checked == true)
                    Do(7);
                else
                    Do(8);
                Thread.Sleep((int)numericUpDown1.Value);

                if (checkBox1.Checked == true)
                    Do(6);
                else
                    Do(9);
                Thread.Sleep((int)numericUpDown1.Value);
            }
        }

        void Do(int item)
        {           
            try
            {
                context.Send(new SendOrPostCallback(delegate(object state)
                {
                    int bla = Math.Abs(15 - (int)item);
                    write(bla);

                    if (checkBox2.Checked == true)
                        return;
                    this.userControl11.writeLED((int)item);                    
                    this.Refresh();

                }), null);
            }
            catch (InvalidOperationException oex)
            {
                MessageBox.Show(oex.Message);
            }
        }

        void write(int item)
        {
            uint writtenLength = 0;
            byte[] step = { Convert.ToByte(item) };
            if (myFtdiDevice == null) return;
            ftStatus = myFtdiDevice.Write(step, step.Length, ref writtenLength);

            if (checkBox2.Checked == true)
                return;

            if (ftStatus == FTDI.FT_STATUS.FT_OK)
                this.log.Items.Add("Seted Write item = " + item);
            else
                this.log.Items.Add("Not set Write=" + ftStatus.ToString());
        }

        private void SetLogText(string text)
        {
            this.log.Items.Add(text);
        }
        private void start_Click(object sender, EventArgs e)
        {
            UInt32 ftdiDeviceCount = 0;
            ftStatus = FTDI.FT_STATUS.FT_OK;

            // Create new instance of the FTDI device class
            if (myFtdiDevice!=null && myFtdiDevice.IsOpen == true)
            {
                myFtdiDevice.Close();
                myFtdiDevice = null;
            }
            myFtdiDevice = new FTDI();

            // Determine the number of FTDI devices connected to the machine
            ftStatus = myFtdiDevice.GetNumberOfDevices(ref ftdiDeviceCount);

            this.log.Items.Clear();
            // Check status
            if (ftStatus == FTDI.FT_STATUS.FT_OK)
            {
                this.log.Items.Add("Number of FTDI devices: " + ftdiDeviceCount.ToString());
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
                            this.log.Items.Add("Device Index: " + i.ToString());
                            this.log.Items.Add("Flags: " + String.Format("{0:x}", ftdiDeviceList[i].Flags));
                            this.log.Items.Add("Type: " + ftdiDeviceList[i].Type.ToString());
                            this.log.Items.Add("ID: " + String.Format("{0:x}", ftdiDeviceList[i].ID));
                            this.log.Items.Add("Location ID: " + String.Format("{0:x}", ftdiDeviceList[i].LocId));
                            this.log.Items.Add("Serial Number: " + ftdiDeviceList[i].SerialNumber.ToString());
                            this.log.Items.Add("Description: " + ftdiDeviceList[i].Description.ToString());
                            this.log.Items.Add("");
                        }
                    }

                    ftStatus = myFtdiDevice.OpenByIndex(0);
                    if (ftStatus == FTDI.FT_STATUS.FT_OK)
                    {
                        this.log.Items.Add("Set OpenByIndex");
                    }
                    else
                    {
                        this.log.Items.Add("Not set OpenByIndex=" + ftStatus.ToString());
                    }

                    ftStatus = myFtdiDevice.SetBitMode(0xFF, FTD2XX_NET.FTDI.FT_BIT_MODES.FT_BIT_MODE_ASYNC_BITBANG);
                    if (ftStatus == FTDI.FT_STATUS.FT_OK)
                        this.log.Items.Add("Set bit mode async  bitbang");
                    else
                        this.log.Items.Add("Not set bit mode async  bitbang error=" + ftStatus.ToString());


                    ftStatus = myFtdiDevice.SetBaudRate(9600);
                    if (ftStatus == FTDI.FT_STATUS.FT_OK)
                    {
                        this.log.Items.Add("Set SetBaudRate");
                    }
                    else
                    {
                        this.log.Items.Add("Not set SetBaudRate=" + ftStatus.ToString());
                    }

                    DisposeThread();
                    thread = new Thread(new ThreadStart(OneRound));
                    thread.Start();

                                    
                    // Wait for a key press
                    this.log.Items.Add("Press any key to continue.");
                }
                else   
                    // Wait for a key press
                    this.log.Items.Add("Failed to get number of devices (error " + ftStatus.ToString() + ")");
             }
            else
            {
                // Wait for a key press
                this.log.Items.Add("Failed to get number of devices (error " + ftStatus.ToString() + ")");
            }
        }

        private void DisposeThread()
        {
            if (thread != null)
            {
                thread.Abort();
                thread = null;
            }
        }
      

        private void stop_Click(object sender, EventArgs e)
        {
            // Close our device
            ftStatus = myFtdiDevice.Close();
            DisposeThread();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            InitStart();           
        }

        private void InitStart()
        {
            if (stepMode != null)
            {
                stepMode.Stop();
                stepMode = null;
            }
            if (radioButton1.Checked == true)
            {
                stepMode = new HighTorqueStepping();
            }
            else if(radioButton2.Checked==true)
            {
            }
            else if (radioButton3.Checked == true)
            {
            }
            stepMode.Sleep = (int)numericUpDown1.Value;
            stepMode.Inverse = checkBox1.Checked;
            stepMode.Start();
        }
    }
}
