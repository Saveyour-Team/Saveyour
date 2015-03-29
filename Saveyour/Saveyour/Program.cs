using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Saveyour
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            String appGuid = "c0a76b5a-12ab-45c5-b9d9-d693fab6e7b9";

            using (Mutex mutex = new Mutex(false, "Global\\" + appGuid))
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("Saveyour is already running. Please close that instance before opening a new one.");
                    return;
                }

                Shell shell = Shell.getShell();
                Application.Run();
                
              // shell.startApp();              
            }
            
        }
    }
}
