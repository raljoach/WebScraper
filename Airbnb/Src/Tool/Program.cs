using Airbnb.Scraper;
using Airbnb.Scraper.Pages;
using Airbnb.Scraper.Workers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.ScraperTool
{
    class Program
    {
        static void Main(string[] args)
        {
            // Acquire
            ScrapeAirbnbTest1();
            ScrapeAirbnbTest2();
            ScrapeAirbnbTest3();
            
        }

        private static void ScrapeAirbnbTest3()
        {
            var destinations = new List<string>() { "Uruguay", "Armenia", "Maldives", "New Zealand", "Bali", "Sochi" }
            ScrapeAirbnb(destinations);
        }
        private static void ScrapeAirbnbTest2()
        { 
            List<Destination> destinations = Create("Uruguay,Armenia,Maldives,New Zealand,Bali,Sochi");
            ScrapeAirbnb(destinations);
        }

        private static List<Destination> Create(string destinations)
        {
            var result = new List<Destination>();
            var tokens = destinations.Split(',');
            foreach(var name in tokens)
            {
                result.Add(new Destination(name));
            }
            return result;
        }

        private static void ScrapeAirbnbTest1()
        {            
            ScrapeAirbnb(
                "Depoe Bay, OR",
                new DateTime(2021, 05, 03),
                new DateTime(2021, 05, 10));            
        }

        private static void ScrapeAirbnb(List<string> places)
        {
            var destinations = new Buffer<Destination>(100);
            var reservations = new Buffer<Reservation>(100);
            var listings = new Buffer<AirbnbListing>(100);
            var listingService = new ListingService();           
            new AirbnbDestinationWorker(destinations, reservations).Start();
            new AirbnbDestinationWorker(destinations, reservations).Start();
            new AirbnbDestinationWorker(destinations, reservations).Start();
            new AirbnbReservationWorker(reservations,listings).Start();
            new AirbnbReservationWorker(reservations, listings).Start();
            new AirbnbReservationWorker(reservations, listings).Start();
            new AirbnbListingWorker(listings, listingService).Start();
            new AirbnbListingWorker(listings, listingService).Start();
            new AirbnbListingWorker(listings, listingService).Start();

            Parallel.ForEach(places, (place) => new AirbnbDestinationProducer(destinations).Add(new Destination(place)));

        }

        private static void ScrapeAirbnb(List<Destination> destinations)
        {
            foreach (var destination in destinations)
            {
                string query = destination.Name;
                List<Reservation> reservations = Create(DateTime.Now, DateTime.Now.Add(TimeSpan.FromDays(30)));
                foreach (var reservation in reservations)
                {
                    DateTime checkInDate = reservation.CheckIn;
                    DateTime checkoutDate = reservation.CheckOut;
                    ScrapeAirbnb(query, checkInDate, checkoutDate);
                }
            }
        }

        private static List<Reservation> Create(DateTime start, DateTime end)
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
                    result.Add(new Reservation(currentStart, currentEnd));
                }

                currentStart.AddDays(1);
            }
            return result;
        }

        private static void ScrapeAirbnb(string query, DateTime checkInDate, DateTime checkoutDate)
        {
            var airbnb = new AirbnbSearch();
            airbnb.SetQuery(query);
            airbnb.SetCheckIn(checkInDate);
            airbnb.SetCheckOut(checkoutDate);
            var results = airbnb.Search();
            var listings = results.GetListings();
            Console.WriteLine("Listings: " + listings.Count);
            var json = JsonConvert.SerializeObject(listings);
            File.WriteAllText(CreateFileLocation(query, checkInDate, checkoutDate), json);
            Console.WriteLine("Program has ended. Hit enter to exit.");
            Console.ReadLine();
        }

        private static string CreateFileLocation(string query, DateTime checkInDate, DateTime checkoutDate)
        {
            //return @"c:\data\airbnb\depoebay\20210503-20210510.json";
            var root = @"c:\data\airbnb";

            query = query.Replace(" ", "");
            var tokens = query.Split(',');
            var folder = string.Empty;

            foreach(var token in tokens)
            {
                if(folder != string.Empty)
                {
                    folder += "-";
                }
                folder += token.ToLower();
            }

            var folderPath = Path.Combine(root, folder);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var ci = checkInDate;
            var co = checkoutDate;
            var file = $"{ci.Year}{ci.Month}{ci.Day}-{co.Year}{co.Month}{co.Day}.json";
            var fullPath = Path.Combine(folderPath, file);
            Console.WriteLine($"Path: {fullPath}");
            return fullPath;
        }
    }
}
