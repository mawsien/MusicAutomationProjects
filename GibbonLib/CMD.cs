using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace GibbonLib
{
    public class CMD
    {
         Process _process = new System.Diagnostics.Process();

         public void ExceuteNoWait(string fileName, string args)
         {
             
             ProcessStartInfo info = new ProcessStartInfo(fileName);
             info.UseShellExecute = false;
             info.Arguments = args;
           
             // info.RedirectStandardInput = true;
             info.RedirectStandardOutput = true;
             info.CreateNoWindow = true;
             info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;

             string returnvalue = string.Empty;

             using (Process process = Process.Start(info))
             { }
         }

        public string ExceuteAndReturn(string fileName, string args)
        {

            ProcessStartInfo info = new ProcessStartInfo(fileName);
            info.UseShellExecute = false;
            info.Arguments = args;
           // info.Verb = "runas";
           // info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.CreateNoWindow = true;
            info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;

                string returnvalue = string.Empty;

                using (Process process = Process.Start(info))
                {
                        StreamReader sr = process.StandardOutput;
                        process.WaitForExit(3000);
                        returnvalue = sr.ReadToEnd();
                   
                }

                return returnvalue;
        }
        public void ExceuteLogcatDump(string fileName, string args)
        {
            //   Logging.WriteLine("Monkey runner path is "+ fileName + "  : " + args);
            //  Console.WriteLine("11");
            //  ProcessStartInfo info = new ProcessStartInfo(fileName);
            ProcessStartInfo info = new ProcessStartInfo(fileName);
            info.UseShellExecute = false;
            info.Arguments = args;
               info.RedirectStandardInput = true;
            //    info.RedirectStandardOutput = true;
            //  info.RedirectStandardError = true;
            //   info.CreateNoWindow = true;

            //  info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            //    Console.WriteLine("22");
            Logging.WriteLine(info.ToString(),"");

            using (Process process = Process.Start(info))
            {
                process.EnableRaisingEvents = true;
                process.StandardInput.WriteLine(args);
                //  process.ErrorDataReceived += new DataReceivedEventHandler(process_ErrorDataReceivedMonkey);
                //  process.OutputDataReceived+=new DataReceivedEventHandler(process_OutputDataReceivedMonkey);
                process.WaitForExit(100000);
            }
        }

        public void Exceute(string fileName, string args)
        {
         //   Logging.WriteLine("Monkey runner path is "+ fileName + "  : " + args);
          //  Console.WriteLine("11");
          //  ProcessStartInfo info = new ProcessStartInfo(fileName);
            ProcessStartInfo info = new ProcessStartInfo(fileName);
            info.UseShellExecute = false;
            info.Arguments = args;
        //    info.RedirectStandardInput = true;
       //    info.RedirectStandardOutput = true;
          //  info.RedirectStandardError = true;
          info.CreateNoWindow = true;
          
          //  info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
        //    Console.WriteLine("22");
            Logging.WriteLine(info.ToString(),"");

            using (Process process = Process.Start(info))
            {
                process.EnableRaisingEvents = true;
                //  process.ErrorDataReceived += new DataReceivedEventHandler(process_ErrorDataReceivedMonkey);
              //  process.OutputDataReceived+=new DataReceivedEventHandler(process_OutputDataReceivedMonkey);
                process.WaitForExit(100000);
            }
        }

        void process_ErrorDataReceivedMonkey(object sender, DataReceivedEventArgs e)
        {
            Logging.WriteLine("Monkey Out : " + e.Data,"");
        }

        void process_OutputDataReceivedMonkey(object sender, DataReceivedEventArgs e)
        {
            Logging.WriteLine("Monkey Error : " + e.Data,"");
        }

        void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
           
        }

        /// <summary>
        /// Execute a shell command
        /// </summary>
        /// <param name="_FileToExecute">File/Command to execute</param>
        /// <param name="_CommandLine">Command line parameters to pass</param>
        /// <param name="_outputMessage">returned string value after executing shell command</param>
        /// <param name="_errorMessage">Error messages generated during shell execution</param>
        public void ExecuteShellCommand(string _FileToExecute, string _CommandLine, ref string _outputMessage, ref string _errorMessage)
        {
            // Set process variable
            // Provides access to local and remote processes and enables you to start and stop local system processes.
          
            try
            {
                _process = new System.Diagnostics.Process();

                // invokes the cmd process specifying the command to be executed.
                string _CMDProcess = string.Format(System.Globalization.CultureInfo.InvariantCulture, @"{0}\cmd.exe", new object[] { Environment.SystemDirectory });

                // pass executing file to cmd (Windows command interpreter) as a arguments
                // /C tells cmd that we want it to execute the command that follows, and then exit.
                string _Arguments = string.Format(System.Globalization.CultureInfo.InvariantCulture, "/C {0}", new object[] { _FileToExecute });

                // pass any command line parameters for execution
                if (_CommandLine != null && _CommandLine.Length > 0)
                {
                    _Arguments += string.Format(System.Globalization.CultureInfo.InvariantCulture, " {0}", new object[] { _CommandLine, System.Globalization.CultureInfo.InvariantCulture });
                }

                // Specifies a set of values used when starting a process.
                 System.Diagnostics.ProcessStartInfo _ProcessStartInfo = new System.Diagnostics.ProcessStartInfo(_CMDProcess, _Arguments);
                // sets a value indicating not to start the process in a new window.
                _ProcessStartInfo.CreateNoWindow = false;
                // sets a value indicating not to use the operating system shell to start the process.
               // _ProcessStartInfo.UseShellExecute = false;
                // sets a value that indicates the output/input/error of an application is written to the Process.
                _ProcessStartInfo.RedirectStandardOutput = true;
                _ProcessStartInfo.RedirectStandardInput = true;
                _ProcessStartInfo.RedirectStandardError = true;
                _ProcessStartInfo.UseShellExecute = false;
                //_ProcessStartInfo.Verb = "runas";
                //_ProcessStartInfo.Arguments = "/env /user:" + "Administrator" + " cmd";

                _process.StartInfo = _ProcessStartInfo;
                _process.OutputDataReceived += new DataReceivedEventHandler(process_OutputDataReceived);
                // Starts a process resource and associates it with a Process component.
                _process.Start();

                // Instructs the Process component to wait indefinitely for the associated process to exit.
                _errorMessage = _process.StandardError.ReadToEnd();
                _process.WaitForExit();

                // Instructs the Process component to wait indefinitely for the associated process to exit.
                _outputMessage = _process.StandardOutput.ReadToEnd();
                _process.WaitForExit();
            }

            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }
            finally
            {
                // close process and do cleanup
              //  process.Close();
              //  process.Dispose();
              //  process = null;
            }
        }
        public void ExceuteMonkeyRunnerCommand(string fileName, string args)
        {
            //   Logging.WriteLine("Monkey runner path is "+ fileName + "  : " + args);
            //  Console.WriteLine("11");
            //  ProcessStartInfo info = new ProcessStartInfo(fileName);
            ProcessStartInfo info = new ProcessStartInfo(fileName);
            info.UseShellExecute = false;
            info.Arguments = args;
            //    info.RedirectStandardInput = true;
            //   info.RedirectStandardOutput = true;
            //  info.RedirectStandardError = true;
            //   info.CreateNoWindow = true;
            //  info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            //    Console.WriteLine("22");
            Logging.WriteLine(info.ToString(),"");

            using (Process process = Process.Start(info))
            {
                //  process.ErrorDataReceived += new DataReceivedEventHandler(process_ErrorDataReceivedMonkey);
                //  process.OutputDataReceived+=new DataReceivedEventHandler(process_OutputDataReceivedMonkey);
                process.WaitForExit(100000);
            }
        }

        //void process_ErrorDataReceivedMonkey(object sender, DataReceivedEventArgs e)
        //{
        //    Logging.WriteLine("Monkey Out : " + e.Data);
        //}

        //void process_OutputDataReceivedMonkey(object sender, DataReceivedEventArgs e)
        //{
        //    Logging.WriteLine("Monkey Error : " + e.Data);
        //}


    }
}
