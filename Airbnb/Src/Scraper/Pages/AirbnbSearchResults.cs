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

        // V3
        public List<AirbnbListing> GetListings()
        {
            var results = new List<AirbnbListing>();

            HashSet<string> alreadyHave = new HashSet<string>();
            IWebElement next = null;
            do
            {
                var listings = _driver.FindElements(By.CssSelector(Listings));
                //foreach (var listing in listings)
                for (var i = 0; i < listings.Count; i++)
                {
                    var listing = listings[i];
                    var attempts = 0;
                    //var found
                    do
                    {
                        try
                        {
                            var url = listing.GetAttribute("href");

                            if (alreadyHave.Add(url))
                            {
                                var otherListing = _driver.FindElement(By.CssSelector(GetListingFromAbove(url)));
                                var parent = otherListing.FindElement(By.XPath(".."));

                                var airbnbListing = new AirbnbListing(url);
                                airbnbListing.Description = listing.GetAttribute("aria-label");

                                var spans = parent.FindElements(By.XPath("descendant::span"));
                                var count = 0;
                                foreach (var s in spans)
                                {
                                    if (s.Text.Contains(" total"))
                                    {
                                        ++count;
                                        airbnbListing.Total = s.Text;//listing.FindElement(By.XPath("//span[contains(text(),' total')]")).Text;

                                    }
                                    else if (s.Text.Contains(" per night"))
                                    {
                                        ++count;
                                        airbnbListing.PerNight = s.Text;//listing.FindElement(By.XPath("//span[contains(text(),' per night')]")).Text;                                       
                                    }
                                    else
                                    {
                                        var aria = s.GetAttribute("aria-label");
                                        if (aria!=null && aria.StartsWith("Rating ") && aria.Contains(" out of "))
                                        {
                                            ++count;
                                            airbnbListing.Rating = s.Text;//listing.FindElement(By.XPath("//span[starts-with(@aria-label,'Rating ') and contains(@aria-label,' out of ')]")).Text;
                                        }
                                    }

                                    if(count==3)
                                    {
                                        break;
                                    }
                                }

                                if (airbnbListing.Total == null)
                                {
                                    throw new Exception("Total not found!");
                                }

                                if (airbnbListing.PerNight == null)
                                {
                                    throw new Exception("PerNight not found!");
                                }


                                var divs = parent.FindElements(By.XPath("descendant::div"));
                                foreach (var d in divs)
                                {
                                    if (d.Text.Contains(" in "))
                                    {
                                        airbnbListing.Location = d.Text;//listing.FindElement(By.XPath("//div[contains(text(),' in ')]")).Text;
                                        break;
                                    }
                                }

                                if (airbnbListing.Location == null)
                                {
                                    throw new Exception("Location not found!");
                                }

                                /*
                                try
                                {                                    
                                    airbnbListing.Rating = parent.FindElement(By.CssSelector("descendant::span[starts-with(@aria-label,'Rating ') and contains(@aria-label,' out of ')]")).Text;//listing.FindElement(By.XPath("//span[starts-with(@aria-label,'Rating ') and contains(@aria-label,' out of ')]")).Text;
                                }
                                catch(NoSuchElementException)
                                { }
                                */
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
                    } while (attempts < 2);
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
                catch (NoSuchElementException)
                {
                    break;
                }
            } while (next != null);
            return results;
        }
    }
}
