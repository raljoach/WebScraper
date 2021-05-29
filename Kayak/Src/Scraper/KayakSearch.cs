using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kayak.Scraper
{
    public class KayakSearch : KayakBase
    {
        private const string SEARCH_URL = "https://www.kayak.com/flights";
        private IWebDriver _driver;
        public KayakSearch()
        {
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("headless");
            _driver = new ChromeDriver(chromeOptions);

            // navigate to url
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _driver.Navigate().GoToUrl(SEARCH_URL);

            // wait for location box to appear
            wait.Until(webDriver => webDriver.FindElement(By.CssSelector(FromTextBox)).Displayed);
        }

        public string From { get; set; }
        public string To { get; set; }
        public DateTime Depart { get; set; }
        public DateTime Return { get; set; }

        public void Search()
        {

        }
    }
}
