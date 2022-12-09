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
        private const string IncomesCategoryName = "Incomes";
        private const string ExpensesCategoryName = "Expenses";

        [Test]
        public async Task AddingIncomeEnlargesBalance()
        {
            using var driver = new EdgeDriver();
            driver.Navigate().GoToUrl(ApplicationUrl);
            var currentBalanceElementValueBefore = GetBalance(driver);

            var newItemValue = GetFinancialItemValueToAddElementValue(driver);
            
            SetCategoryForItemToAdd(driver, IncomesCategoryName);

            await Task.Delay(1000);

            GetAddButton(driver).Click();

            await Task.Delay(1000);

            var currentBalanceElementValueAfter = GetBalance(driver);

            await Task.Delay(2000);

            Assert.AreEqual(currentBalanceElementValueBefore + newItemValue, currentBalanceElementValueAfter);

        }

        [Test]
        public async Task AddingIncomeDecreasesBalance()
        {
            using var driver = new EdgeDriver();
            driver.Navigate().GoToUrl(ApplicationUrl);
            var currentBalanceElementValueBefore = GetBalance(driver);

            var newItemValue = GetFinancialItemValueToAddElementValue(driver);

            SetCategoryForItemToAdd(driver, ExpensesCategoryName);

            await Task.Delay(1000);

            GetAddButton(driver).Click();

            await Task.Delay(1000);

            var currentBalanceElementValueAfter = GetBalance(driver);

            await Task.Delay(2000);

            Assert.AreEqual(currentBalanceElementValueBefore - newItemValue, currentBalanceElementValueAfter);
        }

        private decimal GetBalance(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            var element =  wait.Until(ExpectedConditions.ElementIsVisible(By.Id("currentBalance")));
            return decimal.Parse(element.Text);
        }

        private IWebElement GetIncomesAndExpensesTable(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            return wait.Until(ExpectedConditions.ElementIsVisible(By.Id("incomesAndExpensesTable")));
        }

        private IWebElement GetOccurredAtToAddElement(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            return wait.Until(ExpectedConditions.ElementIsVisible(By.Id("occurredAtToAdd")));
        }

        private IWebElement GetFinancialItemValueToAddElement(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            return wait.Until(ExpectedConditions.ElementIsVisible(By.Id("financialItemValueToAdd")));
        }

        private decimal GetFinancialItemValueToAddElementValue(IWebDriver driver)
        {
            var element = GetFinancialItemValueToAddElement(driver);
            return decimal.Parse(element.GetAttribute("value"));
        }

        private IWebElement GetOperationTypeToAddElement(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            return wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("operationTypeToAdd")));
        }

        private void SetCategoryForItemToAdd(IWebDriver driver, string category)
        {
            var select = GetOperationTypeToAddElement(driver);
            select.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            var liElements = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.TagName("li")));

            var element = liElements.First(x => x.GetAttribute("data-value") == category);
            element.Click();
        }

        private IWebElement GetAddButton(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            return wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("addButton")));
        }
    }
}