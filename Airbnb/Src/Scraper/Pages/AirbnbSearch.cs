using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Airbnb.Scraper.Pages
{
    public class AirbnbSearch : AirbnbBase
    {
        private const string SEARCH_URL = "https://www.airbnb.com/newport-or/stays";
        private IWebDriver _driver;
        public AirbnbSearch()
        {
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("headless");
            _driver = new ChromeDriver(chromeOptions);

            // navigate to url
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _driver.Navigate().GoToUrl(SEARCH_URL);

            // wait for location box to appear
            wait.Until(webDriver => webDriver.FindElement(By.CssSelector(Query)).Displayed);
        }

        public void SetQuery(string location)
        {
            var locationBox = _driver.FindElement(By.CssSelector(Query));
            for (var i = 0; i < "Newport, OR".Length; i++)
            {
                locationBox.SendKeys(Keys.Backspace);
            }

            locationBox.SendKeys(location);

            //_driver.FindElements(By.TagName("ul"))[0].FindElements(By.TagName("li"))[0].FindElements(By.CssSelector("*"))[0].Click();

            /*
            var locationDropDown = _driver.FindElement(By.CssSelector(Location));
            locationDropDown.FindElements(By.TagName("li"))[0].FindElements(By.CssSelector("*"))[0].Click();
            */
            /*
            var label = locationBox.FindElement(By.XPath("parent::label"));
            var div = label.FindElement(By.XPath("parent::div"));
            var dropDown = div.FindElement(By.XPath("following-sibling::ul"));
            dropDown.Click();
            */
            
            var ac = new Actions(_driver);
            ac.MoveToElement(locationBox).MoveByOffset(10, 10).Click().Perform();
        }

        public void SetCheckIn(DateTime date)
        {
            var checkin = _driver.FindElement(By.CssSelector(CheckIn));                        
            SelectDate(checkin, date, "checkin");
        }

        public void SetCheckOut(DateTime date)
        {
            var checkout = _driver.FindElement(By.CssSelector(CheckOut));            
            SelectDate(checkout, date, "checkout");
        }

        public AirbnbSearchResults Search()
        {
            var submit = _driver.FindElement(By.CssSelector(SearchButton));
            submit.Click();

            var newTb = new List<string>(_driver.WindowHandles);
            //switch to new tab
            _driver.SwitchTo().Window(newTb[1]);
            return new AirbnbSearchResults(_driver);
        }

        private void SelectDate(IWebElement dateControl, DateTime setDate, string controlName)
        {
            var js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("document.getElementsByName('" + controlName + "')[0].setAttribute('value', '" + setDate.ToString("ddd") + ", " + setDate.ToString("MMM") + " " + setDate.Day + "')");

            dateControl.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            var calendar = _driver.FindElements(By.TagName("table"))[1];            
            var monthControl = calendar.FindElement(By.XPath("preceding-sibling::div")).FindElement(By.TagName("div"));
            var tokens = monthControl.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var currentMonthName = tokens[0];
            var currentYear = int.Parse(tokens[1]);
            var currentMonthNum = GetMonthNumber(currentMonthName);
            var setMonthName = setDate.ToString("MMMMMMMMM").Trim();
            var setMonthNum = GetMonthNumber(setMonthName);
            var setYear = setDate.Year;

            while (currentYear < setYear || currentMonthNum < setMonthNum)
            {                                                
                var next =_driver.FindElement(By.CssSelector(NextMonth)).FindElement(By.TagName("button"));
                next.Click();
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                calendar = _driver.FindElements(By.TagName("table"))[1];
                monthControl = calendar.FindElement(By.XPath("preceding-sibling::div")).FindElement(By.TagName("div"));
                tokens = monthControl.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                currentMonthName = tokens[0];
                currentYear = int.Parse(tokens[1]);
                currentMonthNum = GetMonthNumber(currentMonthName);
            }

            while (currentYear > setYear || currentMonthNum > setMonthNum)
            {
                var previous = _driver.FindElement(By.CssSelector(PreviousMonth)).FindElement(By.TagName("button"));
                previous.Click();
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                calendar = _driver.FindElements(By.TagName("table"))[1];
                monthControl = calendar.FindElement(By.XPath("preceding-sibling::div")).FindElement(By.TagName("div"));
                tokens = monthControl.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                currentMonthName = tokens[0];
                currentYear = int.Parse(tokens[1]);
                currentMonthNum = GetMonthNumber(currentMonthName);
            }            

            var days = calendar.FindElements(By.TagName("td"));
            foreach (var day in days)
            {
                if (int.TryParse(day.Text, out int num) && num == setDate.Day)
                {
                    day.Click();
                    break;
                }
            }
        }

        private static int GetMonthNumber(string monthname)
        {
            int monthNumber = 0;
            monthNumber = DateTime.ParseExact(monthname, "MMMM", CultureInfo.CurrentCulture).Month;
            return monthNumber;
        }

        ~AirbnbSearch()
        {
            if(_driver!=null)
            {
                _driver.Close();
                _driver = null;
            }
        }

    }
}
