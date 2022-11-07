using Microsoft.Xrm.Sdk;
using NUnit.Framework;
using OpportunityManagement.Common.Extensions;
using OpportunityManagement.Common.Metadata;
using OpportunityManagement.Common.Metadata.Entity;
using System;
using TechTalk.SpecFlow;

namespace OpportunityManagement.AcceptanceTests.Steps
{
    public partial class RatingSteps
    {
        [Then(@"an error with message ""(.*)"" is shown")]
        public void ThenAnErrorWithGivenMessageIsShown(string expectedMessage)
        {
            var actualMessage = (ScenarioContext.Current[ParameterNames.TestException] as Exception)?.Message;
            Assert.AreEqual(actualMessage, expectedMessage);
        }

        [Then(@"Opportunity Rating is (.*)")]
        public void ThenOpportunityRatingIs(string ratingName)
        {
            var opportunity = ScenarioContext.Current[ParameterNames.Entity] as Entity;

            var expectedRatingValue = ratingName.ResolveEnumValue<Opportunity.Rating>();
            var actualRatingValue = _fixture.OpportunityRepository.GetRating(opportunity.Id)?.Value;

            Assert.AreEqual(expectedRatingValue, actualRatingValue);
        }

        [Then(@"record is not saved")]
        public void ThenRecordIsNotSaved()
        {
            Assert.IsTrue(ScenarioContext.Current.ContainsKey(ParameterNames.TestException));
        }

        [Then(@"record is saved successfuly")]
        public void ThenRecordIsSavedSuccessfuly()
        {
            Assert.IsFalse(ScenarioContext.Current.ContainsKey(ParameterNames.TestException));
        }
    }
}