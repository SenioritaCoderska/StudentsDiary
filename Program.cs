using System;
using System.IO;
using System.Windows.Forms;

namespace StudentsDiary
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static string filePath = Path.Combine(Environment.CurrentDirectory, "students.txt");
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
