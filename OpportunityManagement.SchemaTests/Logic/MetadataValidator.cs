using OpportunityManagement.SchemaTests.Models;

namespace OpportunityManagement.SchemaTests.Logic
{
    public abstract class MetadataValidator<T> : IMetadataValidator<T> where T : IMetadata
    {
        public IMetadataRule<T> Rule { get; set; }

        public IValidationResult Validate(T metadata)
        {
            return Rule?.Validate(metadata);
        }
    }
}