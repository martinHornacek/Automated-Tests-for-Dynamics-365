using OpportunityManagement.SchemaTests.Logic;
using OpportunityManagement.SchemaTests.Models;
using System.Text.RegularExpressions;

namespace OpportunityManagement.SchemaTests.Rules.Attribute
{
    public class DisplayNameShouldHaveProperSpacing : IMetadataRule<AttributeMetadata>
    {
        public IValidationResult Validate(AttributeMetadata metadata)
        {
            if (metadata == null) return null;

            var result = new ValidationResult { IsValid = HasProperSpacing(metadata.DisplayName) };
            if (!result.IsValid) result.ErrorMessage = $"DisplayName of '{metadata.EntityLogicalName}.{metadata.LogicalName}' attribute should have proper spacing.";

            return result;
        }

        private bool HasProperSpacing(string input)
        {
            var beginRegex = new Regex("^\\s");
            var insideRegex = new Regex("\\s{2,}");
            var endRegex = new Regex("\\s$");

            if (beginRegex.Match(input).Success || insideRegex.Match(input).Success || endRegex.Match(input).Success) return false;

            return true;
        }
    }
}