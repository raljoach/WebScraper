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
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
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
                    var attempts = 0;
                    //var found
                    do
                    {
                        try
                        {
                            var url = listing.GetAttribute("href");
                            if (!alreadyHave.Add(url))
                            {
                                var airbnbListing = new AirbnbListing(url);
                                results.Add(airbnbListing);
                            }
                            break;
                        }
                        catch (StaleElementReferenceException)
                        {
                            attempts++;
                            if (attempts < 2)
                            {
                                listings = _driver.FindElements(By.CssSelector(Listings));
                            }
                            else
                                throw;
                        }
                    } while (attempts<2);
                }
                try
                {
                    next = _driver.FindElement(By.CssSelector(PaginateNext));
                    if (next != null)
                    {
                        next.Click();
                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
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
