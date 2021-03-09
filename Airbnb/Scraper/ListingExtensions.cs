using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Scraper
{
    public static class ListingExtensions
    {
        public static void ExportCsv(this List<Listing> listings, string fileName)
        {
            throw new NotImplementedException();
        }

        public static void ToJsonFile(this List<Listing> listings, string fileName)
        {
            var json = listings.ToJson();
            File.WriteAllText(fileName, json);
        }

        public static string ToJson(this List<Listing> listings)
        {
            throw new NotImplementedException();
        }
    }
}
