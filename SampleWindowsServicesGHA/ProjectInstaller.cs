using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ServiceProcess;

namespace SampleWindowsServicesGHA
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void ServiceProcessInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }

        private void ServiceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {
            try
            {
                using (ServiceController sc = new ServiceController
              (serviceInstaller1.ServiceName, Environment.MachineName))
                {
                    if (sc.Status != ServiceControllerStatus.Running)
                        sc.Start();
                }
            }
            catch (Exception ee)
            {
                EventLog.WriteEntry("Application", ee.ToString(), EventLogEntryType.Error);
            }
        }

        private void ServiceInstaller1_Committed(object sender, InstallEventArgs e)
        {
            try
            {
                using (ServiceController sc = new ServiceController(serviceInstaller1.ServiceName))
                {
                    SetRecoveryOptions(sc.ServiceName);
                }
            }
            catch (Exception e1)
            {
                EventLog.WriteEntry("Application", e1.ToString(), EventLogEntryType.Error);
                return;
            }
        }

        static void SetRecoveryOptions(string serviceName)
        {
            int exitCode;
            using (var process = new Process())
            {
                var startInfo = process.StartInfo;
                startInfo.FileName = "sc";
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;

                // tell Windows that the service should restart if it fails
                startInfo.Arguments = string.Format
             ("failure \"{0}\" reset= 0 actions= restart/60000", serviceName);

                process.Start();
                process.WaitForExit();
                exitCode = process.ExitCode;
            }

            if (exitCode != 0)
                throw new InvalidOperationException();
        }
    }
}
