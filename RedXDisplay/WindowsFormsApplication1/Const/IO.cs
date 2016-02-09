using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Const{
    public class IO{
        public static readonly string Path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\Pictures";
        public static readonly string[] SupportedExtension = new string[] { ".PNG", ".BMP", ".GIF", ".JEPG", ".JPG" };
        public static readonly string TextPath = Path.Replace(@"\Pictures", @"\Input");
    }
}
