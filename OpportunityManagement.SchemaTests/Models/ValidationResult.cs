namespace OpportunityManagement.SchemaTests.Models
{
    public class ValidationResult : IValidationResult
    {
        public string ErrorMessage { get; set; }
        public bool IsValid { get; set; }
    }
}