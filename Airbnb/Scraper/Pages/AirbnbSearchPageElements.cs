using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Scraper.Pages
{
    public class AirbnbSearchPageElements : BasePage
    {
        protected string Query
        {
            get
            {
                return Css("input", "name", "query");
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
    }
}
