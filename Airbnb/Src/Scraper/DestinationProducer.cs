using Airbnb.Scraper.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Scraper
{
    public class DestinationProducer : Producer<Destination>
    {
        public DestinationProducer(Buffer<Destination> buffer) : base(buffer)
        {
            
        }        
    }
}
