using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedX.Diagnostics.Client;
using RedX.Diagnostics.Client.Exceptions;
using RedX.Launcher.Const;

namespace RedX.Launcher{
    class InitialLauncher{
        private static String ProcessName = "RedXApplication";
        private static String LaunchOption = "--checkLauncher";

        static void Main(string[] args){
            if (args.Length == 0) {
                Console.Error.WriteLine("Veuillez entrer au moins un paramètre de démarrage. Les paramètres actuellement disponible sont: {0}", LaunchOption);
                Console.ReadKey();
                return;
            }

            if(args[0] == LaunchOption){
                Process[] pname = Process.GetProcessesByName(ProcessName);
                if (pname.Length >= 1){
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine("Impossible de démarrer le programme car le processus est déjà en cours d'exécution.");
                    Console.ReadKey();
                    return;
                }

                try{
                    if (SystemDiagnostics.CanLaunch()){
                        var displayType = Console.ReadLine(); // Choisir entre --Text, --Image ou --Video (ce dernier ne sera pas utilisé)
                        var process = new Process();

                        process.StartInfo.FileName =  Const.Const.GetCurrentDir + "RedXDisplay_WinForm.exe"; // Définir le chemin d'accès au processus
                        process.StartInfo.Arguments = "--" + displayType;
                        process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                        process.Start();
                    }

                        
                } catch(LauncherGenericException e){
                    Console.Error.WriteLine("Le processus ne peut être démarré dû à un des composants suivant: Usage CPU ou usage RAM.");
                    Environment.Exit((int)(e.Status));
                }
            }

        }
    }
}
