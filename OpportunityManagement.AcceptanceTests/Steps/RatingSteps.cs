using Microsoft.Xrm.Sdk;
using OpportunityManagement.AcceptanceTests.Fixtures;
using OpportunityManagement.Common.Metadata;
using System.Collections.Generic;
using System.Configuration;
using TechTalk.SpecFlow;

namespace OpportunityManagement.AcceptanceTests.Steps
{
    [Binding]
    public partial class RatingSteps
    {
        private static RatingFixture _fixture;

        [BeforeFeature]
        public static void FeatureSetUp()
        {
            _fixture = new RatingFixture();
        }

        [BeforeScenario]
        public static void ScenarioSetUp()
        {
            ScenarioContext.Current[ParameterNames.Entity] = new Entity();
            ScenarioContext.Current[ParameterNames.TestData] = new List<EntityReference>();
        }

        [AfterScenario]
        public static void ScenarioTearDown()
        {
            var performCleanUp = ConfigurationManager.AppSettings["CleanUpTestData"];
            if (performCleanUp != bool.TrueString) return;

            var testData = ScenarioContext.Current[ParameterNames.TestData] as List<EntityReference>;

            for (var i = testData.Count - 1; i >= 0; i--)
            {
                var record = testData[i]; _fixture.RunningUserService.Delete(record.LogicalName, record.Id);
            }
        }
    }
}