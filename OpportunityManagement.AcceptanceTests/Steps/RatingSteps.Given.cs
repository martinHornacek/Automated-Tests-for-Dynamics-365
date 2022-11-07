using Microsoft.Xrm.Sdk;
using OpportunityManagement.Common.Extensions;
using OpportunityManagement.Common.Metadata;
using OpportunityManagement.Common.Metadata.Entity;
using OpportunityManagement.Common.Utilities;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace OpportunityManagement.AcceptanceTests.Steps
{
    public partial class RatingSteps
    {
        [Given(@"I have not specified an Account")]
        public void GivenIHaveNotSpecifiedAnAccount()
        {
            var opportunity = ScenarioContext.Current[ParameterNames.Entity] as Entity;
            opportunity[Opportunity.ParentAccountReference] = null;
        }

        [Given(@"I have specified an Account with Number of Employees equal to (.*)")]
        public void GivenIHaveSpecifiedAnAccountWithNumberOfEmployeesEqualTo(string numberOfEmployees)
        {
            var opportunity = ScenarioContext.Current[ParameterNames.Entity] as Entity;
            var testData = ScenarioContext.Current[ParameterNames.TestData] as List<EntityReference>;

            var account = new EntityBuilder().Create(Account.EntityLogicalName)
                                             .WithAttribute(Account.NumberOfEmployees, numberOfEmployees.ToNullableInt())
                                             .Build();

            account.Id = _fixture.AccountRepository.Create(account).Id; testData.Add(account.ToEntityReference());
            opportunity[Opportunity.ParentAccountReference] = account.ToEntityReference();
        }

        [Given(@"I want to Create an Opportunity")]
        public void GivenIWantToCreateAnOpportunity()
        {
            var opportunity = ScenarioContext.Current[ParameterNames.Entity] as Entity;
            opportunity.LogicalName = Opportunity.EntityLogicalName;
        }
    }
}