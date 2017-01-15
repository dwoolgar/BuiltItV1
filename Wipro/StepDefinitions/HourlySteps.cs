using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using Wipro.Support.Contexts;
using OpenQA.Selenium.Support;

namespace Wipro.StepDefinitions
{
    [Binding]
    public sealed class HourlySteps
    {
        [Then(@"the Hourly forecast details for the selected day is displayed")]
        public void ThenTheHourlyForecastDetailsForTheSelectedDayIsDisplayed()
        {
            var _testElement = CustomContexts.TheDriver.FindElement(By.CssSelector(CustomContexts.MappingObject.GetProperty("weather_detail_rows")));
            Assert.IsTrue(_testElement.Displayed);
        }

        [Then(@"the Hourly forecast details for the selected day is closed")]
        public void ThenTheHourlyForecastDetailsForTheSelectedDayIsClosed()
        {
            var _testElement = CustomContexts.TheDriver.FindElement(By.CssSelector(CustomContexts.MappingObject.GetProperty("weather_detail_rows")));
            Assert.IsFalse(_testElement.Displayed);
        }

        [Then(@"the current hourly weather description is displayed on Row (.*)s summary")]
        public void ThenTheCurrentHourlyWeatherDescriptionIsDisplayedOnRowSSummary(int rowValue)
        {
            //ToDo:  for the selected row number locate the detail row for the current timespan and then confirm that the 
            // summary row is displaying the same image (eg cloud, sun) as the current timespan in the detail row
        }

        [Then(@"the current hourly wind speed is displayed on Row (.*)s summary")]
        public void ThenTheCurrentHourlyWindSpeedIsDisplayedOnRowSSummary(int rowValue)
        {
            //ToDo:  for the selected row number locate the detail row for the current time and then confirm that the 
            // summary row is displaying the same value as the current wind speed as the current 
            // timespan in the detail row
        }

        [Then(@"the current hourly wind direction is displayed on Row (.*)s summary")]
        public void ThenTheCurrentHourlyWindDirectionIsDisplayedOnRowSSummary(int rowValue)
        {
            //ToDo:  for the selected row number locate the detail row for the current time and then confirm that the 
            // summary row is displaying the same value as the current wind direction as the current 
            // timespan in the detail row
        }

        [Then(@"the total amount of daily rainfall is displayed on Row (.*)s summary")]
        public void ThenTheTotalAmountOfDailyRainfallIsDisplayedOnRowSSummary(int rowValue)
        {
            //ToDo:  for the selected row number locate the detail row and calculate the total expected rainfall.  The
            // calculated value should then be dislayed in the summary row for the total rainfall.  All calculated 
            // values to be rounded down
        }

        [Then(@"the Maximum temperature is displayed on Row (.*)s summary")]
        public void ThenTheMaximumTemperatureIsDisplayedOnRowSSummary(int rowValue)
        {
            //ToDo:  for the selected row number locate the detail row and locate the highest value in the maximum
            // column.  Confirm that this value should be confirmed as being displayed in the summary row for the 
            // selected day.  All calculated values to be rounded down
        }

        [Then(@"the Minimum temperature is displayed on Row (.*)s summary")]
        public void ThenTheMinimumTemperatureIsDisplayedOnRowSSummary(int rowValue)
        {
            //ToDo:  for the selected row number locate the detail row and locate the highest value in the minimum
            // column.  Confirm that this value should be confirmed as being displayed in the summary row for the 
            // selected day.  All calculated values to be rounded down
        }

    }
}
