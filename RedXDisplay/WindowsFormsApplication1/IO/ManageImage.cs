using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.IO
{
    /// <summary>
    /// Classe gérant les images à diffuser/créer
    /// </summary>
    public class ManageImage : IDisposable
    {
        private bool _disposed;

        private static ManageImage _instance;

        public List<string> pathPicture { get; private set; }
        public List<string> pathGif { get; private set; }

        public static ManageImage GetInstance()
        {
            if (_instance == null)
                _instance = new ManageImage();
            return _instance;
        }

        public ManageImage()
        {
            Refresh();
        }

        /// <summary>
        /// Permet de rafraichir la liste d'image à partir du dossier, tout en filtrant les extensions
        /// </summary>
        public void Refresh()
        {
            try {
                this.pathPicture = new List<string>();
                this.pathGif = new List<string>();
                System.IO.Directory.GetFiles(Const.IO.PathPictures).ToList().ForEach(
                    delegate (string s)
                    {
                        string exten = System.IO.Path.GetExtension(s).ToUpper();
                        if (Const.IO.SupportedExtension.Contains(exten))
                            this.pathPicture.Add(s);
                    });
                System.IO.Directory.GetDirectories(Const.IO.PathGifs).ToList().ForEach(
                    delegate (string s)
                    {
                        if (System.IO.Directory.GetFiles(s).Length >= 4)
                            this.pathGif.Add(s);
                    });
                Check();
            }
            catch{}
        }

        /// <summary>
        /// Vérifie qu'il n'y a pas d'erreur sur notre liste d'image
        /// </summary>
        private void Check()
        {
            if (this.pathPicture == null || this.pathPicture.Count < 1)
                throw new Exception("Pas d'image :/");
        }

        /// <summary>
        /// Créer une image fond transparent, et un texte blanc
        /// </summary>
        /// <param name="nameImage">Défini un nom d'image (sans extension)</param>
        /// <param name="text">Le texte qui apparaitra dans l'image</param>
        /// <param name="num">Si le fichier existe déjà il incrémente</param>
        public void CreateImageWithWhiteText(string nameImage, string text, int num = -1)
        {
            string pathFinal = Const.IO.PathPictures +"\\"+ nameImage;
            if (num >= 0) pathFinal += "(" + num + ")";
            pathFinal += ".png";
            //Si l'image existe déjà
            if (File.Exists(pathFinal))
            {
                //Je l'incrémente
                CreateImageWithWhiteText(nameImage, text, num + 1);
                return;
            }
            //Je créer un nouveau fichier
            File.Create(pathFinal).Dispose();
            //Je copie une image toute blanche dedans
            WindowsFormsApplication1.Properties.Resources.Sans_titre.Save(pathFinal);
            FileStream fs = new FileStream(pathFinal, FileMode.Open, FileAccess.Read);
            Image image = Image.FromStream(fs);
            fs.Close();

            Bitmap b = new Bitmap(image);
            Graphics graphics = Graphics.FromImage(b);
            //Pour chaque pixels
            for (int x = 0; x < b.Height; x++)
            {
                for (int y = 0; y < b.Width; y++)
                {
                    Color col = b.GetPixel(x, y);
                    //S'il est blanc
                    if (col.ToArgb() == Color.White.ToArgb())
                        //Il devient transparent
                        b.SetPixel(x, y, Color.Transparent);
                }
            }
            //J'écris mon texte
            graphics.DrawString(text, new Font(new FontFamily("Times New Roman"), 18f, FontStyle.Regular), Brushes.White, 0, 0);
            //Je sauvegarde l'image
            b.Save(pathFinal, image.RawFormat);
            //Je libère la mémoire
            image.Dispose();
            b.Dispose();

            Refresh();
        }

        /// <summary>
        /// Libère l'objet
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    this.pathPicture = null;
                    this.pathGif = null;
                }
                _disposed = true;
            }
        }
    }
}
