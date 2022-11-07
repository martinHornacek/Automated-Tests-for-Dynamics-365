using Microsoft.Xrm.Sdk;

namespace OpportunityManagement.Plugins.Logic
{
    public interface IRatingLogic
    {
        OptionSetValue DetermineRating(Entity opportunity);
    }
}