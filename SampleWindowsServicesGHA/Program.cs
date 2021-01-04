using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SampleWindowsServicesGHA
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

            try
            {
                if (Environment.UserInteractive)
                    ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
                else
                    ServiceBase.Run(new SampleWindowsServicesGHA());
            }
            catch (Exception)
            {

            }
        }
    }
}
