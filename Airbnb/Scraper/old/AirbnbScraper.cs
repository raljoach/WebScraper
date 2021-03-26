using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Scraper
{
    public class AirbnbScraper
    {
        private Page _page = null;
        public AirbnbScraper(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            _page = new Page()
            {
                Content = doc
            };
        }

        public string FindNextPage()
        {
            throw new NotImplementedException();
        }
    }
}
