using Microsoft.Xrm.Sdk;
using Moq;
using NUnit.Framework;
using OpportunityManagement.Common.Metadata.Entity;
using OpportunityManagement.Common.Repositories;
using OpportunityManagement.Plugins.Logic;
using OpportunityManagement.UnitTests.TestData;
using System;

namespace OpportunityManagement.UnitTests.Tests
{
    [TestFixture, Category("Unit")]
    public class RatingLogicTests
    {
        [Test]
        public void DetermineRating_NoParentAccountReference_Throws()
        {
            // Arrange
            var accountRepoMock = new Mock<IAccountRepository>();
            var ratingLogic = new RatingLogic(accountRepoMock.Object);

            var opportunity = new Entity
            {
                LogicalName = Opportunity.EntityLogicalName,
                Id = Guid.NewGuid(),
                Attributes =
                {
                    [Opportunity.ParentAccountReference] = null
                }
            };

            // Act
            void DetermineRating() => ratingLogic.DetermineRating(opportunity);

            // Assert
            Assert.Throws<InvalidPluginExecutionException>(DetermineRating);
        }

        [Test]
        public void DetermineRating_NullReferencePassedAsInput_Throws()
        {
            // Arrange
            var accountRepoMock = new Mock<IAccountRepository>();
            var ratingLogic = new RatingLogic(accountRepoMock.Object);

            // Act
            void DetermineRating() => ratingLogic.DetermineRating(null);

            // Assert
            Assert.Throws<ArgumentNullException>(DetermineRating);
        }

        [Test]
        [TestCaseSource(typeof(RatingLogicTestData), nameof(RatingLogicTestData.Cases))]
        public OptionSetValue DetermineRating_OpportunityHasParentAccount_WithGivenNumberOfEmployees(int? numberOfEmployees)
        {
            // Arrange
            var parentAccountReference = new EntityReference(Account.EntityLogicalName, Guid.NewGuid());

            var opportunity = new Entity
            {
                LogicalName = Opportunity.EntityLogicalName,
                Id = Guid.NewGuid(),
                Attributes =
                {
                    [Opportunity.ParentAccountReference] = parentAccountReference
                }
            };

            var accountRepoMock = new Mock<IAccountRepository>();
            accountRepoMock.Setup(r => r.GetNumberOfEmployees(parentAccountReference.Id))
                           .Returns(numberOfEmployees);

            var ratingLogic = new RatingLogic(accountRepoMock.Object);

            // Act & Assert
            return ratingLogic.DetermineRating(opportunity);
        }
    }
}