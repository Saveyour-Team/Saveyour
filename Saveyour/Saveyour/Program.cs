using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            Shell shell = new Shell();
            shell.startApp();
        }
    }
}
