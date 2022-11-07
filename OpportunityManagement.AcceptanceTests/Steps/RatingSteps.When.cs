using Microsoft.Xrm.Sdk;
using OpportunityManagement.Common.Metadata;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace OpportunityManagement.AcceptanceTests.Steps
{
    public partial class RatingSteps
    {
        [When(@"I try to save the record")]
        public void WhenITryToSaveTheRecord()
        {
            try
            {
                var opportunity = ScenarioContext.Current[ParameterNames.Entity] as Entity;
                var testData = ScenarioContext.Current[ParameterNames.TestData] as List<EntityReference>;

                opportunity.Id = _fixture.OpportunityRepository.Create(opportunity).Id; testData.Add(opportunity.ToEntityReference());
            }
            catch (Exception ex)
            {
                ScenarioContext.Current[ParameterNames.TestException] = ex;
            }
        }
    }
}