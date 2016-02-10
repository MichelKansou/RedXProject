using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsFormsApplication1.Const
{
    //Les constantes relatives à l'utilisation d'IO
    public class IO
    {
        public static readonly string PathPictures = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\Pictures";
        public static readonly string PathGifs = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\Gifs";
        public static readonly string[] SupportedExtension = new string[] { ".PNG", ".BMP", ".GIF", ".JEPG", ".JPG" };
    }
}
