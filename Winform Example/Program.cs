using System;
using System.Windows.Forms;
using Winform_Example.Classes;

namespace Winform_Example
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Detect if your application is running in a Virual Machine / Sandboxie...
            // Anti_Analysis.Init();
            //This connects your file to the AuthGuard.net API, and sends back your application settings and such
            Guard.Initialize("PROGRAMSECRET", "VERSION", "VARIABLESECRET");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
    }
}
