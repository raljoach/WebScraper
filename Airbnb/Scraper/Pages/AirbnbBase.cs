using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Scraper.Pages
{
    public class AirbnbBase : BasePage
    {
        protected string Query
        {
            get
            {
                return Css("input", "name", "query");
            }
        }

        protected string Location
        {
            get
            {
                return Css("ul", "aria-label", "Location");
            }
        }

        protected string CheckIn
        {
            get
            {
                return Css("input", "name", "checkin");
            }
        }

        protected string CheckOut
        {
            get
            {
                return Css("input", "name", "checkout");
            }
        }

        protected string SearchButton
        {
            get
            {
                return Css("button", "id", "submit_btn");
            }
        }

        protected string PreviousMonth
        {
            get
            {
                return Css("div", "data-testid", "calendar-nav-back");
            }
        }

        protected string NextMonth
        {
            get
            {
                return Css("div", "data-testid", "calendar-nav-next");
            }
        }

        protected string Listings
        {
            get
            {
                return Css("a", new List<List<string>>() { 
                    new List<string>() { "href", BeginsWith, "/rooms" },
                    new List<string>() { "target", BeginsWith, "listing" } }
                );
            }
        }

        protected string PaginateNext
        {
            get
            {
                return Css("a", "aria-label", "Next");
            }
        }
    }
}
