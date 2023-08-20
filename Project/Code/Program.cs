using System;
using System.IO;
using System.Windows.Forms;

namespace tilecon
{
    static class Program
    {
        static void WriteLog(Exception e)
        {
            var d = DateTime.Now;
            string filename = $"crash {d.Year}-{d.Month}-{d.Day} {d.Hour}-{d.Minute}-{d.Second}.log";
            File.WriteAllText(filename, e.StackTrace);
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new FormTilecon());
            } catch(Exception e)
            {
                MessageBox.Show("An Error Ocurred.");
                WriteLog(e);
            }
        }
    }
}
