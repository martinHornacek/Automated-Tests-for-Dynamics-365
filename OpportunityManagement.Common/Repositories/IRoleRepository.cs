using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;

namespace OpportunityManagement.Common.Repositories
{
    public interface IRoleRepository
    {
        List<Entity> GetRoles(Guid businessUnitId, params string[] securityRolesNames);
    }
}