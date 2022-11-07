using OpportunityManagement.SchemaTests.Models;

namespace OpportunityManagement.SchemaTests.Logic
{
    public interface IMetadataRule<T> where T : IMetadata
    {
        IValidationResult Validate(T metadata);
    }
}