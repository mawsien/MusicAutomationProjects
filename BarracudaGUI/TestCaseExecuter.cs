using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GibbonLib;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Android;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Timers;
namespace Testing
{
 
    class TestCaseExecuter
    {
      const  int A = 0, B = 1, C = 2, D = 3, E = 4, F = 5, G = 6;
        List<DeviceController> _DeviceControllers;
        RouterController _RouterController;

        DeviceController _A = null;
        DeviceController _B = null;
        DeviceController _C = null;
        private  Timer aTimer;
        List<string> bands = Utility.GetFileLinesFromFile("Channels\\all.txt");
        #region Methods
        public  TestCaseExecuter()
        {
          
            
        }
        public void SetDevices(List<DeviceController> device_Controllers, RouterController routerController)
        {

            _DeviceControllers = device_Controllers;
            if (routerController != null)
                _RouterController = routerController;

            if (_DeviceControllers.Count>=1)
            {
                _A = _DeviceControllers[A];
            }
            if (_DeviceControllers.Count >=2)
            {
                _B = _DeviceControllers[B];
            }
            if (_DeviceControllers.Count >=3)
            {
                _C = _DeviceControllers[C];
            }
        }
        public void ExecuteTestCaseRegDereg(TestItemResult testCaseItem)
        {
                try
                {
                    _DeviceControllers[0].DisablWiFiCalling();
                    _DeviceControllers[0].EnableWiFiCalling();
                    _DeviceControllers[0].IsIMSRegistered();
                     testCaseItem.TestCaseVerdict = STEPRESULT.PASSED;
                }
                catch (Exception ex)
                {
                    testCaseItem.TestCaseVerdict = STEPRESULT.FAILED;
                }
        }
        public void ExecuteTestCase(TestItemResult testCaseItem)
        {

            foreach (DeviceController deviceController in _DeviceControllers)
            {
               // deviceController.GoAllBack();
            }

            RouterSettings routerSettings = null;
            if (testCaseItem.TestCaseID == "0.1")
            {
                switch (testCaseItem.MusiceServiceName)
                {
                    case "Pandora":
                        Pandora(testCaseItem);
                        break;
                    case "SoundCloud":
                        SoundCloud(testCaseItem);
                        break;
                        case "BlackPlanet":
                        BlackPlanet(testCaseItem);
                        break;
                        case "Spotify":
                        Spotify(testCaseItem);
                        break;
                       
                            
                }
            }
        }
        #endregion
        #region Phones Helper functions
        private void ResetPhones(int numberOfPhoneNeeded)
        {
            Logging.WriteLine("Reseting the " + numberOfPhoneNeeded + " Phones");

            for (int i = 0; i < numberOfPhoneNeeded; i++)
            {
                _DeviceControllers[i].HangupACAll();
            }
        }
        private void WaitToAnswerTheCall()
        {
            Logging.WriteLine("Wait 10 seconds and then asnwer the call");
            System.Threading.Thread.Sleep(10000);
        }
        private void WaitToEnableWiFiCalling()
        {
            System.Threading.Thread.Sleep(30000);
        }
        private void Wait(int seconds)
        {
            Logging.WriteLine("Wait " + seconds + " seconds");
            System.Threading.Thread.Sleep(seconds * 1000);

        }
        private void MergeTheCall()
        {
            /*Merge the call for conference*/
            Logging.WriteLine("Phone A Merge the call");
            _DeviceControllers[A].TakeScreenShot();
            _DeviceControllers[A].MergeCall();

            Logging.WriteLine("Wait 10 seconds for the merge");
            Wait(10);
            Logging.WriteLine("Verify Conference call is going..");
            _DeviceControllers[A].TakeScreenShot();
            _DeviceControllers[A].VerifyConferenceCallGoing();
        }
        #endregion Phones Helper functions
        #region Radios
        public static bool StopTest = false;
        private  void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
              StopTest = true;
              Logging.WriteLine("Stoping the radio for");
              aTimer.Elapsed -= OnTimedEvent;
              aTimer.Enabled = false;
        }
      
       
        private string PostSteps(TestItemResult testCaseItem, string dataUsageDisplay, double dataUsageStart, GibbonLib.CMD commandLine1)
        {
            double dataUsageEnd = GetDataUsageHelper(ref dataUsageDisplay);

            _DeviceControllers[A].ClickByText("ok", 1);

            double totalDataUsage = dataUsageEnd - dataUsageStart;

            testCaseItem.DataUsage = GibbonGUI.Utility.BytesToStringHelper(totalDataUsage);

            _DeviceControllers[A].Device.GoAllBack();
            _DeviceControllers[A].ClickByTemplate("DialerIconStart");
            Logging.WriteLine("Stop TCPDump", messageType: 2);
            _DeviceControllers[A].TypeText("*#9900#");
            _DeviceControllers[A].TakeScreenShot();
            _DeviceControllers[A].Device.ScrollPageDownTwoPages();
            _DeviceControllers[A].TakeScreenShot();
            _DeviceControllers[A].ClickTemplate("TCPDUMPSTOP");
            _DeviceControllers[A].ClickTemplate("TCPDUMPSTOP_OK");

            //wait until ok button is exist , it tires 5 time with interval 60 seconds
            _DeviceControllers[A].Device.ScrollpageUp();
            _DeviceControllers[A].Device.ScrollpageUp();
            _DeviceControllers[A].Device.ScrollpageUp();
            _DeviceControllers[A].ClickTemplate("TCPDUMPSTATE");
            _DeviceControllers[A].WaitUntilExistOnScreen("TCPDUMPSTOP_OK", 10, 60);
            _DeviceControllers[A].ClickTemplate("TCPDUMPSTOP_OK");
            Wait(60);
            _DeviceControllers[A].TakeScreenShot();
            _DeviceControllers[A].ClickTemplate("TCPDUMPSTOP_OK");
            _DeviceControllers[A].ClickTemplate("TCPDUMPTOSDCARD");
            _DeviceControllers[A].WaitUntilExistOnScreen("TCPDUMPSTOP_OK", 50, 5);
            _DeviceControllers[A].ClickTemplate("TCPDUMPSTOP_OK");
            _DeviceControllers[A].Device.GoAllBack();
            Logging.WriteLine("Pulling TCP Dump from Device", messageType: 2);
            // Logging.WriteLine("Analysing TCP Dump", messageType: 2);
            //  Logging.WriteLine("AShowing TCP Dump Stats", messageType: 2);
            //adb pull  sdcard/log/tcpdump/ c:/temp/tcpdump

            //   Logging.WriteLine("Pulling and writting tcp dump to this folder", messageType: 2);
            string fileName1 = "tcpdump_" + testCaseItem.Title + "_" + DateTime.Now.Millisecond + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Year;
            string distinationPath = System.Configuration.ConfigurationManager.AppSettings["tcpdumpDestination"];
            string tcpdumpCommand1 = " -s " + _DeviceControllers[A].Device.DeviceID + "  pull  /sdcard/log/tcpdump/ " + distinationPath + "/tcpump/" + fileName1;
            Logging.WriteLine("tcp-dump command  " + tcpdumpCommand1, messageType: 2);

            //System.Reflection.Assembly.GetEntryAssembly().Location.Replace('\\','/')
            commandLine1.Exceute("adb.exe", tcpdumpCommand1);
            // commandLine1.Exceute("adb.exe", " -s " + _DeviceControllers[A].Device.DeviceID + " shell rm -r /sdcard/log");
            Logging.WriteLine("tcp-dump pulled to  " + distinationPath + "/tcpump/" + fileName1, messageType: 2);
            testCaseItem.tcpdumpLogPath = distinationPath + "/tcpump/" + fileName1;
            return dataUsageDisplay;
        }

        private void PreSteps(out string dataUsageDisplay, out double dataUsageStart, out GibbonLib.CMD commandLine1)
        {
            //  _DeviceControllers[A].Device.RunTcpdump("pandora.tcpdump");
            //  System.Threading.Thread.Sleep(30000);
            //  _DeviceControllers[A].Device.StopAndPullTcpdumpFile("pandora.tcpdump");
            //  _DeviceControllers[A].Call("*#9900#");

            //  _DeviceControllers[A].Device.Restart(30);

            // _DeviceControllers[A].TakeScreenShot();
           
            dataUsageDisplay = String.Empty;
            dataUsageStart = 0;
            commandLine1 = new GibbonLib.CMD();
            Logging.WriteLine("Delete Logs and restart device", messageType: 2);

            string deleteLogCommands = " -s " + _DeviceControllers[A].Device.DeviceID + " shell rm -r /sdcard/log";
            Logging.WriteLine("Delete sdcard/log file " + deleteLogCommands, messageType: 2);

            //System.Reflection.Assembly.GetEntryAssembly().Location.Replace('\\','/')
            commandLine1.Exceute("adb.exe", deleteLogCommands);
            System.Threading.Thread.Sleep(10000);
            //   commandLine1.Exceute("adb.exe","reboot");
            //   System.Threading.Thread.Sleep(40000);

            _DeviceControllers[A].Device.GoAllBack();
            dataUsageDisplay = String.Empty;
            dataUsageStart = GetDataUsageHelper(ref dataUsageDisplay);

            //   _DeviceControllers[A].ClickByText("ok", 1);
            _DeviceControllers[A].ClickTemplate("WEB_OK");
            Logging.WriteLine("Start TCPDump", messageType: 2);
            _DeviceControllers[A].ClickTemplate("DialerIconStart");
            _DeviceControllers[A].TypeText("*#9900#");
            //_DeviceControllers[A].Call("*#9900#");
            _DeviceControllers[A].TakeScreenShot();
            _DeviceControllers[A].Device.ScrollPageDownTwoPages();
            _DeviceControllers[A].TakeScreenShot();
            _DeviceControllers[A].ClickTemplate("TCPDUMPSTART");
            _DeviceControllers[A].ClickTemplate("TCPDUMPSTART_ANY");
            _DeviceControllers[A].ClickTemplate("TCPDUMPSTART_OK");
            _DeviceControllers[A].Device.GoAllBack();
        }
        private void BlackPlanet(TestItemResult testCaseItem)
        {
            string dataUsageDisplay = String.Empty;
            double dataUsageStart = 0;
            GibbonLib.CMD commandLine1 = null;
            int maxduration = int.Parse(testCaseItem.Param1) * 60;

            _DeviceControllers[A].AddTemplate("StartIcon", "StartIcon.png", 1, 0.9);
            _DeviceControllers[A].AddTemplate("Play", "Play.png", 1, 0.9);
            _DeviceControllers[A].AddTemplate("Pause", "Pause.png", 1, 0.9);
            _DeviceControllers[A].AddTemplate("CloseAds", "CloseAds.png", 1, 0.9);
            _DeviceControllers[A].AddTemplate("Menu", "Menu.png", 1, 0.9);
            _DeviceControllers[A].AddTemplate("Stations", "Stations.png", 1, 0.9);
            _DeviceControllers[A].AddTemplate("Exit", "Exit", 1, 0.9);
            PreSteps(out dataUsageDisplay, out dataUsageStart, out commandLine1);
            // Device.GoHome();
            //    aTimer = new System.Timers.Timer(int.Parse(testCaseItem.Param1)*1000);
            //   aTimer.Elapsed += OnTimedEvent;
            //   aTimer.Enabled = true;
            //   Logging.WriteLine("Starting timer for   " + testCaseItem.Param4);
            double totalSeconds = 0;
            //keep runing until time passed
            while (true)
            {
                foreach (string band in bands)
                {
                    DateTime timeStart = DateTime.Now;
                    _DeviceControllers[A].Device.GoAllBack();
                    Logging.WriteLine("Play band " + band, messageType: 2);
                    Logging.WriteLine("Start Black Planet", messageType: 2);
                    _DeviceControllers[A].ClickTemplate("StartIcon");
                    if (_DeviceControllers[A].DoesScreenContain("Pause"))
                    {
                        _DeviceControllers[A].ClickTemplate("Pause");
                    }

                    if (_DeviceControllers[A].DoesScreenContain("Menu"))
                    {
                        _DeviceControllers[A].ClickTemplate("Menu");
                        _DeviceControllers[A].ClickTemplate("Stations");

                        Logging.WriteLine("Waitting 15 for seconds for the Genere to load");
                        System.Threading.Thread.Sleep(15000);
                    }

                   
                   // _DeviceControllers[A].TakeScreenShot();
                  //  Logging.WriteLine("Waitting 35 for " + band + " to play and skip and ads");
                  //  System.Threading.Thread.Sleep(35000);
                    if (_DeviceControllers[A].ClickTemplate("CloseAds"))
                    {
                        _DeviceControllers[A].TakeScreenShot();
                    }

                    if (_DeviceControllers[A].DoesScreenContain(band))
                    {
                        Logging.WriteLine("Clicking on " + band + " play list", messageType: 2);
                        _DeviceControllers[A].ClickTemplate(band);
                        Logging.WriteLine("Waitting 35 for " + band + " to play and skip and ads");
                        System.Threading.Thread.Sleep(35000);
                        _DeviceControllers[A].TakeScreenShot();
                    }
                    else
                    {

                        totalSeconds += DateTime.Now.Subtract(timeStart).TotalSeconds;
                        timeStart = DateTime.Now;
                        if (totalSeconds > maxduration)
                        {
                            Logging.WriteLine("Test ended  after " + TimeSpan.FromSeconds(totalSeconds).ToString(@"hh\:mm\:ss"));
                            goto outofLoop;
                        }
                        continue; //band was not found to loop to another band
                    }

                    if (_DeviceControllers[A].ClickTemplate("CloseAds"))
                    {
                        _DeviceControllers[A].TakeScreenShot();
                    }
                    if (_DeviceControllers[A].ClickTemplate("RadioStart"))
                    {
                        _DeviceControllers[A].TakeScreenShot();
                    }

                    Logging.WriteLine("Checking if Pandora Playing", messageType: 2);
                    //wait 45 minutes

                    for (int i = 1; i <= 30; i++)
                    // for (int i = 1; i <= 5; i++)
                    {
                        timeStart = DateTime.Now;
                        _DeviceControllers[A].TakeScreenShot();
                        bool radioIsPlaying = false;

                        if (_DeviceControllers[A].DoesScreenContain("Pause"))// check if music is playing
                        {
                            radioIsPlaying = true;
                            Logging.WriteLine("Pandora song is playing, will check every 1 minutes...........", messageType: 3);
                        }


                        if (!radioIsPlaying)
                        {
                            if (_DeviceControllers[A].ClickTemplate("RadioStart"))//check if the Ads conver all the screeen so the start icon is on the top right
                            {
                                if (_DeviceControllers[A].DoesScreenContain("Pause"))//if music playing pause it
                                {
                                    radioIsPlaying = true;
                                }
                            }
                        }
                        if (!radioIsPlaying)
                        {
                            //  throw new Exception("Radio playing is not detected");
                            Logging.WriteLine("Radio playing is not detected");
                            _DeviceControllers[A].Device.GoAllBack();
                            totalSeconds += DateTime.Now.Subtract(timeStart).TotalSeconds;
                            timeStart = DateTime.Now;
                            if (totalSeconds > maxduration)
                            {
                                Logging.WriteLine("Test ended  after " + TimeSpan.FromSeconds(totalSeconds).ToString(@"hh\:mm\:ss"));
                                goto outofLoop;
                            }
                            //Go to another brand
                            break;
                        }
                        totalSeconds += DateTime.Now.Subtract(timeStart).TotalSeconds;
                        timeStart = DateTime.Now;
                        if (totalSeconds > maxduration || StopTest == true)
                        {
                            Logging.WriteLine("Test ended  after " + TimeSpan.FromSeconds(totalSeconds).ToString(@"hh\:mm\:ss"));
                            goto outofLoop;
                        }


                        // if (i % 4 == 0)
                        // {

                        // }

                        //white one minute and check every second if the test stops
                        for (int x = 0; x <= 60; x++)
                        {
                            System.Threading.Thread.Sleep(1000);
                            ++totalSeconds;

                            if (totalSeconds > maxduration || StopTest == true)
                            {
                                Logging.WriteLine("Test ended  after " + TimeSpan.FromSeconds(totalSeconds).ToString(@"hh\:mm\:ss"));
                                goto outofLoop;
                            }
                        }
                        Logging.WriteLine("Test still playing  , duration is  " + TimeSpan.FromSeconds(totalSeconds).ToString(@"hh\:mm\:ss"));
                        //       _DeviceControllers[A].ClickTemplate("PandroaRadioPause");
                        //       _DeviceControllers[A].ClickTemplate("PandoraRadioStart");




                        //     Logging.WriteLine("Stopping Pandora by clicking Pause", messageType: 2);
                        //     _DeviceControllers[A].ClickTemplate("PandroaRadioPause");
                        //    
                    }
                }
            }


        outofLoop:
            _DeviceControllers[A].Device.GoAllBack();
            if (StopTest)
            {
                Logging.WriteLine("Stopping The Run", messageType: 2);
                _DeviceControllers[A].ClickTemplate("PandroaRadioPause");

                _DeviceControllers[A].Device.GoAllBack();
            }
            dataUsageDisplay = PostSteps(testCaseItem, dataUsageDisplay, dataUsageStart, commandLine1);
        }
        private void SoundCloud(TestItemResult testCaseItem)
        {

            string dataUsageDisplay = String.Empty;
            double dataUsageStart = 0;
            GibbonLib.CMD commandLine1 = null;
            int maxduration = int.Parse(testCaseItem.Param1) * 60;
            foreach (string band in bands)
            {
                _DeviceControllers[A].AddTemplate(band, band, 1, 0.9);
            }
            PreSteps(out dataUsageDisplay, out dataUsageStart, out commandLine1);
            double totalSeconds = 0;
            //leep runing until duration time passed
            #region Musicservice
            while (totalSeconds < maxduration)
            {
                foreach (string band in bands)
                {
                    DateTime timeStart = DateTime.Now;
                    _DeviceControllers[A].Device.GoAllBack();
                    Logging.WriteLine("Play band " + band, messageType: 2);
                    Logging.WriteLine("Start music service", messageType: 2);
                    _DeviceControllers[A].ClickTemplate("StartIcon");
                    //  _DeviceControllers[A].TakeScreenShot();
                    _DeviceControllers[A].ClickTemplate("PandroaRadioPause");
                    //  _DeviceControllers[A].TakeScreenShot();
                    _DeviceControllers[A].ClickTemplate("PandoraBackToList");
                    //   _DeviceControllers[A].TakeScreenShot();
                    _DeviceControllers[A].ClickTemplate("CreateStation");
                    //  _DeviceControllers[A].TakeScreenShot();
                    _DeviceControllers[A].TypeText(band);
                    _DeviceControllers[A].TakeScreenShot();
                    _DeviceControllers[A].ClickByText(band, 2);
                    //  _DeviceControllers[A].TakeScreenShot();
                    //  Logging.WriteLine("Clicking on " + band + " play list", messageType: 2);
                    // _DeviceControllers[A].ClickTemplate(band);
                    Logging.WriteLine("Waitting 35 for " + band + " to play and skip and ads");
                    System.Threading.Thread.Sleep(35000);
                    _DeviceControllers[A].TakeScreenShot();

                    if (_DeviceControllers[A].DoesScreenContain("SkipTpPandora"))
                    {
                        _DeviceControllers[A].ClickTemplate("SkipTpPandora");
                        //   _DeviceControllers[A].TakeScreenShot();
                    }

                    if (_DeviceControllers[A].DoesScreenContain("PandoraRadioStart"))
                    {
                        _DeviceControllers[A].ClickTemplate("PandoraRadioStart");
                        //  _DeviceControllers[A].TakeScreenShot();
                    }
                    Logging.WriteLine("Checking if Pandora Playing", messageType: 2);
                    //wait 45 minutes for each channel
                    int minutewait = 30;
                    while (minutewait > 0)
                    {
                        minutewait--;
                        timeStart = DateTime.Now;
                        _DeviceControllers[A].TakeScreenShot();

                        //check if the music is playing
                        if (_DeviceControllers[A].DoesScreenContain("PandroaRadioPause"))// check if music is playing
                        {
                            Logging.WriteLine("Pandora song is playing, will check every 1 minutes...........", messageType: 3);
                        }
                        else
                        {
                            //  throw new Exception("Radio playing is not detected");
                            Logging.WriteLine("Radio playing is not detected");
                            _DeviceControllers[A].Device.GoAllBack();
                            totalSeconds += DateTime.Now.Subtract(timeStart).TotalSeconds;
                            //Go to another brand
                            Logging.WriteLine("Will play another band");
                            break;
                        }

                        System.Threading.Thread.Sleep(1000 * 60);
                        totalSeconds += DateTime.Now.Subtract(timeStart).TotalSeconds;

                        if (totalSeconds > maxduration || StopTest == true)
                        {
                            Logging.WriteLine("Test ended  after " + TimeSpan.FromSeconds(totalSeconds).ToString(@"hh\:mm\:ss"));
                            goto endoftest;
                        }
                        Logging.WriteLine("Test still playing  , duration is  " + TimeSpan.FromSeconds(totalSeconds).ToString(@"hh\:mm\:ss"));
                    }
                }
            }
            #endregion
        endoftest:
            Logging.WriteLine("Test ended  after " + TimeSpan.FromSeconds(totalSeconds).ToString(@"hh\:mm\:ss"));
            _DeviceControllers[A].Device.GoAllBack();
            if (StopTest)
            {
                Logging.WriteLine("Stopping The Run", messageType: 2);
                _DeviceControllers[A].ClickTemplate("PandroaRadioPause");
                _DeviceControllers[A].Device.GoAllBack();
            }
            dataUsageDisplay = PostSteps(testCaseItem, dataUsageDisplay, dataUsageStart, commandLine1);
        }
        private void Pandora(TestItemResult testCaseItem)
        {
            string dataUsageDisplay=String.Empty;
            double dataUsageStart=0;
            GibbonLib.CMD commandLine1 = null;
            int maxduration=int.Parse(testCaseItem.Param1)*60;
            foreach (string band in bands)
            {
                _DeviceControllers[A].AddTemplate(band, band, 1, 0.9);
            }
           PreSteps(out dataUsageDisplay, out dataUsageStart, out commandLine1);
            double totalSeconds=0;
            //leep runing until duration time passed
            #region Musicservice
            while (totalSeconds < maxduration)
            {
                foreach (string band in bands)
                {
                    DateTime timeStart = DateTime.Now;
                    _DeviceControllers[A].Device.GoAllBack();
                    Logging.WriteLine("Play band " + band, messageType: 2);
                    Logging.WriteLine("Start Pandora", messageType: 2);
                    _DeviceControllers[A].ClickTemplate("PandoraStartIcon");
                  //  _DeviceControllers[A].TakeScreenShot();
                    _DeviceControllers[A].ClickTemplate("PandroaRadioPause");
                  //  _DeviceControllers[A].TakeScreenShot();
                    _DeviceControllers[A].ClickTemplate("PandoraBackToList");
                 //   _DeviceControllers[A].TakeScreenShot();
                    _DeviceControllers[A].ClickTemplate("CreateStation");
                  //  _DeviceControllers[A].TakeScreenShot();
                    _DeviceControllers[A].TypeText(band);
                     _DeviceControllers[A].TakeScreenShot();
                    _DeviceControllers[A].ClickByText(band,2);
                  //  _DeviceControllers[A].TakeScreenShot();
                  //  Logging.WriteLine("Clicking on " + band + " play list", messageType: 2);
                   // _DeviceControllers[A].ClickTemplate(band);
                    Logging.WriteLine("Waitting 35 for " + band + " to play and skip and ads");
                    System.Threading.Thread.Sleep(35000);
                    _DeviceControllers[A].TakeScreenShot();
                   
                    if (_DeviceControllers[A].DoesScreenContain("SkipTpPandora"))
                    {
                        _DeviceControllers[A].ClickTemplate("SkipTpPandora");
                     //   _DeviceControllers[A].TakeScreenShot();
                    }

                    if (_DeviceControllers[A].DoesScreenContain("PandoraRadioStart"))
                    {
                        _DeviceControllers[A].ClickTemplate("PandoraRadioStart");
                      //  _DeviceControllers[A].TakeScreenShot();
                    }
                    Logging.WriteLine("Checking if Pandora Playing", messageType: 2);
                    //wait 45 minutes for each channel
                     int minutewait = 30;
                    while (minutewait > 0)
                    {
                        minutewait--;
                        timeStart = DateTime.Now;
                        _DeviceControllers[A].TakeScreenShot();
                    
                        //check if the music is playing
                        if (_DeviceControllers[A].DoesScreenContain("PandroaRadioPause"))// check if music is playing
                        {
                            Logging.WriteLine("Pandora song is playing, will check every 1 minutes...........", messageType: 3);
                        } else {
                            //  throw new Exception("Radio playing is not detected");
                            Logging.WriteLine("Radio playing is not detected");
                            _DeviceControllers[A].Device.GoAllBack();
                            totalSeconds += DateTime.Now.Subtract(timeStart).TotalSeconds;
                            //Go to another brand
                            Logging.WriteLine("Will play another band");
                            break;
                        }
                     
                           System.Threading.Thread.Sleep(1000*60);
                            totalSeconds += DateTime.Now.Subtract(timeStart).TotalSeconds;

                            if (totalSeconds > maxduration || StopTest == true)
                            {
                                Logging.WriteLine("Test ended  after " + TimeSpan.FromSeconds(totalSeconds).ToString(@"hh\:mm\:ss"));
                                goto endoftest;
                            }
                            Logging.WriteLine("Test still playing  , duration is  " + TimeSpan.FromSeconds(totalSeconds).ToString(@"hh\:mm\:ss"));
                      }            
                   }
            }
            #endregion 
        endoftest:
            Logging.WriteLine("Test ended  after " + TimeSpan.FromSeconds(totalSeconds).ToString(@"hh\:mm\:ss"));
            _DeviceControllers[A].Device.GoAllBack();
            if (StopTest)
            {
                Logging.WriteLine("Stopping The Run", messageType: 2);
                _DeviceControllers[A].ClickTemplate("PandroaRadioPause");
                _DeviceControllers[A].Device.GoAllBack();
            }
           dataUsageDisplay = PostSteps(testCaseItem, dataUsageDisplay, dataUsageStart, commandLine1);
        }
        private void Spotify(TestItemResult testCaseItem)
        {
            string dataUsageDisplay = String.Empty;
            double dataUsageStart = 0;
            GibbonLib.CMD commandLine1 = null;
            int maxduration = int.Parse(testCaseItem.Param1) * 60;
            foreach (string band in bands)
            {
                _DeviceControllers[A].AddTemplate(band, band, 1, 0.9);
            }
            //   PreSteps(out dataUsageDisplay, out dataUsageStart, out commandLine1);
            double totalSeconds = 0;
            //leep runing until duration time passed
            #region Musicservice
            while (totalSeconds < maxduration)
            {
                foreach (string band in bands)
                {

                    DateTime timeStart = DateTime.Now;
                    _DeviceControllers[A].Device.GoAllBack();
                    Logging.WriteLine("Play band " + band, messageType: 2);
                    Logging.WriteLine("Start music service", messageType: 2);
                    _DeviceControllers[A].ClickTemplate("StartIcon");
                    //  _DeviceControllers[A].TakeScreenShot();
                    _DeviceControllers[A].ClickTemplate("MenuLines");
                    //  _DeviceControllers[A].TakeScreenShot();
                    _DeviceControllers[A].ClickTemplate("SearchMusic");
                    //  _DeviceControllers[A].TakeScreenShot();
                    _DeviceControllers[A].TypeText(band);
                    System.Threading.Thread.Sleep(5000);
                    _DeviceControllers[A].TakeScreenShot();
                    _DeviceControllers[A].ClickByText(band, 2);
                    //  _DeviceControllers[A].TakeScreenShot();
                    //  Logging.WriteLine("Clicking on " + band + " play list", messageType: 2);
                    // _DeviceControllers[A].ClickTemplate(band);




                    if (_DeviceControllers[A].DoesScreenContain("SufflePlay"))
                    {
                        _DeviceControllers[A].ClickTemplate("SufflePlay");
                        //  _DeviceControllers[A].TakeScreenShot();
                    }
                    /* if (_DeviceControllers[A].DoesScreenContain("SkipAds"))
                    {
                        _DeviceControllers[A].ClickTemplate("SkipAds");
                     //   _DeviceControllers[A].TakeScreenShot();
                    }*/
                    Logging.WriteLine("Waitting 35 for " + band + " to play and skip and ads");
                    System.Threading.Thread.Sleep(35000);
                    _DeviceControllers[A].TakeScreenShot();


                    Logging.WriteLine("Checking if music service Playing", messageType: 2);
                    //wait 45 minutes for each channel
                    int minutewait = 3;
                    while (minutewait > 0)
                    {
                        minutewait--;
                        timeStart = DateTime.Now;
                        _DeviceControllers[A].TakeScreenShot();

                        //check if the music is playing
                        // mchen: 2015_0713
                        // if (true)
                        if (_DeviceControllers[A].DoesScreenContain("Pause"))// check if music is playing
                        {
                            Logging.WriteLine("Music is playing, will check every 1 minutes...........", messageType: 3);
                        }
                        else
                        {
                            //  throw new Exception("Radio playing is not detected");
                            Logging.WriteLine("Music is not detected");
                            _DeviceControllers[A].Device.GoAllBack();
                            totalSeconds += DateTime.Now.Subtract(timeStart).TotalSeconds;
                            //Go to another brand
                            Logging.WriteLine("Will play another band");
                            break;
                        }

                        System.Threading.Thread.Sleep(1000 * 60);
                        totalSeconds += DateTime.Now.Subtract(timeStart).TotalSeconds;

                        if (totalSeconds > maxduration || StopTest == true)
                        {
                            Logging.WriteLine("Test ended  after " + TimeSpan.FromSeconds(totalSeconds).ToString(@"hh\:mm\:ss"));
                            goto endoftest;
                        }
                        Logging.WriteLine("Test still playing  , duration is  " + TimeSpan.FromSeconds(totalSeconds).ToString(@"hh\:mm\:ss"));
                    }
                }
            }
            #endregion
        endoftest:
            Logging.WriteLine("Test ended  after " + TimeSpan.FromSeconds(totalSeconds).ToString(@"hh\:mm\:ss"));
            _DeviceControllers[A].Device.GoAllBack();
            if (StopTest)
            {
                Logging.WriteLine("Stopping The Run", messageType: 2);
                _DeviceControllers[A].ClickTemplate("PandroaRadioPause");
                _DeviceControllers[A].Device.GoAllBack();
            }
            dataUsageDisplay = PostSteps(testCaseItem, dataUsageDisplay, dataUsageStart, commandLine1);
        }
    
        
        private void CheckPlayingMusic(int maxduration, ref double totalSeconds, ref DateTime timeStart, ref int minutewait)
        {
            while (minutewait > 0)
            {
                minutewait--;
                timeStart = DateTime.Now;
                _DeviceControllers[A].TakeScreenShot();

                //check if the music is playing
                if (_DeviceControllers[A].DoesScreenContain("Pause"))// check if music is playing
                {
                    Logging.WriteLine("Pandora song is playing, will check every 1 minutes...........", messageType: 3);
                }
                else
                {
                    //  throw new Exception("Radio playing is not detected");
                    Logging.WriteLine("Radio playing is not detected");
                    _DeviceControllers[A].Device.GoAllBack();
                    totalSeconds += DateTime.Now.Subtract(timeStart).TotalSeconds;
                    //Go to another brand
                    Logging.WriteLine("Will play another band");
                    break;
                }

                System.Threading.Thread.Sleep(1000 * 60);
                totalSeconds += DateTime.Now.Subtract(timeStart).TotalSeconds;

                if (totalSeconds > maxduration || StopTest == true)
                {
                    Logging.WriteLine("Test ended  after " + TimeSpan.FromSeconds(totalSeconds).ToString(@"hh\:mm\:ss"));
                    StopTest = true;
                    break;
                }
                Logging.WriteLine("Test still playing  , duration is  " + TimeSpan.FromSeconds(totalSeconds).ToString(@"hh\:mm\:ss"));
            }
        }
      
     
        static double ConvertGigabytesToBytes(double gigabytes) // BIGGER
        {
            // 1024 gigabytes in a terabyte
            return (gigabytes * 1024.0) * (1024 * 1024);
        }

      
        private double GetDataUsageHelper(ref string dataUsageDisplay)
        {
            _DeviceControllers[A].GoAllBack();
            _DeviceControllers[A].ClickTemplate("DialerIconStart");
          _DeviceControllers[A].TypeText("#932#");
           _DeviceControllers[A].ClickTemplate("DialerIconCall");
         //   _DeviceControllers[A].Call("#932#");
            Logging.WriteLine("Wait 10 seconds to get the datausage message");
            System.Threading.Thread.Sleep(10000);
            
            _DeviceControllers[A].TakeScreenShot();
            string text = _DeviceControllers[A].GetScreenText().ToLower();
            string[] splitters = new string[] { "haveused", "from" };
            dataUsageDisplay = text.Split(splitters, StringSplitOptions.RemoveEmptyEntries)[1];
            double longDataUsageInt = 0;
            if (dataUsageDisplay.ToLower().Contains("mb"))
            {
                double x = double.Parse(dataUsageDisplay.Replace("mb", ""));
                longDataUsageInt = x * (1024 * 1024);
            }else
                if (dataUsageDisplay.ToLower().Contains("gb"))
                {
                    double x = double.Parse(dataUsageDisplay.Replace("gb", ""));
                    longDataUsageInt = ConvertGigabytesToBytes(x);
                }

            return longDataUsageInt;
        }
#endregion
    }
    public enum STEPRESULT { NONE = 0, PASSED = 1, FAILED = 2, INCONC = 3 }
    public class TestCase
    {
        public string TestCaseID { get; set; }
        public string TestCaseName { get; set; }

        public TestItemResult itemResultWeb { get; set; }
        public TestItemResult YouTube { get; set; }

    }
    public class TestItemResult
    {

        public Dictionary<object, object> _Measurments = new Dictionary<object, object>();

        public void AddMeasurments(object key , object value)
        {
            _Measurments.Add(key, value);
        }
        //public Dictionary<object, object> Measurments
        //{
        //    get;
        //    set;
        //}
        public string DeviceID { get; set; }
        public string PhoneNumber { get; set; }
        public string Param1 { get; set; }
        public string Param2 { get; set; }
         public string Param3 { get; set; }
         public string Param4 { get; set; }
        public string TestCaseID { get; set; }
        public string Title { get; set; }

        public DateTime StartTestcaseTime { get; set; }
        public DateTime EndTestcaseTime { get; set; }
       
        public STEPRESULT TestCaseVerdict { get; set; }
        public string Comments { get; set; }
        public int PorgressValue { get; set; }
        public int AttemptQty { get; set; }
        public int NumberOfPhones { get; set; }
     

        public string DataUsage { get; set; }
        public long DataUsageNumber { get; set; }
        public string DataUsagePayload { get; set; }
        public string tcpdumpLogPath { get; set; }
        public string MusiceServiceName { get; set; }
        /// <summary>
        /// The Constructor for the testcaseexecutor
        /// </summary>
        public TestItemResult()
        {
            // StartTestcaseTime = DateTime.Now;
        }
        public override string ToString()
        {
            EndTestcaseTime = DateTime.Now;            

            return String.Format(" {0},  {1} , {2} , {3} , {4} , {5}  ",
                                                                                     TestCaseID,
                                                                                      Title,
                                                                                      StartTestcaseTime.ToString("MM-dd H:mm"),
                                                                                      EndTestcaseTime.ToString("MM-dd H:mm"),
                                                                                      TestCaseVerdict.ToString(),
                                                                                      AttemptQty.ToString() );
        }

        /// <summary>
        /// Write the result item to a file saved in Report folder int he root directory 
        /// </summary>
        public void WriteResultItemToFile()
        {
            string datetime = DateTime.Now.Month + "-" + DateTime.Now.Day;
            string Directory = datetime + "-" + DeviceID;
            string path = "Report\\" + datetime + "-" + DeviceID + "\\";
            string fullPath = path + "Report.csv";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
                //create report file and testcase header

                string header = "TestCaseID,Title,StartTime,EndTime, Verdict , # Attempts , Comments";
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fullPath, true))
                {
                    file.WriteLine(header);
                }
            }

            //System.IO.FileInfo info=new System.IO.FileInfo(path);
            //if(info.Length==0)
            //{
            //  Console.WriteLine("TestCaseID,Title,StartTime,EndTime,RouterSettings,WiFi Con,Web , Video ,WiFi Dis, Rove In/Out , Verdict , # Attempts , Comments");
            //}
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fullPath, true))
            {
                Console.WriteLine(this.ToString());
                file.WriteLine(this.ToString());
            }
        }
        /// <summary>
        /// Write the log cat to a file
        /// </summary>
        /// <param name="logCatlines"></param>
        public void WriteLogcatToFile(string[] logCatlines)
        {
            string datetime = DateTime.Now.Month + "-" + DateTime.Now.Day;
            string Directory = datetime + "-" + DeviceID;
            string path = "Report\\" + datetime + "-" + DeviceID + "\\";
            string fullPath = path + "Logcat_" + TestCaseID + ".txt";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
                //create report file and testcase header
            }
            System.IO.File.WriteAllLines(fullPath, logCatlines);
        }

        public class TestCase
        {

            public int TestId { get; set; }
            public int CallDuration { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public string Status { get; set; }
            public int StatusID
            {
                get
                {

                    switch (Status)
                    {
                        case "PASSED": return 1;
                        case "FAILED": return 2;
                        default: return -1;
                    }
                }
            }
            public string Comment { get; set; }

            public int PorgressValue { get; set; }

        }
    }
}
