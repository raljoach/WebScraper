using Airbnb.Scraper.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Scraper.Services
{
    public class ListingService
    {
        private const string hostPort = "localhost:8080";
        private string baseUrl = $"https://{hostPort}/api/listings/";
        private WebClient webClient = new WebClient();

        public void Post(AirbnbListing listing)
        {
            throw new NotImplementedException();
        }
    }
}
