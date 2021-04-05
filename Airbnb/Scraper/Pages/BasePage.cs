using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Scraper.Pages
{
    public class BasePage
    {
        protected const string BeginsWith = "^=";
        protected string Css(string elementType, string attribute, string value)
        {
            return $"{elementType}[{attribute}='{value}']";
        }

        protected string CssBegins(string elementType, string attribute, string value)
        {
            return $"{elementType}[{attribute}^='{value}']";
        }

        protected string Css(string elementType, List<List<string>> expressions)
        {
            var expr = "";
            foreach(var e in expressions)
            {
                var a = e[0];
                var o = e[1];
                var v = e[2];
                var str = $"[{a}{o}'{v}']";
                expr += str;
            }
            return $"{elementType}{expr}";
        }
    }
}
