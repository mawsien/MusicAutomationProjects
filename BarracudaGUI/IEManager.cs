using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatiN;
using WatiN.Core;
namespace GibbonGUI
{
    class IEManager
    {
        WatiN.Core.IE ie  ;
        public  void GoToComSenderPage(string phone)
        {
            ie = new WatiN.Core.IE(" http://t2packetcore.eng.t-mobile.com/tools/findsub.html");

            TextField subscriberkey = ie.TextField(Find.ByName("subscriberkey"));
            subscriberkey.TypeText("4253013165");
            CheckBox chk = ie.CheckBox(Find.ByName("includeims")); // includeims
            chk.Checked = false;
            chk = ie.CheckBox(Find.ByName("includesgsn")); // includeims
            chk.Checked = false;

            chk = ie.CheckBox(Find.ByName("includemme")); // includeims
            chk.Checked = false;

            chk = ie.CheckBox(Find.ByName("includeggsn")); // includeims
            chk.Checked = true;


            chk = ie.CheckBox(Find.ByName("includeepdg")); // includeims
            chk.Checked = false;

         
            //includemme  includemme includeggsn  includeepdg  includeims
            Button Submit = ie.Button(Find.ById("submit"));
            Submit.Click();
        //    ie.WaitUntilContainsText("Show Full Output",10000);//just to confirm ajax already done

            bool found=false;
            for (int i = 0; i <= 200; i++)
            {
               
                Button btn = ie.Button(Find.ByValue("Show Full Output"));
                if (btn.Exists)
                {
                    break;
                    found = true; 
                }
                System.Threading.Thread.Sleep(1000);
            }
            Div ggsninformation = ie.Div(Find.ById("ggsninformation"));
            Table ggsninformationTable=ggsninformation.Tables[0];


            string ggsn = ggsninformationTable.TableRows[0].TableCells[1].Text;
           
            ie = new WatiN.Core.IE("http://10.81.59.38/comsender/login.jsp");

            TextField username = ie.TextField(Find.ByName("username"));
            username.TypeText("akim");

            TextField password = ie.TextField(Find.ByName("password"));
            password.TypeText("pinkthunder");

             Submit = ie.Button(Find.ByName("Submit"));
            Submit.Click();
            ie.WaitForComplete();
            Link lGGSN = ie.Link(Find.ByText("GGSN"));

            lGGSN.Click();
            ie.WaitForComplete();
            Span sGGSN = ie.Span(Find.ByText("GGSN"));

            Link MusicStreamingUsage = ie.Link(Find.ByText("Music Streaming Usage"));

            MusicStreamingUsage.Click();

            TextField msisdn = ie.TextField(Find.ByName("msisdn"));
            msisdn.TypeText("4253013165");

            WatiN.Core.SelectList ipGgsnMusicStream = ie.SelectList(Find.ByName("ipGgsnMusicStream"));

            ipGgsnMusicStream.Select(ggsn);
            System.Threading.Thread.Sleep(5000);
            Submit = ie.Button(Find.ByName("Submit"));
            Submit.Click();
            ie.WaitForComplete();
            
        }
    }
}
