using System;

namespace OpportunityManagement.Plugins.Logic
{
    public abstract class BusinessLogicFactory
    {
        public static BusinessLogicFactory Instance { get; } = new OpportunityManagementLogicFactory();

        public abstract IRatingLogic GetRatingLogic(IServiceProvider serviceProvider);
    }
}