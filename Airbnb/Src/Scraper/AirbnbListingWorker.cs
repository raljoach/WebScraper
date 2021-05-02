using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Scraper
{
    class AirbnbListingWorker
    {
        public AirbnbListingWorker(object listings)
        {
            Listings = listings;
        }

        public object Listings { get; }
    }
}
