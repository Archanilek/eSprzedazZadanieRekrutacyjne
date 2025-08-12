using eSprzedazZadanieRekrutacyjne.Models;
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
        [Priority(1)]
        public void CheckingAvailabilityOfFieldsDuringRegistration()
        {
            RegistrationPage registrationPage = new RegistrationPage(_driver);
            registrationPage.Navigate();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            Assert.IsTrue(wait.Until(driver => registrationPage.EmailInput.Displayed), "Pole email nie jest widoczne.");
            Assert.IsTrue(wait.Until(driver => registrationPage.PasswordInput.Displayed), "Pole hasło jest widoczne.");
            Assert.IsTrue(wait.Until(driver => registrationPage.ConfirmPasswordInput.Displayed), "Pole powtórz hasło jest widoczne");
            Assert.IsTrue(wait.Until(driver => registrationPage.AcceptTermsCheckbox.Displayed), "Checkbox akceptacji warunków jest widoczne");
            Assert.IsTrue(wait.Until(driver => registrationPage.RegisterButton.Displayed), "Przycisk rejestracji jest widoczny");
        }
        
        [TestMethod]
        [Priority(2)]
        public void SuccessfulRegistrationTest()
        {
            RegistrationPage registrationPage = new RegistrationPage(_driver);
            registrationPage.Navigate();

            string email = TestDataGenerator.GenerateUniqueEmail();
            string password = TestDataGenerator.GetDefaultPassword();

            registrationPage.FillForm(email, password);

            FileHelper.SaveRegistredUserToJsonFile(email, password);

            registrationPage.Submit();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            IWebElement successMessage = wait.Until(driver => driver.FindElement(By.XPath("//div[contains(text(), 'Dziękujemy, zostałeś automatycznie zalogowany')]")));
            Assert.IsTrue(successMessage.Text.Contains("Dziękujemy, zostałeś automatycznie zalogowany"), "Rejestracja zakończyła się sukcesem.");

            registrationPage.CloseConfirmationPopup();
            registrationPage.Logout();
        }

        [TestMethod]
        [Priority(3)]
        public void SuccessfulRegistrationFromJson()
        {
            RegistrationPage registrationPage = new RegistrationPage(_driver);
            
            foreach (RegistrationData user in FileHelper.LoadRegisteredUsersFromJsonFile())
            {
                registrationPage.Navigate();
                registrationPage.FillForm(user.Email, user.EncryptedPassword);
                registrationPage.Submit();

                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

                IWebElement successMessage = wait.Until(driver => driver.FindElement(By.XPath("//div[contains(text(), 'Dziękujemy, zostałeś automatycznie zalogowany')]")));
                Assert.IsTrue(successMessage.Text.Contains("Dziękujemy, zostałeś automatycznie zalogowany"), "Rejestracja zakończyła się sukcesem.");
                
                registrationPage.CloseConfirmationPopup();
                registrationPage.Logout();
            }    
        }


        [TestCleanup]
        public void Teardown()
        {
            _driver.Quit();
        }
    }
}
