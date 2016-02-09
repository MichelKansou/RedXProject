using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.IO
{
    public class ManageImage
    {
        private static ManageImage _instance;

        public List<string> pathPicture { get; private set; }

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

        public void Refresh()
        {
            try {
                this.pathPicture = new List<string>();
                System.IO.Directory.GetFiles(Const.IO.Path).ToList().ForEach(
                    delegate (string s)
                    {
                        string exten = System.IO.Path.GetExtension(s).ToUpper();
                        if (Const.IO.SupportedExtension.Contains(exten))
                            this.pathPicture.Add(s);
                    });
                Check();
            }
            catch{

            }
        }

        private void Check()
        {
            if (this.pathPicture == null || this.pathPicture.Count < 1)
                throw new Exception("Pas d'image :/");
        }

        public void CreateImageWithWhiteText(string nameImage, string text, int num = -1){
            string pathFinal = Const.IO.Path +"\\"+ nameImage;
            if (num >= 0) pathFinal += "(" + num + ")";
            pathFinal += ".png";
            if (File.Exists(pathFinal))
            {
                CreateImageWithWhiteText(nameImage, text, num + 1);
                return;
            }

            File.Create(pathFinal).Dispose();
            WindowsFormsApplication1.Properties.Resources.Sans_titre.Save(pathFinal);
            FileStream fs = new FileStream(pathFinal, FileMode.Open, FileAccess.Read);
            Image image = Image.FromStream(fs);
            fs.Close();

            Bitmap b = new Bitmap(image);
            Graphics graphics = Graphics.FromImage(b);
            for (int x = 0; x < b.Height; x++)
            {
                for (int y = 0; y < b.Width; y++)
                {
                    Color col = b.GetPixel(x, y);
                    if (col.ToArgb() == Color.White.ToArgb())
                        b.SetPixel(x, y, Color.Transparent);
                }
            }
            graphics.DrawString(text, new Font(new FontFamily("Times New Roman"), 18f, FontStyle.Regular), Brushes.White, 0, 0);

            b.Save(pathFinal, image.RawFormat);

            image.Dispose();
            b.Dispose();

            Refresh();
        }
    }
}
