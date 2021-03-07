﻿using Airbnb.Scraper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.ScraperTool
{
    class Program
    {
        static void Main(string[] args)
        {
            var newport = "https://www.airbnb.com/s/Newport--OR--United-States/homes?tab_id=home_tab&refinement_paths%5B%5D=%2Fhomes&flexible_trip_dates%5B%5D=april&flexible_trip_dates%5B%5D=march&flexible_trip_lengths%5B%5D=weekend_trip&date_picker_type=calendar&query=Newport%2C%20OR%2C%20United%20States&place_id=ChIJjZh8TNvVwVQRcvv8YfFyH9Q&checkin=2021-04-04&checkout=2021-04-11&source=structured_search_input_header&search_type=autocomplete_click";
            var urls = new List<string>() { newport };
            var scraper = new AirbnbScraper(urls);
            var table = scraper.Scrape();
            table.ExportCsv("newport.csv");
        }
    }
}
