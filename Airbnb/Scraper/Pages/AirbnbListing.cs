using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Scraper.Pages
{
    public class AirbnbListing
    {        
        public AirbnbListing(string url)
        {
            this.Url = url;
        }

        public string Url { private get; set; }
    }
}
