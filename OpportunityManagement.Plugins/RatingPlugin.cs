using Microsoft.Xrm.Sdk;
using OpportunityManagement.Common.Extensions;
using OpportunityManagement.Common.Metadata.Entity;
using OpportunityManagement.Common.Metadata.Exceptions;
using OpportunityManagement.Common.Metadata.Plugin;
using OpportunityManagement.Plugins.Logic;
using System;

namespace OpportunityManagement.Plugins
{
    public class RatingPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.PrimaryEntityName != Opportunity.EntityLogicalName) throw new InvalidPluginExecutionException(ExceptionMessages.X300001.Message.FormatWith(Opportunity.EntityLogicalName, context.PrimaryEntityName));
            if (context.Stage == Stage.PreOperation && context.MessageName == MessageName.Create) OnPreOperationCreateOpportunity(serviceProvider);
        }

        private static void OnPreOperationCreateOpportunity(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var ratingLogic = BusinessLogicFactory.Instance.GetRatingLogic(serviceProvider);

            if (!context.InputParameters.ContainsKey(InputParameter.Target)) throw new InvalidPluginExecutionException(ExceptionMessages.X300002.Message);
            var opportunity = context.InputParameters[InputParameter.Target] as Entity;

            if (ratingLogic != null) opportunity[Opportunity.RatingCode] = ratingLogic.DetermineRating(opportunity);
        }
    }
}