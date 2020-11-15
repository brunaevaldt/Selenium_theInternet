using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace helper
{
    public class Helper
    {

        public static void Main() { }

        By tableBodyLocator = By.TagName("tbody");
        By tableRowLocator = By.TagName("tr");
        By tableCellLocator = By.TagName("td");

        TimeSpan seconds = new TimeSpan(0, 0, 5);


        public IWebElement FindTableRow(IWebDriver driver, string tableId, By searchCriterion, string text)
        {
            IWebElement row = null;

            IWebElement table = driver.FindElement(By.Id(tableId));
            IWebElement tableBody = table.FindElement(tableBodyLocator);

            IReadOnlyCollection<IWebElement> bodyRows = tableBody
                .FindElements(tableRowLocator);

            foreach (var tableRow in bodyRows)
            {
                var td = tableRow.FindElement(searchCriterion);
                if (td.Text.Contains(text))
                {
                    row = tableRow;
                    break;
                }
            }

            return row;
        }



        public IWebElement FindTableRow(IWebDriver driver, string tableId, string text)
        {
            IWebElement row = null;

            IWebElement table = driver.FindElement(By.Id(tableId));
            IWebElement tableBody = table.FindElement(tableBodyLocator);

            IReadOnlyCollection<IWebElement> bodyRows = tableBody
                .FindElements(tableRowLocator);

            foreach (var tableRow in bodyRows)
            {
                var tds = tableRow.FindElements(tableCellLocator);
                foreach (var td in tds)
                {
                    if (td.Text.Contains(text))
                    {
                        row = tableRow;
                        break;
                    }
                }
                if (row != null) { break; }
            }

            return row;
        }



        public IWebElement WaitForElementToBeClickable(IWebDriver driver, IWebElement element)
        {
            return new WebDriverWait(driver, seconds).Until(
               ExpectedConditions.ElementToBeClickable(element));
        }

        public void WaitForTextElementToBeEnabledAndSendKeys(IWebDriver driver, IWebElement textElement, string text)
        {
            new WebDriverWait(driver, seconds).Until(
              ExpectedConditions.ElementToBeClickable(textElement)).SendKeys
              (text);

        }

        public bool WaitForElementToHaveText(IWebDriver driver, IWebElement element, string text)
        {
             return new WebDriverWait(driver, seconds).Until(
                    ExpectedConditions.TextToBePresentInElement(element,
                    text));

        }


        public bool WaitForElementToHaveTextInValue(IWebDriver driver, IWebElement element, string text)
        {
            return new WebDriverWait(driver, seconds).Until(
                   ExpectedConditions.TextToBePresentInElementValue(element,
                   text));

        }


        public bool WaitForElementToBeRemovedOrInvisible(IWebDriver driver, By elementLocator)
        {
            return new WebDriverWait(driver, seconds).Until(
             ExpectedConditions.InvisibilityOfElementLocated(elementLocator));
        }

        public void PerformLoginUsingUI(IWebDriver driver, string userName, string password)
        {
            if (userName != string.Empty)
            {
                IWebElement userNameField = driver.FindElement(By.Name("username"));
                userNameField.SendKeys(userName);
            }

            IWebElement passwordField = driver.FindElement(By.Name("password"));
            if (password != string.Empty)
            {     
                passwordField.SendKeys(password);
            }
            passwordField.Submit();

        }

        public bool ElementHasClass(IWebDriver driver, IWebElement element, string className)
        {
            string elementClass = element.GetAttribute("class");
            return elementClass != null && elementClass.Contains(className);
        }

        public bool WaitForElementToBeRemoved(IWebDriver driver, IWebElement element)
        {
            return new WebDriverWait(driver, seconds).Until(
                  ExpectedConditions.StalenessOf(element));
        }

        public IWebElement WaitForElementToExist(IWebDriver driver, By elementLocator)
        {
            return new WebDriverWait(driver, seconds).Until(
                 ExpectedConditions.ElementExists(elementLocator));
        }

        public bool WaitForElementSelectionStateToBe(IWebDriver driver, IWebElement element, bool selected)
        {
            return new WebDriverWait(driver, seconds).Until(
                ExpectedConditions.ElementSelectionStateToBe(element, selected));
        }

        public bool WaitForElementToExistAndHaveText(IWebDriver driver,
            By elementLocator, string text)
        {
            return new WebDriverWait(driver, seconds).Until(
               ExpectedConditions.TextToBePresentInElementLocated(elementLocator, text));
        }


        public void ScrollToElement(IWebDriver driver, IWebElement element)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Perform();
        }
    }
}
