using OpportunityManagement.SchemaTests.Logic;
using OpportunityManagement.SchemaTests.Models;
using System.Linq;

namespace OpportunityManagement.SchemaTests.Rules.Attribute
{
    public class DisplayNameShouldNotContainInvalidCharacters : IMetadataRule<AttributeMetadata>
    {
        public static readonly string[] _invalidCharacters = { "&", "<", ">" };

        public IValidationResult Validate(AttributeMetadata metadata)
        {
            if (metadata == null) return null;

            var result = new ValidationResult { IsValid = !_invalidCharacters.Any(ch => metadata.DisplayName.Contains(ch)) };
            if (!result.IsValid) result.ErrorMessage = $"DisplayName of '{metadata.EntityLogicalName}.{metadata.LogicalName}' attribute contains invalid character.";

            return result;
        }
    }
}