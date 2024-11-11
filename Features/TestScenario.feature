Feature: ENSEK energy APIs

As a test user,
I want to able to hit the ENSEK energy APIs
So that I can test the different energy types purchases and order details

@regression @test
Scenario Outline: Buy a quantity of each fuel
	Given User works in baseURL
	When the user call the Post "postResetDataAPI" to reset the data "with" token
	And the user call the Get orders "getOrdersAPI" to get order count
	And the user call the Get "getEnergyAPI" to get specific fuel <energyId> details
	And the user call the Put "putEnergyUnitsAPI" to buy a <energyId> of <quantity>
	Then the response should be 200 with success message for fuelType of <quantity>
	And the user call the Get orders "getOrdersAPI" to get order count after order placed
	Then the user validates the recent order details for quantity <quantity>

Examples: 
| energyId | quantity | 
| 1        | 100      |
| 2        | 120      | 
| 3        | 135      | 
| 4        | 113      |

###TODO
#Individual API endpoints can be verified for different scenarios for valid /invalid 
#Delete, Update Orders scenario can be included in business scenarios

@regression
Scenario: Reset the test data with valid token
	Given User works in baseURL
	When the user call the Post "postResetDataAPI" to reset the data "with" token
	Then the response should be 200 with message "Success"

@regression
Scenario: Reset the test data without token
	Given User works in baseURL
	When the user call the Post "postResetDataAPI" to reset the data "without" token
	Then the response should be 401 with message "Unauthorized"