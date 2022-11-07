using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Tooling.Connector;
using System;

namespace OpportunityManagement.Common.Utilities
{
    public static class OrganizationServiceFactory
    {
        public static IOrganizationService GetImpersonatedOrganizationService(string connectionString, Guid userId)
        {
            var proxy = GetOrganizationServiceProxy(connectionString);
            proxy.CallerId = userId;

            return proxy;
        }

        public static IOrganizationService GetOrganizationService(string connectionString)
        {
            return GetOrganizationServiceProxy(connectionString);
        }

        private static OrganizationServiceProxy GetOrganizationServiceProxy(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));

            var client = new CrmServiceClient(connectionString);

            var proxy = client.OrganizationServiceProxy;
            proxy.Timeout = new TimeSpan(0, 10, 0);
            proxy.EnableProxyTypes();

            return proxy;
        }
    }
}