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
            StartTimer();
            started = false;
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


        private void StartTimer()
        {
            this.Form.timer1.Start();
            this.started = true;
            this.button4.Text = "Stop";
        }

        private void StopTimer()
        {
            this.Form.timer1.Stop();
            this.started = false;
            this.button4.Text = "Start";
        }
    }
}
