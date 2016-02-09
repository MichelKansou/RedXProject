using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedX.Diagnostics.Client;
using RedX.Diagnostics.Client.Exceptions;
using RedX.Launcher.Const;
using System.ServiceProcess;

namespace RedX.Launcher{
    class InitialLauncher{
        private static String ProcessName = "WindowsFormsApplication1";
        private static String LaunchOption = "--checkLauncher";

        private static void StartService(){
            using (var controller = new ServiceController("RedXService")){
                controller.Refresh();
                if (controller.Status != ServiceControllerStatus.Running)
                {
                    controller.Start();
                }
                controller.Refresh();
            }
        }

        static void Main(string[] args){
            if (args.Length == 0) {
                Console.Error.WriteLine("Veuillez entrer au moins un paramètre de démarrage. Les paramètres actuellement disponible sont: {0}", LaunchOption);
                Console.ReadKey();
                return;
            }

            if(args[0] == LaunchOption){
                try {
                    Process[] pname = Process.GetProcessesByName(ProcessName);
                    if (pname.Length >= 1){
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.WriteLine("Impossible de démarrer le programme car le processus est déjà en cours d'exécution.");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadKey();
                        return;
                    }

                    if (SystemDiagnostics.CanLaunch()){
                        var displayType = Console.ReadLine(); // Choisir entre --Text, --Image ou --Video (ce dernier ne sera pas utilisé)
                        var process = new Process();

                        process.StartInfo.FileName = Const.Const.GetCurrentDir /*@"C:\Users\Axel\Source\Repos\RedXProject\RedXDisplay\WindowsFormsApplication1\bin\Debug\"*/ + "WindowsFormsApplication1.exe"; // Définir le chemin d'accès au processus
                        process.StartInfo.Arguments = "--" + displayType;
                        process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        process.Start();
                        StartService();
                    }    
                } catch(LauncherGenericException e){
                    Console.Error.WriteLine("Le processus ne peut être démarré dû à un des composants suivant: Usage CPU ou usage RAM.");
                    Environment.Exit((int)(e.Status));
                } catch(Exception e){
                    Console.Error.WriteLine("Une erreur inconnue bloque le démarrage de l'application.");
                    Environment.Exit((int)(ExceptionStatus.INTERRUPT));
                }
            }

        }
    }
}
