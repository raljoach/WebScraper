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
                //foreach (var listing in listings)
                for (var i=0; i<listings.Count; i++)
                {
                    var listing = listings[i];
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
                                airbnbListing.Description = listing.GetAttribute("aria-label");
                                airbnbListing.Total = listing.FindElement(By.XPath("//span[contains(text(),' total')]")).Text;
                                airbnbListing.PerNight = listing.FindElement(By.XPath("//span[contains(text(),' per night')]")).Text;
                                airbnbListing.Location = listing.FindElement(By.XPath("//div[contains(text(),' in ')]")).Text;
                                try
                                {                                    
                                    airbnbListing.Rating = listing.FindElement(By.XPath("//span[starts-with(@aria-label,'Rating ') and contains(@aria-label,' out of ')]")).Text;
                                }
                                catch(NoSuchElementException)
                                { }
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
                                listing = listings[i];
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
