using Airbnb.Scraper.Objects.Geographic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Scraper.Objects
{
    public class Destination
    {        
        public Destination(string name)
        {
            this.Name = name;
        }

        public Destination(City city) : this(city.ToString())
        {

        }

        public string Name { get; set; }
    }
}
