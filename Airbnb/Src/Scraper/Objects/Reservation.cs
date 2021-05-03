using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Scraper.Objects
{
    public class Reservation
    {
        public Reservation(DateTime checkIn, DateTime checkOut)
        {
            CheckIn = checkIn;
            CheckOut = checkOut;
        }

        public DateTime CheckIn { get; }
        public DateTime CheckOut { get; }
    }
}
