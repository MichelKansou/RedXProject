using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RedX.Regulator.System{
    // Information system
    [StructLayout(LayoutKind.Sequential)]
    public struct SysInfo{
        public String   Environment;
        public DateTime Date;
        public double?  PercentageCPU, PercentageRAM, PercentageDisk;

        public String[] Other; // Additional info, not implemented in the database YET.
        /// <summary>
        /// Check if SysInfo is valid
        /// </summary>
        /// <param name="info">Structure to check.</param>
        /// <returns></returns>
        public static bool CheckInfo(SysInfo info) {
            return (info.Environment != null || info.Environment.Equals("")) && info.Date != null && info.PercentageCPU != null && info.PercentageRAM != null;
        }
    }
}
