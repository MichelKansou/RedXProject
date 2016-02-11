using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Const
{
    //Les constantes relatives à l'utilisation d'IO
    public class IO
    {
        public static readonly string PathPictures = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\Pictures";
        public static readonly string PathGifs = PathPictures.Replace(@"\Pictures",@"\Gifs");
        public static readonly string PathVids = PathGifs.Replace(@"\Gifs", @"\Video");
        public static readonly string[] SupportedExtension = new string[] { ".PNG", ".BMP", ".GIF", ".JEPG", ".JPG" };
    }
}
