using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using Wipro.Support.Contexts;
using OpenQA.Selenium.Support;

namespace Wipro.StepDefinitions
{
    [Binding]
    public sealed class TableSteps
    {
        [Then(@"I should see (.*) summarised entries on the ""(.*)"" page")]
        public void ThenIShouldSeeSummarisedEntriesOnThePage(int testValue, string pageValue)
        {
            var _testElement = CustomContexts.TheDriver.FindElements(By.CssSelector(CustomContexts.MappingObject.GetProperty("weather_summary_rows")));
            if (_testElement != null)
                Assert.IsTrue(_testElement.Count == testValue, "Number of summary rows found = {0}", _testElement.Count);
            else
                Assert.Fail("Failed to find any weather forecast summary row elements");
        }


        [Then(@"the First rows Day and Date values are todays")]
        public void ThenTheFirstRowsDayAndDateValuesAreTodays()
        {
            // Get todays day name
            //Console.WriteLine("Two letters: {0:ddd}", time);
            try
            {
                var dayname = string.Format("{0:ddd}", DateTime.Today);
                var daydate = DateTime.Today.Day;

                var _testElement = CustomContexts.TheDriver.FindElement(By.CssSelector(CustomContexts.MappingObject.GetProperty("r1_day_text")));
                if (_testElement != null)
                    Assert.IsTrue(dayname.ToLower().Equals(_testElement.Text.Trim().ToLower()), "Todays expected day is {0} but was {1}", dayname, _testElement.Text);
                else
                    Assert.Fail("Failed to find the first day element");

                _testElement = CustomContexts.TheDriver.FindElement(By.CssSelector(CustomContexts.MappingObject.GetProperty("r1_date_text")));
                if (_testElement != null)
                {
                    int day;
                    if (int.TryParse(_testElement.Text, out day))
                        Assert.IsTrue(daydate == day, "Todays expected day date is {0} but was {1}", daydate, _testElement.Text);
                }
                else
                    Assert.Fail("Failed to find the first day element");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        [Then(@"the Second rows Day and Date values are in (.*) days time")]
        public void ThenTheSecondRowsDayAndDateValuesAreInDaysTime(int expectedDays)
        {
            // Get todays day name
            //Console.WriteLine("Two letters: {0:ddd}", time);
            var expectedDate = DateTime.Today.AddDays(expectedDays);
            var dayname = string.Format("{0:ddd}", expectedDate);
            var daydate = expectedDate.Day;

            var _testElement = CustomContexts.TheDriver.FindElement(By.CssSelector(CustomContexts.MappingObject.GetProperty("r2_day_text")));
            if (_testElement != null)
                Assert.IsTrue(dayname.ToLower().Equals(_testElement.Text.Trim().ToLower()), "The Expected day is {0} but was {1}", dayname, _testElement.Text);
            else
                Assert.Fail("Failed to find the day element");

            _testElement = CustomContexts.TheDriver.FindElement(By.CssSelector(CustomContexts.MappingObject.GetProperty("r2_date_text")));
            if (_testElement != null)
            {
                int day;
                if (int.TryParse(_testElement.Text, out day))
                    Assert.IsTrue(daydate == day, "Todays expected day date is {0} but was {1}", daydate, _testElement.Text);
            }
            else
                Assert.Fail("Failed to find the {0} day element", expectedDate);
        }

        [Then(@"the Third rows Day and Date values are in (.*) days time")]
        public void ThenTheThirdRowsDayAndDateValuesAreInDaysTime(int expectedDays)
        {
            // Get todays day name
            //Console.WriteLine("Two letters: {0:ddd}", time);
            var expectedDate = DateTime.Today.AddDays(expectedDays);
            var dayname = string.Format("{0:ddd}", expectedDate);
            var daydate = expectedDate.Day;

            var _testElement = CustomContexts.TheDriver.FindElement(By.CssSelector(CustomContexts.MappingObject.GetProperty("r3_day_text")));
            if (_testElement != null)
                Assert.IsTrue(dayname.ToLower().Equals(_testElement.Text.Trim().ToLower()), "The Expected day is {0} but was {1}", dayname, _testElement.Text);
            else
                Assert.Fail("Failed to find the day element");

            _testElement = CustomContexts.TheDriver.FindElement(By.CssSelector(CustomContexts.MappingObject.GetProperty("r3_date_text")));
            if (_testElement != null)
            {
                int day;
                if (int.TryParse(_testElement.Text, out day))
                    Assert.IsTrue(daydate == day, "Todays expected day date is {0} but was {1}", daydate, _testElement.Text);
            }
            else
                Assert.Fail("Failed to find the {0} day element", expectedDate);
        }

        [Then(@"the Fourth rows Day and Date values are in (.*) days time")]
        public void ThenTheFourthRowsDayAndDateValuesAreInDaysTime(int expectedDays)
        {
            // Get todays day name
            //Console.WriteLine("Two letters: {0:ddd}", time);
            var expectedDate = DateTime.Today.AddDays(expectedDays);
            var dayname = string.Format("{0:ddd}", expectedDate);
            var daydate = expectedDate.Day;

            var _testElement = CustomContexts.TheDriver.FindElement(By.CssSelector(CustomContexts.MappingObject.GetProperty("r4_day_text")));
            if (_testElement != null)
                Assert.IsTrue(dayname.ToLower().Equals(_testElement.Text.Trim().ToLower()), "The Expected day is {0} but was {1}", dayname, _testElement.Text);
            else
                Assert.Fail("Failed to find the day element");

            _testElement = CustomContexts.TheDriver.FindElement(By.CssSelector(CustomContexts.MappingObject.GetProperty("r4_date_text")));
            if (_testElement != null)
            {
                int day;
                if (int.TryParse(_testElement.Text, out day))
                    Assert.IsTrue(daydate == day, "Todays expected day date is {0} but was {1}", daydate, _testElement.Text);
            }
            else
                Assert.Fail("Failed to find the {0} day element", expectedDate);
        }

        [Then(@"the Fifth rows Day and Date values are in (.*) days time")]
        public void ThenTheFifthRowsDayAndDateValuesAreInDaysTime(int expectedDays)
        {
            // Get todays day name
            //Console.WriteLine("Two letters: {0:ddd}", time);
            var expectedDate = DateTime.Today.AddDays(expectedDays);
            var dayname = string.Format("{0:ddd}", expectedDate);
            var daydate = expectedDate.Day;

            var _testElement = CustomContexts.TheDriver.FindElement(By.CssSelector(CustomContexts.MappingObject.GetProperty("r5_day_text")));
            if (_testElement != null)
                Assert.IsTrue(dayname.ToLower().Equals(_testElement.Text.Trim().ToLower()), "The Expected day is {0} but was {1}", dayname, _testElement.Text);
            else
                Assert.Fail("Failed to find the day element");

            _testElement = CustomContexts.TheDriver.FindElement(By.CssSelector(CustomContexts.MappingObject.GetProperty("r5_date_text")));
            if (_testElement != null)
            {
                int day;
                if (int.TryParse(_testElement.Text, out day))
                    Assert.IsTrue(daydate == day, "Todays expected day date is {0} but was {1}", daydate, _testElement.Text);
            }
            else
                Assert.Fail("Failed to find the {0} day element", expectedDate);
        }

        [When(@"I click on a Day label on the ""(.*)"" page")]
        public void WhenIClickOnADayLabelOnThePage(string pageValue)
        {
            var _testElement = CustomContexts.TheDriver.FindElement(By.CssSelector(CustomContexts.MappingObject.GetProperty("r1_day_text")));
            _testElement.Click();
        }

    }
}
