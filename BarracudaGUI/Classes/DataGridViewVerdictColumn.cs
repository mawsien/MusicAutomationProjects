
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;


    class DataGridViewVerdictColumn : DataGridViewImageColumn
{
        public DataGridViewVerdictColumn()
    {
        CellTemplate = new DataGridViewVerdictCell();
    }
}
    class DataGridViewVerdictCell  : DataGridViewImageCell
    {
        // Used to make custom cell consistent with a DataGridViewImageCell
        static Image emptyImage;
        static DataGridViewVerdictCell()
        {
            emptyImage = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }
        public DataGridViewVerdictCell()
        {
            this.ValueType = typeof(int);
        }
        // Method required to make the Progress Cell consistent with the default Image Cell. 
        // The default Image Cell assumes an Image as a value, although the value of the Progress Cell is an int.
        protected override object GetFormattedValue(object value,
                            int rowIndex, ref DataGridViewCellStyle cellStyle,
                            TypeConverter valueTypeConverter,
                            TypeConverter formattedValueTypeConverter,
                            DataGridViewDataErrorContexts context)
        {
            return emptyImage;
        }

        protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            int Val=0;
            if (value != null)
            {
                Val = (int)value;
            }

             // Draws the cell grid
            base.Paint(g, clipBounds, cellBounds,
             rowIndex, cellState, value, formattedValue, errorText,
             cellStyle, advancedBorderStyle, (paintParts & ~DataGridViewPaintParts.ContentForeground));
            Color CellColor = Color.Red;
            string VerdictString = string.Empty;
            switch (Val)
            {
                case 0:
                   // CellColor = Color.DimGray;
                  //  VerdictString = "N/A";
                    CellColor = Color.Transparent;
                    break;
                case 1:
                    CellColor = Color.GreenYellow;
                    VerdictString = "PASSED";
                    break;
                case 2:
                    CellColor = Color.Red;
                    VerdictString = "FAILED";
                    break;
                case 3: 
                    CellColor = Color.Gray;
                    VerdictString = "INCOMPLETE";
                    break;
             
                case 5:
                    CellColor = Color.Gray;
                    VerdictString = "INCOMPLETE";
                    break;
             
                default:
                    CellColor = Color.Transparent;
                    break;

            }
            if (Val != 4)
            {
                g.FillRectangle(new SolidBrush(CellColor), cellBounds.X + 2, cellBounds.Y + 2, cellBounds.Width - 2, cellBounds.Height - 2);
                g.DrawString(VerdictString, cellStyle.Font, Brushes.Black, (cellBounds.X + 10), cellBounds.Y + 2);
                
            }
            else
            {
                g.FillRectangle(new SolidBrush(Color.Transparent), cellBounds.X + 2, cellBounds.Y + 2, cellBounds.Width - 2, cellBounds.Height - 2);
                g.DrawString("In Progress", cellStyle.Font, Brushes.Black, (cellBounds.X + 10), cellBounds.Y + 2);
            }
            }
    }

