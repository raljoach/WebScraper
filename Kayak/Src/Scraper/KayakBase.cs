using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kayak.Scraper
{
    public class KayakBase : BasePage
    {
        protected string FromTextBox => Css("div", "class", "vvTc-item-value");
            
    }
}
