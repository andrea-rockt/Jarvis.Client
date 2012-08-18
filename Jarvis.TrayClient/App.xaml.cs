using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.ServiceProcess;
using System.Windows;

namespace Jarvis.TrayClient
{
    /// <summary>
    /// Logica di interazione per App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            bool isAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent())
                   .IsInRole(WindowsBuiltInRole.Administrator) ? true : false;

            if (!isAdmin)
            {
                MessageBox.Show("You need admin privileges to run this application,\n" +
                                "this is required to start/stop the service");

                Application.Current.Shutdown(-1);
            }


            bool servicePresent =
                ServiceController.GetServices().Any(service =>
                                                        {
                                                            bool result = service.ServiceName.Contains("JarvisService");
                                                            service.Close();
                                                            return result;
                                                        });

            if(!servicePresent)
            {
                MessageBox.Show("Jarvis service was not correctly installed on your system, please run setup again.");
                Shutdown(-1);
            }
        }
    }
}
