using Microsoft.Xrm.Sdk;
using OpportunityManagement.Common.Metadata.Entity;
using OpportunityManagement.Common.Repositories;
using System;
using System.Linq;

namespace OpportunityManagement.Common.Utilities
{
    public class SystemUserConfigurator
    {
        private readonly ISystemUserRepository _repository;

        public SystemUserConfigurator(IOrganizationService service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));
            _repository = new SystemUserRepository(service);
        }

        public void ConfigureUser(Guid userId, params string[] securityRoleNames)
        {
            var roles = _repository.GetUserSecurityRoles(userId).Select(role => role.GetAttributeValue<string>(Role.Name)).ToList();

            var rolesToAdd = securityRoleNames.Except(roles).ToArray();
            var rolesToRemove = roles.Except(securityRoleNames).ToArray();

            _repository.AssociateUserWithSecurityRoles(userId, rolesToAdd);
            _repository.DisassociateSecurityRolesFromUser(userId, rolesToRemove);
        }
    }
}