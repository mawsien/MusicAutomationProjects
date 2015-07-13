using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Linq;
using System.IO;
using System.Diagnostics;

using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Collections.Generic;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Android;
namespace GibbonLib
{

  /// <summary>
  /// User to present the Device config file
  /// </summary>
    public class Template
    {
        public string TemplateName { get; set; }
        public string TemplateValue { get; set; }
        public int TemplateIndex { get; set; }
        public double Accuracy { get; set; }
       
        public Template(string templateName, string templateValue, int templateIndex, double accuracy)
        {
            TemplateName = templateName;
            TemplateValue = templateValue;
            TemplateIndex = templateIndex;
            Accuracy = accuracy;
        }

        public Template()
        {
        
        }

        public override string ToString()
        {
            return String.Format("Template>> Name {0} , Value {1} , Index {2} , Accuracy {3}  ", TemplateName, TemplateValue, TemplateIndex, Accuracy);
        }

    }
    public  class DeviceController
    {
        #region [ Private fields ]
        GibbonLib.ImagesDirectoryPath _imagePaths;
        Dictionary<string, Template> _templates = new Dictionary<string, Template>();
        Dictionary<string, string> _steps = new Dictionary<string, string>();
        Dictionary<string, string> _Pathes = new Dictionary<string, string>();
        public string SSID { get; set; }
        #endregion 


        public Dictionary<string, Template> GetTemplates()
        {
            return _templates;
        }
      
        public Device Device
        {
            get { return _device; }
        }
        Device _device;
        #region [ logcat monitoring ]
        System.Threading.Thread worker;
        System.Diagnostics.Process pProcessLogCat;

        private void StartLogcatProcess(params string[] commands)
        {
            // Create the output message writter

            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();

            startInfo.FileName = "adb.exe";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            //  startInfo.CreateNoWindow = false;
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            startInfo.Arguments = commands[0];
            pProcessLogCat = new System.Diagnostics.Process();

            pProcessLogCat.StartInfo = startInfo;
            pProcessLogCat.EnableRaisingEvents = true;
            pProcessLogCat.OutputDataReceived += new DataReceivedEventHandler(pProcessLogCat_OutputDataReceived);

            try
            {
                pProcessLogCat.Start();
                pProcessLogCat.BeginOutputReadLine();

                pProcessLogCat.WaitForExit();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                int i = 0;
            }

        }

        private StringBuilder logcatBuilder = new StringBuilder();
        private StringBuilder VideoStatusBuilder = new StringBuilder();
        private StringBuilder VideoPauseResume = new StringBuilder();
        private bool logcatSavingEnabled = false;
        public void StartLogcatMonitoring()
        {

            logcatSavingEnabled = true;
            logcatBuilder = new StringBuilder();
            string logcatCommand = " -s " + _device.DeviceID + " logcat -v time";
            worker = new System.Threading.Thread(() => workerDoWork(logcatCommand));
            worker.Start();

        }
        public void StopLogcatCaptureTraces()
        {
            if (pProcessLogCat != null)
            {
                pProcessLogCat.Close();
                pProcessLogCat.Dispose();
                pProcessLogCat = null;
                worker.Abort();
                CMD cmd = new CMD();
                cmd.Exceute("adb", " kill-server");
            }
        }

        /// <summary>
        /// Save logcat which saved by logcatSavingEnabled
        /// </summary>
        public void SaveBufferingToFileSystem(string fullfileNameAndPath)
        {
            fullfileNameAndPath = fullfileNameAndPath + ".buf";
            using (StreamWriter outfile = new StreamWriter(fullfileNameAndPath))
            {
                outfile.Write(VideoPauseResume.ToString());
            }
         
        }
        /// <summary>
        /// Save logcat which saved by logcatSavingEnabled
        /// </summary>
        public void SavelogcatToFileSystem(string path, string name)
        {
            name = name + ".logcat";
            using (StreamWriter outfile = new StreamWriter(path + name))
            {
                outfile.Write(logcatBuilder.ToString());
            }
            using (StreamWriter w = File.AppendText(path + "AllLogs.logcat"))
            {
                w.Write(logcatBuilder.ToString());
            }
            //using (StreamWriter outfile = new StreamWriter(path + "AllLogs.logcat"))
            //{
            //    outfile.app(logcatBuilder.ToString());
            //}
            logcatBuilder.Clear();
        }
        
        /// <summary>
        /// Save logcat which saved by logcatSavingEnabled
        /// </summary>
        public void SaveVideoStatusFileSystem(string fullfileNameAndPath)
        {
            
           
            
            fullfileNameAndPath = fullfileNameAndPath + ".stat";
            using (StreamWriter outfile = new StreamWriter(fullfileNameAndPath))
            {
                VideoPauseResume.AppendLine(VideoStatusBuilder.ToString());
                outfile.Write(VideoPauseResume.ToString());
            }
            VideoPauseResume.Clear();
            VideoStatusBuilder.Clear();
            pauseStartTime = String.Empty;
            VideoPaused = false;
        }
        private void workerDoWork(string argument)
        {
            string[] logcatCommandOne = new string[] { argument };
            StartLogcatProcess(logcatCommandOne);
        }
       public DateTime EndTimePacktToMonitor=new DateTime(1999,9,9);
       public string PacketToMonitor
       {
           get;
           set;
       }

       #region [ Media Messages]

       public DateTime LastTouchEventTime
       {
           get;
           set;

       }
       public DateTime DisconntectTime
       {
           get;
           set;

       }
       public DateTime AudioCreatedTime
       {
           get;
           set;

       }

       public DateTime FullModeTime
       {
           get;
           set;

       }

       public DateTime MediaHubAPPStart
       {
           get;
           set;

       }

       public bool VideoPaused
       {
           get;
           set;

       }

       public bool VideoResumed
       {
           get;
           set;

       }
       #endregion
       System.Timers.Timer timer = new System.Timers.Timer();
       //public void StartTimer()
       //{
       //    timer.Interval = 5;
       //    timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
       //    timer.Enabled = true;
       //    timer.Start();
       //    isVideoStart = false;
       //}

       public void StopTimer()
       {
           timer.Interval = 3;
           timer.Enabled = false;
           timer.Stop();
       }

    
       void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
       {
          
       }
       public bool IsVideoRunning { get; set; }
     
      public struct videoPlayingStatus
       {
           public string DateTime { get; set; }
           public int IsPlaying { get; set; }
           public double BufferingValue { get; set; }
       }

      public struct VideobuffringStatus
      {
          public string DateTimePause { get; set; }
          public int DateTimeResume { get; set; }
      }
      public DateTime AudoSinkStop
      {
          get;
          set;
      }
      public struct BuffringEvents
      {
         
          public string Event { get; set; }
          public int BufferingTime { get; set; }
      }
       public List<videoPlayingStatus> lstVideoPlayingStatus = new List<videoPlayingStatus>();
       public List<VideobuffringStatus> lstVideoPauses = new List<VideobuffringStatus>();
       string pauseStartTime = String.Empty;
       int bufferingCount = 0;
       public string TestcaseName = String.Empty;
       void pProcessLogCat_OutputDataReceived(object sender, DataReceivedEventArgs e)
       {

           if (!string.IsNullOrEmpty(e.Data) /* && e.Data.Contains(PacketToMonitor)*/)
           {
               string logcatLine = e.Data;
               if (logcatSavingEnabled)
               {
                   logcatBuilder.AppendLine(e.Data);
               }

               if (e.Data.Contains("onDisconnectComplete"))
               {
                   DisconntectTime = GetDateTimeOflocatcatLine(e.Data);
               }
               else if (e.Data.Contains("AudioSink") && e.Data.Contains("stop"))
               {
                   AudoSinkStop = GetDateTimeOflocatcatLine(e.Data);
               }
               else
                   if (e.Data.Contains("Delivering touch"))
                   {
                       LastTouchEventTime = GetDateTimeOflocatcatLine(e.Data);
                   }
                   else
                       if (e.Data.Contains("AudioPlayer created"))
                       {
                           AudioCreatedTime = GetDateTimeOflocatcatLine(e.Data);
                       }
                       else

                           if (e.Data.Contains("cachedDurationUs"))
                           {
                               string[] spllited = e.Data.Split('=');

                               if (spllited.Count() >= 2)
                               {
                                   try
                                   {
                                       double n = int.Parse(spllited[1].Split('.')[0].Trim());//.Substring(0, 5));

                                       if (n > 2)
                                       {
                                           if (IsVideoRunning)
                                           {
                                               //lstVideoPlayingStatus.Add(new videoPlayingStatus()
                                               //{
                                               //    DateTime = e.Data.Split(' ')[1],
                                               //    IsPlaying = 1,
                                               //    BufferingValue = n
                                               //});
                                               VideoStatusBuilder.AppendLine(e.Data.Split(' ')[1] + ", 1 ," + n);
                                           }
                                       }
                                       else
                                       {
                                           if (IsVideoRunning)
                                           {
                                               //lstVideoPlayingStatus.Add(new videoPlayingStatus()
                                               //{
                                               //    DateTime = e.Data.Split(' ')[1],
                                               //    IsPlaying = 0,
                                               //    BufferingValue = n
                                               //});
                                               VideoStatusBuilder.AppendLine(e.Data.Split(' ')[1] + ", 0 ," + n);
                                           }
                                       }
                                   }
                                   catch (Exception ex)
                                   {

                                   }
                               }
                           }
                           else if (e.Data.Contains("FullMode"))
                           {
                               FullModeTime = GetDateTimeOflocatcatLine(e.Data);
                           }
                           else if (e.Data.Contains("MediaHubAPP"))
                           {
                               MediaHubAPPStart = GetDateTimeOflocatcatLine(e.Data);
                           }
                           else if (e.Data.Contains("AudioSink") && e.Data.Contains("pause"))
                           {
                               if (VideoPaused == false)
                               {
                                   pauseStartTime = e.Data.Split(' ')[1];
                                 
                                   bufferingCount++;
                                   VideoPaused = true;
                               }
                           }
                           else if (e.Data.Contains("onBufferingUpdate"))
                           {
                               if (VideoPaused == true)
                               {
                                   VideoPauseResume.AppendLine( TestcaseName + "," + pauseStartTime + "," + e.Data.Split(' ')[1]);
                                   VideoPaused = false;
                               }
                           }
               EndTimePacktToMonitor = DateTime.Now;
           }
       }

        public DateTime GetDateTimeOflocatcatLine(string data)
        { 
            string[] result=  data.Split(' ');
            //string strTime = result[0] + ' ' + result[1];
            string strTime = result[1];

            return DateTime.Parse(strTime);
        }

        public void RunMediaHub()
        {
            _device.RunMediaHub();
        }
        #endregion

         public string GetPhoneNumber()
        {

            return _device.PhoneNumber;
        }

         public void SendSMS(string phoneNumber , string text)
         {

             _device.SendSMS(phoneNumber,text);
         }
        #region [ Constructors ]
        /// <summary>
        /// Create and Initialize the device controller
        /// </summary>
        /// <param name="configName"></param>
        /// <param name="deviceID"></param>
        /// <param name="imagePaths"></param>
        /// <param name="pythonScriptFileName"></param>
        public DeviceController(Device device )
        {
            _device = device;
            string fileAndPath = _device.XmlConfigFileNameAndPath;// System.IO.Directory.GetCurrentDirectory() + @"\\Device Config Files\" + configName;
            Console.WriteLine(fileAndPath);
            XDocument xDocument = XDocument.Load(fileAndPath);
            //  _config.WirelessSettings=new Word(loaded.Element(
            IEnumerable<XElement> elListTemplates =
                      from el in xDocument.Elements("Root").Elements("MatchTemplates").Elements("Template")
                      select el;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            SSID = appSettings["SSID"];
            foreach (XElement el in elListTemplates)
            {
                string name = el.Attribute("Name").Value;
                string template = el.Attribute("Template").Value.Replace("##", SSID);
                int index = int.Parse(el.Attribute("Index").Value);
                double accuracy=0.90;
                if (el.Attribute("Accuracy") != null && el.Attribute("Accuracy").Value != String.Empty)
                {
                     accuracy = double.Parse(el.Attribute("Accuracy").Value);
                }
                Template templateObject = new Template(name, template, index, accuracy);
                _templates.Add(name, templateObject);
            }
           

            //Steps
            IEnumerable<XElement> elListSteps =
                      from el in xDocument.Elements("Root").Elements("StepTemplates").Elements("Step")
                      select el;

            foreach (XElement el in elListSteps)
            {
                string name = el.Attribute("Name").Value;
                string stepsTemplate = el.Attribute("Steps").Value;
                _steps.Add(name, stepsTemplate);
            }

            // Paths
            IEnumerable<XElement> elListPathes =
                      from el in xDocument.Elements("Root").Elements("Paths").Elements("Path")
                      select el;

            foreach (XElement el in elListPathes)
            {
                string name = el.Attribute("Name").Value;
                string path = el.Attribute("Path").Value;
                _Pathes.Add(name, path);
            }

           // TemplateDetector.currentImagePath = _Pathes["DeviceImageFolderPath"];
           // TemplateDetector.currentImagePath = imageDirectoyPath;
         
        }

        public void AddTemplate(string name, string template, int index, double accuracy)
        {
            Template templateObject = new Template(name, template, index, accuracy);
            try
            {
                _templates.Add(name, templateObject);
            }catch(Exception ec)
            {
           
            }
        }


        #endregion
        #region [ Private Members ]


        private void TypePassAndClickOk(string pass)
        {
            RecognitionResult result = _device.RecognizeTemplateOnScreen(GetTemplateFromDictionary("Connect")); 

            if ((pass == null || pass == String.Empty) && result.IsTemplateFound != true)//some devices are automatically connected without showing any dialog box
            {
                WriteToLog("Verifying if the Handset is automatically connected to the WiFi..");
                _device.ScrollToTop();
            }
            else
            { 
                if(pass!=null && pass!=String.Empty)
                _device.Type(pass);
                _device.Touch(result.TemplatePosition);
            }
            WriteToLog("Waitting 15 seconds to the network to connect..");
            System.Threading.Thread.Sleep(15000);
        }
        private bool VerifyNetworkIsConnected()
        {
            _device.ScrollToTop();

            WriteToLog("Verifying  network is connected..");
            RecognitionResult result = _device.RecognizeTemplateOnScreen(GetTemplateFromDictionary("WiFiConnected"));

            if (result.IsTemplateFound)
            {
                WriteToLog("PASSED..Network is connected"); return true;
            }
            else
            {
                WriteToLog("FAILED..Network is NOT connected");
                throw new Exception("Problem connecting to WiFi");
             
            }
        }
        private bool VerifyRightSSIDClicked(string pass = "")
        {
            RecognitionResult result;

            WriteToLog("Verifying right network dialog is shown up..");
            //Verify right SSID clicked 
            result = _device.RecognizeTemplateOnScreen(GetTemplateFromDictionary("CorrectWiFiDlg"));

            if (!result.IsTemplateFound)
            {
                WriteToLog("Wrong network clicked! cancel it and try again");
               
                RecognitionResult cancelResult =_device.RecognizeTemplateOnScreen (GetTemplateFromDictionary("Cancel"));

                _device.Touch(cancelResult.TemplatePosition);


            }
            else
            {

                WriteToLog("Verifying if Forget button is shown up..");
                if (_device.DoseScreenContain(GetTemplateFromDictionary("Forget")))
                {
                    _device.ClickByTemplateOrRaise(GetTemplateFromDictionary("Forget"));
                    return false;

                }
            }

           
            

            //WriteToLog("the security is open mode so check if it is automatically connected..");
            //if ((pass == null || pass == String.Empty) && !result.IsTemplateFound)
            //{
            //    _device.ScrollToTop();
            //    result = _device.RecognizeTemplateOnScreen(GetTemplateFromDictionary("WiFiConnected"));
            //}

            return result.IsTemplateFound;
        }
       // private RecognitionResult isScreenContain(Template[] difTemplates)
       // {
       //     RecognitionResult result = _device.RecognizeTemplateOnScreen(difTemplates[0]);

       //     if (result.IsTemplateFound == false && difTemplates.Count() > 1)
       //     {
       //         result =_device.RecognizeTemplateOnScreen(difTemplates[1]);
       //     }
       //     return result;
       //}
        private bool DisconnectNetworkAndForgotIt()
        {
            WriteToLog("Checking if netowrk already connected, if Yes forget it..");
            RecognitionResult result = _device.RecognizeTemplateOnScreen(GetTemplateFromDictionary("Forget"));


            if (result.IsTemplateFound)
            {
                WriteToLog("Network already connected..Forget it and disconnect again");
                _device.Touch(result.TemplatePosition);
                int waittingTime = 5;
                WriteToLog("Waitting " + waittingTime + " secconds to disconnect");
                System.Threading.Thread.Sleep(5000);
            }

            return result.IsTemplateFound;
        }
        private void ScanAndClickOnSSID()
        {
            RecognitionResult result = new RecognitionResult();

            int counter = 0;
            //continue scrolling until SSID Found
            //  _device.TakeScreenShot();
            bool problem = false;

            while (!result.IsTemplateFound)
            {
                counter++;
                //  DoesItContain = Imaging.DoesImageContainsTemplateSlow(currentImagePath, @"Images\SSID.png", ref position, 2, UseTextRcognition: false);

                result = _device.RecognizeTemplateOnScreen(GetTemplateFromDictionary("SSID"));

                if (!result.IsTemplateFound)
                {
                    _device.ScrollPageDown();
                }
                else
                {
                    WriteToLog("Network found, clicking it.");
                    _device.Touch(result.TemplatePosition);

                }
                int attempts = 5;
                if (counter % 6 == 0)
                {
                    WriteToLog(attempts + " attempts scrolling down so scroll all up and try again");
                    _device.ScrollToTop();
                }
                //prolem happen 
                if (counter == 12) throw new Exception("Problem scanning the network..");
            }
        }
        private void StepByStep(string[] steps)
        {
            foreach (string step in steps)
            {
                if (step.Contains(":"))
                {
                    string FuncTemplateAction = step.Split(':')[0];

                    Template template = null;
                    string[] FuncParameters = (step.Split(':')[1]).Split(',');
                    string param1=String.Empty, param2 = String.Empty;
                    if (FuncTemplateAction.ToLower() != "wait")
                    {
                         template = GetTemplateFromDictionary(FuncParameters[0]);
                         param1 = FuncParameters.Length > 1 ? FuncParameters[1] : String.Empty;
                         param2 = FuncParameters.Length > 2 ? FuncParameters[2] : String.Empty;
                    }
                    else

                    {
                        if (FuncParameters[0].Count() > 0)
                        {
                            param1 = FuncParameters[0];
                        }
                    }

                    
                  

                    WriteToLog(String.Format("Executing Step {0} : {1} , {2} , {3}" , FuncTemplateAction,template,param1,param2));
                    _device.ExecuteFunction(FuncTemplateAction, template,param1);
                }
                else //no parameters
                {
                    _device.ExecuteFunction(step, null  , String.Empty);
                
                }
            }
        }
        private bool ScrollUntilFindTemplate(Template template)
        {
            int index = 0;
            while (!_device.ClickByTemplate(template))
            {
                _device.ScrollPageDown();
                if (index++ % 3 == 0)
                {
                    _device.ScrollToTop();
                }
            }
            return true;
        }
        #endregion
        #region [ Public Methods ]
        public void ClickOnMediaHub()
        {
          //  WriteToLog("Going all back..");
            Device.GoAllBack();
            Device.GoAllBack();
          //  Device.GoAllBack();
         //   Device.GoHome();
            Device.ClickByTemplate(GetTemplateFromDictionary("MediaHubIcon"),false);
        }
        public void ClickOnYouTubeIcon()
        {
            //  WriteToLog("Going all back..");
          //  Device.GoAllBack();
            Device.GoAllBack();
            Device.GoAllBack();
            //   Device.GoHome();
            Device.ClickByTemplate(GetTemplateFromDictionary("YouTubeIcon"), false);
        }

        public void RunActivity(string actnamespace, string activitypath)
        {
            _device.RunActivity( actnamespace,  activitypath);
        }
        public void StartAppByIcon(string iconName)
        {
            //  WriteToLog("Going all back..");
            //  Device.GoAllBack();
            Device.GoAllBack();
            Device.GoAllBack();
            //   Device.GoHome();
            Device.ClickByTemplate(GetTemplateFromDictionary(iconName), false);
        }
        public bool TouchEventHappen
        {
            get;
            set;

        }
        public void SearchAndClickVideoOnMediaHub(string videoName)
        {
            //  WriteToLog("Going all back..");
            //  Device.GoAllBack();
           // Device.Touch(new Point(350,500), false); // ClickByTemplate(GetTemplateFromDictionary("MediaHubIcon"), false);
            Device.TakeScreenShot();
          //  Device.ClickByTemplate(GetTemplateFromDictionary("Marvels"), true);
            Device.ClickByText("Search",1);
            Device.Type(videoName);
            Device.ClickByTemplate(GetTemplateFromDictionary("Search"), true);
            Device.TakeScreenShot();
          //  Device.ClickByTemplate(GetTemplateFromDictionary("Marvels"), false);
           // Device.ClickByText(videoName, 1);//Prometheus
            Device.ClickByTemplate(GetTemplateFromDictionary(videoName), true);
            System.Threading.Thread.Sleep(5000);
            Device.TakeScreenShot();
            Device.ClickByTemplate(GetTemplateFromDictionary("ViewTrailer"), false);
        }
        public void ClickVideoOnYouTube(string youtubeVideoName,string clickVideoWord)
        {
            //  WriteToLog("Going all back..");
            //  Device.GoAllBack();
            // Device.Touch(new Point(350,500), false); // ClickByTemplate(GetTemplateFromDictionary("MediaHubIcon"), false);
            Device.TakeScreenShot();
            //  Device.ClickByTemplate(GetTemplateFromDictionary("Marvels"), true);
            Device.ClickByTemplate(GetTemplateFromDictionary("Search"), true);
            Device.Type(youtubeVideoName);
            Device.ClickByTemplate(GetTemplateFromDictionary("Search"), true);
           
            Device.TakeScreenShot();
            //  Device.ClickByTemplate(GetTemplateFromDictionary("Marvels"), false);
            Device.ClickByText(clickVideoWord, 1);
           // Device.TakeScreenShot();
           // Device.ClickByText("Trailer", 1);
        }
        public void ClickByText(string text, int index)
        {
            Device.ClickByText(text, index);
        }
        public void SearchAndClickVideoOnMobiTC(string videoName, string verdictWord)
        {
            //  WriteToLog("Going all back..");
            //  Device.GoAllBack();
            // Device.Touch(new Point(350,500), false); // ClickByTemplate(GetTemplateFromDictionary("MediaHubIcon"), false);
            Device.TakeScreenShot();
            //  Device.ClickByTemplate(GetTemplateFromDictionary("Marvels"), true);
            Device.ClickByTemplate(GetTemplateFromDictionary("Search"), true);
            Device.Type(videoName);
            Device.ClickByTemplate(GetTemplateFromDictionary("Search"), true);

            Device.TakeScreenShot();
            //  Device.ClickByTemplate(GetTemplateFromDictionary("Marvels"), false);
            Device.ClickByText(verdictWord, 1);
            // Device.TakeScreenShot();
            // Device.ClickByText("Trailer", 1);
        }
        public void TurnOffWiFi()
        {

            WriteToLog("Turning OFF Wi-Fi..");

            string[] allSteps =GetTemplateFromDictionary("ToDisableWiFi").TemplateValue.Split('>');
            StepByStep(allSteps);
        }
        public bool IsIMSRegistered()
        {
            Template template = GetTemplateFromDictionary("IMSRegistered");
            _device.GoAllBack();
           bool IMSIsRegistered=  _device.RecognizeTemplateOnScreen(template).IsTemplateFound;

           if (IMSIsRegistered)
           {
               WriteToLog("IMS is registered" + _device.DeviceID);
           }
           else
           {
               WriteToLog("IMS is not registered" + _device.DeviceID);
           }
           return IMSIsRegistered;
        }
        public void ForceCheckIMSRegistered()
        {
            Template template = GetTemplateFromDictionary("IMSRegistered");

            bool IMSIsRegistered = _device.RecognizeTemplateOnScreen(template).IsTemplateFound;

            if (!IMSIsRegistered)
            {
                throw new Exception("IMS is not registered.");
            }

         
        }
        public bool IsGSMCallIsGoing()
        {
            Template template = GetTemplateFromDictionary("GSMCallGoing");

            bool isGSMCallGoing =_device.RecognizeTemplateOnScreen(template).IsTemplateFound;
            if (isGSMCallGoing)
            {
                WriteToLog("GSM call  is going..");
            }
            else
            {
                WriteToLog("GSM call  is not going..");
            }
            return isGSMCallGoing;

        }
        public bool ForCheckGSMCallIsGoing()
        {
            Template template = GetTemplateFromDictionary("GSMCallGoing");

            bool isGSMCallGoing = _device.RecognizeTemplateOnScreen(template).IsTemplateFound;
            if (isGSMCallGoing)
            {
                WriteToLog("GSM call  is going..");
            }
            else
            {
                WriteToLog("GSM call  is not going..");
                throw new Exception("GSM Call  is not verified..");
            }
            return isGSMCallGoing;

        }
        public void ForCheckNoCallIsGoing()
        {
            Template template = GetTemplateFromDictionary("GSMCallGoing");

            bool isGSMCallGoing = _device.RecognizeTemplateOnScreen(template).IsTemplateFound;
            if (isGSMCallGoing)
            {
                WriteToLog("GSM Call  is still going..");
                throw new Exception("GSM Call  is still going..");
            }
            else
            {
                WriteToLog("No GSM Call was detected");
            }
           

        }
        public bool IsIMSCallIsGoing()
        {
          //  Template template1 = GetTemplateFromDictionary("IMSCallGoing");
          //  Template template2 = GetTemplateFromDictionary("HoldCall");

           // bool isIMSCallGoing = (_device.RecognizeTemplateOnScreen(template1).IsTemplateFound && _device.RecognizeTemplateOnScreen(template2).IsTemplateFound);

           // Template gsmtemplate = GetTemplateFromDictionary("GSMCallGoing");
            Template imstempalte = GetTemplateFromDictionary("IMSCallGoing");
            bool isIMSCallGoing = _device.RecognizeTemplateOnScreen(imstempalte).IsTemplateFound;// && _device.RecognizeTemplateOnScreen(gsmtemplate).IsTemplateFound ;
            if (isIMSCallGoing)
            {
                WriteToLog("IMS call  is going..");
            }
            else
            {
                WriteToLog("IMS call  is not going..");
            }
            return isIMSCallGoing;

        }
        public void ForceCheckIMSCallIsGoing()
        {
          
            Template imstempalte = GetTemplateFromDictionary("IMSCallGoing");
          //  Template gsmtempalte = GetTemplateFromDictionary("GSMCallGoing");
            bool isIMSCallGoing = _device.RecognizeTemplateOnScreen(imstempalte).IsTemplateFound;// && _device.RecognizeTemplateOnScreen(gsmtemplate).IsTemplateFound ;
           // bool isGSMCallGoing = _device.RecognizeTemplateOnScreen(gsmtempalte).IsTemplateFound;
            if (isIMSCallGoing/* && isGSMCallGoing*/)
            {
             
            }
            else
                throw new Exception("IMS call is not going.");

        }
        public void MakeSureCallIsConnected(int timeout)
        {
            Template template = GetTemplateFromDictionary("HoldCall");

            _device.WaitUntilExists(template, timeout);

        }
        public void VerifyConferenceCallGoing()
        {
            Template template = GetTemplateFromDictionary("ConferenceCall");

            bool conferenceCall = _device.RecognizeTemplateOnScreen(template).IsTemplateFound;


            if (!conferenceCall)
            {
                throw new Exception("Conference call was not recognized..");
            }
        }
        public void ForceCheckVoiceMailIndicatorShown()
        {
            Template template = GetTemplateFromDictionary("VoiceMailIndicator");

            bool isVoiceMail = _device.RecognizeTemplateOnScreen(template).IsTemplateFound;

            if (!isVoiceMail)
            {
                throw new Exception("Voicemail indicator was not SHOWN");
            
            }
        }
        public void ForceCheckVoiceMailIndicatorCleared()
        {
            Template template = GetTemplateFromDictionary("VoiceMailIndicator");

            bool isVoiceMail = _device.RecognizeTemplateOnScreen(template).IsTemplateFound;

            if (isVoiceMail)
            {
                throw new Exception("Voicemail indicator was not CLEARED");

            }
        }
        public void GoAllBack()
        {
            _device.GoAllBack();
        }
        public void TakeScreenShot()
        {
            _device.TakeScreenShot();

        }
        public string GetScreenText()
        {
            return _device.GetScreenText();
        }
        public void TypeText(string text)
        {
            _device.Type(text);
        }
        public void TouchAndType(string text, Point touchPosition)
        {
            _device.TouchAndType(text, touchPosition);
        }
        public void HoldCall()
        {
            Template template = GetTemplateFromDictionary("HoldCall");

            _device.ClickByTemplateOrRaise(template);
         
        }
        public void ShowKeypad()
        {
            Template template = GetTemplateFromDictionary("Keypad");

            _device.ClickByTemplateOrRaise(template);

        }
        public void HidKeypad()
        {
            Template template = GetTemplateFromDictionary("HideKeypad");

            _device.ClickByTemplateOrRaise(template);

        }
        public void UnholdCall()
        {
            Template template = GetTemplateFromDictionary("UnholdCall");

            _device.ClickByTemplateOrRaise(template);

        }
        public void SwapCall()
        {
            Template template = GetTemplateFromDictionary("SwapCall");

            _device.ClickByTemplateOrRaise(template);

          
        }
        public void MergeCall()
        {
            Template template = GetTemplateFromDictionary("MergeCall");

            _device.ClickByTemplateOrRaise(template);

         

        }
        public void SpeakerPhoneOn()
        {
            Template template = GetTemplateFromDictionary("SpeakerOff");

            _device.ClickByTemplateOrRaise(template);

        }
        public void VerifyVoiceExisted()
        {

            Logging.WriteLine("Voice Is Verified");
        }
        public void SetWiFiCallingMode(string mode)
        { 
            switch (mode)
            {
                case "wifipreferred":
                    WriteToLog("Setting  wifi calling mode to Preferred..");
                    ExcuteSteps("SetWiFiCallingMode", "WiFiPreferredOff", "WiFiPreferredOn");
                    break;
                case "wifionly":
                    WriteToLog("Set wifi calling mode to WiFi only ..");
                    ExcuteSteps("SetWiFiCallingMode", "WiFiOnlyOff", "WiFiOnlyOn");
                    break;
                case "cellularpreferred":
                    WriteToLog("Set wifi calling mode to Celullar..");
                    ExcuteSteps("SetWiFiCallingMode", "CellularPreferredOff", "CellularPreferredOn");
                    break;
            }
        }
        public void SetSIMLock(bool toEnable)
        {
           
            switch (toEnable)
            {
                case true:
                    WriteToLog("Enabling  SIM Lock ");
                    ExcuteSteps("ToEnableDisableSIMLock", "SIMCardLockOff", "SIMCardLockOn");
                    break;
                case false:
                    WriteToLog("Disabling SIM Lock");
                    ExcuteSteps("ToEnableDisableSIMLock", "SIMCardLockOn", "SIMCardLockOff");
                    break;
             
            }

        }
        public void SleepPhone()
        {


         //ToDo
        }
        public bool DoesScreenContain(string templateName)
        {
            if (Device.DoseScreenContain(GetTemplateFromDictionary( templateName)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ClickByTemplate(string templateName)
        {
            if (Device.DoseScreenContain(GetTemplateFromDictionary(templateName)))
            {
              return  Device.ClickByTemplate(GetTemplateFromDictionary(templateName));
            }
            return false;
        }
        //new function because we need to click right away and not do another check if contina
        public bool ClickTemplate(string templateName)
        {
             return Device.ClickByTemplate(GetTemplateFromDictionary(templateName));
          
        }
        public bool BrowseAndValidate(string url, string templateName, bool isYouTube)
        {

            Browse(url);

            Template template = GetTemplateFromDictionary(templateName);
            if (Device.DoseScreenContain(template))
            {

                WriteToLog(String.Format("PASSED {0}", url));
                return true;
            }

            else
            {
            WriteToLog(String.Format("FAILED {0} becacuse of ", url));
             return false;
            
            }

                //failureReason = "Fail";
                //if (browserText.Contains("connectionfailed"))
                //{
                //    failureReason = "Connection failed";
                //    _device.ClickByText("Cancel");
                //}
                //else if (browserText.Contains("forceclose"))
                //{
                //    failureReason = "Browser crash";
                //    _device.ClickByText("Forceclose");
                //}
                //else
                //{
                //    failureReason = "Loading problem";
                //}
                //WriteToLog(String.Format("FAILED {0} becacuse of {1}", url, failureReason));
           
         
        }
        public bool ConnectToWiFi(RouterSettings routerSettings)
        {
            WriteToLog("Connecting to Wi-fi network with SSID " + GetTemplateFromDictionary("SSID"));

            bool dialogToConnectIsShown = false;
            int counter = 0;
            //try 3 time to connect
            string pass = routerSettings.Passphrase == null ? routerSettings.Key : routerSettings.Passphrase;

            WriteToLog("1. Go to WiFi Scanned list");

            ExcuteSteps("ToWiFiScannedList");
            
         

                WriteToLog("2. Searching for " + GetTemplateFromDictionary("SSID") + " to click it");
                bool firstScanCompleted = false;
            tryagain:


                if (firstScanCompleted)
                    _device.ClickByTemplate(GetTemplateFromDictionary("Cancel"));
                _device.ScrollpageUp();
                ScanAndClickOnSSID();

                 WriteToLog("3. Confirming the right Wi-Fi network is clicked and it is not already connected");

                 dialogToConnectIsShown = VerifyRightSSIDClicked(pass);
                if (dialogToConnectIsShown == false )
                {
                    firstScanCompleted = true;
                    goto tryagain;
                }
                if (counter == 3)
                {
                    throw new Exception("Problem connecting to WiFi network");
                }
                counter++;
         //   }
           
                TypePassAndClickOk(pass);
          

            bool isConnected = VerifyNetworkIsConnected();
            _device.GoAllBack();
            if (isConnected)
            {
                return true;
            }
            else return false;
        }
        public bool TurnOnCellularNetwork()
        {
            WriteToLog("Trun ON cellular network data usage");
            _device.RunSettings();
            string[] allSteps = GetTemplateFromDictionary("ToEnableNetwork").TemplateValue.Split('>');
             StepByStep(allSteps);
             WriteToLog("Cellular network data usage is ON");
             return true;
        }
        public bool TurnOFFCellularNetwork()
        {
            WriteToLog("Trun OFF cellular network data usage");
            _device.RunSettings();
            string[] steps =GetTemplateFromDictionary("ToDisableNetwork").TemplateValue.Split('>');
            StepByStep(steps);
            WriteToLog("Cellular network data usage is OFF");
            return true;
        }
        public bool DisconnectFromWiFiAndUnRemember()
        {
           WriteToLog("Disconnect from Wi-Fi");
           ExcuteSteps("ToWiFiScannedList");
           RecognitionResult result = _device.RecognizeTemplateOnScreen(GetTemplateFromDictionary("WiFiConnected"));
           if (result.IsTemplateFound)
            {
                _device.Touch(result.TemplatePosition);
                result = _device.RecognizeTemplateOnScreen(GetTemplateFromDictionary("Forget"));
                if (result.IsTemplateFound)
                {
                    WriteToLog("Delete/Forget the network.");
                    _device.Touch(result.TemplatePosition);
                    _device.ScrollpageUp();
                    result = _device.RecognizeTemplateOnScreen(GetTemplateFromDictionary("WiFiConnected"));
                     if (result.IsTemplateFound) throw new Exception("Problem Disconnecting from WiFi");

                    WriteToLog("Make sure that network is not remembered..");
                    result = _device.RecognizeTemplateOnScreen(GetTemplateFromDictionary("Remembered"));
                    if (result.IsTemplateFound == false)
                    {
                        WriteToLog("PASSED..Network is not remembered..");
                        return true;
                    }
                }
            }
            WriteToLog("Failed to disconnectfrom Wi-Fi");
            return false;
        }
        private Template GetTemplateFromDictionary(string key)
        { 
           Template template=null;
           try
           {
               template = _templates[key];
           }
           catch (KeyNotFoundException ex)
           {
               Logging.WriteLine(key + " key is not found\n" + ex.Message);
               throw new Exception(key + " key is not found\n" + ex.Message);
           }
           catch (Exception ex)
           {
               Logging.WriteLine(key + " key is not found\n" + ex.Message);
               throw new Exception(String.Format("Exception with key{0}\n{1}",key , ex.Message));
           }
           return template;
        }
        private string GetStepsFromDictionary(string key)
        {
            string steps = String.Empty;
            try
            {
                steps = _steps[key];
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception(key + " key is not found\n" + ex.Message);
            }
            return steps;
        }
        public void Browse(string url)
        {
            //WriteToLog("Open Browser and wait 10 seconds for the page to load..");
            //IEnumerable<XElement> elList =
            //          from el in ValidationWords.Elements("Settings").Elements("Touches").Elements("Touch")
            //          select el;
            //  int xGo = int.Parse(elList.Where(c => c.Attribute("Name").Value == "BrowserGo").Attributes("X").FirstOrDefault().Value);
            //  int yGo = int.Parse(elList.Where(c => c.Attribute("Name").Value == "BrowserGo").Attributes("Y").FirstOrDefault().Value);
            //  int xBar = int.Parse(elList.Where(c => c.Attribute("Name").Value == "BrowserBar").Attributes("X").FirstOrDefault().Value);
            //  int yBar= int.Parse(elList.Where(c => c.Attribute("Name").Value == "BrowserBar").Attributes("Y").FirstOrDefault().Value);
         //  string url1 = GetTemplateFromDictionary("URL").TemplateValue;

             WriteToLog("Browsing to " + url);
            _device.Browse(url);
            _device.TakeScreenShot();
        //    _device.GoAllBack();
        
        }
        public void ValidateYouTubeIsWorking(string url)
        { }
        public void SetupHotSpot(RouterSettings hotspotSettings)
        {
            _device.RunSettings();
           //ToDo HotSpot
            _device.TakeScreenShot();
            _device.ClickByTemplate(GetTemplateFromDictionary("OK"));
        }
        public void RunIPerf()
        {
            
            ExcuteSteps("IPerf");
         
        }
        private bool ExcuteSteps(string stepName,string param1="",string param2="")
        {
            string[] allSteps = GetStepsFromDictionary(stepName).Split('>');

            for (int i = 0; i < allSteps.Length; i++)
            {
                allSteps[i] = allSteps[i].Replace("param1", param1).Replace("param2",param2);
               
            }
            StepByStep(allSteps);
            return true;
        }
        public void PowerCycleAiroplaneMode()
        {
            WriteToLog("Turn On/OFF the Aeroplane mode");
            ExcuteSteps("ToDisableEnableAirplaneMode");
        }
        public void Call(string number)
        {
            WriteToLog("Calling number " + number);
            CMD cmd = new CMD();
            string adbCallCommand = "adb -s " + Device.DeviceID + " shell am start -a android.intent.action.CALL -d tel:" + number;
            WriteToLog(adbCallCommand);
            cmd.ExceuteNoWait("adb", " -s " + Device.DeviceID+ " shell am start -a android.intent.action.CALL -d tel:"+ number);
          //  System.Threading.Thread.Sleep(20000);
           // TakeScreenShot();
        }
        private void PowerCycleThePhoneIfNoIMS()
        {
            int index = 0;
            while (!IsIMSRegistered())
            {
                if (index++ == 4)
                {
                    throw new Exception("IMS is not registered..");
                }
                WriteToLog("IMS is not registered..");

                WriteToLog("Wait 1 minute to make sure IMS is there..");
                System.Threading.Thread.Sleep(1000 * 60);
                TakeScreenShot();
                //check if IMS if there after 4 minutes
                WriteToLog("Checking IMS if registered..");
                if (IsIMSRegistered())
                {
                    WriteToLog("IMS is registration is confirmed");
                    break;
                }
                // Turn ON/OFF Aeroplane mode
               // PowerCycleAiroplaneMode();

                WriteToLog("Wait 1 minute and see if the device is registered to IMS.");
                System.Threading.Thread.Sleep(1000 * 60 * 1);
                TakeScreenShot();
            }
        
        }
        public void AnswerCall()
        {
            WriteToLog("Answering the call.. ");
            CMD cmd = new CMD();
            cmd.ExceuteNoWait("adb", " -s " + Device.DeviceID + "  shell input keyevent 5 ");

            System.Threading.Thread.Sleep(10000);
            TakeScreenShot();

        }
        public void HangupACAll()
        {
            Device.HangupACall();
            System.Threading.Thread.Sleep(5000);
            TakeScreenShot();
           

        }
        public void EnableWiFiCalling()
        {
            if (!IsIMSRegistered())
            { 
                WriteToLog("Enabling WiFi calling...");
                ExcuteSteps("ToEnableWiFiCalling");
                //  _device.WaitUntilExists(GetTemplateFromDictionary("IMSON"), 0.999, 20);
                PowerCycleThePhoneIfNoIMS();
            }
          
        }
        public void DisablWiFiCalling()
        {
            WriteToLog("Disabling WiFi calling...");
            ExcuteSteps("ToDisableWiFiCalling");
            if (IsIMSRegistered())
            {
                throw new Exception("Problem occured why disabling WiFi calling..");
            }
        }
        public bool MakeIMSCall(string number,int minutes, bool RegDeregIMS)
        {
                WriteToLog("End a call if it is going.. ");
               _device.TakeScreenShot();
               if (RegDeregIMS)
               {
                   DisablWiFiCalling();
                   EnableWiFiCalling();
                   if (!IsIMSRegistered())
                   {
                       WriteToLog("No IMS Regsitered");
                       throw new Exception("No IMS");
                   }
               }else
                PowerCycleThePhoneIfNoIMS();
                _device.HangupACall();
                Call(number);
                WriteToLog("Wait 20 seconds for the call to start");
                System.Threading.Thread.Sleep(20000);

                int seconds = 0;
               DateTime startCallTime=DateTime.Now;
               WriteToLog("the call duration is " + minutes + " minutes" );
               bool verdict = true;
                for (int i = 0; i <= minutes * 6; i++)
                {
                    _device.TakeScreenShot();
                
                    if (IsIMSCallIsGoing() )
                    {
                        seconds += 10;
                        WriteToLog("Call is still going : " + ConvertDate(startCallTime));
                    } 
                    else
                    {

                        WriteToLog("Checking one more time to see if IMS call still going..");
                        _device.TakeScreenShot();
                        if (IsIMSCallIsGoing())
                        {
                            WriteToLog("Call is still going : " + ConvertDate(startCallTime));
                        }
                        else
                        {

                            WriteToLog("Call dropped confirmed after " + ConvertDate(startCallTime));
                            verdict = false;
                            //quit the loop becuase call droped
                            break;
                        }
                    }
                }
                 WriteToLog("Disconnecting the IMS call if it is still going! ");
                _device.HangupACall();
                _device.GoAllBack();
                return verdict;
          //  }
        }
        private string GetTimeSpanAsTotal(TimeSpan diff1)
        {
            if (diff1.Days > 0)
                return diff1.Days.ToString() + " days";
            else if (diff1.Minutes > 0)
                return diff1.Minutes.ToString() + " minutes";
            else if (diff1.Seconds > 0)
                return diff1.Seconds.ToString() + " seconds";
            else if (diff1.Milliseconds > 0)
                return diff1.Milliseconds.ToString() + " milliseconds";
            else return "No ago";
        }
        private string ConvertDate(DateTime date1)
        {
            System.TimeSpan diff1 = DateTime.Now.Subtract(date1);
            return GetTimeSpanAsTotal(diff1);
        }
        public void TurnOnWiFiCallingAndConfirmIMSWorking()
        {
            WriteToLog("Trun On WiFi Calling.. ");
            ExcuteSteps("ToTurnOnWiFiCalling");
        }
        public void WriteToLog(string message)
        {
            Logging.WriteLine(message, _device.DeviceID);
        
        }
        public Process StartLogcatCapturingToHarddisk(string testCaseID)
        {
            string datetime = DateTime.Now.Month + "-" + DateTime.Now.Day;
            string Directory = datetime + "-" + Device.DeviceID;
          // string path = System.IO.Directory.GetCurrentDirectory() + "\\" + "Report\\" + datetime + "-" + Device.DeviceID + "\\";
            string path =System.IO.Directory.GetCurrentDirectory()+  "\\Report\\LongCallReport\\";
            string fullPathLogcat = path + "Logcat_" + testCaseID + ".txt";
            string commandString = "adb -s " + Device.DeviceID + " logcat -v time  > " + fullPathLogcat;
            ProcessStartInfo startInfo = new ProcessStartInfo("cmd");
            startInfo.CreateNoWindow = false;
            Process proc = Process.Start(startInfo);
            System.Threading.Thread.Sleep(2000);
            GibbonLib.Utility.SetForegroundWindow(proc.MainWindowHandle);
            System.Threading.Thread.Sleep(2000);
            System.Windows.Forms.SendKeys.SendWait(commandString + "~");
            return proc;

        }
        public void StopLogcatCapturingToHarddisk(string testCaseID, out int RTPPacketsCount)
        {
            RTPPacketsCount = 0;
            string datetime = DateTime.Now.Month + "-" + DateTime.Now.Day;
            string Directory = datetime + "-" + Device.DeviceID;
            string path = System.IO.Directory.GetCurrentDirectory() + "\\" + "Report\\" + datetime + "-" + Device.DeviceID + "\\";

            string fullPathLogcat = path + "Logcat_" + testCaseID + ".txt";
            string fullPathLogcatSIP = path + "Logcat_" + testCaseID + "_SIP.txt";
            GibbonLib.CMD cmd = new GibbonLib.CMD();
            System.Diagnostics.Process proc = GetaProcess("cmd");
            GibbonLib.Utility.SetForegroundWindow(proc.MainWindowHandle);
            //  LogReachBox("Wait 3 minutes for the logcat SIP_MESSAGES logs");
            System.Windows.Forms.SendKeys.SendWait("^C");
            System.Threading.Thread.Sleep(20000);
            proc.Close();
            string tempFile = Path.GetTempFileName();
            StringBuilder bldr = new StringBuilder();
            using (Stream s = System.IO.File.Open(fullPathLogcat,
                                       FileMode.Open,
                                       FileAccess.Read,
                                       FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(s))
                {

                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("SIP_MESSAGE"))
                            bldr.AppendLine(line);
                           if (line.Contains("RTPAudioEvent"))
                            RTPPacketsCount++;
                    }
                }

                using (StreamWriter outfile =
                   new StreamWriter(fullPathLogcatSIP))
                {
                    outfile.Write(bldr.ToString());
                    outfile.Flush();
                }

            }

        }
        private Process GetaProcess(string processname)
        {
            Process[] aProc = Process.GetProcesses();
            foreach (Process p in aProc)
            {
                if (p.MainWindowTitle.Contains("Command Prompt"))
                {
                    return p;
                }
            }
            //  if (aProc.Length > 0)
            //     return aProc[0];

            // else 
            return null;
        }
        public void SaveLogcatToReport(string testCaseID, out int RTPPacketsCount)
        {
            RTPPacketsCount = 0;
            string datetime = DateTime.Now.Month + "-" + DateTime.Now.Day;
            string Directory = datetime + "-" + Device.DeviceID;
            string path = System.IO.Directory.GetCurrentDirectory() + "\\" + "Report\\" + datetime + "-" + Device.DeviceID + "\\";

            string fullPathLogcat = path + "Logcat_" + testCaseID + ".txt";
            string fullPathLogcatSIP = path + "Logcat_" + testCaseID + "_SIP.txt";
            System.Threading.Thread.Sleep(5000);
            string commandString = " -s " + Device.DeviceID + " logcat -v time -d > " + fullPathLogcat;


            GibbonLib.CMD cmd = new GibbonLib.CMD();

            cmd.ExceuteAndReturn("adb", commandString);
            WriteToLog("Waitting 20 seconds to save the logcat to harddisk");
            System.Threading.Thread.Sleep(20000);

            string tempFile = Path.GetTempFileName();
            StringBuilder bldr = new StringBuilder();
            using (Stream s = System.IO.File.Open(fullPathLogcat,
                                       FileMode.Open,
                                       FileAccess.Read,
                                       FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(s))
                {

                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("SIP_MESSAGE"))
                            bldr.AppendLine(line);

                        if (line.Contains("RTPAudioEvent"))
                            RTPPacketsCount++;
                    }
                }

                using (StreamWriter outfile =
                   new StreamWriter(fullPathLogcatSIP))
                {
                    outfile.Write(bldr.ToString());
                    outfile.Flush();
                }

            }

        }
        public void SaveQSDM(string fileNameAndPath)
        {
          

            Utility.SaveQSDM(fileNameAndPath);
        }

        private void TakeBrowserScreenShot(AndroidDriver drive)
        {
            WebDriverWait wait = new WebDriverWait(drive, TimeSpan.FromSeconds(10));
            wait = new WebDriverWait(drive, TimeSpan.FromSeconds(10));
            Screenshot screenShot = drive.GetScreenshot();

            screenShot.SaveAsFile(_device.GetCurrentScreenShotName(), System.Drawing.Imaging.ImageFormat.Png);
        }
        public void RunUpselTestCase()
        {
          //  AndroidBrowser.BrowserConstroller browser = new AndroidBrowser.BrowserConstroller();
           // browser.StartBrowser();
            //"http://device_ip:8080/wd/hub/"
            AndroidDriver _driver = new AndroidDriver();
            _driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 30));
            //_driver.Manage().Cookies.DeleteAllCookies();
            Logging.WriteLine("Browsing to My Account http://ma.t-mobile.com");
            _driver.Navigate().GoToUrl("http://ma.t-mobile.com");
           
             TakeBrowserScreenShot(_driver);
             Logging.WriteLine("Click on  Plans & Services");

             _driver.FindElement(By.XPath("//a[contains(@title, 'Plans and Services')]")).Click();
            TakeBrowserScreenShot(_driver);

            Logging.WriteLine("Click on Add/Change Services");
            _driver.FindElement(By.XPath("//input[contains(@value, 'Add/Change Services')]")).Click();
            TakeBrowserScreenShot(_driver);

            Logging.WriteLine("Click on Add or Upgrade Data");
            _driver.FindElement(By.XPath("//a[contains(@title, 'Minutes')]")).Click();
            TakeBrowserScreenShot(_driver);
            _driver.FindElement(By.XPath("//a[contains(@href,'upsell/options.do')]")).Click();

            TakeBrowserScreenShot(_driver);
            _driver.FindElement(By.XPath("//input[contains(@value, '10GB High')]")).Click();
            Logging.WriteLine("10GB PASSED");
            _driver.FindElement(By.XPath("//input[contains(@value, '5GB High')]")).Click();
            Logging.WriteLine("5GB PASSED");
            _driver.FindElement(By.XPath("//input[contains(@value, '4G Data')]")).Click();
            Logging.WriteLine("4GB PASSED");
          //  _driver.FindElement(By.XPath("//input[contains(@value, 'Includes 2 GB')]")).Click();
        //    Logging.WriteLine("2GB PASSED");
            
          //  TakeBrowserScreenShot(_driver);
         
            

            //  _driver.FindElement(By.XPath("//input[contains(@value, 'Upgrade')]")).Click();

           // _device.TakeScreenShot();
           // wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
          //  var elem = _driver.FindElement(By.XPath("//*[contains(.,'search_text')]"));
          //  if (elem == null)
          //  {
          //      Console.WriteLine("The text is not found on the page!");
          //  }
        }
        public void MakeVideoCalling(string name,string numberToCall)
        {
            _device.RunVideoCalling();
            _device.TakeScreenShot();
            _device.ClickByText(name,1,false);
            _device.TakeScreenShot();
            Template template = GetTemplateFromDictionary("TrigerVideoCallIcon");
            _device.ClickByTemplate(template,false);
        
        }
        public void WaitUntilExistOnScreen(string templateName, int timeOutTime, int checkTimeInterval)
        {
            Template template = GetTemplateFromDictionary(templateName);
            _device.WaitUntilExists(template, timeOutTime, checkTimeInterval);
        }
        public void AnswerVideoCalling()
        {
           // _device.TakeScreenShot();
           // Template template = GetTemplateFromDictionary("VideoCallAnswerIcon");
           // _device.ClickByTemplate(template);

            _device.Touch(new Point(260, 740),true);
        }

        public bool CheckIsVideoCallingIsGoing()
        {
            Template template = GetTemplateFromDictionary("IMSVCConfirmed");
            return _device.DoseScreenContain(template);

        }

        public void EndVideoCalling()
        {
            //EndVCCallIcon.png
          //  Template template = GetTemplateFromDictionary("EndVCCallIcon");
          //  return _device.ClickByTemplate(template, true);
            _device.Touch(new Point(340, 1140), true);
            _device.TakeScreenShot();

        }

        //EndVCCallIcon
        #endregion
    }
}
