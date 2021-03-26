using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Scraper.Pages
{
    public class BasePage
    {
        protected string Css(string elementType, string attribute, string value)
        {
            return $"{elementType}[{attribute}='{value}']";
        }
    }
}
