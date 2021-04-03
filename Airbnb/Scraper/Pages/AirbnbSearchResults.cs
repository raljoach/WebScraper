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
            var listings = _driver.FindElements(By.CssSelector(Listings));
            foreach(var listing in listings)
            {
                var url = listing.GetAttribute("href");
                var airbnbListing = new AirbnbListing(url);
                results.Add(airbnbListing);
            }
            return results;
        }
    }
}
