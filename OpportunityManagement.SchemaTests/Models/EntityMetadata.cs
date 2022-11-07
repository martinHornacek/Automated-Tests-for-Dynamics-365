using System.Collections.Generic;

namespace OpportunityManagement.SchemaTests.Models
{
    public class EntityMetadata
    {
        public IEnumerable<AttributeMetadata> Attributes { get; set; }

        public string Name { get; set; }

        public int? ObjectTypeCode { get; set; }
    }
}