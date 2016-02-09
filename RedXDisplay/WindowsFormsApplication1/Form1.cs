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
    public partial class Form1 : Form
    {
        private int i = 0;

        public Form1()
        {
            InitializeComponent();

            if (!Directory.Exists(Const.IO.Path))
                Directory.CreateDirectory(Const.IO.Path);

            this.timer1.Interval = 1000;
            Management man = new Management(this);
            man.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            string path = "";
            if (open.ShowDialog() == DialogResult.OK)
                path = open.FileName;
            if (path.Length < 1)
                return;
            ShowPicture(path);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try {
                ShowPicture(IO.ManageImage.GetInstance().pathPicture[i]);
                if ((i + 1) >= IO.ManageImage.GetInstance().pathPicture.Count)
                    i = 0;
                else
                    i++;
            } catch
            {

            }
        }

        private void ShowPicture(string path)
        {
            try
            {
                if (this.pictureBox1.Image != null)
                {
                    this.pictureBox1.Image.Dispose();
                    this.pictureBox2.Image.Dispose();
                    this.pictureBox3.Image.Dispose();
                    this.pictureBox4.Image.Dispose();
                }

                Bitmap img1 = new Bitmap(Image.FromFile(path), new Size(180, 180));
                this.pictureBox1.Image = img1;

                Bitmap img2 = new Bitmap(Image.FromFile(path), new Size(180, 180));
                img2.RotateFlip(RotateFlipType.Rotate90FlipNone);
                this.pictureBox2.Image = img2;

                Bitmap img3 = new Bitmap(Image.FromFile(path), new Size(180, 180));
                img3.RotateFlip(RotateFlipType.Rotate180FlipNone);
                this.pictureBox4.Image = img3;

                Bitmap img4 = new Bitmap(Image.FromFile(path), new Size(180, 180));
                img4.RotateFlip(RotateFlipType.Rotate270FlipNone);
                this.pictureBox3.Image = img4;
            }
            catch(OutOfMemoryException e)
            {
                this.pictureBox1.Image = null;
                this.pictureBox2.Image = null;
                this.pictureBox3.Image = null;
                this.pictureBox4.Image = null;
                ShowPicture(path);
            }
        }
    }
}
