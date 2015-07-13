using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GibbonGUI
{
    public partial class PhoneForm : Form
    {
        public AndroidDeviceControl GetAndroidDeviceControl()
        {
            return androidDeviceControl1;
        }
        public PhoneForm()
        {
            InitializeComponent();
        }
    }
}
