namespace GibbonGUI
{
    partial class PhoneForm
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
            this.androidDeviceControl1 = new GibbonGUI.AndroidDeviceControl();
            this.SuspendLayout();
            // 
            // androidDeviceControl1
            // 
            this.androidDeviceControl1.BackColor = System.Drawing.Color.Transparent;
            this.androidDeviceControl1.Device = null;
            this.androidDeviceControl1.Location = new System.Drawing.Point(12, 12);
            this.androidDeviceControl1.Name = "androidDeviceControl1";
            this.androidDeviceControl1.Size = new System.Drawing.Size(284, 456);
            this.androidDeviceControl1.TabIndex = 0;
            // 
            // PhoneForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 492);
            this.Controls.Add(this.androidDeviceControl1);
            this.Name = "PhoneForm";
            this.Text = "PhoneForm";
            this.ResumeLayout(false);

        }

        #endregion

        private AndroidDeviceControl androidDeviceControl1;
    }
}