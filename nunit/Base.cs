using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using helper;
namespace TheInternet

{

    public class Base
    {

        public IWebDriver Driver { get; set; }

        public string BaseUrl { get; set; }

        public Helper Helper { get; set; }

        [SetUp]
        public void start_Browser()
        {
            BaseUrl = "https://the-internet.herokuapp.com";
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.Url = BaseUrl;

            Helper = new Helper();
        }

        [TearDown]
        public void close_Browser()
        {
            Driver.Quit();
        }

        public void NavigateTo(string url)
        {
            Driver.Navigate().GoToUrl(string.Format("{0}{1}", BaseUrl, url));
        }
    }
}
