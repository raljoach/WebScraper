using Airbnb.Scraper.Objects;
using Airbnb.Scraper.Services;
using Airbnb.Scraper.Workers.Generic;
using System;

namespace Airbnb.Scraper.Workers
{
    public class AirbnbListingWorker
    {
        private Consumer<AirbnbListing> consumer;
        private ListingService listingService;

        public AirbnbListingWorker(Buffer<AirbnbListing> listings, ListingService listingService)
        {
            consumer = new Consumer<AirbnbListing>(listings);
            this.listingService = listingService;
        }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}
