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
using System.Linq.Expressions;
using RedXInitialLauncher.Properties;

namespace RedX.Launcher{
    internal class InitialLauncher{
        // Prerequisites
        #region Prerequisites & constants
        private readonly  static String ProcessName = "WindowsFormsApplication1";
        private readonly  static String LaunchOption = "--checkLauncher";
        #endregion


        private delegate void Show(Action<String> Predicate, params String[] Inputs);
        private readonly static Action<String> lambda_Cnl = (@in) => { Console.WriteLine(@in); };
        private readonly static Action<String> lambda_Err = (@in) => { Console.Error.WriteLine(@in); };
        private readonly static Action<String> ProcessStart = (displayType) => {
            var process = new Process();
            process.StartInfo.FileName = Const.Const.GetCurrentDir + "\\" + ProcessName + ".exe"; // Définir le chemin d'accès au processus
            process.StartInfo.Arguments = displayType; //Unlock when parameters available
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.Start();
        };

        // Service start, if the service isn't active, we run it. Otherwise, nothing is done.
        /// <summary>
        /// Starting Service
        /// </summary>
        private static void StartService(){
            ImpersonateUser iu = new ImpersonateUser();
            
            iu.Impersonate(@".", "Axel", Settings.Default.Password);

            using (var controller = new ServiceController("RedXService")){
                ServiceControllerPermission scp = new ServiceControllerPermission(ServiceControllerPermissionAccess.Control, ".", "RedXService");
                scp.Assert();

                controller.Refresh();
                if (controller.Status != ServiceControllerStatus.Running){
                    controller.Start();
                }
                controller.WaitForStatus(ServiceControllerStatus.StartPending);
                controller.WaitForStatus(ServiceControllerStatus.Running);

                controller.Refresh();
            }
  
            iu.Undo();
        }

        /// <summary>
        /// Uses Predicate (Action) to display text.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="input"></param>
        private static void ShowText(Action<String> predicate, params String[] input){
            var outputString = "";
            foreach (var str in input)
                outputString += str;
            predicate.Invoke(outputString);
        }

        static void Main(string[] args){
            Show TextDisplay = ShowText;

            // If args size is 0 or null, then we leave
            if (args.Length == 0) {
                TextDisplay(lambda_Err, "Veuillez entrer au moins un paramètre de démarrage.Les paramètres actuellement disponible sont ", LaunchOption);
                Console.ReadKey();
                return;
            }

            // Else, if the option is okay
            if(args[0] == LaunchOption){
                try {
                    Process[] pname = Process.GetProcessesByName(ProcessName);
                    if (pname.Length >= 1){
                        Console.ForegroundColor = ConsoleColor.Red;
                        TextDisplay(lambda_Err, "Impossible de démarrer le programme car le processus est déjà en cours d'exécution.");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadKey();
                        return;
                    }

                    if (SystemDiagnostics.CanLaunch()){
                        TextDisplay(lambda_Cnl, "Veuillez choisir une option entre --Text");//, --Image et --Video.")
                        //var displayType = // Choisir entre --Text, --Image ou --Video (ce dernier ne sera pas utilisé)
                        ProcessStart.Invoke(Console.ReadLine());
                        StartService();
                    }    
                } catch(RamException e){
                    TextDisplay(lambda_Err, "Le processus ne peut pas être démarré. Raison : ", e.ToString());
                    Environment.Exit((int)(e.Status));
                }
                catch (CpuException e){
                    TextDisplay(lambda_Err, "Le processus ne peut pas être démarré. Raison : ", e.ToString());
                    Environment.Exit((int)(e.Status));
                }
                catch (LauncherGenericException e){
                    TextDisplay(lambda_Err, "Le processus ne peut pas être démarré. Raison : ", e.ToString());
                    Environment.Exit((int)(e.Status));
                }
                catch (Exception e){
                    TextDisplay(lambda_Err, "Une erreur inconnue bloque le démarrage de l'application: ", e.Message, e.StackTrace);
                    Environment.Exit((int)(ExceptionStatus.INTERRUPT));
                }
            }

        }
    }

    
}
