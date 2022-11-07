using OpportunityManagement.SchemaTests.Logic;
using OpportunityManagement.SchemaTests.Models;
using System.Linq;

namespace OpportunityManagement.SchemaTests.Rules.Attribute
{
    public class LogicalNameShouldNotContainMoreThanOneUnderscore : IMetadataRule<AttributeMetadata>
    {
        public static readonly string[] _exceptions = { "_base" }; // exceptional logical name endings that are allowed

        public IValidationResult Validate(AttributeMetadata metadata)
        {
            if (metadata == null) return null;

            var result = new ValidationResult { IsValid = _exceptions.Any(ex => metadata.LogicalName.EndsWith(ex)) || metadata.LogicalName.IndexOf('_') == metadata.LogicalName.LastIndexOf('_') };
            if (!result.IsValid) result.ErrorMessage = $"LogicalName of '{metadata.EntityLogicalName}.{metadata.LogicalName}' attribute should not contain more than one underscore.";

            return result;
        }
    }
}