using Airbnb.Scraper.Objects;
using Airbnb.Scraper.Objects.Geographic;
using Airbnb.Scraper.Services;
using Airbnb.Scraper.Workers.Generic;
using System.Threading.Tasks;

namespace Airbnb.Scraper.Workers
{
    public class PlacesWorker
    {
        private Consumer<string> consumer;
        private Producer<Destination> producer;
        private LocationService locationService;

        public PlacesWorker(Buffer<string> places, Buffer<Destination> destinations, LocationService locationService)
        {
            consumer = new Consumer<string>(places);
            producer = new Producer<Destination>(destinations);
            this.locationService = locationService;
        }

        public void Start()
        {
            consumer.Start(
                (place) =>
                {
                    string query = place;
                    var response = locationService.Post(query);

                    Parallel.ForEach(response, (location) =>
                    {
                        if (location is City)
                        {
                            producer.Add(new Destination(location as City));
                        }
                        else 
                        {
                            Parallel.ForEach(locationService.GetCities(location), (city) =>
                            {
                                 producer.Add(new Destination(location as City));
                            });
                        }
                    });                    
                });
        }
    }
}
