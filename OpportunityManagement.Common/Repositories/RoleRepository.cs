using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using OpportunityManagement.Common.Metadata.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpportunityManagement.Common.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IOrganizationService _service;

        public RoleRepository(IOrganizationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public List<Entity> GetRoles(Guid businessUnitId, params string[] securityRolesNames)
        {
            return _service.RetrieveMultiple(new QueryExpression
            {
                EntityName = Role.EntityLogicalName,
                ColumnSet = new ColumnSet(Role.Name),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(Role.Name, ConditionOperator.In, securityRolesNames),
                        new ConditionExpression(Role.BusinessUnitReference, ConditionOperator.Equal, businessUnitId)
                    }
                },
                NoLock = true
            }).Entities.ToList();
        }
    }
}