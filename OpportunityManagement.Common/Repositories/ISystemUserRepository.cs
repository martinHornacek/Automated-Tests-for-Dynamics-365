using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;

namespace OpportunityManagement.Common.Repositories
{
    public interface ISystemUserRepository
    {
        void AssociateUserWithSecurityRoles(Guid userId, params string[] securityRoleNames);

        void DisassociateSecurityRolesFromUser(Guid systemUserId, params string[] securityRoleNames);

        EntityReference GetUserByDomainName(string domainName);

        List<Entity> GetUserSecurityRoles(Guid systemUserId);
    }
}