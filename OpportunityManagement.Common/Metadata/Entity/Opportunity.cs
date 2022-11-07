namespace OpportunityManagement.Common.Metadata.Entity
{
    public static class Opportunity
    {
        public const string EntityLogicalName = "opportunity";
        public const string OpportunityId = "opportunityid";
        public const string ParentAccountReference = "parentaccountid";
        public const string RatingCode = "opportunityratingcode";

        public enum Rating
        {
            Hot = 1,
            Warm = 2,
            Cold = 3
        }
    }
}