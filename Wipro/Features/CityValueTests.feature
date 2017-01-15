Feature: CityValueTests
# Ideally the the values displayed on the page should be compared against the values brought back via web service
# To achieve this the approprirate http request should be sent at the same time as the City value is entered.
# The values are then returned as a JSon object which can then be parsed to confirm the displayed values are
# displayed in the correct location.

Scenario: When I enter a valid City the appropriate data is displayed on the page
	Given I am on the "5 Weather Forecast" page
	When I enter the value "Edinburgh" into City text field on the "5 Weather Forecast" page
	Then I should see 5 summarised entries on the "5 Weather Forecast" page

Scenario: When I enter an invalid City the appropriate error message is displayed on the page
	Given I am on the "5 Weather Forecast" page
	When I enter the value "Palma" into City text field on the "5 Weather Forecast" page
	Then the Error Message "Error retrieving the forecast" is displayed on the "5 Weather Forecast" page

Scenario: When I perform a page refresh the values displayed on the screen do not change
	Given I am on the "5 Weather Forecast" page
	When I enter the value "Edinburgh" into City text field on the "5 Weather Forecast" page
	Then I should see 5 summarised entries on the "5 Weather Forecast" page
	When I perform a page refresh to the "5 Weather Forecast" page
	Then the weather values for "Edinburgh" remain on the "5 Weather Forecast" page 