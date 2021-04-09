namespace Airbnb.Scraper.Pages
{
    public class AirbnbListing
    {        
        public AirbnbListing(string url)
        {
            this.Url = url;
        }

        public string Url { get; private set; }
        public string PerNight { get; internal set; }
        public string Total { get; internal set; }
        public string Location { get; internal set; }
        public string Description { get; internal set; }
        public string Rating { get; internal set; }
    }
}
