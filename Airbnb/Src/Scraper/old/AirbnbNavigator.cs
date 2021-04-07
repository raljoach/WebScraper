using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace Airbnb.Scraper
{
    public class AirbnbNavigator : IDisposable
    {
        private IWebDriver _driver = null;
        private AirbnbScraper _scraper = null;
                
        public AirbnbNavigator(string url)
        {
            _driver = new ChromeDriver();

            // navigate to url
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _driver.Navigate().GoToUrl(url);            
            wait.Until(webDriver => webDriver.FindElement(AirbnbSelector.HomePage).Displayed);

            var html = _driver.PageSource;
            _scraper = new AirbnbScraper(html);
            
        }

        public List<Listing> GetListings()
        {
            throw new NotImplementedException();
        }

        public bool NextPage()
        {            
            var pagination = _driver.FindElement(AirbnbSelector.Pagination);
            var href = _scraper.FindNextPage();
            bool nextPageExists = !string.IsNullOrWhiteSpace(href);
            if (nextPageExists)
            {
                var nextPage = pagination.FindElement(AirbnbSelector.Anchor("href", href));
                nextPage.Click();
            }
            return nextPageExists;
        }
        
        public void Dispose()
        {
            if (_driver != null)
            {
                _driver.Dispose();
                _driver = null;
            }
        }
    }
}
