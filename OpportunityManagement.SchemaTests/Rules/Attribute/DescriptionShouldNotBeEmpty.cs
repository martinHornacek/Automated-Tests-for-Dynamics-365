using OpportunityManagement.SchemaTests.Logic;
using OpportunityManagement.SchemaTests.Models;

namespace OpportunityManagement.SchemaTests.Rules.Attribute
{
    public class DescriptionShouldNotBeEmpty : IMetadataRule<AttributeMetadata>
    {
        public IValidationResult Validate(AttributeMetadata metadata)
        {
            if (metadata == null) return null;

            var result = new ValidationResult { IsValid = !string.IsNullOrEmpty(metadata.Description) };
            if (!result.IsValid) result.ErrorMessage = $"Description of '{metadata.EntityLogicalName}.{metadata.LogicalName}' attribute should not be empty.";

            return result;
        }
    }
}