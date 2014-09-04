using System;
namespace FT245B
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
               components.Dispose();                         
             }

            if (stepMode != null)
            {
                stepMode.LogMsg -= Log;
                stepMode.Stop();
                ((IDisposable)stepMode).Dispose();
                stepMode = null;
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.log = new System.Windows.Forms.ListBox();
            this.Start = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.radioButtonHTStep = new System.Windows.Forms.RadioButton();
            this.radioButtonSingelStepping = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonCCW = new System.Windows.Forms.RadioButton();
            this.radioButtonCW = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // log
            // 
            this.log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.log.FormattingEnabled = true;
            this.log.Location = new System.Drawing.Point(12, 160);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(356, 160);
            this.log.TabIndex = 0;
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(214, 112);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(75, 23);
            this.Start.TabIndex = 2;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.start_Click);
            // 
            // Stop
            // 
            this.Stop.Enabled = false;
            this.Stop.Location = new System.Drawing.Point(295, 112);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(75, 23);
            this.Stop.TabIndex = 3;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(70, 64);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(79, 20);
            this.numericUpDown1.TabIndex = 8;
            this.numericUpDown1.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Speed (ms)";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(12, 141);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(40, 17);
            this.checkBox2.TabIndex = 10;
            this.checkBox2.Text = "log";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // radioButtonHTStep
            // 
            this.radioButtonHTStep.AutoSize = true;
            this.radioButtonHTStep.Location = new System.Drawing.Point(6, 45);
            this.radioButtonHTStep.Name = "radioButtonHTStep";
            this.radioButtonHTStep.Size = new System.Drawing.Size(129, 17);
            this.radioButtonHTStep.TabIndex = 11;
            this.radioButtonHTStep.Text = "High Torque Stepping";
            this.radioButtonHTStep.UseVisualStyleBackColor = true;
            this.radioButtonHTStep.CheckedChanged += new System.EventHandler(this.radioButtonHTStep_CheckedChanged);
            // 
            // radioButtonSingelStepping
            // 
            this.radioButtonSingelStepping.AutoSize = true;
            this.radioButtonSingelStepping.Location = new System.Drawing.Point(6, 22);
            this.radioButtonSingelStepping.Name = "radioButtonSingelStepping";
            this.radioButtonSingelStepping.Size = new System.Drawing.Size(79, 17);
            this.radioButtonSingelStepping.TabIndex = 12;
            this.radioButtonSingelStepping.Text = "Single Step";
            this.radioButtonSingelStepping.UseVisualStyleBackColor = true;
            this.radioButtonSingelStepping.CheckedChanged += new System.EventHandler(this.radioButtonHTStep_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(6, 68);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(69, 17);
            this.radioButton3.TabIndex = 13;
            this.radioButton3.Text = "Half Step";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButtonHTStep_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButtonHTStep);
            this.groupBox1.Controls.Add(this.radioButtonSingelStepping);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(176, 94);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Step Mode";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 16;
            // 
            // radioButtonCCW
            // 
            this.radioButtonCCW.AutoSize = true;
            this.radioButtonCCW.Location = new System.Drawing.Point(6, 42);
            this.radioButtonCCW.Name = "radioButtonCCW";
            this.radioButtonCCW.Size = new System.Drawing.Size(50, 17);
            this.radioButtonCCW.TabIndex = 18;
            this.radioButtonCCW.TabStop = true;
            this.radioButtonCCW.Text = "CCW";
            this.radioButtonCCW.UseVisualStyleBackColor = true;
            this.radioButtonCCW.CheckedChanged += new System.EventHandler(this.radioButtonCCW_CheckedChanged);
            // 
            // radioButtonCW
            // 
            this.radioButtonCW.AutoSize = true;
            this.radioButtonCW.Location = new System.Drawing.Point(6, 19);
            this.radioButtonCW.Name = "radioButtonCW";
            this.radioButtonCW.Size = new System.Drawing.Size(43, 17);
            this.radioButtonCW.TabIndex = 19;
            this.radioButtonCW.TabStop = true;
            this.radioButtonCW.Text = "CW";
            this.radioButtonCW.UseVisualStyleBackColor = true;
            this.radioButtonCW.CheckedChanged += new System.EventHandler(this.radioButtonCCW_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDown1);
            this.groupBox2.Controls.Add(this.radioButtonCW);
            this.groupBox2.Controls.Add(this.radioButtonCCW);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(194, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(176, 94);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Step Mode Details";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 331);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.log);
            this.Name = "MainForm";
            this.Text = "Multiphase Motor Controller";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox log;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.RadioButton radioButtonHTStep;
        private System.Windows.Forms.RadioButton radioButtonSingelStepping;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButtonCCW;
        private System.Windows.Forms.RadioButton radioButtonCW;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

