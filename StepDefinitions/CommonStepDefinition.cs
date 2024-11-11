using ENSEKTestAPIApplication.StepImplementation;

namespace ENSEKTestAPIApplication.StepDefinitions
{
    [Binding]
    public  class CommonStepDefinition
    {
        private ScenarioContext _scenarioContext;

        public CommonStepDefinition(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"User works in (.*)")]
        public void UserWorksIn(string URL)
        {
            CommonSteps.setBaseURL(URL);
        }
    }
}
