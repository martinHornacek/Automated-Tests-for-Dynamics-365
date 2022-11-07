using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using OpportunityManagement.Common.Metadata.Entity;
using System;
using System.Linq;

namespace OpportunityManagement.Common.Repositories
{
    public class OpportunityRepository : IOpportunityRepository
    {
        private readonly IOrganizationService _service;

        public OpportunityRepository(IOrganizationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public EntityReference Create(Entity opportunity)
        {
            if (opportunity == null) throw new ArgumentNullException(nameof(opportunity));

            var id = _service.Create(opportunity);
            return new EntityReference(Opportunity.EntityLogicalName, id);
        }

        public OptionSetValue GetRating(Guid id)
        {
            return _service.RetrieveMultiple(new QueryExpression
            {
                EntityName = Opportunity.EntityLogicalName,
                ColumnSet = new ColumnSet(Opportunity.RatingCode),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(Opportunity.OpportunityId, ConditionOperator.Equal, id)
                    }
                },
                NoLock = true,
                TopCount = 1
            }).Entities.FirstOrDefault()?.GetAttributeValue<OptionSetValue>(Opportunity.RatingCode);
        }
    }
}