Feature: 5DayForecast

Scenario: A 5 day Forecast is displayed on the page after entering a valid City
	Given I am on the "5 Weather Forecast" page
	When I enter the value "Edinburgh" into City text field on the "5 Weather Forecast" page
	Then I should see 5 summarised entries on the "5 Weather Forecast" page
	And the First rows Day and Date values are todays
	And the Second rows Day and Date values are in 1 days time
	And the Third rows Day and Date values are in 2 days time
	And the Fourth rows Day and Date values are in 3 days time
	And the Fifth rows Day and Date values are in 4 days time

Scenario: The Daily forecast should summarise the 3 hour data
	Given I am on the "5 Weather Forecast" page
	When I enter the value "Edinburgh" into City text field on the "5 Weather Forecast" page
	Then I should see 5 summarised entries on the "5 Weather Forecast" page
	And the current hourly weather description is displayed on Row 1s summary
	And the current hourly wind speed is displayed on Row 1s summary
	And the current hourly wind direction is displayed on Row 1s summary
	And the total amount of daily rainfall is displayed on Row 1s summary
	And the Maximum temperature is displayed on Row 1s summary
	And the Minimum temperature is displayed on Row 1s summary
