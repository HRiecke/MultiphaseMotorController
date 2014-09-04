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
        IStepMode stepMode;

        public MainForm()
        {
            InitializeComponent();
            
            //init UI with default values
            radioButtonHTStep.Checked = true;
            radioButtonCW.Checked = true;            
        }
      
        private void start_Click(object sender, EventArgs e)
        {
            //InitStart();
            stepMode.Start();

            Start.Enabled = false;
            Stop.Enabled = true;
            groupBox1.Enabled = false;
        }
        

        private void stop_Click(object sender, EventArgs e)
        {           
            if (stepMode != null)
                stepMode.Stop();
            
            Start.Enabled = true;
            Stop.Enabled = false;
            groupBox1.Enabled = true;
        }

        private void InitStart()
        {
            if (stepMode != null)
            {
                stepMode.LogMsg -= Log;
                stepMode.Stop();
                ((IDisposable)stepMode).Dispose();
                stepMode = null;
            }
            if (radioButtonHTStep.Checked == true)
            {
                stepMode = new HalfStepping();
            }
            else if (radioButtonSingelStepping.Checked == true)
            {
                stepMode = new SingleStepping();
            }
            else if (radioButton3.Checked == true)
            {
                stepMode = new HighTorqueStepping();
            }
            stepMode.Sleep = (int)numericUpDown1.Value;
            stepMode.Inverse = true;
        }

        private void radioButtonCCW_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCCW.Checked == true)
            {
                radioButtonCW.Checked = false;
                stepMode.IsCW = true;
            }
            else
            {
                radioButtonCCW.Checked = false;
                stepMode.IsCW = false;
            }
        }

        private void radioButtonHTStep_CheckedChanged(object sender, EventArgs e)
        {
            InitStart();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            stepMode.LogMsg -= Log;
            if (checkBox2.Checked == true)
                stepMode.LogMsg += Log;
        }

        private void Log(string text)
        {
            this.log.Items.Add(text);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            stepMode.Sleep = (int)numericUpDown1.Value;
        }    
    }
}
