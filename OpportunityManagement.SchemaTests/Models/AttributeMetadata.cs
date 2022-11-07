namespace OpportunityManagement.SchemaTests.Models
{
    public class AttributeMetadata : IMetadata
    {
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public string EntityLogicalName { get; set; }
        public bool IsCustom => LogicalName.StartsWith("ava_");
        public string LogicalName { get; set; }
        public string Type { get; set; }
    }
}