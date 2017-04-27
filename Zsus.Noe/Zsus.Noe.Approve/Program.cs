using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Zsus.Noe.Approve
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (Environment.UserInteractive)
            {
                // TO USE THIS CHANGE OUTPUT TYPE
                // FROM WINDOWS TO CONSOLE
                NoeService svc = new NoeService();
                svc.ConsoleOnStart();
                Console.WriteLine("Press any key to stop");
                Console.Read();
                svc.ConsoleOnStop();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] { new NoeService() };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
