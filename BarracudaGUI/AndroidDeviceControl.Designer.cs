namespace GibbonGUI
{
    partial class AndroidDeviceControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbBxDevices = new System.Windows.Forms.ComboBox();
            this.btnRefreshPhoneList = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnTakeSnapShot = new System.Windows.Forms.Button();
            this.btnOpenWorkingDirectory = new System.Windows.Forms.Button();
            this.btnOpenDeviceConfigDirectory = new System.Windows.Forms.Button();
            this.txtBxDeviceConfigFile = new System.Windows.Forms.TextBox();
            this.txtBxWorkingDirectoryPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTestAndSetup = new System.Windows.Forms.Button();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.cbmBxActions = new System.Windows.Forms.ComboBox();
            this.txtBxPhoneNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPhoneStatus = new System.Windows.Forms.Label();
            this.picActivityIndicator = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picActivityIndicator)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbBxDevices
            // 
            this.cmbBxDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxDevices.FormattingEnabled = true;
            this.cmbBxDevices.Location = new System.Drawing.Point(10, 9);
            this.cmbBxDevices.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBxDevices.Name = "cmbBxDevices";
            this.cmbBxDevices.Size = new System.Drawing.Size(189, 24);
            this.cmbBxDevices.TabIndex = 15;
            this.cmbBxDevices.SelectedIndexChanged += new System.EventHandler(this.cmbBxDevices_SelectedIndexChanged);
            // 
            // btnRefreshPhoneList
            // 
            this.btnRefreshPhoneList.Location = new System.Drawing.Point(208, 9);
            this.btnRefreshPhoneList.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefreshPhoneList.Name = "btnRefreshPhoneList";
            this.btnRefreshPhoneList.Size = new System.Drawing.Size(138, 26);
            this.btnRefreshPhoneList.TabIndex = 23;
            this.btnRefreshPhoneList.Text = "Get Devices";
            this.btnRefreshPhoneList.UseVisualStyleBackColor = true;
            this.btnRefreshPhoneList.Click += new System.EventHandler(this.btnRefreshPhoneList_Click);

            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(4, 48);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pictureBox1.Size = new System.Drawing.Size(344, 425);
            this.pictureBox1.TabIndex = 24;
            this.pictureBox1.TabStop = false;
            // 
            // btnTakeSnapShot
            // 
            this.btnTakeSnapShot.Location = new System.Drawing.Point(295, 7);
            this.btnTakeSnapShot.Margin = new System.Windows.Forms.Padding(4);
            this.btnTakeSnapShot.Name = "btnTakeSnapShot";
            this.btnTakeSnapShot.Size = new System.Drawing.Size(56, 28);
            this.btnTakeSnapShot.TabIndex = 97;
            this.btnTakeSnapShot.Text = "S";
            this.btnTakeSnapShot.UseVisualStyleBackColor = true;
            this.btnTakeSnapShot.Visible = false;
            this.btnTakeSnapShot.Click += new System.EventHandler(this.btnTakeSnapShot_Click);
            // 
            // btnOpenWorkingDirectory
            // 
            this.btnOpenWorkingDirectory.Location = new System.Drawing.Point(289, 542);
            this.btnOpenWorkingDirectory.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenWorkingDirectory.Name = "btnOpenWorkingDirectory";
            this.btnOpenWorkingDirectory.Size = new System.Drawing.Size(57, 28);
            this.btnOpenWorkingDirectory.TabIndex = 99;
            this.btnOpenWorkingDirectory.Text = "Open";
            this.btnOpenWorkingDirectory.UseVisualStyleBackColor = true;
            this.btnOpenWorkingDirectory.Click += new System.EventHandler(this.btnOpenWorkingDirectory_Click);
            // 
            // btnOpenDeviceConfigDirectory
            // 
            this.btnOpenDeviceConfigDirectory.Location = new System.Drawing.Point(289, 495);
            this.btnOpenDeviceConfigDirectory.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenDeviceConfigDirectory.Name = "btnOpenDeviceConfigDirectory";
            this.btnOpenDeviceConfigDirectory.Size = new System.Drawing.Size(57, 28);
            this.btnOpenDeviceConfigDirectory.TabIndex = 101;
            this.btnOpenDeviceConfigDirectory.Text = "Open";
            this.btnOpenDeviceConfigDirectory.UseVisualStyleBackColor = true;
            this.btnOpenDeviceConfigDirectory.Click += new System.EventHandler(this.btnOpenDeviceConfigDirectory_Click);
            // 
            // txtBxDeviceConfigFile
            // 
            this.txtBxDeviceConfigFile.BackColor = System.Drawing.Color.LightGray;
            this.txtBxDeviceConfigFile.Location = new System.Drawing.Point(5, 495);
            this.txtBxDeviceConfigFile.Margin = new System.Windows.Forms.Padding(4);
            this.txtBxDeviceConfigFile.Name = "txtBxDeviceConfigFile";
            this.txtBxDeviceConfigFile.Size = new System.Drawing.Size(276, 22);
            this.txtBxDeviceConfigFile.TabIndex = 102;
            // 
            // txtBxWorkingDirectoryPath
            // 
            this.txtBxWorkingDirectoryPath.BackColor = System.Drawing.Color.LightGray;
            this.txtBxWorkingDirectoryPath.Location = new System.Drawing.Point(4, 546);
            this.txtBxWorkingDirectoryPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtBxWorkingDirectoryPath.Name = "txtBxWorkingDirectoryPath";
            this.txtBxWorkingDirectoryPath.Size = new System.Drawing.Size(280, 22);
            this.txtBxWorkingDirectoryPath.TabIndex = 103;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 477);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 17);
            this.label1.TabIndex = 104;
            this.label1.Text = "Device Config File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 525);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 17);
            this.label2.TabIndex = 105;
            this.label2.Text = "Device  Image Directoy";
            // 
            // btnTestAndSetup
            // 
            this.btnTestAndSetup.Location = new System.Drawing.Point(232, 610);
            this.btnTestAndSetup.Margin = new System.Windows.Forms.Padding(4);
            this.btnTestAndSetup.Name = "btnTestAndSetup";
            this.btnTestAndSetup.Size = new System.Drawing.Size(100, 28);
            this.btnTestAndSetup.TabIndex = 106;
            this.btnTestAndSetup.Text = "Test and Setup";
            this.btnTestAndSetup.UseVisualStyleBackColor = true;
            this.btnTestAndSetup.Click += new System.EventHandler(this.btnTestAndSetup_Click);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
            // 
            // cbmBxActions
            // 
            this.cbmBxActions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbmBxActions.FormattingEnabled = true;
            this.cbmBxActions.Items.AddRange(new object[] {
            "Browse",
            "TakeScreenShot",
            "ScrollPageDown",
            "ScrollToTop",
            "Connect WiFi",
            "Disconnect WiFi",
            "Enable WiFi Calling",
            "Disable WiFi Calling",
            "SetWiFiPreferedMode",
            "SetCellularPreferredMode",
            "SetWiFiOnlyMode",
            "Get Logcat and save to harddisk",
            "StopLogcatCapturingToHarddisk",
            "iPerf",
            "EnableSIMLock",
            "QSDM",
            "ClickVideoOnMediaHub",
            "DisableSIMLock",
            "ClickOnMediaHub"});
            this.cbmBxActions.Location = new System.Drawing.Point(10, 610);
            this.cbmBxActions.Margin = new System.Windows.Forms.Padding(4);
            this.cbmBxActions.Name = "cbmBxActions";
            this.cbmBxActions.Size = new System.Drawing.Size(179, 24);
            this.cbmBxActions.TabIndex = 107;
            // 
            // txtBxPhoneNumber
            // 
            this.txtBxPhoneNumber.Location = new System.Drawing.Point(112, 474);
            this.txtBxPhoneNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtBxPhoneNumber.Name = "txtBxPhoneNumber";
            this.txtBxPhoneNumber.Size = new System.Drawing.Size(235, 22);
            this.txtBxPhoneNumber.TabIndex = 108;
            this.txtBxPhoneNumber.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 478);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 17);
            this.label3.TabIndex = 109;
            this.label3.Text = "Phone number";
            this.label3.Visible = false;
            // 
            // lblPhoneStatus
            // 
            this.lblPhoneStatus.AutoSize = true;
            this.lblPhoneStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhoneStatus.ForeColor = System.Drawing.Color.Navy;
            this.lblPhoneStatus.Location = new System.Drawing.Point(153, 148);
            this.lblPhoneStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPhoneStatus.Name = "lblPhoneStatus";
            this.lblPhoneStatus.Size = new System.Drawing.Size(58, 25);
            this.lblPhoneStatus.TabIndex = 110;
            this.lblPhoneStatus.Text = "NOP";
            this.lblPhoneStatus.Visible = false;
            // 
            // picActivityIndicator
            // 
            this.picActivityIndicator.Image = global::GibbonGUI.Properties.Resources.ajax_loader;
            this.picActivityIndicator.Location = new System.Drawing.Point(311, 540);
            this.picActivityIndicator.Margin = new System.Windows.Forms.Padding(4);
            this.picActivityIndicator.Name = "picActivityIndicator";
            this.picActivityIndicator.Size = new System.Drawing.Size(40, 27);
            this.picActivityIndicator.TabIndex = 111;
            this.picActivityIndicator.TabStop = false;
            this.picActivityIndicator.Visible = false;
            // 
            // AndroidDeviceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.picActivityIndicator);
            this.Controls.Add(this.lblPhoneStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBxPhoneNumber);
            this.Controls.Add(this.cbmBxActions);
            this.Controls.Add(this.btnTestAndSetup);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBxWorkingDirectoryPath);
            this.Controls.Add(this.txtBxDeviceConfigFile);
            this.Controls.Add(this.btnOpenDeviceConfigDirectory);
            this.Controls.Add(this.btnOpenWorkingDirectory);
            this.Controls.Add(this.btnTakeSnapShot);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnRefreshPhoneList);
            this.Controls.Add(this.cmbBxDevices);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AndroidDeviceControl";
            this.Size = new System.Drawing.Size(367, 626);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picActivityIndicator)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbBxDevices;
        private System.Windows.Forms.Button btnRefreshPhoneList;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnTakeSnapShot;
        private System.Windows.Forms.Button btnOpenWorkingDirectory;
        private System.Windows.Forms.Button btnOpenDeviceConfigDirectory;
        private System.Windows.Forms.TextBox txtBxDeviceConfigFile;
        private System.Windows.Forms.TextBox txtBxWorkingDirectoryPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTestAndSetup;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.ComboBox cbmBxActions;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBxPhoneNumber;
        private System.Windows.Forms.Label lblPhoneStatus;
        private System.Windows.Forms.PictureBox picActivityIndicator;
    }
}
