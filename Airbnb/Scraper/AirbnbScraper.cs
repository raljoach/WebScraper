using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableData;

namespace Airbnb.Scraper
{
    /* Scrapes Airbnb search results pages
     */
    public class AirbnbScraper
    {
        private List<string> _urls;
        public AirbnbScraper(List<string> urls)
        {
            _urls = urls;
        }

        /* For each main search results url
         *   Get Pages
         *   Get listings for each page
         *   Get information for each listing
         *   Add to table
         */
        public Table Scrape()
        {
            var table = new Table();
            foreach (var url in _urls)
            {
                var pages = GetPages(url);
                foreach (var page in pages)
                {
                    var listings = GetListings(page);
                    foreach (var listing in listings)
                    {
                        var info = GetInformation(listing);
                        table.Add(info);
                    }
                }
            }
            return table;
        }

        private TableRow GetInformation(Listing listing)
        {
            throw new NotImplementedException();
        }

        private List<Listing> GetListings(Page page)
        {
            throw new NotImplementedException();
        }

        private List<Page> GetPages(string url)
        {
            throw new NotImplementedException();
        }
    }
}
