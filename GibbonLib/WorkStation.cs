using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GibbonLib
{
  public  class WorkStation
    {
        public  List<string> GetAttachedDevices()
        {
           CMD commandLine = new CMD();
            List<string> devices = new List<string>();
            string returnResult = commandLine.ExceuteAndReturn("adb.exe", "devices");

            if (returnResult.Contains("List of devices attached"))
            {
                string[] splitted = returnResult.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                int index = 0;
                foreach (string line in splitted)
                {
                    if (line.Contains("\t"))
                    {
                        string[] s = line.Split(new string[] { "\t" }, StringSplitOptions.None);

                        if (!line.ToLower().Contains("offline"))
                        {
                            devices.Add(s[0]);
                            Logging.WriteLine(line);
                        }
                        index++;
                    }
                }
            }
            return devices;
        }
    }
}


//////




