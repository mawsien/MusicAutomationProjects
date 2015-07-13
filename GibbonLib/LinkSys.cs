using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;
namespace GibbonLib
{
    [Page(UrlRegex = "http://192.168.5.1/")]
    class LinkSys : Page
    {
        public Link SaveSettings
        {
            get { return Document.Link(Find.ByText("divBT1")); }
        }
    }
    [Page(UrlRegex = "http://192.168.5.1/Wireless_Basic.asp")]
    class WirelessBasic : LinkSys
    {
        
        public TextField NetworkName
        {
            get { return Document.TextField(Find.ByName("wl_ssid")); }
        }

        public SelectList Channels
        {
            get { return Document.SelectList(Find.ById("_wl0_channel")); }
        }
        public SelectList NetworkMode
        {
            get { return Document.SelectList(Find.ById("wl_net_mode")); }
        }
    }

    [Page(UrlRegex = "http://192.168.5.1/WL_WPATable.asp")]
    class WirelessSecurty : LinkSys
    {
        public SelectList SecurityMode
        {
            get { return Document.SelectList(Find.ByName("security_mode2")); }
        }

        public TextField PassPhrase
        {
            get { return Document.TextField(Find.ById("wl_wpa_psk")); }
        }
    }

     [Page(UrlRegex = "http://192.168.5.1/apply.cgi")]
    class Apply : LinkSys
    {
        public Button ReturnButton
        {
            get {  return Document.Button(Find.ByName("btaction")); }
        }
    }
}
