using ENSEKTestAPIApplication.Models.ResponseModel;
using ENSEKTestAPIApplication.Utils;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ENSEKTestAPIApplication.StepImplementation
{
    class CommonSteps : APIWrapper
    {
        private static string baseURL;
        public static void setBaseURL(string url)
        {
            baseURL = (string)apiBaseURLS[url + environment];
        }

        public static string getBaseURL()
        {
            return baseURL;
        }

        public static void verifyAPIStatusCodeAndMessage(int statusCodeExpected, string messageExpected)
        {
            var response = JsonConvert.DeserializeObject<SuccessMessageResponse>(Response.Content);
            if (response != null)
            {       
                Assert.AreEqual(statusCodeExpected, (int)Response.StatusCode);

                Assert.AreEqual(messageExpected, response.message);
            }
        }
    }
}
