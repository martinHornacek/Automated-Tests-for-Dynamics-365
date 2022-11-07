using OpportunityManagement.Common.Extensions;
using OpportunityManagement.SchemaTests.Logic;
using OpportunityManagement.SchemaTests.Models;

namespace OpportunityManagement.SchemaTests.Rules.Attribute
{
    public class LogicalNameShouldBeLowercase : IMetadataRule<AttributeMetadata>
    {
        public IValidationResult Validate(AttributeMetadata metadata)
        {
            if (metadata == null) return null;

            var result = new ValidationResult { IsValid = metadata.LogicalName.IsLowercase() };
            if (!result.IsValid) result.ErrorMessage = $"LogicalName of '{metadata.EntityLogicalName}.{metadata.LogicalName}' attribute should be lowercase.";

            return result;
        }
    }
}