using Airbnb.Scraper.Objects;
using Airbnb.Scraper.Workers.Generic;

namespace Airbnb.Scraper.Workers
{
    public class AirbnbDestinationProducer : Producer<Destination>
    {
        public AirbnbDestinationProducer(Buffer<Destination> buffer) : base(buffer)
        {
            
        }        
    }
}
