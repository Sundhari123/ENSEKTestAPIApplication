using ENSEKTestAPIApplication.Models.ResponseModel;
using ENSEKTestAPIApplication.StepImplementation;
using NUnit.Framework;

namespace ENSEKTestAPIApplication.StepDefinitions
{
    [Binding]
    public class TestStepDefinitions
    {
        private TestSteps TestSteps;

        private ScenarioContext _scenarioContext;

        public TestStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this.TestSteps = new TestSteps();
        }

        [Then(@"the response should be (.*) with message ""([^""]*)""")]
        public void ThenTheResponseShouldBeWithMessage(int statusCode, string message)
        {
            CommonSteps.verifyAPIStatusCodeAndMessage(statusCode, message);
        }

        [When(@"the user call the Post ""([^""]*)"" to reset the data ""([^""]*)"" token")]
        public async Task WhenTheUserCallThePostToResetTheDataTokenAsync(string postResetDataAPI, string token)
        {
            await TestSteps.resetTestData(postResetDataAPI, token);
        }

        [When(@"the user call the Put ""([^""]*)"" to buy a (.*) of (.*)")]
        public async Task WhenTheUserCallThePutToBuyAOf(string putEnergyUnitsAPI, string energyId, int quantity)
        {
            await TestSteps.buyFuelQuantity(putEnergyUnitsAPI, energyId, quantity);
        }

        [When(@"the user call the Get ""([^""]*)"" to get specific fuel (.*) details")]
        public async Task WhenTheUserCallTheGetToGetSpecificFuelDetails(string getEnergyAPI, int energyId )
        {
            _scenarioContext["fuelTypeDetails"]=await TestSteps.getAllEnergyTypes(getEnergyAPI, energyId);
        }

        [Then(@"the response should be (.*) with success message for fuelType of (.*)")]
        public void ThenTheResponseShouldBeWithSuccessMessageForFuelTypeOf(int statusCode, int quantity)
        {
            FuelTypeDetails fuelTypeDetail = (FuelTypeDetails)_scenarioContext["fuelTypeDetails"];
            _scenarioContext["orderId"]= TestSteps.validatePurchaseOfFuel(statusCode,quantity,fuelTypeDetail);
        }

        [When(@"the user call the Get orders ""([^""]*)"" to get order count")]
        public async Task WhenTheUserCallTheGetOrdersToGetOrderCount(string getOrdersAPI)
        {
            _scenarioContext["beforeOrderCount"] = await TestSteps.getOrdersCount(getOrdersAPI);
        }

        [Then(@"the user call the Get orders ""([^""]*)"" to get order count after order placed")]
        public async Task ThenTheUserCallTheGetOrdersToGetOrderCountAfterOrderPlaced(string getOrdersAPI)
        {
            _scenarioContext["afterOrderCount"] = await TestSteps.getOrdersCount(getOrdersAPI);
        }

        [Then(@"the user validates the recent order details for quantity (.*)")]
        public void ThenTheUserValidatesTheRecentOrderDetailsForQuantity(int quantity)
        {
            Assert.AreEqual((int)_scenarioContext["beforeOrderCount"], (int)_scenarioContext["afterOrderCount"] - 1);
            TestSteps.validateRecentOrderDetails((string)_scenarioContext["orderId"], quantity);
        }

    }
}
