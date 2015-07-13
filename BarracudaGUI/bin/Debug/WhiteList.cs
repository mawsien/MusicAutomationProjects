using System;
using System.Collections.Generic;
using System.Text;
using eExNetworkLibrary.IP;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Text.RegularExpressions;
namespace WhiteListStatLib
{

    public class WhiteList
    {

        public long WhiteListedDataUsage = 0;
        public long NotWhiteListedDataUsage = 0;
        public long AllDataUsage = 0;

        public List<Packet> _AllCapturedPackets = new List<Packet>();
        private HashSet<Packet> _PacketsNotInWhiteList = new HashSet<Packet>();
        private HashSet<Packet> _PacketsInWhiteList = new HashSet<Packet>();
    

        List<string> server_ip_address = new List<string>();
        List<string> url_starts_with = new List<string>();
        List<string> url_contain = new List<string>();
        List<string> either_port = new List<string>();
        List<string> http_content_type = new List<string>();

        private string tsharkPath = System.Configuration.ConfigurationSettings.AppSettings["TSharkPath"];

        HashSet<string> lstIPsNotInWhiteLIst = new HashSet<string>();


        public void AnalyseDataUsageStat()
        {
            foreach (Packet packet in _AllCapturedPackets)
            {
                if (IsExistedInWhiteList(packet))
                {
                   _PacketsInWhiteList.Add(packet);
                    WhiteListedDataUsage += packet.FrameLength;
                  
                }
                else
                {
                    _PacketsNotInWhiteList.Add(packet);
                    NotWhiteListedDataUsage += packet.FrameLength;
                }

                AllDataUsage += packet.FrameLength; 
            }
        }
        public HashSet<string> GetNotWhiteListed()
        {
          
            foreach (Packet packet in _PacketsNotInWhiteList)
            {
                try
                {
                    AddPacketToNotWhitelisted(packet);
                }catch(Exception ec){};
            }

            return lstIPsNotInWhiteLIst;
        }
        public void AddPacketToNotWhitelisted(Packet packet)
        {
                try
                {
                    lstIPsNotInWhiteLIst.Add(packet.IPDestination);
                    lstIPsNotInWhiteLIst.Add(packet.IPSource);
                    lstIPsNotInWhiteLIst.Add(packet.HttpHost);
                    lstIPsNotInWhiteLIst.Add(packet.httpContent);
                }
                catch (Exception ec) { };
            
        }
       
       
        public HashSet<Packet> GetListOfPacketInWhiteList()
        {
            return _PacketsInWhiteList;
        }

        public HashSet<Packet> GetListOfPacketNotInWhiteList()
        {
            return _PacketsNotInWhiteList;
        }

       

        public void AddCapturedPacket(Packet p)
        {
            _AllCapturedPackets.Add(p);
        }
        private List<string> GetFileLines(string filePathAndName)
        {
            try
            {
                // Read the file and display it line by line.
                System.IO.StreamReader file = new System.IO.StreamReader(filePathAndName);
                List<string> lines = new List<string>();
                int x = 0;
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Trim() != String.Empty)
                        lines.Add(line);
                }
                file.Close();
                return lines;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public void GetWhiteListFromFile(string path)
        {

            List<string> whiteListLines = GetFileLines(path);
            string[] spiltChars = new string[] { "=", "contains", "starts-with", "either-port" };
            string[] splittedString;

            foreach (string line in whiteListLines)
            {
                splittedString = line.Split(spiltChars, StringSplitOptions.RemoveEmptyEntries);
                if (splittedString.Length > 1)
                {

                    if (line.Contains("ip server-ip-address"))
                        // server_ip_address.Add(splittedString[1].Trim().Split('/')[0]);
                        server_ip_address.Add(splittedString[1].Trim());
                    /* if (splittedString[1].Trim().Contains("/"))
                     {
                         IPAddress[] subnetIPs = IP6.GetRangeOfIP6(splittedString[1].Trim());
                          foreach (IPAddress ip in subnetIPs)
                         {
                             server_ip_address.Add(ip.ToString());
                         }
                     }*/

                    if (line.Contains("url contains"))
                        url_contain.Add(splittedString[1].Trim());
                    if (line.Contains("url starts-with"))
                    {
                        url_starts_with.Add(splittedString[1].Trim());
                        url_starts_with.Add(splittedString[1].Trim().Replace("http://", ""));
                    }
                    if (line.Contains("either-port"))
                        either_port.Add(splittedString[1].Trim());
                    if (line.Contains("http content type"))
                        http_content_type.Add(splittedString[1].Trim());
                    //http content type
                    //dictionaryWhiteList.Add(splittedString[1].Trim(), splittedString[0].Trim());

                }

            }

        }
        string regex = @"((www\.|(http|https|ftp|news|file)+\:\/\/)[&#95;.a-z0-9-]+\.[a-z0-9\/&#95;:@=.+?,##%&~-]*[^.|\'|\# |!|\(|?|,| |>|<|;|\)])";
      
 
        public bool IsExistedInWhiteList(Packet packet)
        {
            //  server_ip_address,url_starts_with,url_contain,either_port ,http_content_type
            //  if (!packet.http_host.Contains("pandora")) 
            //      return false;
            if ( lstIPsNotInWhiteLIst.Contains(packet.IPSource)
                && lstIPsNotInWhiteLIst.Contains(packet.IPDestination)
                && lstIPsNotInWhiteLIst.Contains(packet.HttpHost)
                && lstIPsNotInWhiteLIst.Contains(packet.httpContent))
                return false;

            foreach (string ip in server_ip_address)
            {
                // if (ip.StartsWith("2607:7700:0:14"))
                //  {
                /* if (new IPSubnet(ip).Contains(packet.ipv6_src) || new IPSubnet(ip).Contains(packet.ipv6_dst))
                 {
                      //   Console.WriteLine(new IPSubnet("192.168.168.100/24").Contains("192.168.168.200")); // True
                    //    //  Console.WriteLine(new IPSubnet("fe80::202:b3ff:fe1e:8329/24").Contains("2001:db8::")); // False
                     return true;
                 }*/
                bool res = false;

                if (ip == "127.0.0.1") return false;

                if (!String.IsNullOrEmpty(packet.ipv6_dst))
                {
                     res = IP6.CheckIfItIsInRange(ip, packet.ipv6_dst);
                    if (res)
                        return true;
                }

                if (!String.IsNullOrEmpty(packet.ipv6_src))
                {
                    res = IP6.CheckIfItIsInRange(ip, packet.ipv6_src);
                    if (res)
                        return true;
                }
                if (!String.IsNullOrEmpty(packet.ip_src))
                {
                    res = IP6.CheckIfItIsInRange(ip, packet.ip_src);
                    if (res)
                        return true;
                }
                if (!String.IsNullOrEmpty(packet.ip_dst))
                {
                    res = IP6.CheckIfItIsInRange(ip, packet.ip_dst);
                    if (res)
                        return true;
                }

                //  }
            }
     
            if (!String.IsNullOrEmpty(packet.http_host ))
            {
                foreach (string url in url_starts_with)
                {
                    if (url.StartsWith(packet.http_host)) 
                        return true;
                }
         
                
                Regex r = new Regex(regex, RegexOptions.IgnoreCase);
                foreach (string url in url_contain)
                {
                    if (url.Contains(packet.http_host) )
                        return true;
                }

              
                foreach (string url in url_contain)
                {
                    if (url.Contains(packet.http_host)) 
                        return true;
                }
            }

            if (!String.IsNullOrEmpty(packet.httpContent))
            {
                foreach (string url in http_content_type)
                {
                    if (url.Contains(packet.httpContent))
                        return true;
                }
            }


            /*  if (packet.httpContent != String.Empty)
              {
                  var result3 = http_content_type.Where(c => c.Contains(packet.httpContent));
                  if (result3 != null && result3.Count() > 0) return true;
              }*/
            // var result = dictionaryWhiteList.Where(r => r.Key.StartsWith("card") && r.Value > 0);
            AddPacketToNotWhitelisted(packet);
            return false;

            //  dictionaryWhiteList
        }
      
        private void ReinitLists()
        {
            _AllCapturedPackets.Clear();
        }
        string[] spliOperators = { "\t" };
    
        private bool IsUrlValid(string url)
        {

            string pattern = @"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(url);
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
}
