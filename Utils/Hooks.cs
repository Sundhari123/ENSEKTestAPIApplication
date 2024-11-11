namespace ENSEKTestAPIApplication.Utils
{
    [Binding]
    public class Hooks : APIWrapper
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            SetUp();
            var sample = System.Environment.GetEnvironmentVariable("TestUser");

            environment = sample;
            if (environment == null)
            {
                environment = (string)apiBaseURLS["Environment"];
            }
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            await APIWrapper.getAccessToken();
        }

        private static void SetUp()
        {
            APIWrapper.retrieveCommonDataFile();
            APIWrapper.retrieveAPIEndpoints();
            APIWrapper.retrieveBaseURLs();
        }
    }
}
