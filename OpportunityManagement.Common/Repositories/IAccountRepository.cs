using Microsoft.Xrm.Sdk;
using System;

namespace OpportunityManagement.Common.Repositories
{
    public interface IAccountRepository
    {
        EntityReference Create(Entity account);

        int? GetNumberOfEmployees(Guid accountId);
    }
}