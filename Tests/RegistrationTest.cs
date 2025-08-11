using eSprzedazZadanieRekrutacyjne.Pages;
using eSprzedazZadanieRekrutacyjne.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSprzedazZadanieRekrutacyjne.Tests
{
    [TestClass]
    public class RegistrationTest
    {
        private IWebDriver _driver;

        [TestInitialize]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            _driver = new ChromeDriver(options);
        }

        [TestMethod]
        public void SuccessfulRegistrationTest()
        {
            RegistrationPage registrationPage = new RegistrationPage(_driver);
            registrationPage.Navigate();

            string email = TestDataGenerator.GenerateUniqueEmail();
            string password = TestDataGenerator.GetDefaultPassword();

            registrationPage.FillForm(email, password);
            registrationPage.Submit();

            Thread.Sleep(2000); // Lepiej zastąpić WebDriverWait, ale dla przykładu OK

            IWebElement successMessage = _driver.FindElement(By.XPath("//*[contains(text(),'Dziękujemy, zostałeś automatycznie zalogowany')]"));
            Assert.IsNotNull(successMessage, "Rejestracja nie powiodła się.");
        }

        [TestCleanup]
        public void Teardown()
        {
            _driver.Quit();
        }
    }
}
