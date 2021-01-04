using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SampleWindowsServicesGHA
{
    public partial class SampleWindowsServicesGHA : ServiceBase
    {
        private Timer timer;

        public SampleWindowsServicesGHA()
        {
            InitializeComponent();
            timer = new Timer(30000D);
            timer.AutoReset = true;
            timer.Elapsed += new ElapsedEventHandler(Timer_elasped);
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                timer.Start();
            }
            catch (Exception)
            {
                //log anywhere
            }
        }

        private void Timer_elasped(object sender, ElapsedEventArgs e)
        {
            try
            {
                //perform your task
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnStop()
        {
            if (timer != null)
            {
                timer.Stop();
            }
        }
    }
}
