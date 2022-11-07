using OpportunityManagement.SchemaTests.Logic;
using OpportunityManagement.SchemaTests.Models;
using OpportunityManagement.SchemaTests.Rules.Attribute;
using Xunit;

namespace OpportunityManagement.SchemaTests.Tests
{
    public class AttributeMetadataTests
    {
        private AttributeMetadataValidator _validator;

        public AttributeMetadataTests()
        {
            _validator = new AttributeMetadataValidator();
        }

        [Theory]
        [MemberData(nameof(SolutionAttributeMetadata.All), MemberType = typeof(SolutionAttributeMetadata))]
        public void AttributeLogicalNameShouldNotContainMoreThanOneUnderscore(AttributeMetadata attributeMetadata)
        {
            // Arrange
            _validator.Rule = new LogicalNameShouldNotContainMoreThanOneUnderscore();

            // Act
            var result = _validator.Validate(attributeMetadata);

            // Assert
            Assert.NotNull(result); Assert.True(result.IsValid, result.ErrorMessage);
        }

        [Theory]
        [MemberData(nameof(SolutionAttributeMetadata.All), MemberType = typeof(SolutionAttributeMetadata))]
        public void DescriptionShouldBeFormattedAsSentence(AttributeMetadata attributeMetadata)
        {
            // Arrange
            _validator.Rule = new DescriptionShouldBeFormattedAsSentence();

            // Act
            var result = _validator.Validate(attributeMetadata);

            // Assert
            Assert.NotNull(result); Assert.True(result.IsValid, result.ErrorMessage);
        }

        [Theory]
        [MemberData(nameof(SolutionAttributeMetadata.All), MemberType = typeof(SolutionAttributeMetadata))]
        public void DescriptionShouldNotBeEmpty(AttributeMetadata attributeMetadata)
        {
            // Arrange
            _validator.Rule = new DescriptionShouldNotBeEmpty();

            // Act
            var result = _validator.Validate(attributeMetadata);

            // Assert
            Assert.NotNull(result); Assert.True(result.IsValid, result.ErrorMessage);
        }

        [Theory]
        [MemberData(nameof(SolutionAttributeMetadata.All), MemberType = typeof(SolutionAttributeMetadata))]
        public void DisplayNameShouldBeProperlyCapitalized(AttributeMetadata attributeMetadata)
        {
            // Arrange
            _validator.Rule = new DisplayNameShouldBeProperlyCapitalized();

            // Act
            var result = _validator.Validate(attributeMetadata);

            // Assert
            Assert.NotNull(result); Assert.True(result.IsValid, result.ErrorMessage);
        }

        [Theory]
        [MemberData(nameof(SolutionAttributeMetadata.All), MemberType = typeof(SolutionAttributeMetadata))]
        public void DisplayNameShouldHaveProperSpacing(AttributeMetadata attributeMetadata)
        {
            // Arrange
            _validator.Rule = new DisplayNameShouldHaveProperSpacing();

            // Act
            var result = _validator.Validate(attributeMetadata);

            // Assert
            Assert.NotNull(result); Assert.True(result.IsValid, result.ErrorMessage);
        }

        [Theory]
        [MemberData(nameof(SolutionAttributeMetadata.All), MemberType = typeof(SolutionAttributeMetadata))]
        public void DisplayNameShouldNotContainInvalidCharacters(AttributeMetadata attributeMetadata)
        {
            // Arrange
            _validator.Rule = new DisplayNameShouldNotContainInvalidCharacters();

            // Act
            var result = _validator.Validate(attributeMetadata);

            // Assert
            Assert.NotNull(result); Assert.True(result.IsValid, result.ErrorMessage);
        }

        [Theory]
        [MemberData(nameof(SolutionAttributeMetadata.All), MemberType = typeof(SolutionAttributeMetadata))]
        public void LogicalNameShouldBeLowercase(AttributeMetadata attributeMetadata)
        {
            // Arrange
            _validator.Rule = new LogicalNameShouldBeLowercase();

            // Act
            var result = _validator.Validate(attributeMetadata);

            // Assert
            Assert.NotNull(result); Assert.True(result.IsValid, result.ErrorMessage);
        }

        [Theory]
        [MemberData(nameof(SolutionAttributeMetadata.Lookups), MemberType = typeof(SolutionAttributeMetadata))]
        public void LookupLogicalNameShouldEndWithId(AttributeMetadata attributeMetadata)
        {
            // Arrange
            _validator.Rule = new LogicalNameShouldEndWithId();

            // Act
            var result = _validator.Validate(attributeMetadata);

            // Assert
            Assert.NotNull(result); Assert.True(result.IsValid, result.ErrorMessage);
        }

        [Theory]
        [MemberData(nameof(SolutionAttributeMetadata.NonLookups), MemberType = typeof(SolutionAttributeMetadata))]
        public void NonLookupLogicalNameShouldNotEndWithId(AttributeMetadata attributeMetadata)
        {
            // Arrange
            _validator.Rule = new LogicalNameShouldNotEndWithId();

            // Act
            var result = _validator.Validate(attributeMetadata);

            // Assert
            Assert.NotNull(result); Assert.True(result.IsValid, result.ErrorMessage);
        }
    }
}