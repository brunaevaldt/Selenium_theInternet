using NUnit.Framework;
using OpenQA.Selenium;
using helper;

namespace TheInternet
{
    [TestFixture]
    public class Login : Base
    {
        By messageLocator = By.Id("flash");
        By subheaderLocator = By.ClassName("subheader");

        [SetUp]
        public void navigateToLogin()
        {
            NavigateTo("/login");
        }

        [Test]
        public void CheckLoginWithCorrectCredentials()
        {
            Helper.PerformLoginUsingUI(Driver, "tomsmith", "SuperSecretPassword!");
            CheckLoginResultMessage("success", "You logged into a secure area!");

            Assert.IsTrue(Helper.WaitForElementToExistAndHaveText(Driver, subheaderLocator,
                "Welcome to the Secure Area. When you are done click logout below."));

        }

        [Test]
        public void CheckLoginWithWrongCredentials()
        {
            Helper.PerformLoginUsingUI(Driver, "wrongLogin", "wrongpassord");
            CheckLoginResultMessage("error", "Your username is invalid!");
        }

        [Test]
        public void CheckLoginWithUserNotFilled()
        {
            Helper.PerformLoginUsingUI(Driver, string.Empty, "SuperSecretPassword!");
            CheckLoginResultMessage("error", "Your username is invalid!");
        }

        [Test]
        public void CheckLoginWithPasswordNotFilled()
        {
            Helper.PerformLoginUsingUI(Driver, "tomsmith", string.Empty);
            CheckLoginResultMessage("error", "Your password is invalid!");
        }

        private void CheckLoginResultMessage(string expectedResult, string expectedMessage)
        {
            IWebElement message = Driver.FindElement(messageLocator);
            Assert.IsTrue(Helper.ElementHasClass(Driver, message, expectedResult));
            Assert.IsTrue(Helper.WaitForElementToHaveText(Driver, message,expectedMessage));

        }

    }
}
