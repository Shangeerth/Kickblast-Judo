using Programming_Assigment.Classes;
using Programming_Assigment.Database;
using Programming_Assigment.Formss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programming_Assigment
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 0 && args[0] == "dashboard")
            {
                // Start directly with Dashboard
                Application.Run(new Dashboard());
            }
            else
            {
                //  Start with Login
                Application.Run(new Welcome());

            }    

            
        }






    }
}
