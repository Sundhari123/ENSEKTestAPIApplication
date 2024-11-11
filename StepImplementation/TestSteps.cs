using ENSEKTestAPIApplication.CommonCalls;
using ENSEKTestAPIApplication.Models.ResponseModel;
using ENSEKTestAPIApplication.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace ENSEKTestAPIApplication.StepImplementation
{
    class TestSteps :APIWrapper
    {
        public async Task resetTestData(string postResetDataAPI, string token)
        {
            accessToken = token == "with"?accessToken:token;

            var apiURL = CommonSteps.getBaseURL() + apiEndPoints[postResetDataAPI].ToString();

            Response = await PostCalls.postAPICall(apiURL, accessToken);
        }

        public async Task buyFuelQuantity(string putEnergyUnitsAPI, string fuelType, int quantity)
        {
            var endpoint = apiEndPoints[putEnergyUnitsAPI].ToString().Replace("<energyId>", fuelType).Replace("<quantity>", quantity.ToString());

            var apiURL = CommonSteps.getBaseURL() + endpoint;

            Response = await PutCalls.putAPICall(apiURL, accessToken);
        }

        public string validatePurchaseOfFuel(int statusCode, int quantity, FuelTypeDetails fuelTypeDetails)
        {
            Assert.AreEqual(statusCode, (int)Response.StatusCode);

            var response = JsonConvert.DeserializeObject<SuccessMessageResponse>(Response.Content);

            Assert.That(response.message.Contains("You have purchased " + quantity + " " + fuelTypeDetails.unit_type + " at a cost of " + (fuelTypeDetails.price_per_unit * quantity) + " there are " + (fuelTypeDetails.quantity_of_units - quantity) + " units remaining"));

            Regex regex = new("[0-9a-f]{8}-[0-9a-f]{4}-[0-5][0-9a-f]{3}-[089ab][0-9a-f]{3}-[0-9a-f]{12}");

            return regex.Match(response.message).Value;
        }

        public async Task<FuelTypeDetails> getAllEnergyTypes(string getEnergyAPI, int energyId)
        {
            var apiURL = CommonSteps.getBaseURL() + apiEndPoints[getEnergyAPI].ToString();

            Response = await GetCalls.getAPICall(apiURL, accessToken);

            var response = JsonConvert.DeserializeObject<GetEnergyResponse>(Response.Content);

            List<FuelTypeDetails> fuelTypeDetails = new List<FuelTypeDetails>() { response.oil, response.nuclear, response.gas, response.electric};

            return fuelTypeDetails.FirstOrDefault(a => a.energy_id.Equals(energyId));
        }

        public async Task<int> getOrdersCount(string getOrdersAPI)
        {
            var apiURL = CommonSteps.getBaseURL() + apiEndPoints[getOrdersAPI].ToString();

            Response = await GetCalls.getAPICall(apiURL, accessToken);

            var response = JArray.Parse(Response.Content);

            return response.Count;
        }

        public void validateRecentOrderDetails(string orderId, int quantity)
        {
            var response = JsonConvert.DeserializeObject<List<GetOrdersResponse>>(Response.Content);

            var orderDetail = response.Where(a => a.id == orderId).FirstOrDefault();

            Assert.AreEqual(orderDetail?.quantity, quantity, "Order purchased quantity is not equal to quantity of order detail");
        }
    }
}
