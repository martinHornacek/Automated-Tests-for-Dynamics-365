using OpportunityManagement.SchemaTests.Logic;
using OpportunityManagement.SchemaTests.Models;

namespace OpportunityManagement.SchemaTests.Rules.Attribute
{
    public class LogicalNameShouldEndWithId : IMetadataRule<AttributeMetadata>
    {
        public IValidationResult Validate(AttributeMetadata metadata)
        {
            if (metadata == null) return null;

            var result = new ValidationResult { IsValid = metadata.LogicalName.EndsWith("id") };
            if (!result.IsValid) result.ErrorMessage = $"LogicalName of '{metadata.EntityLogicalName}.{metadata.LogicalName}' attribute should end with 'id'.";

            return result;
        }
    }
}