using Microsoft.VisualBasic.Devices;
using RedX.Diagnostics.Client.Const;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedX.Diagnostics.Client.Exceptions;

namespace RedX.Diagnostics.Client{
    public class SystemDiagnostics{
        public static double[] SystemInfo(){
            var cCounter = 0;
            var rCounter = 0;

            using (var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total", true)) {
                cCounter = (int)cpuCounter.NextValue();
                System.Threading.Thread.Sleep(10000);
                cCounter = (int)cpuCounter.NextValue();

            }

            using (var ram = new PerformanceCounter("Memory", "Available MBytes", true)) {
                rCounter = (int)ram.NextValue();
                System.Threading.Thread.Sleep(10000);
                rCounter = (int)ram.NextValue();
            }

            var cInfo = new Microsoft.VisualBasic.Devices.ComputerInfo();
            double total = cInfo.TotalPhysicalMemory / 1024 / 1024;

            var percentage = (rCounter / total) * 100;
            return new double[]{cCounter, percentage};
        }

        public static bool CanLaunch(){
            var diagnostic = SystemInfo();

            if (diagnostic[0] > Util.PER_MAX_CPU)
                throw new CpuException("Quantité de CPU insuffisant.", ExceptionStatus.INTERRUPT);

            if (diagnostic[1] > Util.PER_MAX_RAM)
                throw new RamException("Quantité de RAM insuffisante.", ExceptionStatus.INTERRUPT);

            return true;
        }
    }
}
