namespace GibbonGUI
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnRun = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.gridTestCases = new System.Windows.Forms.DataGridView();
            this.Execute = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartTestcaseTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndTestcaseTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new DataGridViewVerdictColumn();
            this.DataUsage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataUsagePayload = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Progress = new DataGridViewProgressColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workerCampaingExecution = new System.ComponentModel.BackgroundWorker();
            this.label4 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.lblPassed = new System.Windows.Forms.Label();
            this.lblFailed = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.timerUpdateImage = new System.Windows.Forms.Timer(this.components);
            this.txtBxRouterIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.cmbBxRouters = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.chkBxEnableLogcat = new System.Windows.Forms.CheckBox();
            this.txtBxSSID = new System.Windows.Forms.TextBox();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnSaveToWord = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lblInconcCount = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnAddPhone = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkBxUseRouter = new System.Windows.Forms.CheckBox();
            this.btnRemovePhone = new System.Windows.Forms.Button();
            this.chkBxEnableQXDM = new System.Windows.Forms.CheckBox();
            this.androidDeviceControl1 = new GibbonGUI.AndroidDeviceControl();
            this.dataGridViewVerdictColumn1 = new DataGridViewVerdictColumn();
            this.dataGridViewProgressColumn1 = new DataGridViewProgressColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbBxTestcases = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmbbxDuration = new System.Windows.Forms.ComboBox();
            this.ddlMusicServices = new System.Windows.Forms.ComboBox();
            this.chkBxSendEmailWL = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.chkBxEnableWhiteLIstAnalysing = new System.Windows.Forms.CheckBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnExportToCSV = new System.Windows.Forms.Button();
            this.btnSendEmail = new System.Windows.Forms.Button();
            this.lblWhiteListedUsage = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblAnaylzing = new System.Windows.Forms.Label();
            this.lblTotalProcessed = new System.Windows.Forms.Label();
            this.lblPercentageNotInWhiteList = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblPercentageInWhiteList = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.listView1 = new FlickerFreeListView();
            this.IPSource = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtLogs = new System.Windows.Forms.RichTextBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridTestCases)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRun.Location = new System.Drawing.Point(27, 92);
            this.btnRun.Margin = new System.Windows.Forms.Padding(4);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(152, 41);
            this.btnRun.TabIndex = 5;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(35, 956);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1902, 12);
            this.progressBar1.TabIndex = 6;
            // 
            // gridTestCases
            // 
            this.gridTestCases.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTestCases.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridTestCases.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTestCases.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Execute,
            this.Title,
            this.StartTestcaseTime,
            this.EndTestcaseTime,
            this.Status,
            this.DataUsage,
            this.DataUsagePayload,
            this.Progress,
            this.Comment});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridTestCases.DefaultCellStyle = dataGridViewCellStyle6;
            this.gridTestCases.Location = new System.Drawing.Point(899, 538);
            this.gridTestCases.Margin = new System.Windows.Forms.Padding(4);
            this.gridTestCases.Name = "gridTestCases";
            this.gridTestCases.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTestCases.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.gridTestCases.Size = new System.Drawing.Size(451, 190);
            this.gridTestCases.TabIndex = 12;
            this.gridTestCases.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCalls_CellContentClick);
            this.gridTestCases.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCalls_CellValueChanged);
            // 
            // Execute
            // 
            this.Execute.HeaderText = "Execute";
            this.Execute.Name = "Execute";
            this.Execute.ReadOnly = true;
            this.Execute.Visible = false;
            // 
            // Title
            // 
            this.Title.DataPropertyName = "Title";
            this.Title.HeaderText = "Title";
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            // 
            // StartTestcaseTime
            // 
            this.StartTestcaseTime.DataPropertyName = "StartTestcaseTime";
            dataGridViewCellStyle2.Format = "HH:mm:ss";
            this.StartTestcaseTime.DefaultCellStyle = dataGridViewCellStyle2;
            this.StartTestcaseTime.HeaderText = "Start Time";
            this.StartTestcaseTime.Name = "StartTestcaseTime";
            this.StartTestcaseTime.ReadOnly = true;
            this.StartTestcaseTime.ToolTipText = "Start time of the testing";
            // 
            // EndTestcaseTime
            // 
            this.EndTestcaseTime.DataPropertyName = "EndTestcaseTime";
            dataGridViewCellStyle3.Format = "HH:mm:ss";
            this.EndTestcaseTime.DefaultCellStyle = dataGridViewCellStyle3;
            this.EndTestcaseTime.HeaderText = "End Time";
            this.EndTestcaseTime.Name = "EndTestcaseTime";
            this.EndTestcaseTime.ReadOnly = true;
            this.EndTestcaseTime.ToolTipText = "End time of the call";
            // 
            // Status
            // 
            this.Status.DataPropertyName = "TestCaseVerdict";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.NullValue = null;
            this.Status.DefaultCellStyle = dataGridViewCellStyle4;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Visible = false;
            this.Status.Width = 80;
            // 
            // DataUsage
            // 
            this.DataUsage.DataPropertyName = "DataUsage";
            this.DataUsage.HeaderText = "DataUsage";
            this.DataUsage.Name = "DataUsage";
            this.DataUsage.ReadOnly = true;
            // 
            // DataUsagePayload
            // 
            this.DataUsagePayload.HeaderText = "Payload";
            this.DataUsagePayload.Name = "DataUsagePayload";
            this.DataUsagePayload.ReadOnly = true;
            this.DataUsagePayload.Visible = false;
            // 
            // Progress
            // 
            this.Progress.DataPropertyName = "PorgressValue";
            this.Progress.HeaderText = "Progress";
            this.Progress.Name = "Progress";
            this.Progress.ReadOnly = true;
            this.Progress.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Progress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Progress.Visible = false;
            this.Progress.Width = 133;
            // 
            // Comment
            // 
            this.Comment.DataPropertyName = "Comments";
            dataGridViewCellStyle5.Format = "N2";
            this.Comment.DefaultCellStyle = dataGridViewCellStyle5;
            this.Comment.HeaderText = "Notes";
            this.Comment.Name = "Comment";
            this.Comment.ReadOnly = true;
            this.Comment.Visible = false;
            this.Comment.Width = 300;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Crimson;
            this.label4.Location = new System.Drawing.Point(61, 31);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(258, 29);
            this.label4.TabIndex = 51;
            this.label4.Text = "Barracuda Automation";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1404, 27);
            this.toolStrip1.TabIndex = 64;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(44, 24);
            this.toolStripButton3.Text = "Start";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(74, 24);
            this.toolStripButton1.Text = "About Us";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // lblPassed
            // 
            this.lblPassed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPassed.AutoSize = true;
            this.lblPassed.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblPassed.Location = new System.Drawing.Point(384, 63);
            this.lblPassed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPassed.Name = "lblPassed";
            this.lblPassed.Size = new System.Drawing.Size(28, 29);
            this.lblPassed.TabIndex = 70;
            this.lblPassed.Text = "0";
            // 
            // lblFailed
            // 
            this.lblFailed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFailed.AutoSize = true;
            this.lblFailed.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFailed.ForeColor = System.Drawing.Color.Red;
            this.lblFailed.Location = new System.Drawing.Point(384, 88);
            this.lblFailed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFailed.Name = "lblFailed";
            this.lblFailed.Size = new System.Drawing.Size(28, 29);
            this.lblFailed.TabIndex = 72;
            this.lblFailed.Text = "0";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label13.Location = new System.Drawing.Point(199, 67);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(116, 25);
            this.label13.TabIndex = 75;
            this.label13.Text = "Completed";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(199, 92);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(155, 25);
            this.label14.TabIndex = 76;
            this.label14.Text = "Not Completed";
            // 
            // timerUpdateImage
            // 
            this.timerUpdateImage.Interval = 3000;
            this.timerUpdateImage.Tick += new System.EventHandler(this.timerUpdateImage_Tick);
            // 
            // txtBxRouterIP
            // 
            this.txtBxRouterIP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.txtBxRouterIP.Location = new System.Drawing.Point(120, 96);
            this.txtBxRouterIP.Margin = new System.Windows.Forms.Padding(4);
            this.txtBxRouterIP.Name = "txtBxRouterIP";
            this.txtBxRouterIP.Size = new System.Drawing.Size(111, 22);
            this.txtBxRouterIP.TabIndex = 91;
            this.txtBxRouterIP.Text = "192.168.18.1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 100);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 17);
            this.label3.TabIndex = 92;
            this.label3.Text = "Router IP";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // cmbBxRouters
            // 
            this.cmbBxRouters.FormattingEnabled = true;
            this.cmbBxRouters.Location = new System.Drawing.Point(120, 63);
            this.cmbBxRouters.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBxRouters.Name = "cmbBxRouters";
            this.cmbBxRouters.Size = new System.Drawing.Size(112, 24);
            this.cmbBxRouters.TabIndex = 95;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 63);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 17);
            this.label2.TabIndex = 97;
            this.label2.Text = "Choose Router";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 132);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 17);
            this.label1.TabIndex = 99;
            this.label1.Text = "SSID";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(370, 117);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 25);
            this.label5.TabIndex = 102;
            this.label5.Text = "TC #";
            this.label5.Visible = false;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Location = new System.Drawing.Point(1459, 291);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 28);
            this.button1.TabIndex = 106;
            this.button1.Text = "Test SDK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // chkBxEnableLogcat
            // 
            this.chkBxEnableLogcat.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.chkBxEnableLogcat.AutoSize = true;
            this.chkBxEnableLogcat.Location = new System.Drawing.Point(52, 104);
            this.chkBxEnableLogcat.Margin = new System.Windows.Forms.Padding(4);
            this.chkBxEnableLogcat.Name = "chkBxEnableLogcat";
            this.chkBxEnableLogcat.Size = new System.Drawing.Size(125, 21);
            this.chkBxEnableLogcat.TabIndex = 107;
            this.chkBxEnableLogcat.Text = "Enable Logcat ";
            this.chkBxEnableLogcat.UseVisualStyleBackColor = true;
            // 
            // txtBxSSID
            // 
            this.txtBxSSID.BackColor = System.Drawing.Color.DodgerBlue;
            this.txtBxSSID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxSSID.ForeColor = System.Drawing.Color.White;
            this.txtBxSSID.Location = new System.Drawing.Point(120, 126);
            this.txtBxSSID.Margin = new System.Windows.Forms.Padding(4);
            this.txtBxSSID.Name = "txtBxSSID";
            this.txtBxSSID.ReadOnly = true;
            this.txtBxSSID.Size = new System.Drawing.Size(112, 23);
            this.txtBxSSID.TabIndex = 109;
            // 
            // btnPause
            // 
            this.btnPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPause.Enabled = false;
            this.btnPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPause.Location = new System.Drawing.Point(400, -4);
            this.btnPause.Margin = new System.Windows.Forms.Padding(4);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(79, 39);
            this.btnPause.TabIndex = 112;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Visible = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnSaveToWord
            // 
            this.btnSaveToWord.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSaveToWord.Location = new System.Drawing.Point(1488, 485);
            this.btnSaveToWord.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveToWord.Name = "btnSaveToWord";
            this.btnSaveToWord.Size = new System.Drawing.Size(127, 28);
            this.btnSaveToWord.TabIndex = 114;
            this.btnSaveToWord.Text = "Save to word";
            this.btnSaveToWord.UseVisualStyleBackColor = true;
            this.btnSaveToWord.Visible = false;
            this.btnSaveToWord.Click += new System.EventHandler(this.btnSaveToWord_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Navy;
            this.label6.Location = new System.Drawing.Point(17, 169);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 25);
            this.label6.TabIndex = 117;
            this.label6.Text = "Inconc";
            this.label6.Visible = false;
            // 
            // lblInconcCount
            // 
            this.lblInconcCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInconcCount.AutoSize = true;
            this.lblInconcCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInconcCount.ForeColor = System.Drawing.Color.Navy;
            this.lblInconcCount.Location = new System.Drawing.Point(145, 165);
            this.lblInconcCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInconcCount.Name = "lblInconcCount";
            this.lblInconcCount.Size = new System.Drawing.Size(28, 29);
            this.lblInconcCount.TabIndex = 118;
            this.lblInconcCount.Text = "0";
            this.lblInconcCount.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.comboBox1.Location = new System.Drawing.Point(91, 237);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(41, 24);
            this.comboBox1.TabIndex = 119;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(49, 99);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 17);
            this.label7.TabIndex = 120;
            this.label7.Text = "Logs wait time";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(157, 241);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 17);
            this.label8.TabIndex = 121;
            this.label8.Text = "m";
            // 
            // btnAddPhone
            // 
            this.btnAddPhone.BackColor = System.Drawing.Color.Magenta;
            this.btnAddPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPhone.ForeColor = System.Drawing.Color.White;
            this.btnAddPhone.Location = new System.Drawing.Point(48, 58);
            this.btnAddPhone.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddPhone.Name = "btnAddPhone";
            this.btnAddPhone.Size = new System.Drawing.Size(222, 28);
            this.btnAddPhone.TabIndex = 122;
            this.btnAddPhone.Text = "Add Phone";
            this.btnAddPhone.UseVisualStyleBackColor = false;
            this.btnAddPhone.Click += new System.EventHandler(this.btnAddPhone_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkBxUseRouter);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtBxRouterIP);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbBxRouters);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtBxSSID);
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(12, 22);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(231, 170);
            this.groupBox1.TabIndex = 123;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Router Settings";
            // 
            // chkBxUseRouter
            // 
            this.chkBxUseRouter.AutoSize = true;
            this.chkBxUseRouter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBxUseRouter.Location = new System.Drawing.Point(120, 25);
            this.chkBxUseRouter.Margin = new System.Windows.Forms.Padding(4);
            this.chkBxUseRouter.Name = "chkBxUseRouter";
            this.chkBxUseRouter.Size = new System.Drawing.Size(112, 21);
            this.chkBxUseRouter.TabIndex = 110;
            this.chkBxUseRouter.Text = "Use Router";
            this.chkBxUseRouter.UseVisualStyleBackColor = true;
            // 
            // btnRemovePhone
            // 
            this.btnRemovePhone.BackColor = System.Drawing.Color.Magenta;
            this.btnRemovePhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemovePhone.ForeColor = System.Drawing.Color.White;
            this.btnRemovePhone.Location = new System.Drawing.Point(48, 22);
            this.btnRemovePhone.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemovePhone.Name = "btnRemovePhone";
            this.btnRemovePhone.Size = new System.Drawing.Size(222, 28);
            this.btnRemovePhone.TabIndex = 124;
            this.btnRemovePhone.Text = "Remove Phone";
            this.btnRemovePhone.UseVisualStyleBackColor = false;
            this.btnRemovePhone.Click += new System.EventHandler(this.btnRemovePhone_Click);
            // 
            // chkBxEnableQXDM
            // 
            this.chkBxEnableQXDM.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.chkBxEnableQXDM.AutoSize = true;
            this.chkBxEnableQXDM.Location = new System.Drawing.Point(13, 192);
            this.chkBxEnableQXDM.Margin = new System.Windows.Forms.Padding(4);
            this.chkBxEnableQXDM.Name = "chkBxEnableQXDM";
            this.chkBxEnableQXDM.Size = new System.Drawing.Size(119, 21);
            this.chkBxEnableQXDM.TabIndex = 125;
            this.chkBxEnableQXDM.Text = "Enable QXDM";
            this.chkBxEnableQXDM.UseVisualStyleBackColor = true;
            // 
            // androidDeviceControl1
            // 
            this.androidDeviceControl1.BackColor = System.Drawing.Color.Transparent;
            this.androidDeviceControl1.Device = null;
            this.androidDeviceControl1.Location = new System.Drawing.Point(0, 8);
            this.androidDeviceControl1.Margin = new System.Windows.Forms.Padding(5);
            this.androidDeviceControl1.Name = "androidDeviceControl1";
            this.androidDeviceControl1.Size = new System.Drawing.Size(352, 574);
            this.androidDeviceControl1.TabIndex = 100;
            // 
            // dataGridViewVerdictColumn1
            // 
            this.dataGridViewVerdictColumn1.DataPropertyName = "StatusID";
            this.dataGridViewVerdictColumn1.HeaderText = "Status";
            this.dataGridViewVerdictColumn1.Name = "dataGridViewVerdictColumn1";
            // 
            // dataGridViewProgressColumn1
            // 
            this.dataGridViewProgressColumn1.DataPropertyName = "PorgressValue";
            this.dataGridViewProgressColumn1.HeaderText = "Progress";
            this.dataGridViewProgressColumn1.Name = "dataGridViewProgressColumn1";
            this.dataGridViewProgressColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewProgressColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 62);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(390, 670);
            this.tabControl1.TabIndex = 127;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.androidDeviceControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(382, 641);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "DUT";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Controls.Add(this.cmbBxTestcases);
            this.tabPage2.Controls.Add(this.btnAddPhone);
            this.tabPage2.Controls.Add(this.chkBxEnableLogcat);
            this.tabPage2.Controls.Add(this.btnRemovePhone);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(382, 641);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.chkBxEnableQXDM);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Location = new System.Drawing.Point(46, 197);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(247, 277);
            this.panel2.TabIndex = 129;
            this.panel2.Visible = false;
            // 
            // cmbBxTestcases
            // 
            this.cmbBxTestcases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxTestcases.FormattingEnabled = true;
            this.cmbBxTestcases.Location = new System.Drawing.Point(40, 151);
            this.cmbBxTestcases.Name = "cmbBxTestcases";
            this.cmbBxTestcases.Size = new System.Drawing.Size(241, 24);
            this.cmbBxTestcases.TabIndex = 128;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel3.Controls.Add(this.cmbbxDuration);
            this.panel3.Controls.Add(this.ddlMusicServices);
            this.panel3.Controls.Add(this.chkBxSendEmailWL);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.chkBxEnableWhiteLIstAnalysing);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.lblPassed);
            this.panel3.Controls.Add(this.lblInconcCount);
            this.panel3.Controls.Add(this.lblFailed);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.btnRun);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.btnPause);
            this.panel3.Location = new System.Drawing.Point(412, 556);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(462, 170);
            this.panel3.TabIndex = 129;
            // 
            // cmbbxDuration
            // 
            this.cmbbxDuration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbxDuration.FormattingEnabled = true;
            this.cmbbxDuration.Items.AddRange(new object[] {
            "5",
            "30",
            "60",
            "120",
            "180",
            "240",
            "320",
            "400",
            "480",
            "560"});
            this.cmbbxDuration.Location = new System.Drawing.Point(204, 40);
            this.cmbbxDuration.Name = "cmbbxDuration";
            this.cmbbxDuration.Size = new System.Drawing.Size(121, 24);
            this.cmbbxDuration.TabIndex = 133;
            this.cmbbxDuration.SelectedIndex = 0; 
            // 
            // ddlMusicServices
            // 
            this.ddlMusicServices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlMusicServices.FormattingEnabled = true;
            this.ddlMusicServices.Items.AddRange(new object[] {
            "Pandora",
            "MusicSound",
            "BlackPlanet",
            "Spotify",
            "Rhapsody",
            "MilkMusic",
            "iHeartRadio",
            "RadioMusic"});
            this.ddlMusicServices.Location = new System.Drawing.Point(27, 41);
            this.ddlMusicServices.Name = "ddlMusicServices";
            this.ddlMusicServices.Size = new System.Drawing.Size(152, 24);
            this.ddlMusicServices.TabIndex = 132;
            this.ddlMusicServices.SelectedIndex = 0;
            // 
            // chkBxSendEmailWL
            // 
            this.chkBxSendEmailWL.AutoSize = true;
            this.chkBxSendEmailWL.Location = new System.Drawing.Point(199, 14);
            this.chkBxSendEmailWL.Name = "chkBxSendEmailWL";
            this.chkBxSendEmailWL.Size = new System.Drawing.Size(130, 21);
            this.chkBxSendEmailWL.TabIndex = 131;
            this.chkBxSendEmailWL.Text = "Send WL Email ";
            this.chkBxSendEmailWL.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(204, 120);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(143, 30);
            this.button2.TabIndex = 130;
            this.button2.Text = "White List Analysis";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // chkBxEnableWhiteLIstAnalysing
            // 
            this.chkBxEnableWhiteLIstAnalysing.AutoSize = true;
            this.chkBxEnableWhiteLIstAnalysing.Location = new System.Drawing.Point(27, 14);
            this.chkBxEnableWhiteLIstAnalysing.Name = "chkBxEnableWhiteLIstAnalysing";
            this.chkBxEnableWhiteLIstAnalysing.Size = new System.Drawing.Size(152, 21);
            this.chkBxEnableWhiteLIstAnalysing.TabIndex = 119;
            this.chkBxEnableWhiteLIstAnalysing.Text = " White List Analysis";
            this.chkBxEnableWhiteLIstAnalysing.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnExportToCSV);
            this.tabPage4.Controls.Add(this.btnSendEmail);
            this.tabPage4.Controls.Add(this.lblWhiteListedUsage);
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Controls.Add(this.lblAnaylzing);
            this.tabPage4.Controls.Add(this.lblTotalProcessed);
            this.tabPage4.Controls.Add(this.lblPercentageNotInWhiteList);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Controls.Add(this.label10);
            this.tabPage4.Controls.Add(this.lblPercentageInWhiteList);
            this.tabPage4.Controls.Add(this.label12);
            this.tabPage4.Controls.Add(this.listView1);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(996, 474);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "TCP Dump Stats";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnExportToCSV
            // 
            this.btnExportToCSV.Location = new System.Drawing.Point(261, 405);
            this.btnExportToCSV.Name = "btnExportToCSV";
            this.btnExportToCSV.Size = new System.Drawing.Size(169, 28);
            this.btnExportToCSV.TabIndex = 97;
            this.btnExportToCSV.Text = "Export to CSV";
            this.btnExportToCSV.UseVisualStyleBackColor = true;
            this.btnExportToCSV.Click += new System.EventHandler(this.btnExportToCSV_Click);
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.Location = new System.Drawing.Point(454, 405);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(137, 28);
            this.btnSendEmail.TabIndex = 96;
            this.btnSendEmail.Text = "Send Email";
            this.btnSendEmail.UseVisualStyleBackColor = true;
            this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
            // 
            // lblWhiteListedUsage
            // 
            this.lblWhiteListedUsage.AutoSize = true;
            this.lblWhiteListedUsage.Location = new System.Drawing.Point(167, 57);
            this.lblWhiteListedUsage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWhiteListedUsage.Name = "lblWhiteListedUsage";
            this.lblWhiteListedUsage.Size = new System.Drawing.Size(12, 17);
            this.lblWhiteListedUsage.TabIndex = 95;
            this.lblWhiteListedUsage.Text = " ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(36, 57);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(119, 17);
            this.label9.TabIndex = 94;
            this.label9.Text = "White List Usage:";
            // 
            // lblAnaylzing
            // 
            this.lblAnaylzing.AutoSize = true;
            this.lblAnaylzing.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnaylzing.ForeColor = System.Drawing.Color.Red;
            this.lblAnaylzing.Location = new System.Drawing.Point(30, 216);
            this.lblAnaylzing.Name = "lblAnaylzing";
            this.lblAnaylzing.Size = new System.Drawing.Size(183, 36);
            this.lblAnaylzing.TabIndex = 93;
            this.lblAnaylzing.Text = "Analyzing ...";
            this.lblAnaylzing.Visible = false;
            // 
            // lblTotalProcessed
            // 
            this.lblTotalProcessed.AutoSize = true;
            this.lblTotalProcessed.Location = new System.Drawing.Point(167, 20);
            this.lblTotalProcessed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalProcessed.Name = "lblTotalProcessed";
            this.lblTotalProcessed.Size = new System.Drawing.Size(12, 17);
            this.lblTotalProcessed.TabIndex = 92;
            this.lblTotalProcessed.Text = " ";
            // 
            // lblPercentageNotInWhiteList
            // 
            this.lblPercentageNotInWhiteList.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPercentageNotInWhiteList.AutoSize = true;
            this.lblPercentageNotInWhiteList.Location = new System.Drawing.Point(194, 149);
            this.lblPercentageNotInWhiteList.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPercentageNotInWhiteList.Name = "lblPercentageNotInWhiteList";
            this.lblPercentageNotInWhiteList.Size = new System.Drawing.Size(12, 17);
            this.lblPercentageNotInWhiteList.TabIndex = 91;
            this.lblPercentageNotInWhiteList.Text = " ";
            this.lblPercentageNotInWhiteList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(38, 149);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(141, 17);
            this.label11.TabIndex = 90;
            this.label11.Text = "Not In White List (%):";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(37, 20);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(115, 17);
            this.label10.TabIndex = 89;
            this.label10.Text = "Total Processed:";
            // 
            // lblPercentageInWhiteList
            // 
            this.lblPercentageInWhiteList.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPercentageInWhiteList.AutoSize = true;
            this.lblPercentageInWhiteList.Location = new System.Drawing.Point(202, 115);
            this.lblPercentageInWhiteList.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPercentageInWhiteList.Name = "lblPercentageInWhiteList";
            this.lblPercentageInWhiteList.Size = new System.Drawing.Size(12, 17);
            this.lblPercentageInWhiteList.TabIndex = 88;
            this.lblPercentageInWhiteList.Text = " ";
            this.lblPercentageInWhiteList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(37, 115);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(145, 17);
            this.label12.TabIndex = 87;
            this.label12.Text = "White List Usage (%):";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IPSource});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.ImageList = null;
            this.listView1.Location = new System.Drawing.Point(261, 20);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(355, 378);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.VirtualListSize = 100000;
            // 
            // IPSource
            // 
            this.IPSource.Text = "IPs Not In White List";
            this.IPSource.Width = 200;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtLogs);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(996, 474);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Device Live Logs";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtLogs
            // 
            this.txtLogs.BackColor = System.Drawing.Color.White;
            this.txtLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtLogs.Location = new System.Drawing.Point(3, 3);
            this.txtLogs.Margin = new System.Windows.Forms.Padding(4);
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtLogs.Size = new System.Drawing.Size(990, 468);
            this.txtLogs.TabIndex = 85;
            this.txtLogs.Text = "";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Location = new System.Drawing.Point(408, 31);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1004, 503);
            this.tabControl2.TabIndex = 128;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1404, 738);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnSaveToWord);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.gridTestCases);
            this.Controls.Add(this.progressBar1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "TMO Barracuda Automation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.gridTestCases)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DataGridView gridTestCases;
        private System.ComponentModel.BackgroundWorker workerCampaingExecution;
        private DataGridViewVerdictColumn dataGridViewVerdictColumn1;
        private DataGridViewProgressColumn dataGridViewProgressColumn1;
        private System.Windows.Forms.Label label4;
       
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Label lblPassed;
        private System.Windows.Forms.Label lblFailed;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Timer timerUpdateImage;
        private System.Windows.Forms.TextBox txtBxRouterIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbBxRouters;
        private System.Windows.Forms.Label label1;
        private AndroidDeviceControl androidDeviceControl1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkBxEnableLogcat;
        private System.Windows.Forms.TextBox txtBxSSID;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnSaveToWord;
        private System.Windows.Forms.Label lblInconcCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnAddPhone;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRemovePhone;
        private System.Windows.Forms.CheckBox chkBxUseRouter;
        private System.Windows.Forms.CheckBox chkBxEnableQXDM;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cmbBxTestcases;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RichTextBox txtLogs;
        private System.Windows.Forms.TabPage tabPage4;
        private FlickerFreeListView listView1;
        private System.Windows.Forms.ColumnHeader IPSource;
        private System.Windows.Forms.Label lblPercentageInWhiteList;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblPercentageNotInWhiteList;
        private System.Windows.Forms.Label lblTotalProcessed;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblAnaylzing;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Execute;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartTestcaseTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndTestcaseTime;
        private DataGridViewVerdictColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataUsage;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataUsagePayload;
        private DataGridViewProgressColumn Progress;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.CheckBox chkBxEnableWhiteLIstAnalysing;
        private System.Windows.Forms.Label lblWhiteListedUsage;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSendEmail;
        private System.Windows.Forms.Button btnExportToCSV;
        private System.Windows.Forms.CheckBox chkBxSendEmailWL;
        private System.Windows.Forms.ComboBox ddlMusicServices;
        private System.Windows.Forms.ComboBox cmbbxDuration;
     
    }
}

