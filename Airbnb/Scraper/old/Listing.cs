using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Scraper
{
    public class Listing
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Rating { get; set; }
        public string Facilities { get; set; }
        public string Price { get; set; }
        public string Reviews { get; set; }
        public string RoomInfo { get; set; }
    }
}
