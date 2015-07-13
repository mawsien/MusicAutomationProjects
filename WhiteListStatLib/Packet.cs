using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteListStatLib
{
        public class Packet
        {
            public int IsUplink = 0;

            public string frame_number = String.Empty;//frame.number
            public string frame_time = String.Empty;//frame.time
            public string wlan_sa = String.Empty;//wlan.sa
            public string wlan_da = String.Empty;//wlan.da 
            public string frame_len = String.Empty;//frame.len
            public string httpContent = String.Empty; //http
            public string http_request_method = String.Empty;
            public string ipv6_src = String.Empty;//ipv6.src 
            public string ipv6_dst = String.Empty;//  ipv6.dst 

            public string ip_src = String.Empty;//  ip.src 
            public string ip_dst = String.Empty;//  ip.dst 
            public string ip_version = String.Empty;//  ip.version 
            public string http_host = String.Empty;// http.host 
            public double payLoad
            {
                get;
                set;
            }
            public long FrameLength
            {
                get { return long.Parse(frame_len); }
            }
            public string FrameTime
            {
                get { return frame_time; }
            }
            public string HttpHost
            {
                get
                {
                    return http_host;
                }
            }
           
            public int IPVersion
            {
                get
                {
                    return int.Parse(ip_version);
                }
            }
            public string IPSource
            {
                get
                {
                    if (ip_version == "4")
                    {
                        return ip_src;

                    }
                    else if (ip_version == "6")
                    {
                        return ipv6_src;
                    }
                    return String.Empty;

                }
            }

            public string IPDestination
            {
                get
                {
                    if (ip_version == "4")
                    {
                        return ip_dst;

                    }
                    else if (ip_version == "6")
                    {
                        return ipv6_dst;
                    }
                    return String.Empty;
                }
            }
           
        }
    }


