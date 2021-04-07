using Airbnb.Scraper;
using Airbnb.Scraper.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableData;

namespace Airbnb.ScraperTool
{
    class Program
    {
        static void Main(string[] args)
        {
            //FirstTry();

            var search = new AirbnbSearch();
            search.SetQuery("Depoe Bay, OR");
            search.SetCheckIn(new DateTime(2021, 05, 03));
            search.SetCheckOut(new DateTime(2021, 05, 10));
            
            var results = search.Search();
            var listings = results.GetListings();
            var json = JsonConvert.SerializeObject(listings);
            File.WriteAllText(@"c:\data\airbnb\depoebay\20210503-20210510.json", json);

            Console.ReadLine();
        }        
    }
}
