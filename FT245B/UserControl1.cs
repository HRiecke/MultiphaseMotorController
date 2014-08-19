using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FT245B
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();

            //ovalShape2.Location = new System.Drawing.Point((int)(Math.Cos(45.0) * Width / 2 + Width / 2), (int)(Math.Sin(45.0) * Width/2 + Width/2));
        }  
        
        public void writeLED(int item)
        {
            this.ovalShape1.FillColor = Color.Gray;
            this.ovalShape2.FillColor = Color.Gray;
            this.ovalShape3.FillColor = Color.Gray;
            this.ovalShape4.FillColor = Color.Gray;
            this.ovalShape5.FillColor = Color.Gray;
            this.ovalShape6.FillColor = Color.Gray;
            this.ovalShape7.FillColor = Color.Gray;
            this.ovalShape8.FillColor = Color.Gray;
            switch (item)
            {
                case 1: this.ovalShape1.FillColor = Color.Green;
                    lineShape1.X1 = this.Width/2;
                    lineShape1.Y1 = 20;
                    lineShape1.X2 = this.Height/2;
                    lineShape1.Y2 = this.Width - 20;
                    break;
                case 3: this.ovalShape1.FillColor = Color.Green;
                    this.ovalShape2.FillColor = Color.LightGreen;
                    this.ovalShape3.FillColor = Color.Green;
                    lineShape1.X1 = (int)(Math.Cos(45.0) * Width / 2 + Width / 2);
                    lineShape1.Y1 = (int)(Math.Sin(45.0) * Width / 2 - 20);
                    lineShape1.X2 = (int)(Math.Cos(45.0) * this.Width / 2);
                    lineShape1.Y2 = (int)(Math.Sin(45.0) * Width - 20);
                    break;
                case 2: this.ovalShape3.FillColor = Color.Green;
                    lineShape1.X1 = this.Height - 20;
                    lineShape1.Y1 = this.Height /2;
                    lineShape1.X2 = 20;
                    lineShape1.Y2 = this.Width/2;
                    break;
                case 6: this.ovalShape3.FillColor = Color.Green;
                    this.ovalShape4.FillColor = Color.LightGreen;
                    this.ovalShape5.FillColor = Color.Green;
                    break;
                case 4: this.ovalShape5.FillColor = Color.Green;
                    
                    break;
                case 12: this.ovalShape5.FillColor = Color.Green;
                    this.ovalShape6.FillColor = Color.LightGreen;
                    this.ovalShape7.FillColor = Color.Green;
                    break;
                case 8: this.ovalShape7.FillColor = Color.Green;                    
                    break;
                case 9: this.ovalShape7.FillColor = Color.Green;
                    this.ovalShape8.FillColor = Color.LightGreen;                    
                    this.ovalShape1.FillColor = Color.Green;
                    break;
                default: break;
            }
        }
    }
}
