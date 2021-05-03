using Airbnb.Scraper.Workers.Generic;

namespace Airbnb.Scraper.Workers
{
    public class PlacesProducer : Producer<string>
    {
        public PlacesProducer(Buffer<string> buffer) : base(buffer)
        {

        }
    }
}
