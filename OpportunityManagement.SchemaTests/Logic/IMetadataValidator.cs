using OpportunityManagement.SchemaTests.Models;

namespace OpportunityManagement.SchemaTests.Logic
{
    public interface IMetadataValidator<T> where T : IMetadata
    {
        IMetadataRule<T> Rule { get; set; }

        IValidationResult Validate(T metadata);
    }
}