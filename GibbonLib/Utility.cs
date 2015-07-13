using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;



namespace GibbonLib
{
   public class Utility
    {
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern bool SetForegroundWindow(IntPtr hwnd);

        public static void SaveQSDM(string fileNameAndPath)
        {
         
            //System.Diagnostics.Process[] allProcess = System.Diagnostics.Process.GetProcesses();

            //foreach (System.Diagnostics.Process p in allProcess)
            //{
            //    //if (p.MainModule.ModuleName == "QXDM")
            //    //{ 
                
            //    //}

            //    if (p.ProcessName == "csrss")
            //    { 
                
            //    }

            //}

            System.Diagnostics.Process qxdmProcess = System.Diagnostics.Process.GetProcessesByName("QXDM")[0];
          //  System.Diagnostics.Process qxdmProcess = System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Qualcomm\QXDM\Bin\QXDM.exe");
            Logging.WriteLine("Waitting 25 seconds");
            System.Threading.Thread.Sleep(2000);
            SetForegroundWindow(qxdmProcess.MainWindowHandle);
            Logging.WriteLine("Waitting 5 seconds");
            System.Threading.Thread.Sleep(5000);
           // System.Windows.Forms.SendKeys.SendWait("+^(S)"); // Selects Notification Bar
            Logging.WriteLine("Save the files");
            System.Windows.Forms.SendKeys.SendWait("%N");
            System.Windows.Forms.SendKeys.SendWait("^(i)");
            //  System.Windows.Forms.SendKeys.SendWait("%N"); // Selects Notification Bar

            //  System.Windows.Forms.SendKeys.SendWait("{TAB}");
            //   System.Windows.Forms.SendKeys.SendWait("{DOWN 2}");  // Save As Option
            //   System.Windows.Forms.SendKeys.SendWait("{ENTER}");
            System.Threading.Thread.Sleep(2000);
            System.Windows.Forms.SendKeys.SendWait(fileNameAndPath);  // Enters File Name
            System.Threading.Thread.Sleep(2000);
            System.Windows.Forms.SendKeys.SendWait("{ENTER}");
            System.Threading.Thread.Sleep(2000);
           
        }

       public static Dictionary<string, string> GetFileLineToDictionary(string filePath)
       {
        
           Dictionary<string, string> dict = new Dictionary<string, string>();
           string line = String.Empty; ;
           try
           {
               if (File.Exists(filePath))
               {
                   StreamReader file = null;
                   try
                   {
                       file = new StreamReader(filePath);
                       while ((line = file.ReadLine()) != null)
                       {
                           if (line.Trim() != String.Empty && !line.StartsWith("/*"))
                           {
                               Console.WriteLine(line);
                               string[] keyValue = line.Split(',');
                               dict.Add(keyValue[0], keyValue[1]);
                           }
                       }
                   }
                   finally
                   {
                       if (file != null)
                           file.Close();
                   }
               }
           }
           catch (Exception ex)
           {
               throw new Exception(" ( " + line + " ) " + ex.Message);
           }
           return dict;
       
       }
       public static bool IsImage(string template)
       {
           if (template.EndsWith(".png")) return true;
           else return false;
       }
      
        public static System.Boolean IsNumeric(System.Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;
            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch { } // just dismiss errors but return false
            return false;

        }

        public static string GetValueFromSettingApp(string settingKey)
        {
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
           return appSettings[settingKey];
        }

        public static List<string[]> GetFileLines(string filePathAndName)
       {
           try
           {
               // Read the file and display it line by line.
               System.IO.StreamReader file = new System.IO.StreamReader(filePathAndName);
               List<string[]> lines = new List<string[]>();
               int x = 0;
               string line;
               while ((line = file.ReadLine()) != null)
               {
                   if (line.Contains("TestCaseID")) continue; //ignore the header        
                   if (line.Trim() != String.Empty)
                       lines.Add(line.Split(','));

               }
               file.Close();
               return lines;
           }
           catch (Exception ex)
           {
           Logging.WriteLine( ex.Message);
           }
           return null;
       
       }
        public static List<string> GetFileLinesFromFile(string filePathAndName)
        {
            try
            {
                // Read the file and display it line by line.
                System.IO.StreamReader file = new System.IO.StreamReader(filePathAndName);
                List<string> lines = new List<string>();
                int x = 0;
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    lines.Add(line);
                }
                file.Close();
                return lines;
            }
            catch (Exception ex)
            {
                Logging.WriteLine(ex.Message);
            }
            return null;
        }
    }

   [Serializable]
   public class _ErrorException : Exception
   {
       public string ErrorMessage
       {
           get
           {
               return base.Message.ToString();
           }
       }

       public _ErrorException(string errorMessage)
           : base(errorMessage) { }

       public _ErrorException(string errorMessage, Exception innerEx)
           : base(errorMessage, innerEx) { }
   }

    

}



