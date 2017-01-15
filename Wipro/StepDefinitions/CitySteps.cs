using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using Wipro.Support.Contexts;
using OpenQA.Selenium.Support;

namespace Wipro.StepDefinitions
{
    [Binding]
    public sealed class CitySteps
    {
        [Given(@"I am on the ""(.*)"" page")]
        public void GivenIAmOnThePage(string testValue)
        {
            Assert.AreEqual(CustomContexts.TheDriver.Title, testValue);
        }

        [When(@"I enter the value ""(.*)"" into City text field on the ""(.*)"" page")]
        public void WhenIEnterTheValueIntoCityTextFieldOnThePage(string testValue, string pageValue)
        {
            var _testElement = CustomContexts.TheDriver.FindElement(By.CssSelector(CustomContexts.MappingObject.GetProperty("title_text")));
            _testElement.Click();
            _testElement.Clear();
            _testElement.SendKeys(testValue);
            _testElement.SendKeys(Keys.Tab);
            Assert.AreEqual(_testElement.GetAttribute("value"), testValue);
        }

        [Then(@"the Error Message ""(.*)"" is displayed on the ""(.*)"" page")]
        public void ThenTheErrorMessageIsDisplayedOnThePage(string testValue, string pageValue)
        {
            var _testElement = CustomContexts.TheDriver.FindElement(By.CssSelector(CustomContexts.MappingObject.GetProperty("error_message_text")));
            Assert.AreEqual(_testElement.GetAttribute("value"), testValue);
        }

        [When(@"I perform a page refresh to the ""(.*)"" page")]
        public void WhenIPerformAPageRefreshToThePage(string pageValue)
        {
            CustomContexts.TheDriver.Navigate().Refresh();
        }

        [Then(@"the weather values for ""(.*)"" remain on the ""(.*)"" page")]
        public void ThenTheWeatherValuesForRemainOnThePage(string testValue, string pageValue)
        {
            var _testElement = CustomContexts.TheDriver.FindElement(By.CssSelector(CustomContexts.MappingObject.GetProperty("title_text")));
            Assert.AreEqual(_testElement.GetAttribute("value"), testValue);
        }


        [Then(@"the weather values for ""(.*)"" are populated on the ""(.*)"" page")]
        public void ThenTheWeatherValuesForArePopulatedOnThePage(string testValue, string pageValue)
        {
            //get mapping element basic eg "day-" add the 1 2 or 3 
        }
    }
}
