using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Collections.Generic;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Android;
using System;
using System.Text;
namespace AndroidBrowser
{
   public class BrowserConstroller
    {
        private IWebDriver _driver;

        public   BrowserConstroller()
        {
            
        }
        public void StartBrowser()
        {
            //"http://device_ip:8080/wd/hub/"
            //Screenshot ss =  _driver.Manage().Window.ta
        //    AndroidDriver _driver = new AndroidDriver();
         //    Screenshot screenShot=  _driver.GetScreenshot();
        //    screenShot.SaveAsFile(
            _driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 30));
            _driver.Manage().Cookies.DeleteAllCookies();
            _driver.Navigate().GoToUrl("http://ma.t-mobile.com");
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            System.Threading.Thread.Sleep(10000);
            _driver.FindElement(By.XPath("//a[contains(@title, 'Plans and Services')]")).Click();
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            //  XPathFinder.Find.Tag("div").Containing("Joe Smith!").ToXPathExpression()
            //   WebDriver.FindElement(By.LinkText("/Add/Change Services")).Click();//Add/Change Services
            _driver.FindElement(By.XPath("//input[contains(@value, 'Add/Change Services')]")).Click();
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _driver.FindElement(By.XPath("//a[contains(@title, 'Minutes')]")).Click();
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            _driver.FindElement(By.XPath("//a[contains(@href,'upsell/options.do')]")).Click();

            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            _driver.FindElement(By.XPath("//input[contains(@value, 'Unlimited - Ultra 10GB High Speed Data')]")).Click();
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            //  IWebDriver driver = new RemoteWebDriver(new Uri("http://localhost:3001/wd/hub"), DesiredCapabilities.IPhone());
            // 
            // IWebElement element=  WebDriver.FindElement(
            // Test the autocomplete response
            // IWebElement autocomplete = _driver.FindElement(By.ClassName("ac-row-110457"));

            // string autoCompleteResults = autocomplete.Text;

            // _driver.get_screenshot_as_file("android_google.png")

            _driver.FindElement(By.XPath("//input[contains(@value, 'Upgrade')]")).Click();
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var elem = _driver.FindElement(By.XPath("//*[contains(.,'search_text')]"));
            if (elem == null)
            {
                Console.WriteLine("The text is not found on the page!");
            }
        }

      /*  #region helperMethods
        /// Select a RadioButton or an option on a RadioButtonList.
        /// If optionText is specified for a RadioButtonList, this will select that option.  Otherwise it works for native RadioButtons
        /// prefix = html filter e.g.  "p/" or "tr/td/"
        public void SelectRadio(string htmlControl, string templateName ,string uniqueTemplate, string optionText = "", string prefix = "")
        {
            string path = "//" + prefix + "" + htmlControl + "[contains(@" + templateName + ", '" + uniqueTemplate + "')]";

            if (optionText != "")
                path += "/option[contains(text(), '" + optionText + "')]";

            ClickByXPath(path);
        }

        /// Select a CheckBox
        /// prefix = html filter e.g.  "p/" or "tr/td/"
        public void SelectCheckbox(string checkboxID, string prefix = "")
        {
            string path = "//" + prefix + "input[contains(@id, '" + checkboxID + "')]";

            ClickByXPath(path);
        }

        /// Select an option on a DropDownList
        /// prefix = html filter e.g.  "p/" or "tr/td/"
        public void SelectDropDownOption(string dropdownID, string optionText = "", string prefix = "")
        {
            string path = "//" + prefix + "select[contains(@id, '" + dropdownID + "')]";

            if (optionText != "")
                path += "/option[contains(text(), '" + optionText + "')]";

            ClickByXPath(path);
        }

        /// Enter text into a TextBox and optionally clear it out first.
        public void EnterTextBox(string txtName, string value, bool clearField = false)
        {
            _driver.sendKeysByXPath("//input[contains(@id,'" + txtName + "')]", value, clearField);
        }

        /// Confirm if a condition is true.  If it is, output the confirmationMessage.
        public void ConfirmTrue(bool test, string confirmationMessage)
        {
            Tester.Assert(test, confirmationMessage);
        }

        /// Test to see if a control exists on the page.
        /// Control type = input, div, td or whatever
        public bool ControlExists(string controlType, string controlID)
        {
            string path = "//" + controlType + "[contains(@id,'" + controlID + "')]";
            return _driver.FindElement(By.XPath(path)).ex;
        }

        /// Click a Button
        public void ClickButton(string buttonName, string prefix = "")
        {
            string path = "//" + prefix + "input[contains(@id, '" + buttonName + "')]";

            ClickByXPath(path);
        }

        public void ClickByXPath(string xPath)
        {
            Tester.Driver.ClickByXPath(xPath);
        }

        public void ClickByLinkText(string linkText)
        {
            _driver.clickByLinkText(linkText);
        }
    
        #endregion
       * */
    }
}