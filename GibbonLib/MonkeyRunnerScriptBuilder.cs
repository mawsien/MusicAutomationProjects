using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
namespace GibbonLib
{
/// <summary>
/// 
/// </summary>
    class MonkeyRunnerScriptBuilder
    {

        private Process _MonkeyRunnerShellProcess;
        int processID;
        
        private string DeviceID { get; set; }
        private string MonkeyRunnerPath = String.Empty;
       
        private StringBuilder ScriptBuilder = new StringBuilder();
     
        private Device _Device = null;
        bool oneProcess = false;

        System.IO.FileSystemWatcher m_Watcher = new System.IO.FileSystemWatcher();
        public static bool OneProcessModeEnabled = false;

        private bool _monkeyRunnerScriptDoneExecution = false;

        
        private void AppendPythonLine(string line)
        {
            ScriptBuilder.AppendLine(line);
        }
        public void Clear()
        {
            ScriptBuilder.Clear();
          if(!oneProcess)
            AddMonkeyRunnerHeader();
        
        }
        public MonkeyRunnerScriptBuilder(Device device)
        {
            this._Device = device;
            DeviceID = device.DeviceID;
         
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            MonkeyRunnerPath = appSettings["MonkeyRunnerPath"];
            if (!oneProcess)
             AddMonkeyRunnerHeader();

            #region [ Single Process Execution Python]
            if (oneProcess)
            {
                bool monkeyRunnerShellProcessDead = false;
                try
                {
                    if (_MonkeyRunnerShellProcess.HasExited)
                    {
                        monkeyRunnerShellProcessDead = true;
                    }
                }
                catch (Exception ex)
                { }

                m_Watcher.Path = Directory.GetCurrentDirectory() + "\\py";
                m_Watcher.NotifyFilter = NotifyFilters.LastWrite;
                m_Watcher.Changed += new FileSystemEventHandler(m_Watcher_Changed);
                m_Watcher.Filter = "*.txt";
                m_Watcher.EnableRaisingEvents = true;
                if (monkeyRunnerShellProcessDead || _MonkeyRunnerShellProcess == null)
                {
                     MonkeyRunnerPath = Utility.GetValueFromSettingApp("MonkeyRunnerPath");
                    _MonkeyRunnerShellProcess = new Process();
                    _MonkeyRunnerShellProcess.StartInfo.FileName = MonkeyRunnerPath + "monkeyrunner.bat";
                    _MonkeyRunnerShellProcess.Start();
                    processID = _MonkeyRunnerShellProcess.Id;
                    Logging.WriteLine("Waiting 10 seconds for the monkeyrunner to start..");
                    System.Threading.Thread.Sleep(5000);
                    Utility.SetForegroundWindow(_MonkeyRunnerShellProcess.MainWindowHandle);
                    SendKeys.SendWait("from com.android.monkeyrunner import MonkeyRunner, MonkeyDevice~");
                    System.Threading.Thread.Sleep(1000);
                    SendKeys.SendWait("device = MonkeyRunner.waitForConnection{(}5,'" + device.DeviceID + "'{)}~");
                    //  System.Threading.Thread.Sleep(1000);
                    //  SendKeys.SendWait("device.wake{(}{)}~");
                }
            }
            #endregion
        }

        //used to check if python execution has been completed
        void m_Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            _monkeyRunnerScriptDoneExecution = true;
        }
        public void ExecutePythonLine(string str)
        {
            Utility.SetForegroundWindow(_MonkeyRunnerShellProcess.MainWindowHandle);
            SendKeys.SendWait(str.Replace("(", "{(}").Replace(")", "{)}") + "~");
            // System.Threading.Thread.Sleep(500);
        }
        private void AddMonkeyRunnerHeader()
        {
            AppendPythonLine("from com.android.monkeyrunner import MonkeyRunner, MonkeyDevice");
          
            AppendPythonLine("device = MonkeyRunner.waitForConnection(5,'" + DeviceID + "')");
        
            AppendPythonLine("device.wake()");
        }
      
        public void AddTouch(int x,int y)
        {
            AppendPythonLine("device.touch(" + x + ", " + y + ", 'DOWN')");

        }
        public void AddType(string text)
        {
            foreach (char c in text.ToCharArray())
            {
               AppendPythonLine("device.type('" + c + "')");
               AddSleep(1);
           
            }
        }
        public void AddSleep(double seconds)
        {
            AppendPythonLine("print 'sleeping for "+seconds+" seconds'");
            AppendPythonLine("MonkeyRunner.sleep(" + seconds + ")");
        }
        public void AddActivity(string component,string activity)
        {
            if(!String.IsNullOrEmpty(activity))
               AppendPythonLine("device.startActivity(component=\"" + component + "/" + activity + "\")");
            else AppendPythonLine("device.startActivity(component=\"" + component + "\")");
            AddScrollOnePageUp();
            AddScrollOnePageUp();
        }
        public void AddUrlActivity(string component, string url)
        {
            AppendPythonLine("device.startActivity(component = '" + component + "', uri = '" + url + "')");
        }

        public void AddScreenShot(string screenShotPathAndName)
        {

            AppendPythonLine("MonkeyRunner.sleep(0.5)");
            AppendPythonLine("result = device.takeSnapshot()");
            AppendPythonLine("MonkeyRunner.sleep(0.5)");
         //   string pythonFormatedImageAndPath = _Device.ImageDirectoryPath.CurrentScreenShotPath + screenShotPathAndName;
            string pythonFormatedImageAndPath = screenShotPathAndName.Replace(@"\", "/");
        
            AppendPythonLine("result.writeToFile('" + pythonFormatedImageAndPath + "' ,'png')");
            AppendPythonLine("MonkeyRunner.sleep(0.5)");
           //   WriteFlagForEndScriptExecution();
        }

        private void WriteFlagForEndScriptExecution()
        {
            string currentDirectory = Directory.GetCurrentDirectory().Replace(@"\", "/");
            AppendPythonLine("myfile = open('" + currentDirectory + "/py/EndFlag.txt','w')");
            AppendPythonLine("myfile.write('1')");
            AppendPythonLine("myfile.flush()");
            AppendPythonLine("myfile.close()");
        }
        public void AddPressKey(string keyCode )
        {
            AppendPythonLine("device.press('" + keyCode + "',MonkeyDevice.DOWN_AND_UP)");
           
        }

        public void AddClickBack()
        {
            AppendPythonLine("device.press('KEYCODE_BACK',MonkeyDevice.DOWN_AND_UP)");
           // AddSleep(1);
        }
        public void AddClickHome()
        {
            AppendPythonLine("device.press('KEYCODE_HOME',MonkeyDevice.DOWN_AND_UP)");
        }
        public void AddClickMenu()
        {
            AppendPythonLine("device.press('KEYCODE_MENU',MonkeyDevice.DOWN_AND_UP)");
        }
        public void AddScrollOnePageDown()
        {
            AppendPythonLine("width = int(device.getProperty('display.width'))");
            AppendPythonLine("height = int(device.getProperty('display.height'))");
            AppendPythonLine("x = int(width / 2)");
            AppendPythonLine("ybottom = height - 100");
            AppendPythonLine("ytop = 200");
            AppendPythonLine("device.drag((x,ybottom), (x, ytop), 0.5, 10)");
        }

        public void AddScrollAllPagesDown()
        {
            AppendPythonLine("width = int(device.getProperty('display.width'))");
            AppendPythonLine("height = int(device.getProperty('display.height'))");
            AppendPythonLine("x = int(width / 2)");
            AppendPythonLine("ybottom = height - 100");
            AppendPythonLine("ytop = 200");
            AppendPythonLine("device.drag((x,ybottom), (x, ytop), 0.5, 10)");
            AppendPythonLine("device.drag((x,ybottom), (x, ytop), 0.5, 10)");
            AppendPythonLine("device.drag((x,ybottom), (x, ytop), 0.5, 10)");
            AppendPythonLine("device.drag((x,ybottom), (x, ytop), 0.5, 10)");
            AppendPythonLine("device.drag((x,ybottom), (x, ytop), 0.5, 10)");
            AppendPythonLine("device.drag((x,ybottom), (x, ytop), 0.5, 10)");
        }

        public void AddScrollAllPagesUp()
        {
            AppendPythonLine("width = int(device.getProperty('display.width'))");
            AppendPythonLine("height = int(device.getProperty('display.height'))");
            AppendPythonLine("x = int(width / 2)");
            AppendPythonLine("ybottom = height - 100");
            AppendPythonLine("ytop = 200");
            AppendPythonLine("device.drag((x,ytop ), (x, ybottom), 0.5, 10)");
            AppendPythonLine("device.drag((x,ytop ), (x, ybottom), 0.5, 10)");
            AppendPythonLine("device.drag((x,ytop ), (x, ybottom), 0.5, 10)");
            AppendPythonLine("device.drag((x,ytop ), (x, ybottom), 0.5, 10)");
            AppendPythonLine("device.drag((x,ytop ), (x, ybottom), 0.5, 10)");
        }
        public void AddScrollOnePageUp()
        {
            AppendPythonLine("width = int(device.getProperty('display.width'))");
            AppendPythonLine("height = int(device.getProperty('display.height'))");
            AppendPythonLine("x = int(width / 2)");
            AppendPythonLine("ybottom = height - 100");
            AppendPythonLine("ytop = 200");
            AppendPythonLine("device.drag((x,ytop ), (x, ybottom), 0.5, 10)");
     
        }

        public void AddTouchCenter()
        {
            AppendPythonLine("width = int(device.getProperty('display.width'))");
            AppendPythonLine("height = int(device.getProperty('display.height'))");
            AppendPythonLine("x = int(width / 2)");
            AppendPythonLine("y = int(height / 2)");
            AppendPythonLine("device.touch(x,y, 'DOWN_AND_UP')");

        }
        public void GenerateAndExecute()
        {
            //get script directoy
            string scriptDirectory = MonkeyRunnerPath + "script.py";
            //flush it
            using (StreamWriter file = new StreamWriter(scriptDirectory))
            {
                file.Write(ScriptBuilder.ToString());
                file.Flush();
            }
            //exeute it
             CMD cmd = new CMD();
             cmd.Exceute(MonkeyRunnerPath + "monkeyrunner.bat", scriptDirectory);
        } 
        public void GenerateAndExecuteToProcess()
        {
            ////get script directoy
            //string scriptDirectory = MonkeyRunnerPath + "script.py";
            ////flush it
            //using (StreamWriter file = new StreamWriter(scriptDirectory))
            //{
            //    file.Write(ScriptBuilder.ToString());
            //    file.Flush();
            //}
            String[] lines = ScriptBuilder.Replace("\r","").ToString().Split('\n');
            foreach (String s in lines)
            {
                if (s != String.Empty)
                {
                    ExecutePythonLine(s);
                    System.Threading.Thread.Sleep(10);
                }
            }
          
            //using (System.IO.StreamReader file = new System.IO.StreamReader(scriptDirectory))
            //{
            //    List<string[]> lines = new List<string[]>();
            //    int x = 0;
            //    string line;
            //    while (( line = file.ReadLine()) != null)
            //    {
            //        _Device.ExecutePythonLine(line);
            //    }
            //}
                
                  
            //exeute it
         //   CMD cmd = new CMD();
            //  adnan cmd.Exceute(@"C:\android-sdk\tools\monkeyrunner.bat",currentDirectory);
            //  cmd.Exceute(@"C:\androids\tools\monkeyrunner.bat", currentDirectory);


         //   cmd.Exceute(MonkeyRunnerPath + "monkeyrunner.bat", scriptDirectory);
        }

        /*
         def snap(): 
    from com.android.monkeyrunner import MonkeyRunner, MonkeyDevice 
    print "Waiting for device.." 
    device = MonkeyRunner.waitForConnection() 
    print "Device found.." 
    result = device.takeSnapshot() 
    print "Clicked.." 
    now = datetime.datetime.now() 
    file = "C:\\Workspace\\"+now.strftime("%d%m%Y-%H%M%S")+".png" 
    result.writeToFile(file,'png') 
    print file 

         
         */
    }
}
