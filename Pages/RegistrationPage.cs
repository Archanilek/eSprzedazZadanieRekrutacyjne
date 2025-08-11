using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

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
    }
}
