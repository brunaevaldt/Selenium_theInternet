using NUnit.Framework;
using OpenQA.Selenium;

namespace TheInternet
{
    [TestFixture]
    public class DynamicControls : Base
    {
        By formCheckBoxLocator = By.Id("checkbox-example");
        By buttonLocator = By.TagName("button");
        By checkboxLocator = By.Id("checkbox");
        By messageLocator = By.Id("message");
        By formInputTextLocator = By.Id("input-example");
        By inputLocator = By.TagName("input");


        [SetUp]
        public void NavigateToLogin()
        {
            NavigateTo("/dynamic_controls");
        }

        [Test]
        public void CheckRemoveAndAddCheckBox()
        {
            string message = string.Empty;

            IWebElement removeAddButton = Driver.FindElement(formCheckBoxLocator).FindElement(buttonLocator);

            IWebElement checkbox = Driver.FindElement(checkboxLocator);

            removeAddButton.Click();
            AssertCheckBoxWasRemoved();

            removeAddButton.Click();
            AssertCheckBoxWasAdded();

        }


        private void AssertCheckBoxWasAdded()
        {
            IWebElement checkbox = Helper.WaitForElementToExist(Driver, checkboxLocator);
            checkbox.Click();


            Assert.IsTrue(Helper.WaitForElementSelectionStateToBe(Driver, checkbox, true),
                "Checkbox was not clicked");

            AssertMessageIsAsExpected("It's back!", Driver.FindElement(messageLocator).Text);
        }

        private void AssertCheckBoxWasRemoved()
        {

            Assert.IsTrue(Helper.WaitForElementToBeRemovedOrInvisible(Driver, checkboxLocator),
                "Checkbox wasn't removed");

            AssertMessageIsAsExpected("It's gone!", Driver.FindElement(messageLocator).Text);
        }



        [Test]
        public void CheckEnableDisableInputText()
        {

            IWebElement enableDisableButton = Driver.FindElement(formInputTextLocator).FindElement(buttonLocator);

            enableDisableButton.Click();
            AssertInputTextIsEnabledAndHasText();

            enableDisableButton.Click();
            AssertInputTextIsDisabled();

        }

        private void AssertInputTextIsDisabled()
        {

            IWebElement inputText = Driver.FindElement(formInputTextLocator).FindElement(inputLocator);

            Assert.IsTrue(Helper.WaitForElementToBeRemoved(Driver, inputText), "InputText is not disabled");


            AssertMessageIsAsExpected("It's disabled!",
                Driver.FindElement(formInputTextLocator).FindElement(messageLocator).Text);
        }

        private void AssertInputTextIsEnabledAndHasText()
        {

            IWebElement inputText = Driver.FindElement(formInputTextLocator).FindElement(inputLocator);
            string textToType = "Element is interactable";

            Helper.WaitForTextElementToBeEnabledAndSendKeys(Driver, inputText, textToType);

            Assert.IsTrue(Helper.WaitForElementToHaveTextInValue(Driver, inputText, textToType),
                "InputText didn't have expected text");

            AssertMessageIsAsExpected("It's enabled!",
                Driver.FindElement(formInputTextLocator).FindElement(messageLocator).Text);

        }


        private void AssertMessageIsAsExpected(string expectedMessage, string actualMessage)
        {
            string errorMessage = "Message was expected to be {0}, but it was actually {1}";

            Assert.AreEqual(expectedMessage, actualMessage, string.Format(errorMessage,
                expectedMessage, actualMessage));
        }
    }
}

