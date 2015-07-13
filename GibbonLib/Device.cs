using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Diagnostics;

using System.Windows.Forms;
namespace GibbonLib
{
    public class Device
    {
        /// <summary>
        /// Used if you want to user one process to run the python commands
        /// </summary>
        public string DeviceID
        { get; set; }
        public string PhoneNumber
        { get; set; }
        MonkeyRunnerScriptBuilder _script;

        public TemplateDetector _TemplateDetector
        { get; set; }



         


        private bool _monkeyRunnerScriptDoneExecution = false;

        public ImagesDirectoryPath ImageDirectoryPath
        {
            get;
            set;
        }

        public string PythonScriptFileName
        {
            get;
            set;
        }

        public string XmlConfigFileNameAndPath
        {
            get;
            set;
        }

        public static bool PauseExecution { get; set; }
       
        public Device(string deviceID)
        {
            if (deviceID != String.Empty)
            {
                this.DeviceID = deviceID;
                _script = new MonkeyRunnerScriptBuilder(this);
                ImageDirectoryPath = new ImagesDirectoryPath();
                _TemplateDetector = new TemplateDetector(ImageDirectoryPath);
               
            }
        }

        public Device(string deviceID, ImagesDirectoryPath imagesDirectoyPath, string pythonScriptFileName, string xmlConfigFileNameAndPath)
        {

            if (deviceID != String.Empty)
            {
                this.PythonScriptFileName = pythonScriptFileName;

                this.XmlConfigFileNameAndPath = xmlConfigFileNameAndPath;
                this.DeviceID = deviceID;
                this.ImageDirectoryPath = imagesDirectoyPath;


                _TemplateDetector = new TemplateDetector(ImageDirectoryPath);

                System.Threading.Thread.Sleep(1000);
                _script = new MonkeyRunnerScriptBuilder(this);
            }
        }

        public void ExecuteFunction(string functionName, Template template, string param1 = "")
        {
            switch (functionName)
            {
              
                case "MakeACall":
                    
                    break;

              

                case "WaitUntilExists":

                    int timeOut = 10;
                 
                    if (param1.Trim() != String.Empty)
                    {
                        timeOut = int.Parse(param1);
                    }
                    WaitUntilExists(template, timeOut: timeOut);

                    break;
                case "ScrollToBottom":
                    this.ScrollToBottom(); break;
                case "ForceClick":

                    ClickByTemplateOrRaise(template);

                    break;
                case "Click":

                    ClickByTemplate(template);

                    break;
                case "Wait":
                    int seconds = int.Parse(param1) * 1000;
                   WriteToLog("Waitting " + seconds + " seconds");
                    System.Threading.Thread.Sleep(seconds);
                    break;

                case "Validate":
                    TakeScreenShot();
                    if (!DoseScreenContain(template))
                    {
                        throw new Exception("Template " + template.ToString() + " is not validated");
                    }
                    break;
                case "ClickMenu":
                    ClickMenu();
                    break;
                case "GoHome":
                    ClickHomeButton();
                    break;
                case "TakeScreenShot":
                    TakeScreenShot();
                    break;
                case "GoBack":
                    GoBack();
                    break;
                case "Type":
                    Type(param1);
                    break;
                case "GoAllBack":
                    GoAllBack();
                    break;

                case "RunSettings":
                    RunSettings();
                    break;
                //am start -a android.intent.action.MAIN -n com.android.settings/.wifi.WifiSettings
                case "WiFiSettings":
                    RunWiFiSettings();
                    break;
                case "SecuritySettings":
                    RunSettings();
                    break;
                case "ScrollPageDownTwoPages":
                    ScrollPageDownTwoPages();
                    break;
                case "ScrollDown":
                  ScrollPageDown();
                    break;
                case "ScrollUp":
                 ScrollpageUp();   
                    break;
            }

        }
        public void RunWiFiSettings()
        {
         //   CMD cmd = new CMD();
          //  string command = " -s " + DeviceID + "  shell am start -a android.intent.action.MAIN -n com.android.settings/.wifi.WifiSettings ";
          //  cmd.ExceuteAndReturn("adb", command);

           WriteToLog("Run  WiFi Settings activity.");

            //   Utility.RunMonkeyRunnerScript("StartWiFiSetting.py");
            _script.Clear();
            _script.AddActivity("com.android.settings", ".wifi.WifiSettings");
             AddTakeScreenShotToTheScript();
             GenerateScriptAndExecute();
        
        }
        public void RunActivity(string actnamespace, string activitypath)
        {
            WriteToLog("Run  activity");

            //   Utility.RunMonkeyRunnerScript("StartWiFiSetting.py");
            _script.Clear();
            _script.AddActivity(actnamespace, activitypath);
            AddTakeScreenShotToTheScript();
            GenerateScriptAndExecute();
        }
        #region Phone Function


        public void WaitUntilExists(Template template, int timeOut = 5, int checkTimeInterval=1)
        {
            int timeOutIndex = timeOut + 1;
           WriteToLog("Wait " + timeOut + " seconds until template " + template + " exists");
            while ((timeOutIndex-- != 0))
            {
                if (DoseScreenContain(template))

                    return;
                TakeScreenShot();
                System.Threading.Thread.Sleep(checkTimeInterval * 1000);
            }
            throw new Exception("Template " + template + " is not exists");
        }

        public bool DoseScreenContain(Template template)
        {
            if (Utility.IsImage(template.TemplateValue))
                return RecognizeTemplateOnScreen(template).IsTemplateFound;
            else return GetScreenText().ToLower().Contains(template.TemplateValue.ToLower());

        }

        public RecognitionResult RecognizeTemplateOnScreen(Template template)
        {
           // string template, int templateIndex = 1, double accuracy = 0.99
            string[] templates = template.TemplateValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            RecognitionResult result = null;
            foreach (string templ in templates)
            {
                Logging.WriteLine("verifying template " + template.TemplateName + "  " + template.TemplateValue);
                result = _TemplateDetector.Recognize(templ, template.TemplateIndex, accuracy: template.Accuracy, deviceID: DeviceID);

                if (result != null && result.IsTemplateFound)
                {
                    break;
                }

            }
            return result;
        }
        public void ScrollPageDown()
        {
           WriteToLog("Scrolling one page down..");
            _script.Clear();

            _script.AddScrollOnePageDown();
            AddTakeScreenShotToTheScript();
            GenerateScriptAndExecute();

        }
        public void ScrollPageDownTwoPages()
        {
            WriteToLog("Scrolling two page down..");
            _script.Clear();
            _script.AddScrollOnePageDown();
            _script.AddScrollOnePageDown();
            AddTakeScreenShotToTheScript();
            GenerateScriptAndExecute();

        }

        private void GenerateScriptAndExecute()
        {
      
            _script.GenerateAndExecute();
          //  System.Threading.Thread.Sleep(1000);
            string screenShotFileName = _TemplateDetector.GetImagesDirectoryPath().CurrentScreenShotFullPathAndName;
            while (!File.Exists(screenShotFileName))
            {
                
                 WriteToLog("Screenshot is not yet saved  " + screenShotFileName + " so we wait 15 seconds");
                 System.Threading.Thread.Sleep(15000);
                //check again if it is already saved on the disk
                 screenShotFileName = _TemplateDetector.GetImagesDirectoryPath().CurrentScreenShotFullPathAndName;
                if (!File.Exists(screenShotFileName))
                {//go out of the loop and break  , the screenshot saved
                   
                    WriteToLog("Problem taking screen shot for deviceID " + DeviceID + " so  take it again, the name is " + screenShotFileName);
                    _script.Clear();
                    _script = new MonkeyRunnerScriptBuilder(this);
                    TakeScreenShot();
                    System.Threading.Thread.Sleep(5000);
                    screenShotFileName = _TemplateDetector.GetImagesDirectoryPath().CurrentScreenShotFullPathAndName;
                }else //we take the screenshot again
                {
                    break;
                }
            }
        }
        public void ScrollpageUp()
        {
           WriteToLog("Scrolling  one page UP..");
            _script.Clear();
            _script.AddScrollOnePageUp();
            AddTakeScreenShotToTheScript();
            GenerateScriptAndExecute();

            // RunMonkeyRunnerScript("ScrollAllUp.py");
        }
        public void ScrollToTop()
        {
           WriteToLog("Scrolling all pages up..");
            _script.Clear();
           // _script.AddScrollAllPagesUp();
            _script.AddScrollOnePageUp();
            AddTakeScreenShotToTheScript();
            GenerateScriptAndExecute();

            // RunMonkeyRunnerScript("ScrollAllUp.py");
        }
        public void ScrollToBottom()
        {
           WriteToLog("Scrolling all pages down..");
            _script.Clear();
            _script.AddScrollAllPagesDown();
            AddTakeScreenShotToTheScript();
            GenerateScriptAndExecute();
        }


        //public  void TakeScreenShot(string extension="png")
        //{
        //    string pythonFileName =  @"TakeScreenShotPNG.py";
        //    switch (extension)
        //    {
        //        case "png":
        //            pythonFileName = @"TakeScreenShotPNG.py";
        //            break;
        //        case "jpg":
        //            pythonFileName = @"TakeScreenShotJPG.py";
        //            break;

        //    }
        //   WriteToLog("Processing...");
        //    Utility.RunMonkeyRunnerScript(pythonFileName);
        //}
        public void TakeScreenShot()
        {

           WriteToLog("Taking screen shot...");
            _script.Clear();
            AddTakeScreenShotToTheScript();
            GenerateScriptAndExecute();
        }
     
        private void AddTakeScreenShotToTheScript()
        {
            string name = string.Format(DeviceID + "_currentScreen-{0:yyyy-MM-dd_hh-mm-ss-tt}.png", DateTime.Now);
            _TemplateDetector.ChangeCurrentScreenShotName(name);
            _script.AddScreenShot(Directory.GetCurrentDirectory() + "\\" + ImageDirectoryPath.CurrentScreenShotPath + name);


        }

        public string GetCurrentScreenShotName()
        {
            string name = string.Format(DeviceID + "_currentScreen-{0:yyyy-MM-dd_hh-mm-ss-tt}.png", DateTime.Now);
            _TemplateDetector.ChangeCurrentScreenShotName(name);

            return Directory.GetCurrentDirectory() + "\\" + ImageDirectoryPath.CurrentScreenShotPath + name;

        }
        /// <summary>
        /// Start Wi-Fi settings and Enable Wi-Fi if it is Disabled!
        /// </summary>
        public void RunSettings()
        {
           WriteToLog("Run Settings component.");
/*
            //   Utility.RunMonkeyRunnerScript("StartWiFiSetting.py");
            _script.Clear();
            _script.AddActivity("com.android.SETTINGS","");
            _script.AddClickBack();
            _script.AddClickBack();
            _script.AddClickBack();
            _script.AddClickBack();
            _script.AddActivity("com.android.SETTINGS","" );
            AddTakeScreenShotToTheScript();
            GenerateScriptAndExecute();
            */
        
           CMD cmd = new CMD();
           string command = " -s " + DeviceID + " shell am start -a android.settings.SETTINGS  ";
           cmd.ExceuteNoWait("adb", command);
           TakeScreenShot();
        }

        public void HangupACall()
        {
            
            WriteToLog("Hangingup the call.. ");
            CMD cmd = new CMD();

            string command = " -s " + DeviceID + " shell input keyevent 6  ";
            cmd.ExceuteNoWait("adb", command);

            System.Threading.Thread.Sleep(5000);
            cmd.ExceuteAndReturn("adb", command);

         
        }
        public void SendSMS(string phoneNumber,string text)
        { 
            WriteToLog("Sending SMS..");
            CMD cmd = new CMD();
            string command = " -s " + DeviceID + " shell am start -a android.intent.action.SENDTO -d sms:" + phoneNumber + " --es sms_body \"" + text + "\" --ez exit_on_sent true  ";
            WriteToLog(command);
            cmd.ExceuteNoWait("adb", command);
            System.Threading.Thread.Sleep(3000);
            command = " -s " + DeviceID + " shell input keyevent 22 ";
            WriteToLog(command);
            cmd.ExceuteAndReturn("adb", command);
            System.Threading.Thread.Sleep(3000);
            command = " -s " + DeviceID + " shell input keyevent 66 ";
            WriteToLog(command);
            cmd.ExceuteAndReturn("adb", command);
            System.Threading.Thread.Sleep(3000);
        }

        public void RunMediaHub()
        {

            WriteToLog("Runing Media Hub.. ");
            CMD cmd = new CMD();

            string command = " -s " + DeviceID + " shell am start -n com.samsung.mediahub/.Main  ";
        //   cmd.ExceuteNoWait("adb", command);

        //    System.Threading.Thread.Sleep(5000);
       cmd.ExceuteAndReturn("adb", command);


        }
        public void Touch(Point touchPosition, bool addScreenShot=true)
        {
             WriteToLog(String.Format("Touching on  X={0}, Y={1}", touchPosition.X, touchPosition.Y));
            _script.Clear();
            _script.AddTouch(touchPosition.X, touchPosition.Y);
            if (addScreenShot)
            {
                _script.AddSleep(1);
                AddTakeScreenShotToTheScript();
            }
            GenerateScriptAndExecute();
        }

      

        public void Type(string text)
        {
           WriteToLog(String.Format("typing {0}", text));
            _script.Clear();
            _script.AddType(text);
            AddTakeScreenShotToTheScript();
            GenerateScriptAndExecute();
        }

        public void TouchAndType(string text, Point touchPosition)
        {
            WriteToLog(String.Format("Touching  X={0}, Y={1} and and then Typing {2}", touchPosition.X, touchPosition.Y, text));
            _script.Clear();
            _script.AddTouch(touchPosition.X, touchPosition.Y);
            _script.AddSleep(1);
            _script.AddType(text);
            AddTakeScreenShotToTheScript();
            GenerateScriptAndExecute();
        }

        public bool ClickByTemplate(Template template, bool takeScreenShot=true)
        {
            WriteToLog(String.Format("Clicking by template " + template.TemplateValue));
            RecognitionResult result = _TemplateDetector.Recognize(template.TemplateValue, template.TemplateIndex, template.Accuracy, deviceID: DeviceID);
            if (result.IsTemplateFound)
            {
                //if (param3 != String.Empty)
                //{
                //    string[] splitted = param3.Split('|');
                //    Point p = new Point(int.Parse(splitted[0]), int.Parse(splitted[1]));
                //    Touch(p);
                //}
                //else
                Touch(result.TemplatePosition, takeScreenShot);
                return true;
            }
            else
            {
                //Logging.WriteLine(String.Format("Template {0} was not found ", param1));
                return false;
            }
        }

        public bool ClickByText(string text, int index,bool takeScreenShot = true)
        {
            WriteToLog(String.Format("Clicking by template " + text));
            RecognitionResult result = _TemplateDetector.Recognize(text, index, 1, deviceID: DeviceID);
            if (result.IsTemplateFound)
            {
                //if (param3 != String.Empty)
                //{
                //    string[] splitted = param3.Split('|');
                //    Point p = new Point(int.Parse(splitted[0]), int.Parse(splitted[1]));
                //    Touch(p);
                //}
                //else
                Touch(result.TemplatePosition, takeScreenShot);
                return true;
            }
            else
            {
                //Logging.WriteLine(String.Format("Template {0} was not found ", param1));
                return false;
            }
        }

        public void ClickByTemplateOrRaise(Template template)
        {

           WriteToLog(String.Format("Clicking on Template  " + template));

            RecognitionResult result = _TemplateDetector.Recognize(template.TemplateValue,template.TemplateIndex, template.Accuracy , deviceID:DeviceID);
            if (result.IsTemplateFound)
            {
                Touch(result.TemplatePosition);
            }
            else throw new Exception("Coudn't find template ( " + template + " )");
        }


        public string GetScreenText()
        {
            return _TemplateDetector.GetAllText();
        }

        public bool ClickAfterLabel(string template, int textPosition = 1)
        {
            RecognitionResult result = _TemplateDetector.Recognize(template, tempalteIndex:  textPosition, deviceID:DeviceID);
            if (result.IsTemplateFound)
            {
                result.AddToY(40);
                Touch(result.TemplatePosition);
                return true;
            }
            else return false;
        }
        public void Browse(string url)
        {
            //   Point barPostion = new Point(300, 100);
            //_script.Clear();
            //string package ="com.android.browser" ;                                         
            //string activity = ".BrowserActivity" ;
            //string component = package + "/" + activity;
            //_script.AddActivity(package, activity);
            //_script.AddSleep(0.5);
            //_script.AddScrollOnePageUp();
            //_script.AddTouch(barPostion.X, barPostion.Y);
            //_script.AddSleep(0.2);
            //_script.AddType("about:home");
            //_script.AddPressKey("KEYCODE_ENTER");
            //_script.AddSleep(1);
            //_script.AddTouch(barPostion.X, barPostion.Y);
            //_script.AddType(url);
            //_script.AddPressKey("KEYCODE_ENTER");
            //if (isYouTube)
            //{
            //   WriteToLog("YouTube streaming..waitting 30 seconds and then play it..");
            //    _script.AddSleep(30);
            //    _script.AddTouchCenter();
            //}
            //else
            //{
            //    _script.AddSleep(30);
            //    _script.AddScrollOnePageUp();
            //   // _script.AddScrollAllPagesDown();
            //   // _script.AddScrollAllPagesDown();
            //}
            //AddTakeScreenShotToTheScript();
            //GenerateScriptAndExecute();
             WriteToLog("Browsing  " + url);
            CMD cmd = new CMD();
             http://mygreatsite.info
          //  string command = " -s " + DeviceID + "  shell am start -a android.intent.action.VIEW -d "+url+" ";

            string command = " -s " + DeviceID + " shell am start -a android.intent.action.VIEW -n com.android.chrome/.Main -d " + url + " ";
            cmd.ExceuteNoWait("adb", command);
            System.Threading.Thread.Sleep(15000);

           WriteToLog("Wait 15 seconds for the page to load..");

        }

        public void ClickHomeButton()
        {
           WriteToLog("click Home button.");
            _script.Clear();
            _script.AddClickHome();
            // _script.AddClickBack();//wothout it clicking Home shows multiple screens
            AddTakeScreenShotToTheScript();
            GenerateScriptAndExecute();
        }
        public void GoAllBack()
        {

             WriteToLog("Going Back to Home screen.");
            _script.Clear();
            _script.AddClickBack();
            _script.AddClickBack();
            _script.AddClickBack();
            _script.AddClickBack();
            _script.AddClickBack();
            _script.AddClickBack();
            _script.AddClickBack();
            _script.AddClickBack();
            _script.AddClickBack();
            _script.AddClickBack();
            AddTakeScreenShotToTheScript();
            GenerateScriptAndExecute();
        }

        public void GoBack()
        {

           WriteToLog("Going back on View");
            _script.Clear();
            _script.AddClickBack();
            AddTakeScreenShotToTheScript();
            GenerateScriptAndExecute();
        }
        public void ClickMenu()
        {
           WriteToLog("click Menu button..");
            _script.Clear();
            _script.AddClickMenu();
            AddTakeScreenShotToTheScript();
            GenerateScriptAndExecute();
        }

        public void Restart(int timeToWaitAfterRestart)
        {
            string command = " -s " + DeviceID + " shell reboot"; // key event for go back

            GibbonLib.CMD commandLine = new GibbonLib.CMD();
            commandLine.Exceute("adb.exe", command);

        }
        //public void VerifyTextExists()
        //{
        //   WriteToLog("Verifying Text Exists.");
        //   if(Image.get
        //}
        public void Reset()
        {
           

            if ( DeviceID!=null && DeviceID != String.Empty )
            {
                string exiteLogcatCommand = " -s " + DeviceID + " logcat -c";
                //   string backCommand = " -s " + DeviceID + " shell input keyevent 4"; // key event for go back
                string killAdbServer = " -s " + DeviceID + " kill-server"; // key event for go back
                GibbonLib.CMD commandLine = new GibbonLib.CMD();
                commandLine.Exceute("adb.exe", exiteLogcatCommand);
                System.Threading.Thread.Sleep(500);
            }

          //  GoAllBack();

            //  commandLine.Exceute("adb.exe", backCommand);
        }

        Process processTCPDump = null;
        System.Threading.Thread TCPDumpWorker;
        //adb pull /sdcard/namelog.txt c:\temp
        //adb shell tcpdump -s 0 -w /sdcard/zzzzzzz.txt
        public void RunTcpdump(string fileName)
        {
            string tcpdumpCommand = " -s " + DeviceID + " shell tcpdump -s 0 -w /sdcard/" + fileName + "";
            // CommandLine commandLine = new CommandLine();
            // commandLine.Run("adb.exe", tcpdumpCommand);

            TCPDumpWorker = new Thread(() => workerDoWorkTCPDump(tcpdumpCommand));
            TCPDumpWorker.Start();
        }
        public void StopAndPullTcpdumpFile(string fileName)
        {
            if (processTCPDump != null)
                processTCPDump.Close();
            string tcpdumpCommand = " -s " + DeviceID + "  pull  /sdcard/" + fileName + " /TCPDump/" + fileName;
            GibbonLib.CMD commandLine = new GibbonLib.CMD();

            commandLine.Exceute("adb.exe", tcpdumpCommand);
        }


        void workerDoWorkTCPDump(string argument)
        {
            string[] logcatCommandOne = new string[] { argument };
            RunProcess(processTCPDump, logcatCommandOne);
        }
        private void RunProcess(Process process, string[] commands)
        {
            // Create the output message writter

            ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();

            startInfo.FileName = "adb.exe";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = false;
            startInfo.CreateNoWindow = false;
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            startInfo.Arguments = commands[0];
            if (process == null)
            {
                process = new System.Diagnostics.Process();
                process.StartInfo = startInfo;
                process.Start();
            }
            else
            {

            }
        }



        public void WriteToLog(string message)
        {
          Logging.WriteLine(message, DeviceID);
        
        }
        #endregion
        private void RunShellCommand(string shellCommand)
        {
            string fullCcommand = " -s " + DeviceID + " " + shellCommand; // key event for go back
            GibbonLib.CMD commandLine = new GibbonLib.CMD();
            commandLine.Exceute("adb.exe", fullCcommand);
        }
        #region Video Calling IMS
       
        public void RunVideoCalling()
        {
            RunShellCommand("shell am start -n com.movial.video/.phonebook.VCContactOverviewActivity");
            System.Threading.Thread.Sleep(2000); //wait 2 seocnd for the app to run
        }
        #endregion
    }


}
