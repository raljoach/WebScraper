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
            wait.Until(webDriver => webDriver.FindElement(By.CssSelector(QueryInput)).Displayed);
        }

        public void SetLocation(string location)
        {
            var locationBox = _driver.FindElement(By.CssSelector(QueryInput));
            locationBox.SendKeys(location);
        }

        public void SetCheckIn(DateTime date)
        {
            var checkin = _driver.FindElement(By.CssSelector(CheckIn));
            checkin.SendKeys(date.DayOfWeek + ", " + date.Month + " " + date.Day);
        }

        public void SetCheckOut(DateTime date)
        {
            var checkout = _driver.FindElement(By.CssSelector(CheckOut));
            checkout.SendKeys(date.DayOfWeek + ", " + date.Month + " " + date.Day);
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
