using Microsoft.Xrm.Sdk;
using NUnit.Framework;
using OpportunityManagement.Common.Metadata.Entity;
using OpportunityManagement.Common.Metadata.Exceptions;
using OpportunityManagement.Common.Utilities;
using OpportunityManagement.IntegrationTests.Fixtures;
using OpportunityManagement.IntegrationTests.TestData;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;

namespace OpportunityManagement.IntegrationTests.Tests
{
    [TestFixture, Category("Integration")]
    public class RatingTests
    {
        private RatingFixture _fixture;
        private List<EntityReference> _testData;

        [OneTimeTearDown]
        public void CleanUpTestData()
        {
            var performCleanUp = ConfigurationManager.AppSettings["CleanUpTestData"];
            if (performCleanUp != bool.TrueString) return;

            _testData.Reverse(); // delete the Test Data in backwards order to avoid cascading deletes

            foreach (var record in _testData)
            {
                _fixture.RunningUserService.Delete(record.LogicalName, record.Id);
            }
        }

        [Test]
        public void DetermineRating_NoParentAccountReference_Throws()
        {
            // Arrange
            var opportunity = new EntityBuilder().Create(Opportunity.EntityLogicalName)
                                                 .WithAttribute(Opportunity.ParentAccountReference, null)
                                                 .Build();

            //  Act
            var ex = Assert.Throws<FaultException<OrganizationServiceFault>>(() => _fixture.OpportunityRepository.Create(opportunity));

            // Assert
            Assert.AreEqual(ExceptionMessages.X300003.Message, ex.Message);
        }

        [Theory]
        [TestCaseSource(typeof(RatingTestData), nameof(RatingTestData.Cases))]
        public OptionSetValue DetermineRating_OpportunityHasParentAccount_WithGivenNumberOfEmployees(int? numberOfEmployees)
        {
            // Arrange
            var account = new EntityBuilder().Create(Account.EntityLogicalName)
                                             .WithAttribute(Account.NumberOfEmployees, numberOfEmployees)
                                             .Build();

            var parentAccountReference = _fixture.AccountRepository.Create(account); _testData.Add(parentAccountReference);

            var opportunity = new EntityBuilder().Create(Opportunity.EntityLogicalName)
                                                 .WithAttribute(Opportunity.ParentAccountReference, parentAccountReference)
                                                 .Build();

            // Act
            var opportunityReference = _fixture.OpportunityRepository.Create(opportunity); _testData.Add(opportunityReference);

            // Assert
            return _fixture.OpportunityRepository.GetRating(opportunityReference.Id);
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            _fixture = new RatingFixture();
            _testData = new List<EntityReference>();
        }
    }
}