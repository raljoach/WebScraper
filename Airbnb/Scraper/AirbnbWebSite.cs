using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Scraper
{
    public class AirbnbWebSite
    {
        private List<string> _urls;
        public AirbnbWebSite(List<string> urls)
        {
            _urls = urls;
        }

        public List<Listing> GetListings()
        {
            List<Listing> results = new List<Listing>();
            foreach (var url in _urls)
            {
                using (var nav = new AirbnbNavigator(url))
                {
                    do
                    {
                        var listings = nav.GetListings();
                        results.AddRange(listings);
                    }
                    while (nav.NextPage());
                }
            }
            return results;
        }
    }
}
