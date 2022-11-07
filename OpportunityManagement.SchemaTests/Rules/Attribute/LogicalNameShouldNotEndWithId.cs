using OpportunityManagement.SchemaTests.Logic;
using OpportunityManagement.SchemaTests.Models;
using System.Linq;

namespace OpportunityManagement.SchemaTests.Rules.Attribute
{
    public class LogicalNameShouldNotEndWithId : IMetadataRule<AttributeMetadata>
    {
        public static readonly string[] _exceptions = { "guid" }; // exceptional logical name endings that are allowed

        public IValidationResult Validate(AttributeMetadata metadata)
        {
            if (metadata == null) return null;

            var result = new ValidationResult { IsValid = _exceptions.Any(ex => metadata.LogicalName.EndsWith(ex)) || !metadata.LogicalName.EndsWith("id") };
            if (!result.IsValid) result.ErrorMessage = $"LogicalName of '{metadata.EntityLogicalName}.{metadata.LogicalName}' attribute should not end with 'id'.";

            return result;
        }
    }
}