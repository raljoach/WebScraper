using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Scraper.Objects.Geographic
{
    public class City : Location
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string State { get; private set; }

        public override string ToString()
        {
            return this.Name + ", " + (this.Country=="US"?State:Country);
        }
    }
}
