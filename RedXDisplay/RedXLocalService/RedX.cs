using RedX.Diagnostics.Client;
using RedX.Regulator.DBAccess.Framework;
using RedX.Regulator.System;
using RedX.Service;
using RedX.Service.IO;
using RedX.Service.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedXLocalService{
    public partial class RedXInfoService : ServiceBase{
        private EventLog eLog;
        private LogWriter wLog;
        private SecuredConnector sc;
        private SysInfo lastData;

        public RedXInfoService(){
            InitializeComponent();
            wLog = new LogWriter(null, null);
            sc = new SecuredConnector();

            ThreadPool.SetMaxThreads(2, 2);
            ThreadPool.QueueUserWorkItem(delegate { lastData.Environment = Const.GetOS(); });
        }



        private void updateAll(){

            wLog.WriteIntoFile("Service started at " + DateTime.Today.TimeOfDay.TotalSeconds);
            wLog.WriteIntoFile("Initiating all componants.");
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = Const.Interval;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            wLog.WriteIntoFile("Timer configured.");
            timer.Start();

            wLog.WriteIntoFile("Timer started.");
            wLog.WriteIntoFile("Status: Online.");
            wLog.WriteIntoFile("Service: Online.");
        }

        protected override void OnStart(string[] args)
        {
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;

            try
            {
                serviceStatus.dwWaitHint = 100000;
                SetServiceStatus(this.ServiceHandle, ref serviceStatus);

                updateAll();

                serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
                SetServiceStatus(this.ServiceHandle, ref serviceStatus);
                base.OnStart(args);
            }
            catch (Exception e){
                wLog.WriteIntoFile("Failure detected. Stopping the program.");
                wLog.WriteIntoFile("Retrieving cause:");
                wLog.WriteIntoFile(e.Message);
                wLog.WriteIntoFile(e.StackTrace);
                serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
                Stop();
            }
        }
        
        private void OnTimer(object sender, System.Timers.ElapsedEventArgs args){
            try{
                ThreadPool.QueueUserWorkItem(
                    delegate {
                        var data = SystemDiagnostics.SystemInfo();
                        lastData.PercentageCPU = data[0];
                        lastData.PercentageRAM = data[1];
                        lastData.Date = DateTime.Now;

                        sc.Add(lastData);
                        wLog.WriteIntoFile("Données sauvegardées sur la base distante");
                    }
                );
            }
            catch (Exception e)
            {
                wLog.WriteIntoFile("Exception Caught:");
                wLog.WriteIntoFile(e.Message + ":" + e.StackTrace);
                this.Stop();
            }
        }


        protected override void OnStop()
        {
            try
            {
                wLog.WriteIntoFile("Stopping the service.");
                wLog.WriteIntoFile("Service stopped at " + DateTime.Today.TimeOfDay.TotalSeconds);
            }
            catch { }
            finally
            {
                base.OnStop();
            }
        }

        protected override void OnPause()
        {
            wLog.WriteIntoFile("Service Paused.");
            base.OnPause();
        }

        protected override void OnContinue()
        {
            try
            {
                updateAll();
                base.OnContinue();
            }
            catch (Exception e)
            {
                Stop();
            }
        }

        protected override void OnShutdown()
        {
            try
            {
                wLog.WriteIntoFile("Service shutdown.");
                wLog.WriteIntoFile("Service on shutdown at " + DateTime.Today.TimeOfDay);
            }
            catch { }
            finally
            {
                base.OnShutdown();
            }
        }

        protected override void OnCustomCommand(int command)
        {
            base.OnCustomCommand(command);
        }

        
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

    }
}
