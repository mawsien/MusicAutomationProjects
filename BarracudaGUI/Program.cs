using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GibbonGUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                /*Check paths*/

                string settingName = System.Configuration.ConfigurationManager.AppSettings["MonkeyRunnerPath"];
                if (!System.IO.Directory.Exists(settingName))
                {
                    MessageBox.Show(settingName + " direcltory is not setup right for MonkeyRunnerPath!", "MonkeyRunnerPath path error", MessageBoxButtons.OK);
                    return;
                }
                 settingName = System.Configuration.ConfigurationManager.AppSettings["WhiteListPath"];
                if (!System.IO.File.Exists(settingName))
               {
                   MessageBox.Show(settingName + " file is not setup right!", "whitelist path error", MessageBoxButtons.OK);
                   return;
               }

               
                settingName = System.Configuration.ConfigurationManager.AppSettings["Testcases"];
                if (!System.IO.File.Exists(settingName))
                {
                    MessageBox.Show(settingName + " file is not setup right for test settings!", "Testcases path error", MessageBoxButtons.OK);
                    return;
                }

                settingName = System.Configuration.ConfigurationManager.AppSettings["tcpdumpDestination"];
                if (!System.IO.Directory.Exists(settingName))
                {
                    MessageBox.Show(settingName + " Directory is not setup right for saving tcpdump!", "tcpdumpDestination path error", MessageBoxButtons.OK);
                    return;
                }

                settingName = System.Configuration.ConfigurationManager.AppSettings["tcpdumpDestination"];
                if (!System.IO.Directory.Exists(settingName))
                {
                    MessageBox.Show(settingName + " Directory is not setup right for saving tcpdump!", "tcpdumpDestination directory error", MessageBoxButtons.OK);
                    return;
                }

                settingName = System.Configuration.ConfigurationManager.AppSettings["TSharkPath"];
                if (!System.IO.File.Exists(settingName))
                {
                    MessageBox.Show(settingName + " Directory is not setup right for tshark path!", "TSharkPath path error", MessageBoxButtons.OK);
                    return;
                }
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
