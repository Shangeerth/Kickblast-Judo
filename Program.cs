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
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Welcome());
        }



        public static class NavigationManager
        {
            private static Stack<Form> formHistory = new Stack<Form>();

            public static void OpenForm(Form currentForm, Form newForm)
            {
                formHistory.Push(currentForm); // Store the current form
                newForm.Show();
                currentForm.Hide();
            }

            public static void GoBack()
            {
                if (formHistory.Count > 0)
                {
                    Form previousForm = formHistory.Pop(); // Get the last opened form
                    previousForm.Show();
                }
                else
                {
                    Dashboard dashboard = new Dashboard();
                    dashboard.Show();
                }
            }

            public static bool CanGoBack()
            {
                return formHistory.Count > 0;
            }
        }



    }
}
