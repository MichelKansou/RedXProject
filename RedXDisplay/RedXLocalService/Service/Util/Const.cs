using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RedX.Service.Util{
    public class Const{
        #region Output Strings
        // Standard output
        private static String _STD_OUTPATH = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\STD_Out";
        private static String _STD_OUTFILE = "hS00" + Math.Round(System.DateTime.Now.TimeOfDay.TotalSeconds, 0) + ".sout";

        public static String OutPath { get { return _STD_OUTPATH; } set { _STD_OUTPATH = value ?? _STD_OUTPATH; } }
        public static String OutFile { get { return _STD_OUTFILE; } }

        // Custom log output
        private static String _LOG_OUTPATH = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\Logs";
        private static String _LOG_OUTFILE = "l" + Math.Round(System.DateTime.Now.TimeOfDay.TotalSeconds, 0) + ".slog";

        public static String LogPath { get { return _LOG_OUTPATH; } }
        public static String LogFile { get { return _LOG_OUTFILE; } }

        public static readonly long Interval = 300000;

        // Target (dir to save) 
        private static String _ORIGIN_PATH = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\..\Test\";

        public static String ORIGINPATH { get { return _ORIGIN_PATH; } set { _ORIGIN_PATH = value ?? _ORIGIN_PATH; } }

        #endregion

        #region Other
        private static String Guid = typeof(Const).GUID.ToString();

        public static String GUID { get { return Guid; } }

        public static string GetOS(){
            return Environment.OSVersion.VersionString;
        }
        #endregion


    }
}
