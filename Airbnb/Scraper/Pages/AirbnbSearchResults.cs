using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Scraper.Pages
{
    public class AirbnbSearchResults : AirbnbBase
    {
        private IWebDriver _driver;

        internal AirbnbSearchResults(IWebDriver driver)
        {
            _driver = driver;
        }

        public List<AirbnbListing> GetListings()
        {
            var results = new List<AirbnbListing>();
            
            HashSet<string> alreadyHave = new HashSet<string>();
            IWebElement next = null;
            do
            {
                var listings = _driver.FindElements(By.CssSelector(Listings));
                foreach (var listing in listings)
                {
                    var url = listing.GetAttribute("href");
                    if (!alreadyHave.Add(url))
                    {
                        var airbnbListing = new AirbnbListing(url);
                        results.Add(airbnbListing);
                    }
                }
                try
                {
                    next = _driver.FindElement(By.CssSelector(PaginateNext));
                    if (next != null)
                    {
                        next.Click();
                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                        //listings = _driver.FindElements(By.CssSelector(Listings));
                    }
                }
                catch(NoSuchElementException)
                {
                    break;
                }
            } while (next != null);
            return results;
        }
    }
}
