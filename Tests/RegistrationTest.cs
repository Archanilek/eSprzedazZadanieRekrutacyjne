using eSprzedazZadanieRekrutacyjne.Pages;
using eSprzedazZadanieRekrutacyjne.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
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

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement successMessage = _driver.FindElement(By.XPath("//div[normalize-space(text())='Dziękujemy, zostałeś automatycznie zalogowany na swoje konto.']"));
            Assert.IsNotNull(successMessage, "Rejestracja nie powiodła się.");

            //Zakmniecie okiekna z informacją o poprawnej rejestracji
            _driver.FindElement(By.XPath("/div[4]/div[2]/div/div/div")).Click();
            
            //wylogowanie


        }


        [TestCleanup]
        public void Teardown()
        {
            _driver.Quit();
        }
    }
}
