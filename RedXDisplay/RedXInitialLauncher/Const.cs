using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RedX.Launcher.Const{
    public class Const{
        private static String _GetCurrentDir = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        public static String GetCurrentDir{ get { return _GetCurrentDir; } }
    }
}
