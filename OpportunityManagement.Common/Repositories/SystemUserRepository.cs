using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using OpportunityManagement.Common.Metadata.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpportunityManagement.Common.Repositories
{
    public class SystemUserRepository : ISystemUserRepository
    {
        private readonly IOrganizationService _service;
        private readonly IRoleRepository _roleRepository;

        public SystemUserRepository(IOrganizationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _roleRepository = new RoleRepository(_service);
        }

        public void AssociateUserWithSecurityRoles(Guid systemUserId, params string[] securityRoleNames)
        {
            if (securityRoleNames == null || securityRoleNames.Length < 1) return;

            var systemUser = _service.Retrieve(SystemUser.EntityLogicalName, systemUserId, new ColumnSet(SystemUser.BusinessUnitReference));
            var businessUnitId = systemUser.GetAttributeValue<EntityReference>(SystemUser.BusinessUnitReference).Id;
            var rolesToAdd = _roleRepository.GetRoles(businessUnitId, securityRoleNames).Select(role => role.ToEntityReference()).ToList();

            _service.Associate(SystemUser.EntityLogicalName, systemUserId, new Relationship(SystemUserRoles.Relationships.SystemUserRolesAssociation), new EntityReferenceCollection(rolesToAdd));
        }

        public void DisassociateSecurityRolesFromUser(Guid systemUserId, params string[] securityRoleNames)
        {
            if (securityRoleNames == null || securityRoleNames.Length < 1) return;

            var systemUser = _service.Retrieve(SystemUser.EntityLogicalName, systemUserId, new ColumnSet(SystemUser.BusinessUnitReference));
            var businessUnitId = systemUser.GetAttributeValue<EntityReference>(SystemUser.BusinessUnitReference).Id;
            var rolesToRemove = _roleRepository.GetRoles(businessUnitId, securityRoleNames).Select(role => role.ToEntityReference()).ToList();

            _service.Disassociate(SystemUser.EntityLogicalName, systemUserId, new Relationship(SystemUserRoles.Relationships.SystemUserRolesAssociation), new EntityReferenceCollection(rolesToRemove));
        }

        public List<Entity> GetUserSecurityRoles(Guid systemUserId)
        {
            return _service.RetrieveMultiple(new QueryExpression
            {
                EntityName = Role.EntityLogicalName,
                ColumnSet = new ColumnSet(Role.Name),
                LinkEntities =
                {
                    new LinkEntity
                    {
                        LinkFromEntityName = Role.EntityLogicalName,
                        LinkFromAttributeName = Role.RoleId,
                        LinkToEntityName = SystemUserRoles.EntityLogicalName,
                        LinkToAttributeName = SystemUserRoles.RoleRerefence,
                        LinkEntities =
                        {
                            new LinkEntity
                            {
                                LinkFromEntityName = SystemUserRoles.EntityLogicalName,
                                LinkFromAttributeName = SystemUserRoles.SystemUserReference,
                                LinkToEntityName = SystemUser.EntityLogicalName,
                                LinkToAttributeName = SystemUser.SystemUserId,
                                LinkCriteria =
                                {
                                    Conditions =
                                    {
                                        new ConditionExpression(SystemUserRoles.SystemUserReference, ConditionOperator.Equal, systemUserId)
                                    }
                                }
                            }
                        }
                    }
                },
                NoLock = true
            }).Entities.ToList();
        }

        public EntityReference GetUserByDomainName(string domainName)
        {
            return _service.RetrieveMultiple(new QueryExpression
            {
                EntityName = SystemUser.EntityLogicalName,
                ColumnSet = new ColumnSet(false),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(SystemUser.DomainName, ConditionOperator.Equal, domainName)
                    }
                },
                NoLock = true,
                TopCount = 1
            }).Entities.FirstOrDefault()?.ToEntityReference();
        }
    }
}