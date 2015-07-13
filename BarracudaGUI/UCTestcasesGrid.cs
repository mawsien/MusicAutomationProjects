using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.Odbc;

namespace GibbonGUI
{
    public partial class UCTestcasesGrid : UserControl
    {
        public UCTestcasesGrid()
        {
            InitializeComponent();
            loadCSV(Directory.GetCurrentDirectory()+ @"\Testcases\TestCases.csv");
        }
        private void loadCSV(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show(this, "File does not exist:\r\n" + path, "No File", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            try
            {
                string conStr = @"Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=" + Path.GetDirectoryName(Path.GetFullPath(path)) + ";Extensions=csv,txt";
                OdbcConnection conn = new OdbcConnection(conStr);
                OdbcDataAdapter da = new OdbcDataAdapter("Select * from [" + Path.GetFileName(path) + "]", conn);

               DataTable  dt = new DataTable(path);
                da.Fill(dt);
              
                dataGridView1.DataSource = dt;
                da.Dispose();
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "There was an error loading the CSV file:\r\n" + ex.Message, "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }

}
