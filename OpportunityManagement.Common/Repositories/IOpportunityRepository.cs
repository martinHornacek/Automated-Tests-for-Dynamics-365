using Microsoft.Xrm.Sdk;
using System;

namespace OpportunityManagement.Common.Repositories
{
    public interface IOpportunityRepository
    {
        EntityReference Create(Entity opportunity);

        OptionSetValue GetRating(Guid id);
    }
}