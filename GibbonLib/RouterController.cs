using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;
using WatiN.Core.DialogHandlers;
using WatiN.Core.Native.Windows;
using WatiN.Core.Native.InternetExplorer;
using System.Text.RegularExpressions;

using System.Xml.Linq;
using System.Windows.Automation;
using System.Linq.Expressions;
using System.Diagnostics;

namespace GibbonLib
{
    #region Watin Helper classes
    public class OKDialogHandler : BaseDialogHandler
    {
        public override bool HandleDialog(Window window)
        {
          
            var button = GetOKButton(window);
            if (button != null)
            {
                button.Click();
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool CanHandleDialog(Window window)
        {
            return GetOKButton(window) != null;
        }

        private WinButton GetOKButton(Window window)
        {
          
            var windowButton = new WindowsEnumerator().GetChildWindows(window.Hwnd, w => w.ClassName == "Button" && new WinButton(w.Hwnd).Title == "OK").FirstOrDefault();
            if (windowButton == null)
                return null;
            else
                return new WinButton(windowButton.Hwnd);
        }
    }
    public class Windows7LogonDialogHandler : BaseDialogHandler
    {
        private readonly string _username;
        private readonly string _password;
        AndCondition _conditions = new AndCondition(new PropertyCondition(AutomationElement.IsEnabledProperty, true),
                                       new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window));

        readonly AndCondition _listCondition = new AndCondition(new PropertyCondition(AutomationElement.IsEnabledProperty, true),
                                        new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ListItem));

        readonly AndCondition _editCondition = new AndCondition(new PropertyCondition(AutomationElement.IsEnabledProperty, true),
                                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));

        readonly AndCondition _buttonConditions = new AndCondition(new PropertyCondition(AutomationElement.IsEnabledProperty, true),
                                     new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button));

        public Windows7LogonDialogHandler(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public override bool HandleDialog(Window window)
        {
            if (CanHandleDialog(window))
            {
                var win = AutomationElement.FromHandle(window.Hwnd);
                var lists = win.FindAll(TreeScope.Children, _listCondition);
                var buttons = win.FindAll(TreeScope.Children, _buttonConditions);
                //var another = (from AutomationElement list in lists
                //               where list.Current.ClassName == "UserTile"
                //               where list.Current.Name == "Use another account"
                //               select list).First();
                //another.SetFocus();

                foreach (var edit in from AutomationElement list in lists
                                     where list.Current.ClassName == "UserTile"
                                     select list.FindAll(TreeScope.Children, _editCondition)
                                         into edits
                                         from AutomationElement edit in edits
                                         select edit)
                {
                    if (edit.Current.Name.Contains("User name"))
                    {
                        edit.SetFocus();
                        var usernamePattern = edit.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
                        if (usernamePattern != null) usernamePattern.SetValue(_username);
                    }
                    if (edit.Current.Name.Contains("Password"))
                    {
                        edit.SetFocus();
                        var passwordPattern = edit.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
                        if (passwordPattern != null) passwordPattern.SetValue(_password);
                    }
                }
                foreach (var submitPattern in from AutomationElement button in buttons
                                              where button.Current.AutomationId == "SubmitButton"
                                              select button.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern)
                {
                    submitPattern.Invoke();
                    break;
                }
                return true;
            }
            return false;
        }

        public override bool CanHandleDialog(Window window)
        {
            return window.ClassName == "#32770";
        }
    }
    #endregion
     public class RouterSettings
    {
        public string SSID { get; set; }
        public string SecurityMode { get; set; }
        public string Encryption { get; set; }
        public string Passphrase { get; set; }
        public string Key { get; set; }
        public string NetworkMode { get; set; }
        public string ChannelBandwidth { get; set; }
        public string Channel { get; set; }
        public string ChannelWidth { get; set; }
        
        public  RouterSettings()
        {
            ChannelBandwidth = "2.4";
        }
    }
   public class RouterController
    {

        public string RouterIP { get; set; }

        public string ConfigNamePrimaryRouter { get; set; }

        private IE browser = null;

        Dictionary<string, string> _elements = new Dictionary<string, string>();
      
        private string _routerIP { get; set; }
        #region Wi-Fi Settings
        public string SSID { get;set; }
        #endregion
        private string GetTemplateFromDictionary(string key)
        {
            string template = String.Empty;
            try
            {
                template = _elements[key];
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception(key + " key is not found\n" + ex.Message);
            }
            return template;
        }
        public RouterController(string routerIP,string configFileNameAndPath)
        {
            _routerIP = routerIP;
            _elements = Utility.GetFileLineToDictionary(configFileNameAndPath);
             RunInternetExplorer();
        }

        private void RunInternetExplorer()
        {

        
            Regex reg = new Regex("^(" + _routerIP + ")");
            bool isBrowserCreated = true;
            try
            {
                browser = IE.AttachTo<IE>(Find.ByUrl(reg), 3);
            }
            catch (WatiN.Core.Exceptions.BrowserNotFoundException ex)
            {
                isBrowserCreated = false;
                Logging.WriteLine("Browser component is already created.");
            }
            if (!isBrowserCreated)
            {
                Logging.WriteLine("Creating new Browser component.");
                browser = new IE();

                AddDialogWatcher();
                browser.GoTo(_routerIP);
           
               // browser.GoTo(_routerIP);
        
            }
            //if (browser.HtmlDialogs.Count != 0)
            //{
            //    HtmlDialog dialog = browser.HtmlDialogs[0];

            //    if (dialog.Exists)
            //    {
            //        dialog.TextFields[0].TypeText("admin");
            //        dialog.TextFields[1].TypeText("admin");
            //        dialog.Buttons[0].Click();

            //    }
            //}
        }

        private void AddDialogWatcher()
        {
            browser.DialogWatcher.Clear();
            browser.DialogWatcher.Add(new Windows7LogonDialogHandler(@"admin", @"admin"));
            // browser.AddDialogHandler(new OKDialogHandler());
            browser.DialogWatcher.Add(new OKDialogHandler());
         
        }
       
        #region Methods
        System.Timers.Timer timer = null;
        public  bool ChangeSecurityMode(RouterSettings settings)
        {
                AddDialogWatcher();
                string changeSecurityUrl = _routerIP + "/" + GetTemplateFromDictionary("securityPage");
                Logging.WriteLine("Go to " + changeSecurityUrl);
                browser.GoTo(changeSecurityUrl);
                browser.WaitForComplete();
           
                 Logging.WriteLine(string.Format("Changing security mode to {0}", GetTemplateFromDictionary(settings.SecurityMode)));
                 SelectList securityModeList = null;
                 timer = new System.Timers.Timer();
                 timer.Interval = 10000;    
                  timer.Start();
                 timer.Enabled = true;
                
                 timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
                //try
                //{
                //    ConfirmDialogHandler handler = new ConfirmDialogHandler();
                //    using (new UseDialogOnce(browser.DialogWatcher, handler))
                //    {
                        securityModeList= browser.SelectList(Find.ByName(GetTemplateFromDictionary("ddlSecurityMode")));
                        securityModeList.SelectByValue(GetTemplateFromDictionary(settings.SecurityMode));
                //        handler.WaitUntilExists();
                //        handler.OKButton.Click();
                //    }
                //    browser.WaitForComplete();
                 
                //}catch(Exception ex){};

                System.Threading.Thread.Sleep(2000);

                timer.Stop();
                timer.Enabled = false;
              //  System.Windows.Forms.SendKeys.SendWait("{TAB}"); //in case if the focus was on cancel button
              //  System.Windows.Forms.SendKeys.SendWait("{ENTER}"); 
           
                System.Threading.Thread.Sleep(5000);
                //  System.Windows.Forms.SendKeys.Send("~");  
                browser.WaitForComplete();

                if (settings.SecurityMode == "wep64" || settings.SecurityMode == "wep128")
                {
                    AddDialogWatcher();
                    string encriptionBit = String.Empty;
                    System.Threading.Thread.Sleep(1000);
                    Logging.WriteLine(String.Format("Changing Encryption  to {0} bit", settings.Encryption));
                    browser.SelectList(Find.ByName(GetTemplateFromDictionary("ddlEncryption"))).SelectByValue(GetTemplateFromDictionary(settings.Encryption));


                    Logging.WriteLine(String.Format("Change key to {0}", settings.Key));
                    //  browser.TextField(Find.ByName("wl_passphrase")).TypeText(String.Empty);
                    TextField keytext = browser.TextField(Find.ByName(GetTemplateFromDictionary("wl_key1")));
                    keytext.TypeText(settings.Key);
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    TextField passwordField = browser.TextField(Find.ByName(GetTemplateFromDictionary("passphrass")));
                    if (passwordField.Exists)
                    {
                        System.Threading.Thread.Sleep(1000);
                        passwordField.TypeText(settings.Passphrase);
                        System.Threading.Thread.Sleep(2000);
                    }
                }

                if (AcceptAndCheckIfSucess())
                {
                    return true;
                }
                throw new Exception("Problem changing security Mode for  Router to " + settings.SecurityMode);
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Process proce = Process.GetProcessById(browser.ProcessID);
            GibbonLib.Utility.SetForegroundWindow(proce.MainWindowHandle);
           System.Windows.Forms.SendKeys.SendWait("{ENTER}"); //in case if the focus was on ok button
        }
        public bool ChangeNetworkMode(RouterSettings settings)
        {
            AddDialogWatcher();
            string changeSecurityUrl = _routerIP + "/" + GetTemplateFromDictionary("channelPage");
            Logging.WriteLine("Go to " + changeSecurityUrl);

            browser.GoTo(changeSecurityUrl);
            Logging.WriteLine(String.Format("Changing network mode to {0}", settings.NetworkMode));
            System.Threading.Thread.Sleep(2000);

            if (settings.ChannelBandwidth == "2.4")
            {
            
          
               browser.SelectList(Find.ByName(GetTemplateFromDictionary("2_4_ddlNetworkMode"))).SelectByValue(GetTemplateFromDictionary(settings.NetworkMode));
            
            }
            else if (settings.ChannelBandwidth == "5.0")
            {
                browser.SelectList(Find.ByName(GetTemplateFromDictionary("5_0_ddlNetworkMode"))).SelectByValue(GetTemplateFromDictionary(settings.NetworkMode));
                browser.WaitForComplete();

                if (settings.ChannelWidth!=null && settings.ChannelWidth!=String.Empty)
                {
                  var  ddlChannelWidth= browser.SelectList(Find.ByName(GetTemplateFromDictionary("5_0_ChannelWidth")));

                  ddlChannelWidth.SelectByValue(settings.ChannelWidth);
              
                }
            }
            browser.WaitForComplete();
            System.Threading.Thread.Sleep(2000);
            return AcceptAndCheckIfSucess();
        }
        public bool ChangeDHCPLeaseTime(int dhcpLeaseTime)
        {
            AddDialogWatcher();
            string dhcpTimePage = _routerIP + "/index.asp";
            Logging.WriteLine("Go to " + dhcpTimePage);

            browser.GoTo(dhcpTimePage);
            browser.WaitForComplete();
            System.Threading.Thread.Sleep(2000);
            Logging.WriteLine(String.Format("Changing DHCP lease time to {0} minutes", 10));

            browser.TextField(Find.ByName("dhcp_lease")).TypeText(dhcpLeaseTime.ToString());

            System.Threading.Thread.Sleep(2000);

           
            return AcceptAndCheckIfSucess();
        }
        public bool ChangeChannel(RouterSettings settings)
        {

         
            string channelUrl = _routerIP + "/" + GetTemplateFromDictionary("channelPage");
            Logging.WriteLine("Go to " + channelUrl);

            browser.GoTo(channelUrl);
            Logging.WriteLine(String.Format("Changing channel to {0}", settings.Channel));
           
            System.Threading.Thread.Sleep(2000);
            browser.SelectList(Find.ByName(GetTemplateFromDictionary("ddlChannel"))).SelectByValue(settings.Channel);
            browser.WaitForComplete();
            System.Threading.Thread.Sleep(2000);
            return AcceptAndCheckIfSucess();
        }
        #endregion

        #region Helper Methods
        private bool AcceptAndCheckIfSucess()
        {
            Logging.WriteLine("Click Accept");
            Link accept = browser.Link(Find.ByText("Save Settings"));
            accept.Click();
            System.Threading.Thread.Sleep(5000);
            browser.WaitForComplete();
          
            return ValidateAllSuccess();
        }
       
        private bool ValidateAllSuccess()
        {

            if (browser.ContainsText(GetTemplateFromDictionary("SuccessText")))
            {
                Logging.WriteLine("Router setting are succesfully saved..");
                Button continueButton = browser.Buttons[0];
                if (continueButton.Exists)
                {
                    continueButton.Click();
                    browser.WaitForComplete();
                }
                else
                {
                    browser.Back();
                }
                return true;
            } else
            {
                Logging.WriteLine("Failed to save router settings!!");
                return false;
            }
        }
        #endregion

    }
}
