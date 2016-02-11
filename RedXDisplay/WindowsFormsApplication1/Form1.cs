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
        private int j = 0;

        public delegate void Shows(string path);
        public delegate void Incre();
        public delegate void Decre();

        public Form1()
        {
            InitializeComponent();
            if (!Directory.Exists(Const.IO.PathPictures))
                Directory.CreateDirectory(Const.IO.PathPictures);

            if (!Directory.Exists(Const.IO.PathGifs))
                Directory.CreateDirectory(Const.IO.PathGifs);

            if (!Directory.Exists(Const.IO.PathVids))
                Directory.CreateDirectory(Const.IO.PathVids);


            this.timer1.Interval = 1000;
            Management man = new Management(this);
            man.Show();
            //ShowGif("C:\\Users\\Alexandre\\Desktop\\Gifs\\Ours");
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

        /// <summary>
        /// Fait défiler les images toutes les X s
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            IncrementPicture();
        }

        #region Increment/Decrement
        /// <summary>
        /// Incremente l'index des images et affiche l'image associée à l'index, retourne à 0  si index est supérieur au max
        /// </summary>
        public void IncrementPicture()
        {
            try {
                ShowPicture(IO.ManageImage.GetInstance().pathPicture[i]);
                if ((i + 1) >= IO.ManageImage.GetInstance().pathPicture.Count)
                    i = 0;
                else
                    i++;
            }
            catch { }
        }

        /// <summary>
        /// Décremente l'index des images et affiche l'image associée à l'index, retourne au max si index est inférieur à 0
        /// </summary>
        public void DecrementPicture()
        {
            try {
                if ((i - 1) < 0)
                    i = IO.ManageImage.GetInstance().pathPicture.Count - 1;
                else
                    i--;
                ShowPicture(IO.ManageImage.GetInstance().pathPicture[i]);
            }
            catch { }
        }

        /// <summary>
        /// Incremente l'index des gifs et affiche le gif associé à l'index, retourne à 0  si index est supérieur au max
        /// </summary>
        public void IncrementGif()
        {
            try
            {
                ShowGif(IO.ManageImage.GetInstance().pathGif[j]);
                if ((j + 1) >= IO.ManageImage.GetInstance().pathGif.Count)
                    j = 0;
                else
                    j++;
            }
            catch { }
        }

        /// <summary>
        /// Décremente l'index des gifs et affiche le gif associé à l'index, retourne au max si index est inférieur à 0
        /// </summary>
        public void DecrementGif()
        {
            try {
                if ((j - 1) < 0)
                    j = IO.ManageImage.GetInstance().pathGif.Count - 1;
                else
                    j--;
                ShowGif(IO.ManageImage.GetInstance().pathGif[j]);
            }
            catch { }
        }
        #endregion

        #region Show
        /// <summary>
        /// Affiche un gif dans toutes les rotations
        /// </summary>
        /// <param name="path"></param>
        public void ShowGif(string path)
        {
            List<string> pathGif = System.IO.Directory.GetFiles(path).ToList();
            if (pathGif.Count < 4) throw new Exception("Erreur dossier gif");
            this.pictureBox1.ImageLocation = pathGif[0];
            this.pictureBox2.ImageLocation = pathGif[1];
            this.pictureBox3.ImageLocation = pathGif[3];
            this.pictureBox4.ImageLocation = pathGif[2];
        }

        /// <summary>
        /// Affiche une image dans toutes les rotations
        /// </summary>
        /// <param name="path">Le chemin de l'image</param>
        public void ShowPicture(string path)
        {
            try
            {
                //Si l'image de la boite à image n'est pas null
                if (this.pictureBox1.Image != null)
                {
                    //Je libère la mémoire
                    this.pictureBox1.Image.Dispose();
                    this.pictureBox2.Image.Dispose();
                    this.pictureBox3.Image.Dispose();
                    this.pictureBox4.Image.Dispose();
                }

                //Attribution et rotation des images
                Bitmap img1 = new Bitmap(Image.FromFile(path), new Size(180, 180));
                img1.RotateFlip(RotateFlipType.RotateNoneFlipX);
                this.pictureBox1.Image = img1;

                Bitmap img2 = new Bitmap(Image.FromFile(path), new Size(180, 180));
                img2.RotateFlip(RotateFlipType.Rotate90FlipY);
                this.pictureBox2.Image = img2;

                Bitmap img3 = new Bitmap(Image.FromFile(path), new Size(180, 180));
                img3.RotateFlip(RotateFlipType.Rotate180FlipX);
                this.pictureBox4.Image = img3;

                Bitmap img4 = new Bitmap(Image.FromFile(path), new Size(180, 180));
                img4.RotateFlip(RotateFlipType.Rotate270FlipY);
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

        public void ShowVideo(string path)
        {
            try
            {
                this.axWindowsMediaPlayer1.Visible = true;
                this.axWindowsMediaPlayer1.URL = path;
                this.axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            catch { }
        }
        #endregion

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.axWindowsMediaPlayer1.Height = this.Height -45;
            this.axWindowsMediaPlayer1.Width = this.Width;
            int with = this.Width - this.panel1.Width;
            int hei = this.Height - this.panel1.Height;
            this.panel1.Location = new Point(with / 2, hei / 2);
        }
    }
}
