using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Windows.Forms;
namespace GibbonGUI
{
    class Utility
    {
        public  static String BytesToStringHelper(double byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            double bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));

            // mchen, 2015-07-14
            // double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            double num = Math.Round(bytes / Math.Pow(1024, place), 3);
            return (Math.Sign(byteCount) * num).ToString() + ' ' + suf[place];
        }
        public static void KillProccess(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);

            foreach (Process process in processes)
            {
                if (process.ProcessName == processName)
                {
                    if (process.HasExited == false)
                    {
                        process.Close();
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }
        }
       
        public static Image resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }

        public static Image GetImageFromFile(string imageName, GibbonLib.Device device)
        {
            try
            {
                // if (File.Exists(device.ImageDirectoryPath.CurrentScreenShotPath + imageName))
                //  {
                Image image = null;

                if (imageName.Contains("currentScreen-"))
                {
                    int counter = 0;
                    System.Threading.Thread.Sleep(1000);
                    while (!File.Exists(device.ImageDirectoryPath.CurrentScreenShotPath + imageName))
                    {
                        System.Threading.Thread.Sleep(500);

                        if (counter++ == 10) break;
                    }


                    string directoryPathAndName = device.ImageDirectoryPath.CurrentScreenShotPath + imageName;
                    //  string directoryPathAndName =  imageName;
                    image = Image.FromFile(directoryPathAndName);
                }
                else
                {
                    try
                    {
                        if (imageName.Contains(","))
                        {
                            image = Image.FromFile(device.ImageDirectoryPath.TemplatesPath + imageName.Split(',')[0]);
                        }
                        else
                        {
                            image = Image.FromFile(device.ImageDirectoryPath.TemplatesPath + imageName);
                        }
                    }
                    catch (Exception ex)
                    {
                        //  System.Windows.Forms.MessageBox.Show(ex.Message);
                        GibbonLib.Logging.WriteLine(ex.Message);

                    }
                }
                return image;
                //    }
                //     else
                //    {
                //       Console.WriteLine("Error showing image" + Device.ImageDirectoryPath.CurrentScreenShotPath + imageName);
                //       return null;
                //    }
            }
            catch (Exception ex)
            {


            }

            return Image.FromFile("problem.png");
        }

      
    }

    public class FileSizeFormatProvider : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter)) return this;
            return null;
        }

        private const string fileSizeFormat = "fs";
        private const Decimal OneKiloByte = 1024M;
        private const Decimal OneMegaByte = OneKiloByte * 1024M;
        private const Decimal OneGigaByte = OneMegaByte * 1024M;

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (format == null || !format.StartsWith(fileSizeFormat))
            {
                return defaultFormat(format, arg, formatProvider);
            }

            if (arg is string)
            {
                return defaultFormat(format, arg, formatProvider);
            }

            Decimal size;

            try
            {
                size = Convert.ToDecimal(arg);
            }
            catch (InvalidCastException)
            {
                return defaultFormat(format, arg, formatProvider);
            }

            string suffix;
            if (size > OneGigaByte)
            {
                size /= OneGigaByte;
                suffix = "GB";
            }
            else if (size > OneMegaByte)
            {
                size /= OneMegaByte;
                suffix = "MB";
            }
            else if (size > OneKiloByte)
            {
                size /= OneKiloByte;
                suffix = "kB";
            }
            else
            {
                suffix = " B";
            }

            string precision = format.Substring(2);
            if (String.IsNullOrEmpty(precision)) precision = "2";
            return String.Format("{0:N" + precision + "}{1}", size, suffix);

        }

        private static string defaultFormat(string format, object arg, IFormatProvider formatProvider)
        {
            IFormattable formattableArg = arg as IFormattable;
            if (formattableArg != null)
            {
                return formattableArg.ToString(format, formatProvider);
            }
            return arg.ToString();
        }

    }

    class ListViewToCSV
    {
        public static void ExportListViewToCSV(ListView listView, string filePath, bool includeHidden)
        {
            //make header string
            StringBuilder result = new StringBuilder();
            WriteCSVRow(result, listView.Columns.Count, i => includeHidden || listView.Columns[i].Width > 0, i => listView.Columns[i].Text);

            //export data rows
            foreach (ListViewItem listItem in listView.Items)
                WriteCSVRow(result, listView.Columns.Count, i => includeHidden || listView.Columns[i].Width > 0, i => listItem.SubItems[i].Text);

            File.WriteAllText(filePath, result.ToString());
        }

        private static void WriteCSVRow(StringBuilder result, int itemsCount, Func<int, bool> isColumnNeeded, Func<int, string> columnValue)
        {
            bool isFirstTime = true;
            for (int i = 0; i < itemsCount; i++)
            {
                if (!isColumnNeeded(i))
                    continue;

                if (!isFirstTime)
                    result.Append(",");
                isFirstTime = false;

                result.Append(String.Format("\"{0}\"", columnValue(i)));
            }
            result.AppendLine();
        }
    }
}