using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace GibbonLib
{
   

    public class Logging
    {

        public delegate void LogHandler(string message,string deviceID,int messageType=0);

        public static event LogHandler Log;


        private static void WriteConsole(string s)
        {
           
            if (s.Contains("PASSED"))
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (s.Contains("FAILED"))
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            Console.WriteLine(s);

            Console.ResetColor();
        }

        #region FileSystem
        public static string LogPath;
        public static string GenerateLogFile(string BaseFileName)
        {
            if (LogPath != String.Empty)
            {
                LogPath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + BaseFileName + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Year + ".log";
                // Create the file and write to it.
                // DANGER: System.IO.File.Create will overwrite the file
                // if it already exists. This can occur even with
                // random file names.
                if (!System.IO.File.Exists(LogPath))
                {
                    using (System.IO.FileStream fs = System.IO.File.Create(LogPath))
                    {  }
                }
            } return LogPath;

        }

        /// </summary>
        /// <param name="LogPath"></param>
        /// <param name="Message"></param>
        public static void WriteLine(string message,string deviceID="", int messageType=0)
        {
            try
            {

                if (LogPath == null || LogPath == String.Empty)
                {
                   throw new Exception("Logging file is not created");

                 //   Logging.WriteLine("Logging file is not created");
                }
                else
                {
                    using (StreamWriter s = File.AppendText(LogPath))
                    {
                        
                            s.WriteLine(DateTime.Now + " : " + message);
                            WriteConsole(DateTime.Now + " : " + message);
                      
                    }
                }
            }

            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            if (Log != null)
            {
                Log(message, deviceID, messageType);
            }
        }
    
        #endregion
  
    }
}
