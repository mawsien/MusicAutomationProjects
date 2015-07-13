
    partial class LogForm
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
            this.rtfTerminal = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportToWordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtfTerminal
            // 
            this.rtfTerminal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtfTerminal.BackColor = System.Drawing.Color.White;
            this.rtfTerminal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtfTerminal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtfTerminal.ForeColor = System.Drawing.Color.Black;
            this.rtfTerminal.Location = new System.Drawing.Point(2, 11);
            this.rtfTerminal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rtfTerminal.Name = "rtfTerminal";
            this.rtfTerminal.ReadOnly = true;
            this.rtfTerminal.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtfTerminal.Size = new System.Drawing.Size(652, 646);
            this.rtfTerminal.TabIndex = 24;
            this.rtfTerminal.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToWordsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(195, 26);
            // 
            // exportToWordsToolStripMenuItem
            // 
            this.exportToWordsToolStripMenuItem.Name = "exportToWordsToolStripMenuItem";
            this.exportToWordsToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(188)))), ((int)(((byte)(230)))));
            this.ClientSize = new System.Drawing.Size(656, 690);
            this.Controls.Add(this.rtfTerminal);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "LogForm";
            this.Text = "Logs";
            this.Shown += new System.EventHandler(this.LogForm_Shown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtfTerminal;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exportToWordsToolStripMenuItem;
    }
