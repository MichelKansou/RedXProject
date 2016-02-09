using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedX.Regulator.System{
    // Information system
    public struct SysInfo{
        public String   Environment;
        public DateTime Date;
        public double?  PercentageCPU, PercentageRAM, PercentageDisk;


        public String[] Other;

        public static bool CheckInfo(SysInfo info) {
            return (info.Environment != null || info.Environment.Equals("")) && info.Date != null && info.PercentageCPU != null && info.PercentageRAM != null;
        }
    }
}
