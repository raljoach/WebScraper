using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Scraper.Pages
{
    public class AirbnbSearchPage : AirbnbSearchPageElements
    {
        private const string SEARCH_URL = "https://www.airbnb.com/newport-or/stays";
        private IWebDriver _driver;
        public AirbnbSearchPage()
        {
            _driver = new ChromeDriver();

            // navigate to url
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _driver.Navigate().GoToUrl(SEARCH_URL);

            // wait for location box to appear
            wait.Until(webDriver => webDriver.FindElement(By.CssSelector(Query)).Displayed);
        }

        public void SetQuery(string location)
        {
            var locationBox = _driver.FindElement(By.CssSelector(Query));
            //locationBox.Clear();
            //locationBox.SendKeys(location);

            /*var js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("document.getElementsByName('query')[0].setAttribute('value', '   ')");

            locationBox.Clear();
            locationBox.SendKeys(location);
            //var dropDown = _driver.FindElement(By.Id("query__listbox"));
            //if (dropDown != null) Console.WriteLine("Found");
            */
            for (var i = 0; i < "Newport, OR".Length; i++)
            {
                locationBox.SendKeys(Keys.Backspace);
            }

            locationBox.SendKeys(location);
            _driver.FindElements(By.TagName("ul"))[0].FindElements(By.TagName("li"))[0].Click();
            //locationBox.SendKeys(Keys.Enter);
            //locationBox.Click();
        }

        public void SetCheckIn(DateTime date)
        {
            var checkin = _driver.FindElement(By.CssSelector(CheckIn));
            //checkin.SendKeys(date.ToString("ddd") + ", " + date.ToString("MMM") + " " + date.Day);

            var js = (IJavaScriptExecutor)_driver;                  
            js.ExecuteScript("document.getElementsByName('checkin')[0].setAttribute('value', '"+ date.ToString("ddd") + ", " + date.ToString("MMM") + " " + date.Day + "')");

            var calendar = _driver.FindElements(By.TagName("table"))[1];

            // TODO: Change the month -> it's a sibling previous sibling
            var month = calendar.FindElement(By.XPath("preceding-sibling::div")).FindElement(By.TagName("div"));
            if(!month.Text.StartsWith(date.ToString("MMMMMMMMM").Trim()))
            {
                // TODO: click on previous or next button to get to the correct month
            }

            // This assumes the current month is the correct month
            
            var days = calendar.FindElements(By.TagName("td"));
            foreach(var day in days)
            {
                if(int.Parse(day.Text) == date.Day)
                {
                    day.Click();
                    break;
                }
            }
        }

        public void SetCheckOut(DateTime date)
        {
            var checkout = _driver.FindElement(By.CssSelector(CheckOut));
            //checkout.SendKeys(date.DayOfWeek + ", " + date.Month + " " + date.Day);

            var js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("document.getElementsByName('checkout')[0].setAttribute('value', '" + date.ToString("ddd") + ", " + date.ToString("MMM") + " " + date.Day + "')");
        }

        public void Search()
        {
            var submit = _driver.FindElement(By.CssSelector(SearchButton));
            submit.Click();
        }

        

        ~AirbnbSearchPage()
        {
            if(_driver!=null)
            {
                _driver.Close();
                _driver = null;
            }
        }

    }
}
