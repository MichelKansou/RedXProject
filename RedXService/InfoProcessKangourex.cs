static void Main(string[] args)
        {
            PerformanceCounter cpuCounter;
            PerformanceCounter ram;

            // Initialise l'objet en spécifiant les informations à aller chercher
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total", true);
            ram = new PerformanceCounter("Memory", "Available MBytes", true);


            // Associe la valeur courante du CPU en pourcent et met à jour l'affichage de la progress bar
            int i = (int)cpuCounter.NextValue();
            System.Threading.Thread.Sleep(10000);
            i = (int)cpuCounter.NextValue();
            Console.WriteLine("CPU : " + i + " %");

            i = (int)ram.NextValue();
            System.Threading.Thread.Sleep(10000);
            i = (int)ram.NextValue();
            Console.WriteLine("RAM Avai : " + i +"MB");

            Microsoft.VisualBasic.Devices.ComputerInfo k = new Microsoft.VisualBasic.Devices.ComputerInfo();
            double total = k.TotalPhysicalMemory/1024/1024;
            double effectif = i;
            double per = (effectif / total)*100;
            Console.WriteLine(effectif + "/" + total);
            Console.WriteLine(per +"%");
            Console.Read();
        }
