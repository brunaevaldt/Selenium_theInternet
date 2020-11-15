using NUnit.Framework;
using OpenQA.Selenium;



namespace TheInternet
{
    [TestFixture]
    public class Tables : Base
    {
        By editLinkLocator = By.LinkText("edit");
        By emailCellClassLocator = By.ClassName("email");

        [SetUp]
        public void NavigateToTables()
        {
            NavigateTo("/tables");
        }

        [Test]
        public void CheckClickEditLinkTableWithoutClasses()
        {

            IWebElement row = Helper.FindTableRow(Driver, "table1", "fbach@yahoo.com");
            IWebElement link = row.FindElement(editLinkLocator);

            Helper.ScrollToElement(Driver,link);
            link.Click();

            Assert.IsTrue(Driver.Url.Contains("/tables#edit"));
        }

        [Test]
        public void CheckClickEditLinkTableWithClasses()
        {
            IWebElement row = Helper.FindTableRow(Driver, "table2", emailCellClassLocator, "jdoe@hotmail.com");
            IWebElement link = row.FindElement(editLinkLocator);

            Helper.ScrollToElement(Driver, link);
            link.Click();

            Assert.IsTrue(Driver.Url.Contains("/tables#edit"));

        }

    }
}
