using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Diagnostics;
namespace GibbonGUI
{
    public partial class AndroidDeviceControl : UserControl
    {
        Process pProcessLogCat = null;
        System.Threading.Thread worker;
        //private GibbonLib.Device _Device=null; 
        public void SetPhoneStatus(string status)
        {
            lblPhoneStatus.Text = status;
        }
     
        public GibbonLib.Device Device
        {
            get;
            set;
        }

        public void UpdateDeviceImagePath(GibbonLib.ImagesDirectoryPath paths)
        {
            Device._TemplateDetector = new GibbonLib.TemplateDetector(paths);
        }
        public string GetDeviceID()
        {
            return Device.DeviceID;
        }
        System.Timers.Timer aTimer = new System.Timers.Timer();
        public AndroidDeviceControl()
        {
            InitializeComponent();
            aTimer.Interval = 6000 * 60; //8 minutes
            aTimer.Enabled = false;
            aTimer.Elapsed += new System.Timers.ElapsedEventHandler(aTimer_Elapsed);
            string path = String.Empty;
            //if (!DesignMode)
            //{

            //    path = System.IO.Directory.GetCurrentDirectory() + "\\Device Config Files";
            //}
            //else
            //{ 
            //    path  =@"C:\T-Mobile\Development\Gibbon\GibbonGUI\Device Config Files\\Device Config Files";
            //}


            //    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(path);
            //    System.IO.DirectoryInfo[] directroies = dirInfo.GetDirectories();

            //    cmbBxVendors.DataSource = directroies.OrderBy(c => c.Name).Select(c => c.Name).ToList<string>();
            //    cmbBxVendors.Update();


        }

        public Image UpdateImage(string fullPathAndName)
        {
            int index = 0;
        redo:
            try
            {
                // System.Threading.Thread.Sleep(1000);
                // Construct an image object from a file in the local directory.
                // ... This file must exist in the solution.
                if (File.Exists(fullPathAndName) && fullPathAndName.EndsWith("png"))
                {
                    Image image = Image.FromFile(fullPathAndName);
                    image = image.Resize(pictureBox1.Width, pictureBox1.Height);//Utility.resizeImage(image, new Size(pictureBox1.Width, pictureBox1.Height));
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    pictureBox1.Image = image;
                    // pictureBox1.Width += 50;
                    return image;
                }
            }
            catch (OutOfMemoryException ex)
            {
                //GibbonLib.Logging.WriteLine("UpdateImage Error!"+ ex.Message);
                //index++;
                //if(index!=5) 
                //goto redo;
            }
            return null;

        }

        public void StartTracing()
        {
            string deviceId = "";
            cmbBxDevices.Invoke(new EventHandler(delegate
            {
                deviceId = cmbBxDevices.SelectedItem.ToString();

            }));
            Device.DeviceID = deviceId;

        }
        public void StopTracing()
        {

        }
        public void RunTCPDump(string fileName)
        {
            Device.RunTcpdump(fileName);

        }
        public void ResetDevice()
        {
           
            Device.Reset();
        }
        public void StopTCPDump(string fileName)
        {
            Device.StopAndPullTcpdumpFile(fileName);
        }

        List<string> attachedDevices;
        private void btnRefreshPhoneList_Click(object sender, EventArgs e)
        {
            cmbBxDevices.Items.Clear();
            GibbonLib.WorkStation workStation = new GibbonLib.WorkStation();

            attachedDevices = workStation.GetAttachedDevices();
            if (attachedDevices.Count >= 0)
            {
                foreach (string line in attachedDevices)
                {
                    cmbBxDevices.Items.Add(line);
                }
            }

            //cmbBxDeviceConfigs.Items.Clear();

            //string[] filePaths = Directory.GetFiles(@"Device Config Files", "*.xml");

            //foreach (string path in filePaths)
            //{

            //    cmbBxDeviceConfigs.Items.Add(path.Replace("Device Config Files\\",""));

            //}

        }

        private void cmbBxDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            Device = new GibbonLib.Device(cmb.SelectedItem.ToString());

        }

        private void btnTakeSnapShot_Click(object sender, EventArgs e)
        {
            Device.DeviceID = cmbBxDevices.SelectedItem.ToString();
            Device.TakeScreenShot();
        }

        private void btnOpenWorkingDirectory_Click(object sender, EventArgs e)
        {
            string startupPath = Application.StartupPath;
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Open the folder where Image directory for the device";
                dialog.ShowNewFolderButton = false;
                dialog.SelectedPath = System.IO.Directory.GetCurrentDirectory() + "\\ImageFiles";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string folder = dialog.SelectedPath;
                    Device.ImageDirectoryPath.TemplatesPath = folder + "\\";
                    txtBxWorkingDirectoryPath.Text = folder + "\\";// "..." + folder.Substring(folder.Length / 3);

                    MessageBox.Show("The folder selected is " + Device.ImageDirectoryPath.TemplatesPath);

                    foreach (string fileName in Directory.GetFiles(folder, "*.xml", SearchOption.TopDirectoryOnly))
                    {

                    }
                }
            }
        }


        public void StartCapturingTimer()
        {
            aTimer.Enabled = true;
            lines.Clear();
            aTimer.Start();
            //  device.Reset();
            StopLogcatCaptureTraces();
            StartLogcatCaptureTraces();
        }
        public void StopCapturingTimer()
        {

            aTimer.Stop();
            aTimer.Enabled = false;
        }
        void aTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            //GibbonLib.Logging.WriteLine("Reset logcat timer");
            //device.Reset();
            //StopLogcatCaptureTraces();
            //StartLogcatCaptureTraces();
        }

        public void StartLogcatCaptureTraces(string fileName = "")
        {
            lines.Clear();
            // Process.Start("cmd.exe", "/c foo.exe -arg >" + dumpDir + "\\foo_arg.txt");
            string logcatCommandOne = String.Empty;

            //if(fileName!=String.Empty)

            //{
            //    logcatCommandOne = "adb -s " + device.DeviceID + " logcat  -d > "  +WorkingDirectoryPath + fileName + ".txt";
            //}else  

            logcatCommandOne = " -s " + Device.DeviceID + " logcat ";
            worker = new System.Threading.Thread(() => workerDoWork(logcatCommandOne));
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
            }
        }
        private void workerDoWork(string argument)
        {
            string[] logcatCommandOne = new string[] { argument };
            RunExternalProcess1(logcatCommandOne);
        }
        private void RunExternalProcess1(params string[] commands)
        {
            // Create the output message writter

            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();

            startInfo.FileName = "adb.exe";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = false;
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            startInfo.Arguments = commands[0];
            bool processAlreadyExited = false;
            //try
            //{
            //    if (pProcessLogCat != null && pProcessLogCat.HasExited)
            //    {
            //        //....
            //    }
            //}

            //catch (System.InvalidOperationException e)
            //{
            //    processAlreadyExited = true;
            //}
            //if (pProcessLogCat == null || processAlreadyExited)
            //{
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
            // }//
            //else
            //{


            //}
        }
        public List<logcatLine> lines = new List<logcatLine>();
        void pProcessLogCat_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {

            if (!string.IsNullOrEmpty(e.Data))
            {
                lines.Add(new logcatLine { DateTime = DateTime.Now, DataLine = (DateTime.Now.ToString() + ">>" + e.Data) });
            }
        }

        public string GetCurrentScreenShotPathAndName()
        {
            if (Device._TemplateDetector != null)
            {
                GibbonLib.ImagesDirectoryPath paths = Device._TemplateDetector.GetImagesDirectoryPath();
                if (paths != null && paths.CurrentScreenShotName != null && paths.CurrentScreenShotPath != null)
                {
                    return Device._TemplateDetector.GetImagesDirectoryPath().CurrentScreenShotFullPathAndName;
                }

            }

            return null;

        }


        public void SetPhoneNumber(int lineSequence)
        {
            string phoneSettingsFilePathAndName = "Device Config Files\\Devices.txt";

           List<string[]> lines = GibbonLib.Utility.GetFileLines(phoneSettingsFilePathAndName);

           foreach (string[] s in lines)
            {
             if( s[0].StartsWith("-")) continue;
               if (s[0].StartsWith((lineSequence).ToString()))
                {

                    txtBxPhoneNumber.Text = s[1];
                    txtBxDeviceConfigFile.Text = s[2];
                    txtBxWorkingDirectoryPath.Text = s[3];
                }
            }

        }

        public string GetPhoneNUmber
        {
            get { return txtBxPhoneNumber.Text; }
        }

        private void btnOpenDeviceConfigDirectory_Click(object sender, EventArgs e)
        {
            string startupPath = Application.StartupPath;
            using (OpenFileDialog dialog = new OpenFileDialog())
            {

                dialog.InitialDirectory = startupPath + "\\Device Config Files";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string xMlConfigFileName = dialog.FileName;
                    txtBxDeviceConfigFile.Text = xMlConfigFileName;
                    Device.XmlConfigFileNameAndPath = xMlConfigFileName;

                }
            }
        }

    
        public void TryFunctions(object function)
        {
            GenerateScreenShotsFileSetupDeviceAndStartMonitoring();

            GibbonLib.DeviceController controller = new GibbonLib.DeviceController(Device);
            switch (function.ToString())
            {
                case "Browse":
                    Device.Browse("");
                    break;
                case "TakeScreenShot":
                    Device.TakeScreenShot();
                    break;
                case "ScrollToTop":
                    Device.ScrollToTop();
                    break;
                case "Connect WiFi":
                    controller.ConnectToWiFi(new GibbonLib.RouterSettings());
                    break;
                case "Disconnect WiFi":
                    controller.DisconnectFromWiFiAndUnRemember();
                    break;

                case "Enable WiFi Calling":
                    controller.Device.GoAllBack();
                    controller.EnableWiFiCalling();
                    break;

                case "Disable WiFi Calling":
                    controller.Device.GoAllBack();
                    controller.DisablWiFiCalling();
                    break;
                case "Get Logcat and save to harddisk":

                    int rtpCount = 0;
                    controller.SaveLogcatToReport("1.1.1", out rtpCount);
                    break;

                case "StopLogcatCapturingToHarddisk":
                    int rtpCount1 = 0;
                    controller.StopLogcatCapturingToHarddisk("1.1.1", out rtpCount1);
                    break;

                case "iPerf":
                    controller.RunIPerf();
                    break;
                case "SetWiFiPreferedMode":
                    controller.SetWiFiCallingMode("wifipreferred");
                    break;
                case "SetWiFiOnlyMode":
                    controller.SetWiFiCallingMode("wifionly");
                    break;
                case "SetCellularPreferredMode":
                    controller.SetWiFiCallingMode("cellularpreferred");
                    break;

                case "EnableSIMLock":
                    controller.SetSIMLock(true);
                    break;

                case "DisableSIMLock":
                    controller.SetSIMLock(false);
                    break;
                case "RouterEnableWEP":
                    GibbonLib.RouterSettings routerSettings = new GibbonLib.RouterSettings() { SecurityMode = "wep64", Encryption = "64", Key = "012345abcd", NetworkMode = "mixed" };
                    GibbonLib.RouterController routerController = new GibbonLib.RouterController("192.168.18.1", System.IO.Directory.GetCurrentDirectory() + "\\Router Config Files\\LinksysE2500.txt");
                    routerController.ChangeSecurityMode(routerSettings);
                    break;

                case "ClickOnMediaHub":
                    controller.ClickOnMediaHub();
                    break;

                case "ClickVideoOnMediaHub":
                    controller.TakeScreenShot();
                    controller.SearchAndClickVideoOnMediaHub("");
                    break;

                            case "QSDM":
                   string fileName =  Guid.NewGuid().ToString() + ".qxdm";
                 string datetime = DateTime.Now.Month + "-" + DateTime.Now.Day;
                 string Directory = datetime + "-" + controller.Device.DeviceID;
                string path = "Report\\" + datetime + "-" + controller.Device.DeviceID + "\\";

                 string currentDirectory = System.IO.Directory.GetCurrentDirectory();
                 controller.SaveQSDM(currentDirectory + "\\" + path + fileName);
             
                    break;

            }
            picActivityIndicator.Invoke(new EventHandler(delegate
            {
                picActivityIndicator.Visible = false;

            }));
           
        }
        System.Threading.Thread threadTryFunctions;
        private void btnTestAndSetup_Click(object sender, EventArgs e)
        {
            picActivityIndicator.Visible = true;
            threadTryFunctions=new System.Threading.Thread(TryFunctions);
            threadTryFunctions.ApartmentState=System.Threading.ApartmentState.STA;
         
          
            
           
            if (cbmBxActions.SelectedItem != null)
            {
            string function=   cbmBxActions.SelectedItem.ToString();
                threadTryFunctions.Start(function);
                
            }
            //GenerateScreenShotsFileAndStartMonitoring();

            //GibbonLib.DeviceController controller = new GibbonLib.DeviceController(Device);
            //Device.PythonScriptFileName = "script.py";

            //Device.TakeScreenShot();
        }

        public bool GenerateScreenShotsFileSetupDeviceAndStartMonitoring()
        {
            string datetime = DateTime.Now.Month + "-" + DateTime.Now.Day;
            string Directory = datetime + "-" + Device.DeviceID;
            Device.ImageDirectoryPath.CurrentScreenShotPath = "Report\\" + datetime + "-" + Device.DeviceID + "\\ScreenShots\\";


            if (!System.IO.Directory.Exists(Device.ImageDirectoryPath.CurrentScreenShotPath))
            {
                System.IO.Directory.CreateDirectory(Device.ImageDirectoryPath.CurrentScreenShotPath);

            }
            fileSystemWatcher1.Path = Device.ImageDirectoryPath.CurrentScreenShotPath;
            Device.PhoneNumber = txtBxPhoneNumber.Text;
            Device.XmlConfigFileNameAndPath = txtBxDeviceConfigFile.Text;
            Device.ImageDirectoryPath.TemplatesPath = txtBxWorkingDirectoryPath.Text;

            return true;
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            Image image = UpdateImage(e.FullPath);
            GibbonLib.Logging.WriteLine(e.Name, deviceID:Device.DeviceID);
            //try
            //{
            //    if (image != null)
            //    {
            //        image = image.Resize(100, 140);
            //        Bitmap bitmap = new Bitmap(image);
            //        //set class variable for then using it to copy image to pictureoBox:

            //        Clipboard.SetDataObject(bitmap);
            //        DataFormats.Format myFormat = DataFormats.GetFormat(DataFormats.Bitmap);
            //        if (txtLogs.CanPaste(myFormat))
            //        {
            //            txtLogs.Paste(myFormat);
            //            txtLogs.AppendText(System.Environment.NewLine);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    GibbonLib.Logging.WriteLine(ex.Message);
            //}
            // System.Threading.Thread.Sleep(1000);
        }
        public void StartTimeStatus()
        { 
        
        }
       
    }

    public class logcatLine
    {
        public DateTime DateTime
        {
            get;
            set;
        }
        public string DataLine
        {
            get;
            set;
        }
        public override string ToString()
        {
            return String.Format("{0} >> {1}", DateTime.ToString(), DataLine);
        }
    }


}
