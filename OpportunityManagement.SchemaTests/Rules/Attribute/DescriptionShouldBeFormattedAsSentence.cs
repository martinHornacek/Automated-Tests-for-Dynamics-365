using OpportunityManagement.SchemaTests.Logic;
using OpportunityManagement.SchemaTests.Models;
using System.Linq;

namespace OpportunityManagement.SchemaTests.Rules.Attribute
{
    public class DescriptionShouldBeFormattedAsSentence : IMetadataRule<AttributeMetadata>
    {
        public static readonly string[] _sentenceEndings = { ".", "?", "!", ")" };

        public IValidationResult Validate(AttributeMetadata metadata)
        {
            if (metadata == null) return null;

            var result = new ValidationResult { IsValid = IsFormattedLikeSentence(metadata.Description) };
            if (!result.IsValid) result.ErrorMessage = $"Description of '{metadata.EntityLogicalName}.{metadata.LogicalName}' attribute should be formatted as sentence.";

            return result;
        }

        private bool IsFormattedLikeSentence(string input)
        {
            if (string.IsNullOrEmpty(input)) return true;
            return _sentenceEndings.Aggregate(false, (current, sentenceEnding) => current || input.EndsWith(sentenceEnding));
        }
    }
}