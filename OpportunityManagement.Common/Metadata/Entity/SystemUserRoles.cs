namespace OpportunityManagement.Common.Metadata.Entity
{
    public static class SystemUserRoles
    {
        public const string EntityLogicalName = "systemuserroles";
        public const string RoleRerefence = "roleid";
        public const string SystemUserReference = "systemuserid";

        public static class Relationships
        {
            public const string SystemUserRolesAssociation = "systemuserroles_association";
        }
    }
}