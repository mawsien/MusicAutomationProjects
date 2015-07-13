
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


public enum LogMsgType { Info, Event,Debug};   
public partial class LogForm : Form
    {
        // Various colors for logging info
    private Color[] LogMsgTypeColor = { Color.Blue, Color.Red, Color.Black};
    private int InfoDebugMessagesCounter = 0;
    private bool _LogData = false;
    public void DisableLogging()
    {
        _LogData = false;
    }
    public void EnableLogging()
    {
        _LogData = true;
    }
    public LogForm()
        {
            InitializeComponent();
      

        }
   
        /// <summary> Log data to the terminal window. </summary>
        /// <param name="msgtype"> The type of message to be written. </param>
        /// <param name="msg"> The string containing the message to be shown. </param>
        private void Log(LogMsgType msgtype, string msg, bool IsInfo)
        {
           
                LogReachBox(msgtype, msg);
        }
   
      private void LogReachBox(LogMsgType msgtype, string msg)
        {
        rtfTerminal.Invoke(new EventHandler(delegate
                 {
                     rtfTerminal.SelectedText = string.Empty;
                     rtfTerminal.SelectionColor = LogMsgTypeColor[(int)msgtype];
                     rtfTerminal.AppendText(msg);
                     rtfTerminal.Select(rtfTerminal.TextLength, 0);
                     rtfTerminal.ScrollToCaret();
                 }));
       }
        

        private void LogForm_Shown(object sender, EventArgs e)
        {
            GibbonGUI.MainForm oFrmMain = this.Owner as GibbonGUI.MainForm;

            if (oFrmMain != null)
            {
             //   oFrmMain.passLogForm = new VideoLab.LogDelegate(Log);
            
             //   oFrmMain.Show();
            }
        }

    public void CleanLogs()
    {
        if(DialogResult.Yes==MessageBox.Show("Do you want to Clean the logs","",MessageBoxButtons.YesNo))
        {
            rtfTerminal.Text = String.Empty;
        }
     }
}