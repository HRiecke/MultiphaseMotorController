using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTD2XX_NET;

namespace FT245B
{
    public class LCDKonector
    {
        FTDI.FT_STATUS ftStatus;
        FTDI myFtdiDevice = null;

        public class MSGEvent : EventArgs
        { 
            string msg;
            public MSGEvent(string inMsg)
            {
                msg= inMsg;
            }
            public string Msg{ get {return  msg;}}
        }

         // Der Delegat muß die gleiche Signatur aufweisen wie
      // die Eventhandler-Methode.
      public delegate void EventDelegate(MSGEvent x);
      // Das Event-Objekt ist vom Typ dieses Delegaten
      public event EventDelegate MyEvent;


        public void Open()
        {
            UInt32 ftdiDeviceCount = 0;
            ftStatus = FTDI.FT_STATUS.FT_OK;

            // Create new instance of the FTDI device class
            myFtdiDevice = new FTDI();

            // Determine the number of FTDI devices connected to the machine
            ftStatus = myFtdiDevice.GetNumberOfDevices(ref ftdiDeviceCount);
            // Check status
            if (ftStatus == FTDI.FT_STATUS.FT_OK)
            {
                if(MyEvent!=null)
                    MyEvent(new MSGEvent("Number of FTDI devices: " + ftdiDeviceCount.ToString()));
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
                            if (MyEvent != null)
                            {
                                MyEvent(new MSGEvent("Device Index: " + i.ToString()));
                                MyEvent(new MSGEvent("Flags: " + String.Format("{0:x}", ftdiDeviceList[i].Flags)));
                                MyEvent(new MSGEvent("Type: " + ftdiDeviceList[i].Type.ToString()));
                                MyEvent(new MSGEvent("ID: " + String.Format("{0:x}", ftdiDeviceList[i].ID)));
                                MyEvent(new MSGEvent("Location ID: " + String.Format("{0:x}", ftdiDeviceList[i].LocId)));
                                MyEvent(new MSGEvent("Serial Number: " + ftdiDeviceList[i].SerialNumber.ToString()));
                                MyEvent(new MSGEvent("Description: " + ftdiDeviceList[i].Description.ToString()));
                                MyEvent(new MSGEvent(""));
                            }
                        }
                    }

                    ftStatus = myFtdiDevice.OpenByIndex(0);
                    if (ftStatus == FTDI.FT_STATUS.FT_OK)
                    {
                        MyEvent(new MSGEvent("Set OpenByIndex"));
                    }
                    else
                    {
                        MyEvent(new MSGEvent("Not set OpenByIndex=" + ftStatus.ToString()));
                    }

                    ftStatus = myFtdiDevice.SetBitMode(0xFF, FTD2XX_NET.FTDI.FT_BIT_MODES.FT_BIT_MODE_ASYNC_BITBANG);
                    if (ftStatus == FTDI.FT_STATUS.FT_OK)
                        MyEvent(new MSGEvent("Set bit mode async  bitbang"));
                    else
                        MyEvent(new MSGEvent("Not set bit mode async  bitbang error=" + ftStatus.ToString()));


                    ftStatus = myFtdiDevice.SetBaudRate(9600);
                    if (ftStatus == FTDI.FT_STATUS.FT_OK)
                    {
                        MyEvent(new MSGEvent("Set SetBaudRate"));
                    }
                    else
                    {
                        MyEvent(new MSGEvent("Not set SetBaudRate=" + ftStatus.ToString()));
                    }

                   
                    // Wait for a key press
                    MyEvent(new MSGEvent("Press any key to continue."));
                }
                else
                    // Wait for a key press
                    MyEvent(new MSGEvent("Failed to get number of devices (error " + ftStatus.ToString() + ")"));
            }
            else
            {
                // Wait for a key press
                MyEvent(new MSGEvent("Failed to get number of devices (error " + ftStatus.ToString() + ")"));
            }
        }
    }
}
