using OpenQA.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSprzedazZadanieRekrutacyjne.Pages
{
    internal class RegistrationPage
    {
        private readonly IWebDriver _driver;

        public RegistrationPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement EmailInput => _driver.FindElement(By.Name("email"));
        private IWebElement PasswordInput => _driver.FindElement(By.Name("password"));
        private IWebElement ConfirmPasswordInput => _driver.FindElement(By.Name("password_confirm"));
        private IWebElement AcceptTermsCheckbox => _driver.FindElement(By.XPath("//div[@id='maincontent']/div/div/form/div/div/label/div[2]"));
        private IWebElement RegisterButton => _driver.FindElement(By.CssSelector("button[type='submit']"));
        private IWebElement CloseButtonInConfirmationRegistrationPopUp => _driver.FindElement(By.CssSelector(".l-popup__message-close.at-message-close.js-close-popup"));
        private IWebElement AccountButton => _driver.FindElement(By.Id("header-account"));
        private IWebElement LogOutButton => _driver.FindElement(By.XPath("//a[contains(@href, 'wylogowanie')]"));


        public void Navigate()
        {
            _driver.Navigate().GoToUrl("https://demo.sellingo.pl/rejestracja");
        }

        public void FillForm(string email, string password)
        {
            EmailInput.SendKeys(email);
            PasswordInput.SendKeys(password);
            ConfirmPasswordInput.SendKeys(password);
            AcceptTermsCheckbox.Click();
        }
        public void Submit()
        {
            RegisterButton.Click();
        }
        public void CloseConfirmationPopup()
        {
            CloseButtonInConfirmationRegistrationPopUp.Click();
        }
        public void Logout()
        {
            AccountButton.Click();
            WebDriverWait wait = new WebDriverWait(_driver,TimeSpan.FromSeconds(5));
            wait.Until(d => LogOutButton.Displayed && LogOutButton.Enabled);
            LogOutButton.Click();
        }
    }
}
