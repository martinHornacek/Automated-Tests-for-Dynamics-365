using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using OpportunityManagement.Common.Metadata.Entity;
using System;
using System.Linq;

namespace OpportunityManagement.Common.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IOrganizationService _service;

        public AccountRepository(IOrganizationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public EntityReference Create(Entity account)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));

            var id = _service.Create(account);
            return new EntityReference(Account.EntityLogicalName, id);
        }

        public int? GetNumberOfEmployees(Guid accountId)
        {
            return _service.RetrieveMultiple(new QueryExpression
            {
                EntityName = Account.EntityLogicalName,
                ColumnSet = new ColumnSet(Account.NumberOfEmployees),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(Account.AccountId, ConditionOperator.Equal, accountId)
                    }
                },
                NoLock = true,
                TopCount = 1
            }).Entities.FirstOrDefault()?.GetAttributeValue<int?>(Account.NumberOfEmployees);
        }
    }
}