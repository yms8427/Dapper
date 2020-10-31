using BilgeAdam.Data.Transform.Helpers;
using BilgeAdam.Data.Transform.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilgeAdam.Data.Transform
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var dependencies = new DependencyResolver();
            dependencies.Register<IDataManager, CustomDataManager>();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
