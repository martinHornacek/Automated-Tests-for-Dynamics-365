namespace OpportunityManagement.SchemaTests.Models
{
    public interface IValidationResult
    {
        string ErrorMessage { get; set; }
        bool IsValid { get; set; }
    }
}