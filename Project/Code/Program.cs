using System;
using System.IO;
using System.Windows.Forms;

namespace tilecon
{
    static class Program
    {
        static void WriteLog(Exception e)
        {
            string filename = $"crash {DateTime.Now}.log";
            File.WriteAllText(filename, e.StackTrace);
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
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
