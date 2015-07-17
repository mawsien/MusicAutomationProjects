using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;
using GibbonLib;
using WatiN;
using Outlook = Microsoft.Office.Interop.Outlook;
namespace GibbonGUI
{
    public partial class MainForm : Form
    {
        Thread threadCampaign;
        List<Testing.TestItemResult> itemResultList = new List<Testing.TestItemResult>();
        List<Testing.TestCase> lstTestCases = new List<Testing.TestCase>();
        System.Collections.Generic.List<PhoneForm> phoneForms = new System.Collections.Generic.List<PhoneForm>();
        List<DeviceController> _DeviceControllers = new List<DeviceController>();
        string testcaseID = String.Empty;
        int _PhoneCount = 0;

        string columnFilter = " -e frame.len -e http -e ipv6.src -e ipv6.dst -e ip.src -e ip.dst -e  ip.version  -e http.host -e frame.number -e frame.time -e data ";

        public MainForm()
        {

            //  IEManager manager = new IEManager();
            //   manager.GoToComSenderPage("4352013165");
            //GibbonLib.CMD cmd = new GibbonLib.CMD();
            //cmd.Exceute("adb", " kill-server");
            InitializeComponent();
            gridTestCases.AutoGenerateColumns = false;
            workerCampaingExecution.WorkerSupportsCancellation = true;
            string[] filePaths = Directory.GetFiles(@"Router Config Files", "*.txt");

            foreach (string path in filePaths)
            {
                if (path.EndsWith(".txt"))
                {
                    cmbBxRouters.Items.Add(path);
                }
            }
            string testCaseFolder = System.Configuration.ConfigurationManager.AppSettings["Testcases"];
         //   string[] testcaseFiles = System.IO.File.ReadAllLines(testCaseFolder);
            string[] testcaseSettings = System.IO.File.ReadAllLines(testCaseFolder);
            List<string> testcaseFileNames = new List<string>();

            // Copy the files and overwrite destination files if they already exist. 
          //  foreach (string s in testcaseFiles)
           // {
                // Use static Path methods to extract only the file name from the path.
           //     testcaseFileNames.Add(System.IO.Path.GetFileName(s));
           // }
            cmbBxTestcases.DataSource = testcaseFileNames;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            txtBxSSID.Text = appSettings["SSID"];
            //    string phoneSettingsFilePathAndName = "Device Config Files\\Devices.txt";

            //   phoneSettings = GibbonLib.Utility.GetFileLines(phoneSettingsFilePathAndName);
            androidDeviceControl1.SetPhoneNumber(0);
            // tabControl1.TabPages.Remove(tabPage2);

        }
        private void LogReachBox(string msg, int messageType = 0)
        {
            txtLogs.Invoke(new EventHandler(delegate
            {
                System.Drawing.Color selectionColor = System.Drawing.Color.Blue;

                if (messageType == 2)
                {
                    selectionColor = System.Drawing.Color.OrangeRed;
                }
                else if (messageType == 3)
                {
                    selectionColor = System.Drawing.Color.Black;
                }
                txtLogs.SelectedText = string.Empty;
                if (msg.Contains("FAILED")) selectionColor = System.Drawing.Color.Red;
                else if (msg.Contains("PASSED")) selectionColor = System.Drawing.Color.Green;
                txtLogs.SelectionColor = selectionColor;
                string strTestCaseID = testcaseID;
                if (testcaseID != String.Empty)
                {
                    strTestCaseID += ">>";
                }
                txtLogs.AppendText(String.Format("{0:d/M/yyyy HH:mm:ss}", DateTime.Now) + ">>" + strTestCaseID + msg);

                txtLogs.AppendText(System.Environment.NewLine);
                txtLogs.Select(txtLogs.TextLength, 0);
                txtLogs.ScrollToCaret();
            }));
        }
        private void btnRun_Click(object sender, EventArgs e)
        {

            //     string packageName = "com.qiktmobile.android";
            //    string dropCallCommand = " -s  HT09DRM00017 shell am start -a com.qik.android.action.CALL_DROP -n " + packageName + "/com.qik.android.m2m.activity.Main";
            //    CommandLine.Run("adb.exe", dropCallCommand);
            //    Utility.KillProccess("adb");

            try
            {
                if (btnRun.Text == "Run")
                {
                    if (MessageBox.Show("Press OK to Start", "Press OK to Start", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        if (androidDeviceControl1.GetDeviceID() == String.Empty)
                        {
                            MessageBox.Show("Press select device, press refresh to populate devices", "Press select device, press refresh to populate devices", MessageBoxButtons.OKCancel);
                            return;
                        }
                        Testing.TestCaseExecuter.StopTest = false;

                        GibbonLib.Logging.Log += new GibbonLib.Logging.LogHandler(Logging_Log);
                        GibbonLib.Logging.GenerateLogFile("Log");
                        itemResultList.Clear();
                        _DeviceControllers.Clear();
                        timerUpdateImage.Enabled = true;
                        timerUpdateImage.Start();
                        txtLogs.Clear();
                        lblFailed.Text = "0";
                        lblPassed.Text = "0";
                        lblInconcCount.Text = "0";
                        progressBar1.Value = 0;
                        btnPause.Enabled = true;

                        // gridTestCases.Rows.Clear();
                        btnRun.Text = "Stop";
                        btnRun.BackColor = Color.Magenta;
                        try
                        {

                            threadCampaign = new Thread(new ThreadStart(RunTest));

                            threadCampaign.SetApartmentState(ApartmentState.STA);

                            threadCampaign.Start();
                        }
                        catch (Exception ex)
                        {
                            GibbonLib.Logging.WriteLine("Main Exception : " + ex.Message);
                        }
                    }
                }
                else
                {

                    if (MessageBox.Show("Press OK to Stop", "Message", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        btnRun.Text = "Run";
                        Testing.TestCaseExecuter.StopTest = true;
                        btnRun.BackColor = Color.Green;
                        btnPause.Enabled = false;
                        // threadCampaign.Abort();
                        // KillADP();
                        GibbonLib.Logging.Log -= new GibbonLib.Logging.LogHandler(Logging_Log);
                        timerUpdateImage.Enabled = false;
                        timerUpdateImage.Start();
                        //Utility.KillProccess("adb.exe");
                        //  gridCalls.Rows.Clear();
                    }


                }
            }
            catch (Exception ex)
            {
                LogReachBox("Fatal Error happened! " + ex.Message);
            }


        }
        private void KillADP()
        {
            //CommandLine command = new CommandLine();
            // command.Run("taskkill", "/im adb.exe");//taskkill /im notepad.exe /f
        }
        //TestCaseID,Title,Execute,Num Attempts`,Num Phone,Param1,param2,param3,param4

        double datausage = 0;
        public void RunTest()
        {
            string screenShotsPath = GenerateReportFolder(androidDeviceControl1.GetDeviceID());


            string routerConfigFile = String.Empty;
            string routerSsid = String.Empty;
            cmbBxRouters.Invoke(new EventHandler(delegate
            {
                if (cmbBxRouters.SelectedItem != null)
                {
                    routerConfigFile = cmbBxRouters.SelectedItem.ToString();
                    routerSsid = txtBxSSID.Text;
                }

            }));

            string routerIp = "http://" + txtBxRouterIP.Text;

            string strtestcaseID = "";
            //

            string testCasesFilePathAndName = System.Configuration.ConfigurationManager.AppSettings["Testcases"];

            // testCasesFilePathAndName += cmbBxTestcases.SelectedItem.ToString();

        //    cmbBxTestcases.Invoke(new EventHandler(delegate
         //   {
         //       testCasesFilePathAndName += cmbBxTestcases.SelectedItem.ToString();

         //   }));
            List<string[]> lines = GibbonLib.Utility.GetFileLines(testCasesFilePathAndName);
            //    List<string[]> lines = "";
            try
            {
                foreach (string[] l in lines)
                {
                    if (l[2] == "1")//check if to exexute the testcase
                    {
                        Testing.TestItemResult item = new Testing.TestItemResult();


                        ddlMusicServices.Invoke(new EventHandler(delegate
                        {
                            item.MusiceServiceName = ddlMusicServices.SelectedItem.ToString();

                        }));
                        strtestcaseID = l[0];
                        item.TestCaseID = l[0];
                        item.Title = l[1];
                        item.AttemptQty = int.Parse(l[3]);
                        item.NumberOfPhones = int.Parse(l[4]);
                     //   if (l.Length > 4) commnted to use the ddl duration
                     //       item.Param1 = l[5];
                        if (l.Length > 5)
                            item.Param2 = l[6];
                        if (l.Length > 6)
                            item.Param3 = l[7];
                        if (l.Length > 7)
                            item.Param4 = l[8];

                        itemResultList.Add(item);
                      

                        ddlMusicServices.Invoke(new EventHandler(delegate
                        {
                            item.Param1 = (string)cmbbxDuration.SelectedItem.ToString();

                        }));


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message +  " Problem with your testcases CSV file");
                return;
            }



            androidDeviceControl1.GenerateScreenShotsFileSetupDeviceAndStartMonitoring();
            GibbonLib.DeviceController deviceController = new GibbonLib.DeviceController(androidDeviceControl1.Device);
            _DeviceControllers.Add(deviceController);

            foreach (PhoneForm pFrm in phoneForms)
            {
                pFrm.GetAndroidDeviceControl().GenerateScreenShotsFileSetupDeviceAndStartMonitoring();
                GibbonLib.DeviceController dcontroller = new GibbonLib.DeviceController(pFrm.GetAndroidDeviceControl().Device);
                _DeviceControllers.Add(dcontroller);
            }


            GibbonLib.RouterController routerController = null;


            Testing.TestCaseExecuter executor = new Testing.TestCaseExecuter();
            executor.SetDevices(_DeviceControllers, routerController);
            gridTestCases.Invoke(new EventHandler(delegate
            {
                gridTestCases.DataSource = itemResultList;

            }));
            UpdateTCsGridView();
            int i = 0;
            Process cmdProcess = null;//Used for captruing the logcat , we using the CMD to send command using SendKeys
            int testcaseCount = itemResultList.Count;


            for (int j = 0; j <= testcaseCount - 1; j++)
            {
                try
                {
                    if (itemResultList[j] == null)
                    {

                        continue;
                    }

                    GibbonLib.Logging.WriteLine("******************Start TestCase " + itemResultList[i].TestCaseID + "******************");
                    itemResultList[i].StartTestcaseTime = DateTime.Now;
                    testcaseID = itemResultList[i].TestCaseID;
                    executor.ExecuteTestCase(itemResultList[i]);
                    // itemResultList[i].DataUsagePayload = whiteList.bytesNotInWhiteList;
                    itemResultList[i].EndTestcaseTime = DateTime.Now;
                }
                catch (Exception ex)
                {

                    itemResultList[i].TestCaseVerdict = Testing.STEPRESULT.FAILED;
                    itemResultList[i].Comments += ex.Message;

                }
                itemResultList[i].PorgressValue = 100;
                try
                {
                    progressBar1.Invoke(new EventHandler(delegate
                    {
                        progressBar1.Value += 100 / itemResultList.Count;
                        if (itemResultList.Count == i - 1)
                        {
                            progressBar1.Value = 100;
                        }
                    }));
                }
                catch (Exception ex)
                {
                    Logging.WriteLine("Progress"+ex.Message);
                }
                itemResultList[i].DeviceID = androidDeviceControl1.GetDeviceID();
                itemResultList[i].WriteResultItemToFile();
                androidDeviceControl1.ResetDevice();
                GibbonLib.Logging.WriteLine("************************End TestCase " + itemResultList[i].TestCaseID + "************************");


                if (itemResultList[i].TestCaseVerdict == Testing.STEPRESULT.PASSED)
                {


                    lblPassed.Invoke(new EventHandler(delegate
                    {
                        lblPassed.Text = (int.Parse(lblPassed.Text) + 1).ToString();

                    }));

                }
                else if (itemResultList[i].TestCaseVerdict == Testing.STEPRESULT.FAILED)
                    lblPassed.Invoke(new EventHandler(delegate
                    {
                        lblFailed.Text = (int.Parse(lblFailed.Text) + 1).ToString();
                    }));


                else if (itemResultList[i].TestCaseVerdict == Testing.STEPRESULT.INCONC)
                {
                    lblInconcCount.Invoke(new EventHandler(delegate
                    {
                        lblInconcCount.Text = (int.Parse(lblInconcCount.Text) + 1).ToString();

                    }));
                }

                UpdateTCsGridView();

                bool whiteListAnalysisEnabled = false;
                lblInconcCount.Invoke(new EventHandler(delegate
                    {
                        whiteListAnalysisEnabled = chkBxEnableWhiteLIstAnalysing.Checked;

                    }));

                /*create file report folder*/
                // string tcpdumpFile = itemResultList[i].tcpdumpLogPath;
                string tcpdumpFileName = String.Empty;
                try
                {
                    if (whiteListAnalysisEnabled)
                    {
                        if (itemResultList[i].tcpdumpLogPath == null)
                        {
                            string startupPath = Application.StartupPath;
                            using (OpenFileDialog dialog = new OpenFileDialog())
                            {
                                if (dialog.ShowDialog() == DialogResult.OK)
                                {
                                    tcpdumpFileName = dialog.FileName;
                                }
                            }

                            //   StartWhiteListProcess(@"C:\GDrive\tcpump\tcpdump_Pandora_0_12_17_2014\tcpdump_any_20141217135235.pcap");
                            StartWhiteListProcess(tcpdumpFileName);
                        }
                        else
                        {
                            string[] dirs = Directory.GetFiles(itemResultList[i].tcpdumpLogPath);
                            Logging.WriteLine("The number of files starting with c is " + dirs.Length);
                            string lowerdatausage = itemResultList[i].DataUsage.ToLower().Trim();
                            if (lowerdatausage.Contains("kb"))
                            {
                                string du = lowerdatausage.Replace("kb", "").Trim();
                                datausage = double.Parse(du);
                                
                                // mchen: 2015-07-14
                                // datausage *= 1000;
                                datausage *= 1024;
                            }
                            else
                                if (lowerdatausage.Contains("mb"))
                                {
                                    string du = lowerdatausage.Replace("mb", "").Trim();
                                    datausage = double.Parse(du);

                                    // mchen: 2015-07-14
                                    // datausage *= 1000000;
                                    datausage *= 1024 * 1024;
                                }

                            foreach (string tcpdumpFile in dirs)
                            {
                                StartWhiteListProcess(tcpdumpFile);
                                break;

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logging.WriteLine(" Exception : " + ex.Message);
                    //itemResultList[i].tcpdumpLogPath
                    Logging.WriteLine(" Exception : " + itemResultList[i].tcpdumpLogPath);
                }
                i++;
            }
        }
        private void AddItemsToListView(string s)
        {
            if (s.Trim() != String.Empty)
            {
                string[] items = { s };
                Color color = Color.White;
                ListViewItem item = new ListViewItem(items, 0, Color.Black, color, null);
                listView1.Invoke(new EventHandler(delegate
                {
                    listView1.Items.Add(item);
                }));
            }
        }
        #region White list Analysis

        private Process _procCapture = null;

        bool _tsharkstarted = false;
        WhiteListStatLib.WhiteList whiteList = new WhiteListStatLib.WhiteList();
        public void StartWhiteListProcess(string pathtcpdumplog)
        {
            try
            {
                // tsharkTaskkill();
                //   System.Threading.Thread.Sleep(1000);
                string whitelistPathAndName = System.Configuration.ConfigurationManager.AppSettings["WhiteListPath"];
                Logging.WriteLine("White list path " + whitelistPathAndName);
                whiteList.GetWhiteListFromFile(whitelistPathAndName);
                //  white.StartAnalyseNetworkLogs(@"C:\GDrive\tcpump\tcpdump_Pandora_293_12_9_2014\tcpdump_any_20141208154830.pcap");
                //  whiteList.GetWhiteListFromFile(
                _procCapture = new Process();
                string tsharkPathAndName = System.Configuration.ConfigurationManager.AppSettings["TSharkPath"];
                _procCapture.StartInfo.FileName = tsharkPathAndName;

                _procCapture.StartInfo.Arguments = " -l  -r " + pathtcpdumplog + " -T fields ";
                Logging.WriteLine("Whitelist analysis argument " + whitelistPathAndName);
                // " -e frame.number -e frame.time -e wlan.sa -e wlan.da -e radiotap.datarate 
                //-e radiotap.dbm_antsignal -e frame.len -e frame.protocols -e wlan.fc.retry -e wlan.seq -e wlan.fc.type";
                _procCapture.StartInfo.Arguments += columnFilter;// " -e wlan.sa -e wlan.da -e rtp.seq -e wlan.fc.retry ";

                _procCapture.StartInfo.RedirectStandardOutput = true;
                //m_procCapture.StartInfo.RedirectStandardError = true;||
                _procCapture.StartInfo.UseShellExecute = false;
                _procCapture.StartInfo.CreateNoWindow = false;
                _procCapture.EnableRaisingEvents = true;
                _procCapture.OutputDataReceived += new DataReceivedEventHandler(m_procCapture_OutputDataReceived);

                _procCapture.Start();
                _procCapture.BeginOutputReadLine();
                //    _procCapture.WaitForExit();
                _procCapture.Exited += _procCapture_Exited;
                lblAnaylzing.Invoke(new EventHandler(delegate
                {
                    lblAnaylzing.Visible = true;
                }));

            }
            catch (Exception ex)
            {
                Logging.WriteLine("Exception : " + ex.Message);
            }
        }


        Thread updateGridThread;
        void _procCapture_Exited(object sender, EventArgs e)
        {

            updateGridThread = new Thread(new ThreadStart(UpdateGridMethod));
            updateGridThread.Start();

            bool sendEmailWL = false;
            chkBxSendEmailWL.Invoke(new EventHandler(delegate
                {
                sendEmailWL=    chkBxSendEmailWL.Checked;

                }));
            ExportToCSVFiles(sendEmailWL,"Pandora");

        }
        private void UpdateGridMethod()
        {
            try
            {

                //  lblPercentageInWhiteList.Text = (ProccessedDataAmountLength - bytesNotInWhiteList).ToString();
                whiteList.AnalyzeDataUsageStat();

                lblWhiteListedUsage.Invoke(new EventHandler(delegate
                {
                    lblWhiteListedUsage.Text = GibbonGUI.Utility.BytesToStringHelper(whiteList.AllDataUsage - datausage);

                }));


                foreach (string s in whiteList.GetNotWhiteListed())
                {
                    AddItemsToListView(s);
                }


                lblPassed.Invoke(new EventHandler(delegate
                {
                    // mchen: 2015-0714
                    // lblTotalPorcessed.Text = String.Format(new FileSizeFormatProvider(), "{0:fs}", whiteList.NotWhiteListedDataUsage + whiteList.WhiteListedDataUsage);
                    // lblPercentageNotInWhiteList.Text = String.Format(new FileSizeFormatProvider(), "{0:fs}", whiteList.NotWhiteListedDataUsage);
                    // lblPercentageInWhiteList.Text = String.Format(new FileSizeFormatProvider(), "{0:fs}", whiteList.WhiteListedDataUsage);
                    double totalDataUsage = whiteList.NotWhiteListedDataUsage + whiteList.WhiteListedDataUsage;
                    lblPercentageNotInWhiteList.Text = String.Format(new FileSizeFormatProvider(), "{0:0.00}", whiteList.NotWhiteListedDataUsage / totalDataUsage * 100.0);
                    lblPercentageInWhiteList.Text = String.Format(new FileSizeFormatProvider(), "{0:0.00}", whiteList.WhiteListedDataUsage / totalDataUsage * 100.0);
                    lblWhiteListedUsage.Text = Utility.BytesToStringHelper(whiteList.WhiteListedDataUsage); 
                    lblTotalProcessed.Text = Utility.BytesToStringHelper(whiteList.NotWhiteListedDataUsage + whiteList.WhiteListedDataUsage); 
                    lblAnaylzing.Visible = false;
                }));
            }
            catch (Exception ex)
            {
                Logging.WriteLine("Exception in UpdateGridMethod " + ex.Message);
            }
        }

        static string DisplayPercentage(double ratio)
        {
            string percentage = string.Format("Percentage Included in White List  {0:0.0%}", ratio);
            return percentage;
        }
        string[] spliOperators = { "\t" };
        private void m_procCapture_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (e.Data == null) return;
                //   _CapturedDataBuilder.Append( e.Data+"\n");

                string[] SplittedMessages = e.Data.Split(spliOperators, StringSplitOptions.None);

                if (CheckIfSplittedMessagesItemsIsEmpty(SplittedMessages) == false) return;
                // string columnFilter = " -e frame.len -e http -e ipv6.src -e ipv6.dst -e ip.src -e ip.dst -e  ip.version  -e http.host -e frame.number -e frame.time ";
                WhiteListStatLib.Packet packet = new WhiteListStatLib.Packet();

                packet.frame_len = SplittedMessages[0].Trim();


                packet.httpContent = SplittedMessages[1].Trim();

                packet.ipv6_src = SplittedMessages[2].Trim();
                packet.ipv6_dst = SplittedMessages[3].Trim();
                packet.ip_src = SplittedMessages[4].Trim();
                packet.ip_dst = SplittedMessages[5].Trim();
                packet.ip_version = SplittedMessages[6].Trim();
                packet.http_host = SplittedMessages[7].Trim();
                packet.frame_number = SplittedMessages[8].Trim();
                packet.frame_time = SplittedMessages[9].Trim();
                packet.payLoad = (double)SplittedMessages[10].Length;

                //ignore the packet which has no http
                //  if (packet.http_host != String.Empty)
                // {

                whiteList.AddCapturedPacket(packet);

            }
            catch (Exception ex)
            {
                Logging.WriteLine(ex.Message);
            }
            //   }
            //   AddItemsToListView(packet); 
        }

        private bool CheckIfSplittedMessagesItemsIsEmpty(string[] splitString)
        {


            foreach (string s in splitString)
            {
                if (s.Trim() != String.Empty)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsCaptureProcessAlive(int TSharkProcessID)
        {
            Process[] WindowsProcessList = Process.GetProcesses();

            foreach (Process WindowsProcess in WindowsProcessList)
            {
                if (WindowsProcess.Id == TSharkProcessID)
                {
                    return true;
                }
            }
            return false;
        }
        public void StopWhiteListProcess()
        {

            try
            {
                if (null != _procCapture)
                {

                    _tsharkstarted = false;

                    if (!_procCapture.HasExited)
                    {
                        _procCapture.CancelOutputRead();
                        _procCapture.Kill();
                        _procCapture.WaitForExit();
                        _procCapture.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
        private void SaveCapturedFileToTheDisk(Testing.TestItemResult testItem, out int RtpPacektsCount, Process proc)
        {
            RtpPacektsCount = 0;
            string datetime = DateTime.Now.Month + "-" + DateTime.Now.Day;
            string Directory = datetime + "-" + androidDeviceControl1.GetDeviceID();
            string path = System.IO.Directory.GetCurrentDirectory() + "\\" + "Report\\" + datetime + "-" + androidDeviceControl1.GetDeviceID() + "\\";

            string fullPathLogcat = path + "Logcat_" + testItem.TestCaseID + ".txt";
            string fullPathLogcatSIP = path + "Logcat_" + testItem.TestCaseID + "_SIP.txt";
            //  Process proc = GetaProcess("cmd");
            GibbonLib.Utility.SetForegroundWindow(proc.MainWindowHandle);
            LogReachBox("Wait 5 seconds for the logcat SIP_MESSAGES logs");

            System.Threading.Thread.Sleep(5000);
            string commandString = "adb -s " + androidDeviceControl1.GetDeviceID() + " logcat -v time -d > " + fullPathLogcat;
            System.Windows.Forms.SendKeys.SendWait(commandString + "~");
            System.Threading.Thread.Sleep(5000);
            proc.CloseMainWindow();
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
                            RtpPacektsCount++;
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
        public string GenerateReportFolder(string deviceID)
        {
            string datetime = DateTime.Now.Month + "-" + DateTime.Now.Day;
            string FolderName = datetime + "-" + deviceID.Replace(".","-").Replace(":","--");
            string path = "Report\\" + FolderName + "\\";
            string fullPath = path + "Report.csv";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
                System.IO.Directory.CreateDirectory(path + "ScreenShots");
                //create report file and testcase header

                //  string header = "TestCaseID,Title,StartTime,EndTime,RouterSettings,WiFi Con,Web , Video ,WiFi Dis, Rove In-Out , Verdict , # Attempts , Comments";
                string header = "TestCaseID,Title,StartTime,EndTime, Verdict , # Attempts , Comments";
                using (System.IO.StreamWriter filereport = new System.IO.StreamWriter(fullPath, true))
                {
                    filereport.WriteLine(header);
                }
            }
            string screenShotsPath = System.IO.Directory.GetCurrentDirectory() + "\\" + path + "ScreenShots\\";
            return screenShotsPath; 
        }
        void Logging_Log(string message, string deviceID, int messageType)
        {
            //WIFICallingEnabled.png,WIFICallingEnabled1.png
            //  if( !message.StartsWith("    "))
            //  {
            //       return ;
            //  }

            try
            {
                if (message.Contains("System.Diagnostics.ProcessStartInfo")) return;
                txtLogs.Invoke(new EventHandler(delegate
                {
                    txtLogs.SelectedText = string.Empty;


                    if (message.Contains("PASSED"))
                    {
                        txtLogs.SelectionColor = Color.DarkGreen;
                    }
                    else if (message.Contains("FAILED"))
                    {
                        txtLogs.SelectionColor = Color.DarkRed;
                    }
                    else
                    {
                        txtLogs.SelectionColor = Color.Blue;
                    }

                    if (messageType == 2)
                    {
                        txtLogs.SelectionColor = Color.OrangeRed;
                    }
                    string t = DateTime.Now.ToString("hh:mm:ss.F");

                    string[] spllitted = message.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (deviceID == String.Empty)
                    {
                        txtLogs.AppendText(" " + t + "> ");
                    }
                    else
                        txtLogs.AppendText(" " + t + ">" + deviceID + "> ");
                    foreach (string str in spllitted)
                    {
                      //  txtLogs.AppendText(str + " ");
                       if (GibbonLib.Utility.IsImage(str))
                        {
                            Image image = null;

                            if (deviceID != String.Empty)
                            {
                                GibbonLib.Device device = _DeviceControllers.Where(c => c.Device.DeviceID == deviceID).FirstOrDefault().Device;
                                image = Utility.GetImageFromFile(str, device);
                            }
                            else
                            {
                                image = Utility.GetImageFromFile(str, null);
                            }

                            if (image != null)
                            {
                                try
                                {
                                    // image = image.Resize(60, 30);
                                    Bitmap bitmap = null;

                                    if (str.Contains("currentScreen"))
                                    {
                                        bitmap = new Bitmap(image.Resize(image.Width / 4, image.Height / 4));
                                    }
                                    else
                                    {
                                        bitmap = new Bitmap(image.Resize(image.Width / 2, image.Height / 2));
                                    }

                                    //set class variable for then using it to copy image to pictureoBox:

                                    Clipboard.SetDataObject(bitmap);
                                    DataFormats.Format myFormat = DataFormats.GetFormat(DataFormats.Bitmap);
                                    if (txtLogs.CanPaste(myFormat))
                                    {

                                        txtLogs.Paste(myFormat);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    //   txtLogs.AppendText("Problem showing image" + message);
                                }
                            }
                            else
                            {
                                // txtLogs.AppendText("Problem showing image" + message);
                            }
                        }
                        else
                        {
                            txtLogs.AppendText(str + " ");
                        }
                    }
                  txtLogs.AppendText(Environment.NewLine);
                    /* Image image= androidDeviceControl1.UpdateImage("CurrentPhoneScreen.png");
                image=image.Resize(100, 140);
                Bitmap bitmap = new Bitmap(image);
                //set class variable for then using it to copy image to pictureoBox:
       
                Clipboard.SetDataObject(bitmap);
                DataFormats.Format myFormat = DataFormats.GetFormat(DataFormats.Bitmap);
                if (txtLogs.CanPaste(myFormat)) 
                {
                    txtLogs.Paste(myFormat);
                    txtLogs.AppendText(System.Environment.NewLine);
                }*/

                    txtLogs.Select(txtLogs.TextLength, 0);
                    txtLogs.ScrollToCaret();
                }));
            }
            catch (Exception ex)
            {
                //  Logging.WriteLine("PROBLEM LOGGIN FOR DEVICE ID ");
            }
        }
        private void gridCalls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int colIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0 && colIndex >= 0)
            {
                DataGridViewRow theRow = gridTestCases.Rows[rowIndex];
                if (theRow.Cells[colIndex].Value.ToString() == "FAILED")
                    theRow.DefaultCellStyle.BackColor = Color.Red;
                else if (theRow.Cells[colIndex].Value.ToString() == "PASSED")
                    theRow.DefaultCellStyle.BackColor = Color.Green;
            }
        }
        private void gridCalls_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                string text = gridTestCases.Rows[e.RowIndex].Cells[0].Value.ToString();
                //  txtLogs.Find(text);
            }

        }
        void UpdateTCsGridView()
        {

            gridTestCases.Invoke(new EventHandler(delegate
            {
                gridTestCases.Update();
                gridTestCases.Invalidate();

            }));
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            KillADP();
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            About dlg = new About();
            dlg.Show();
        }
        private void timerUpdateImage_Tick(object sender, EventArgs e)
        {
            //androidDeviceControl1.UpdateImage("Images/CurrentPhoneScreen.png");
            gridTestCases.Update();
            gridTestCases.Invalidate();
        }
        private void saveAsImage(RichTextBox box, string filename, System.Drawing.Imaging.ImageFormat format)
        {
            Rectangle rect = box.DisplayRectangle;
            Bitmap bmp = new Bitmap(rect.Width, rect.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            Point p = default(Point);
            g.CopyFromScreen(box.PointToScreen(p), p, new Size(rect.Width, rect.Height));
            g.Dispose();
            bmp.Save(filename, format);
            bmp.Dispose();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //androidDeviceControl1.ResetDevice();
        }
        private void btnRunPaint_Click(object sender, EventArgs e)
        {
            Process.Start("mspaint.exe");
        }
        private void btnPause_Click(object sender, EventArgs e)
        {
            if (btnPause.Text == "Pause")
            {
                GibbonLib.Device.PauseExecution = true;
                btnPause.Text = "Continu";
            }
            else
            {
                GibbonLib.Device.PauseExecution = false;
                btnPause.Text = "Pause";
            }
        }
        private void btnCheckAllImages_Click(object sender, EventArgs e)
        {
            GibbonLib.DeviceController controller = new GibbonLib.DeviceController(androidDeviceControl1.Device);

        }
        private void btnSaveToWord_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFile1 = new SaveFileDialog();

            // Initialize the SaveFileDialog to specify the RTF extension for the file.
            saveFile1.DefaultExt = "*.rtf";
            saveFile1.Filter = "RTF Files|*.rtf";

            // Determine if the user selected a file name from the saveFileDialog.
            if (saveFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
               saveFile1.FileName.Length > 0)
            {
                // Save the contents of the RichTextBox into the file.
                txtLogs.SaveFile(saveFile1.FileName, RichTextBoxStreamType.PlainText);
            }
        }
        private void btnAddPhone_Click(object sender, EventArgs e)
        {
            PhoneForm phoneform = new PhoneForm();
            phoneform.Text = "Phone " + (phoneForms.Count + 1);
            phoneform.GetAndroidDeviceControl().SetPhoneNumber(_PhoneCount + 1);
            phoneform.Show();
            phoneForms.Add(phoneform);
            _PhoneCount++;

            foreach (PhoneForm form in phoneForms)
            {
                form.Show();
            }
        }
        private void btnRemovePhone_Click(object sender, EventArgs e)
        {

            phoneForms[_PhoneCount - 1].Close();

            phoneForms.RemoveAt(_PhoneCount - 1);

            _PhoneCount--;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string tcpdumpFileName = String.Empty;

            string startupPath = Application.StartupPath;
            using (OpenFileDialog dialog = new OpenFileDialog())
            {

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    tcpdumpFileName = dialog.FileName;
                }
            }
            GibbonLib.Logging.Log += new GibbonLib.Logging.LogHandler(Logging_Log);
            GibbonLib.Logging.GenerateLogFile("Log");
            StartWhiteListProcess(tcpdumpFileName);
            //  StartWhiteListProcess(@"C:\GDrive\tcpump\tcpdump_Pandora_0_12_17_2014\tcpdump_any_20141217135235.pcap");
        }

        //method to send email to outlook
        public void sendEMailThroughOUTLOOK(string attachmentPathAndName, string radio)
        {
            string emailReceipts = System.Configuration.ConfigurationManager.AppSettings["EmailReceipts"];
            
            try
            {
                // Create the Outlook application.
                Outlook.Application oApp = new Outlook.Application();
                // Create a new mail item.
                Outlook.MailItem oMsg = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);
                // Set HTMLBody. 
                //add the body of the email
                oMsg.HTMLBody = "Hello, Not whitelisted IPs for  " + radio;
                //Add an attachment.
                String sDisplayName = "MyAttachment";
                int iPosition = (int)oMsg.Body.Length + 1;
                int iAttachType = (int)Outlook.OlAttachmentType.olByValue;
                //now attached the file
                Outlook.Attachment oAttach = oMsg.Attachments.Add(attachmentPathAndName, iAttachType, iPosition, sDisplayName);
                //Subject line
                oMsg.Subject = "Not whitelisted IPs for  " + radio;
                // Add a recipient.
                Outlook.Recipients oRecips = (Outlook.Recipients)oMsg.Recipients;
                // Change the recipient in the next line if necessary.
                Outlook.Recipient oRecip = (Outlook.Recipient)oRecips.Add(emailReceipts);
                
                oRecip.Resolve();
                // Send.
                oMsg.Send();
                // Clean up.
                oRecip = null;
                oRecips = null;
                oMsg = null;
                oApp = null;
            }//end of try block
            catch (Exception ex)
            {
            }//end of catch
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            sendEMailThroughOUTLOOK("c:\\temp\\export.csv","pandora");
        }
        string lstViewFile;
        private void btnExportToCSV_Click(object sender, EventArgs e)
        {
           AddItemsToListView("one");
              AddItemsToListView("two");
              AddItemsToListView("three");
              ExportToCSVFiles(false,"Pandora");
             
        }//end of Email Method
             
        private void ExportToCSVFiles(bool sendEmail, string radio="")
        {
            string datetime = DateTime.Now.Month + "-" + DateTime.Now.Day;
            string Directory = datetime + "-" + androidDeviceControl1.GetDeviceID();
            string path = System.IO.Directory.GetCurrentDirectory() + "\\" + "Report\\" + datetime + "-" + androidDeviceControl1.GetDeviceID() + "\\notinwhitelist.csv";
            listView1.Invoke(new EventHandler(delegate
            {
                GibbonGUI.ListViewToCSV.ExportListViewToCSV(listView1, path, false);

            }));

            if (sendEmail)
            {
                sendEMailThroughOUTLOOK(path, radio);
            }
        }

    }
}


