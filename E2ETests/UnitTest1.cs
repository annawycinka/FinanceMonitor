using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace E2ETests
{
    [TestFixture]
    public class Tests
    {
        private const string ApplicationUrl = "http://localhost:3000";

        [Test]
        public async Task AddingIncomeEnlargesBalance()
        {
            using var driver = new EdgeDriver();
            driver.Navigate().GoToUrl(ApplicationUrl);

            var currentBalanceElementValueBefore = GetBalance(driver);
            decimal newItemValue = 30;
            var expectedValue = currentBalanceElementValueBefore + newItemValue;

            SetCategoryForNewFinancialItem(driver, "Incomes");
            SetValueOfNewFinancialItem(driver, newItemValue);

            AddNewFinancialItemButton(driver).Click();

            Assert.True(CheckIfBalanceHasExpectedValue(driver, expectedValue));
        }

        [Test]
        public async Task AddingExpenseDecreasesBalance()
        {
            using var driver = new EdgeDriver();
            driver.Navigate().GoToUrl(ApplicationUrl);

            var currentBalanceElementValueBefore = GetBalance(driver);
            decimal newItemValue = 30;
            var expectedValue = currentBalanceElementValueBefore - newItemValue;

            SetCategoryForNewFinancialItem(driver, "Expenses");
            SetValueOfNewFinancialItem(driver, newItemValue);

            AddNewFinancialItemButton(driver).Click();

            var currentBalanceElementValueAfter = GetBalance(driver);

            Assert.True(CheckIfBalanceHasExpectedValue(driver, expectedValue));
        }

        private decimal GetBalance(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var element =  wait.Until(ExpectedConditions.ElementIsVisible(By.Id("currentBalance")));
            return decimal.Parse(element.Text);
        }

        private bool CheckIfBalanceHasExpectedValue(IWebDriver driver, decimal expectedValue)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("currentBalance")));

            return wait.Until(ExpectedConditions.TextToBePresentInElement(element, expectedValue.ToString()));
        }

        private IWebElement GetIncomesAndExpensesTable(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until(ExpectedConditions.ElementIsVisible(By.Id("incomesAndExpensesTable")));
        }

        private IWebElement GetOccurredAtToAddElement(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until(ExpectedConditions.ElementIsVisible(By.Id("occurredAtToAdd")));
        }

        private IWebElement GetFinancialItemValueToAddElement(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("financialItemValueToAdd")));
        }

        private void SetValueOfNewFinancialItem(IWebDriver driver, decimal value)
        {
            var element = GetFinancialItemValueToAddElement(driver);

            element.Click();

            while (element.GetAttribute("value") != string.Empty)
            {
                element.SendKeys(Keys.Backspace);
            }

            element.SendKeys(value.ToString());
        }

        private IWebElement SelectElementOfNewFinancialItem(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("operationTypeToAdd")));
        }

        private void SetCategoryForNewFinancialItem(IWebDriver driver, string category)
        {
            var select = SelectElementOfNewFinancialItem(driver);
            select.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var liElements = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.TagName("li")));

            var element = liElements.First(x => x.GetAttribute("data-value") == category);
            element.Click();
        }

        private IWebElement AddNewFinancialItemButton(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("addButton")));
        }
    }
}