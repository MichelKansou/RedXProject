using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedX.Diagnostics.Client.Const{
    internal class Util{
        // Maximum percentage for RAM / CPU and Disk
        private static double Per_Max_Ram  = 80;
        private static double Per_Max_Cpu  = 50;
        private static double Per_Max_Disk = 20;
            
        public  static double PER_MAX_RAM{
            get { return Per_Max_Ram; }
        }

        public static double PER_MAX_CPU{
            get { return Per_Max_Cpu; }
        }

        public static double PER_MAX_DISK{
            get { return Per_Max_Disk; }
        }

    }
}
