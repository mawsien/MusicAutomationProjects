using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using eExNetworkLibrary;
using eExNetworkLibrary.IP;
using System.Collections;
namespace WhiteListStatLib
{
   public class IP6
    {
        public static IPAddress[] GetRangeOfIP6(string ipaddress)
        {

            //  string strIn = "2001:DB8::/120";
            string strIn = ipaddress;
            List<string> subnetIPAddresses = new List<string>();
            //Split the string in parts for address and prefix
            string strAddress = strIn.Substring(0, strIn.IndexOf('/'));
            string strPrefix = strIn.Substring(strIn.IndexOf('/') + 1);

            int iPrefix = Int32.Parse(strPrefix);
            IPAddress ipAddress = IPAddress.Parse(strAddress);

            //Convert the prefix length to a valid SubnetMask

            int iMaskLength = 32;

            if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
            {
                iMaskLength = 128;
            }

            BitArray btArray = new BitArray(iMaskLength);
            for (int iC1 = 0; iC1 < iMaskLength; iC1++)
            {
                //Index calculation is a bit strange, since you have to make your mind about byte order.
                int iIndex = (int)((iMaskLength - iC1 - 1) / 8) * 8 + (iC1 % 8);

                if (iC1 < (iMaskLength - iPrefix))
                {
                    btArray.Set(iIndex, false);
                }
                else
                {
                    btArray.Set(iIndex, true);
                }
            }

            byte[] bMaskData = new byte[iMaskLength / 8];

            btArray.CopyTo(bMaskData, 0);

            //Create subnetmask
            Subnetmask smMask = new Subnetmask(bMaskData);

            //Get the IP range
            IPAddress ipaStart = IPAddressAnalysis.GetClasslessNetworkAddress(ipAddress, smMask);
            IPAddress ipaEnd = IPAddressAnalysis.GetClasslessBroadcastAddress(ipAddress, smMask);

            //Omit the following lines if your network range is large
            IPAddress[] ipaRange = IPAddressAnalysis.GetIPRange(ipaStart, ipaEnd);

            //Debug output
            //  foreach (IPAddress ipa in ipaRange)
            //  {
            //  Console.WriteLine(ipa.ToString());
            //      subnetIPAddresses.Add(ipa.ToString());
            //  }

            //   Console.ReadLine();
            return ipaRange;

        }
        public static bool CheckIfItIsInRange(string ipaddress, string subntIP)
        {

            //  string strIn = "2001:DB8::/120";
            string strIn = ipaddress;
            List<string> subnetIPAddresses = new List<string>();
            //Split the string in parts for address and prefix
            string strAddress = strIn.Substring(0, strIn.IndexOf('/'));
            string strPrefix = strIn.Substring(strIn.IndexOf('/') + 1);

            int iPrefix = Int32.Parse(strPrefix);
            IPAddress ipAddress = IPAddress.Parse(strAddress);

            //Convert the prefix length to a valid SubnetMask

            int iMaskLength = 32;

            if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
            {
                iMaskLength = 128;
            }

            BitArray btArray = new BitArray(iMaskLength);
            for (int iC1 = 0; iC1 < iMaskLength; iC1++)
            {
                //Index calculation is a bit strange, since you have to make your mind about byte order.
                int iIndex = (int)((iMaskLength - iC1 - 1) / 8) * 8 + (iC1 % 8);

                if (iC1 < (iMaskLength - iPrefix))
                {
                    btArray.Set(iIndex, false);
                }
                else
                {
                    btArray.Set(iIndex, true);
                }
            }

            byte[] bMaskData = new byte[iMaskLength / 8];

            btArray.CopyTo(bMaskData, 0);

            //Create subnetmask
            Subnetmask smMask = new Subnetmask(bMaskData);

            //Get the IP range
            IPAddress ipaStart = IPAddressAnalysis.GetClasslessNetworkAddress(ipAddress, smMask);
            IPAddress ipaEnd = IPAddressAnalysis.GetClasslessBroadcastAddress(ipAddress, smMask);
            IPAddress ip = IPAddress.Parse(subntIP);
            //  IPAddress ip = new IPAddress(subntIP);
            var o = new IPAddressRange(ipaStart, ipaEnd);
            bool res = o.IsInRange(ip);
            return res;
        }
    }

    public class IPAddressRange
    {
        readonly System.Net.Sockets.AddressFamily addressFamily;
        readonly byte[] lowerBytes;
        readonly byte[] upperBytes;

        public IPAddressRange(IPAddress lower, IPAddress upper)
        {
            // Assert that lower.AddressFamily == upper.AddressFamily

            this.addressFamily = lower.AddressFamily;
            this.lowerBytes = lower.GetAddressBytes();
            this.upperBytes = upper.GetAddressBytes();
        }

        public bool IsInRange(IPAddress address)
        {
            if (address.AddressFamily != addressFamily)
            {
                return false;
            }

            byte[] addressBytes = address.GetAddressBytes();

            bool lowerBoundary = true, upperBoundary = true;

            for (int i = 0; i < this.lowerBytes.Length &&
                (lowerBoundary || upperBoundary); i++)
            {
                if ((lowerBoundary && addressBytes[i] < lowerBytes[i]) ||
                    (upperBoundary && addressBytes[i] > upperBytes[i]))
                {
                    return false;
                }

                lowerBoundary &= (addressBytes[i] == lowerBytes[i]);
                upperBoundary &= (addressBytes[i] == upperBytes[i]);
            }

            return true;
        }
    }
    public class IPSubnet
    {
        private readonly byte[] _address;
        private readonly int _prefixLength;

        public IPSubnet(string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            string[] parts = value.Split('/');
            if (parts.Length != 2)
                throw new ArgumentException("Invalid CIDR notation.", "value");

            _address = IPAddress.Parse(parts[0]).GetAddressBytes();
            _prefixLength = Convert.ToInt32(parts[1], 10);
        }

        public bool Contains(string address)
        {
            return this.Contains(IPAddress.Parse(address).GetAddressBytes());
        }

        public bool Contains(byte[] address)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            if (address.Length != _address.Length)
                return false; // IPv4/IPv6 mismatch

            int index = 0;
            int bits = _prefixLength;

            for (; bits >= 8; bits -= 8)
            {
                if (address[index] != _address[index])
                    return false;
                ++index;
            }

            if (bits > 0)
            {
                int mask = (byte)~(255 >> bits);
                if ((address[index] & mask) != (_address[index] & mask))
                    return false;
            }

            return true;
        }


    }
}


