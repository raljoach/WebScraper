using Airbnb.Scraper.Pages;
using Airbnb.Scraper.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Scraper
{
    public class AirbnbListingWorker
    {
        private Consumer<AirbnbListing> consumer;
        private ListingService service;

        public AirbnbListingWorker(Buffer<AirbnbListing> listings, ListingService listingService)
        {
            consumer = new Consumer<AirbnbListing>(listings);
            service = listingService;
        }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}
