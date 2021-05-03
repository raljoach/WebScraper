using Airbnb.Scraper.Objects;
using Airbnb.Scraper.Workers.Generic;
using System;

namespace Airbnb.Scraper.Workers
{
    public class AirbnbReservationWorker
    {
        private Consumer<Reservation> consumer;
        private Producer<AirbnbListing> producer;

        public AirbnbReservationWorker(Buffer<Reservation> reservations, Buffer<AirbnbListing> listings)
        {
            consumer = new Consumer<Reservation>(reservations);
            producer = new Producer<AirbnbListing>(listings);
        }

        public void Start()
        {
            consumer.Start(
                (reservation) =>
                {
                    
                    ScrapeListings(reservation);
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

        private void ScrapeListings(Reservation reservation)
        {
            throw new NotImplementedException();
        }
    }
}
