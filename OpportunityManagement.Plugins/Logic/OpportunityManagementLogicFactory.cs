using Microsoft.Xrm.Sdk;
using OpportunityManagement.Common.Repositories;
using System;

namespace OpportunityManagement.Plugins.Logic
{
    public class OpportunityManagementLogicFactory : BusinessLogicFactory
    {
        public override IRatingLogic GetRatingLogic(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));

            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var service = serviceFactory.CreateOrganizationService(context.InitiatingUserId);
            var accountRepository = new AccountRepository(service);

            return new RatingLogic(accountRepository);
        }
    }
}