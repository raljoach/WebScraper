using HtmlAgilityPack;

namespace Airbnb.Scraper
{
    public class Page
    {
        public string Url { get; set; }
        public HtmlDocument Content { get; set; }
    }
}
