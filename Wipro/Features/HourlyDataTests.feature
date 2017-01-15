Feature: HourlyDataTests

Scenario: I can display the hourly forecast by selecting the Day label
	Given I am on the "5 Weather Forecast" page
	When I enter the value "Edinburgh" into City text field on the "5 Weather Forecast" page
	Then I should see 5 summarised entries on the "5 Weather Forecast" page
	When I click on a Day label on the "5 Weather Forecast" page
	Then the Hourly forecast details for the selected day is displayed


Scenario: I can clsoe the hourly forecast by selecting the Day label
	Given I am on the "5 Weather Forecast" page
	When I enter the value "Edinburgh" into City text field on the "5 Weather Forecast" page
	Then I should see 5 summarised entries on the "5 Weather Forecast" page
	When I click on a Day label on the "5 Weather Forecast" page
	Then the Hourly forecast details for the selected day is displayed
	When I click on a Day label on the "5 Weather Forecast" page
	Then the Hourly forecast details for the selected day is closed