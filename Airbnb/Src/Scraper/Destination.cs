using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Scraper
{
    public class Destination
    {        
        public Destination(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
