using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Scraper
{
    public static class AirbnbSelector
    {
        public static By HomePage
        {
            get
            {                
                return By.CssSelector(HomePageCss);
            }
        }

        public static By Pagination
        {
            get
            {                
                return By.CssSelector(PaginationCss);
            }
        }
                
        public static string HomePageCss
        {
            get
            {
                var homePage = AnchorCss("aria-label", "Airbnb homepage");                
                return homePage;
            }
        }

        public static string PaginationCss
        {
            get
            {
                var pagination = Css("div", "aria-label", "Search results pagination");
                return pagination;
            }
        }

        public static By Anchor(string attribute, string value)
        {
            var anchor = AnchorCss(attribute, value);
            return By.CssSelector(anchor);
        }

        private static string AnchorCss(string attribute, string value)
        {
            var anchor = Css("a", attribute, value);
            return anchor;
        }


        private static string Css(string elementType, string attribute, string value)
        {
            return $"{elementType}[{attribute}='{value}']";
        }

        
    }
}
