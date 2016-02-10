using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Management : Form
    {
        Form1 Form;
        bool started;

        public Management(Form1 _form)
        {
            InitializeComponent();
            this.Form = _form;
            if(IO.ManageImage.GetInstance().pathGif.Count < 1)
            {
                this.trackBar1.Maximum = 0;
                this.label4.Visible = false;
            }
            //ChangeInPicture();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Length < 1)
                return;
            IO.ManageImage.GetInstance().CreateImageWithWhiteText("Bonjour", this.textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IO.ManageImage.GetInstance().Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (started)
                StopTimer();
            else
                StartTimer();
        }

        /// <summary>
        /// Demarre le timer
        /// </summary>
        private void StartTimer()
        {
            this.Form.timer1.Start();
            this.started = true;
            this.button4.Text = "Stop";
            this.button5.Visible = false;
            this.button6.Visible = false;
        }

        /// <summary>
        /// Stop le timer
        /// </summary>
        private void StopTimer()
        {
            this.Form.timer1.Stop();
            this.started = false;
            this.button4.Text = "Start";
            this.button5.Visible = true;
            this.button6.Visible = true;
        }

        private void Management_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Form.Close();
            IO.ManageImage.GetInstance().Dispose();
            this.Form.Dispose();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (this.trackBar1.Value == 1)
                ChangeInGif();
            else if(this.trackBar1.Value == 0)
                ChangeInPicture();
        }

        private void ChangeInGif()
        {
            StopTimer();
            this.groupBox1.Visible = false;
            this.groupBox2.Visible = true;
        }

        private void ChangeInPicture()
        {
            StartTimer();
            this.groupBox1.Visible = true;
            this.groupBox2.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Form.Increment();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Form.Decrement();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.numericUpDown1.Value >= 1 && this.numericUpDown1.Value <= 100)
                this.Form.timer1.Interval = (int)(this.numericUpDown1.Value * 1000);
        }
    }
}
