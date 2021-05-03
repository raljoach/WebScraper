using Airbnb.Scraper.Objects;
using Airbnb.Scraper.Workers.Generic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airbnb.Scraper.Workers
{
    public class AirbnbDestinationWorker 
    {
        private Consumer<Destination> consumer;
        private Producer<Reservation> producer;

        public AirbnbDestinationWorker(Buffer<Destination> destinations, Buffer<Reservation> reservations)
        {                       
            consumer = new Consumer<Destination>(destinations);
            producer = new Producer<Reservation>(reservations);
        }                

        public void Start()
        {
            consumer.Start(
                (destination)=>
                {
                    string query = destination.Name;
                    DateTime start = DateTime.Now ;
                    DateTime end = start.AddDays(30);
                    CreateReservations(start, end);
                    /*
                    Parallel.ForEach(Create(start, end), (reservation) =>
                      {
                          producer.Add(reservation);
                      });
                    */
                    /*
                    List<Reservation> reservations = Create(DateTime.Now, DateTime.Now.Add(TimeSpan.FromDays(30)));
                    foreach (var reservation in reservations)
                    {
                        DateTime checkInDate = reservation.CheckIn;
                        DateTime checkoutDate = reservation.CheckOut;
                        ScrapeAirbnb(query, checkInDate, checkoutDate);
                    }
                    */
                });
        }
        /*
        private static IEnumerable<Reservation> Create(DateTime start, DateTime end)
        {
            var result = new List<Reservation>();
            var numDays = end.Subtract(start).Days;
            var currentStart = start;

            while (currentStart <= end)
            {
                int day = 0;
                while (++day <= numDays)
                {
                    var currentEnd = currentStart.AddDays(1);
                    if (currentEnd > end)
                        break;
                  yield return new Reservation(currentStart, currentEnd);
                }

                currentStart.AddDays(1);
            }           
        }
        */

        private void CreateReservations(DateTime start, DateTime end)
        {
            var result = new List<Reservation>();
            var numDays = end.Subtract(start).Days;
            var nextStart = start;
            var i = 0;
            //while (nextStart <= end)
            while(i++<numDays)
            {
                //int day = 0;
                Task.Run(() => Enumerate(nextStart, end, numDays)
                /*{

                    
                    var thisStart = currentStart;
                    var thisDay = day;
                    while (++thisDay <= numDays)
                    {
                        var thisEnd = thisStart.AddDays(1);
                        Task.Run(() => Produce(thisStart, thisEnd, end)
                        
                        //{
                           // var thatStart = thisStart;
                          //  var thatEnd = thisEnd;
                        //    if (thatEnd > end)
                       //         return;
                       //     producer.Add(new Reservation(thatStart, thatEnd));
                        //}
                        );
                    }
                    
                }*/
                );

                nextStart.AddDays(1);
            }
        }

        private void Enumerate(DateTime thisStart, DateTime lastDate, int numDays)
        {
            int day = 0;
            while (day++ < numDays)
            {
                var thisEnd = thisStart.AddDays(1);
                Task.Run(() => Produce(thisStart, thisEnd, lastDate)

                //{
                // var thatStart = thisStart;
                //  var thatEnd = thisEnd;
                //    if (thatEnd > end)
                //         return;
                //     producer.Add(new Reservation(thatStart, thatEnd));
                //}
                );
            }
        }

        private void Produce(DateTime thatStart, DateTime thatEnd, DateTime lastDate)
        {            
            if (thatEnd > lastDate || thatStart==lastDate)
                return;
            producer.Add(new Reservation(thatStart, thatEnd));
        }
    }
}
