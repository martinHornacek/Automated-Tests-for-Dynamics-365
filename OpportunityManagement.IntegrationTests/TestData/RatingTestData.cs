using Microsoft.Xrm.Sdk;
using NUnit.Framework;
using OpportunityManagement.Common.Metadata.Entity;
using System.Collections;

namespace OpportunityManagement.IntegrationTests.TestData
{
    public static class RatingTestData
    {
        public static IEnumerable Cases
        {
            get
            {
                yield return new TestCaseData(null).Returns(null);
                yield return new TestCaseData(0).Returns(null);
                yield return new TestCaseData(1).Returns(new OptionSetValue((int)Opportunity.Rating.Cold));
                yield return new TestCaseData(9).Returns(new OptionSetValue((int)Opportunity.Rating.Cold));
                yield return new TestCaseData(10).Returns(new OptionSetValue((int)Opportunity.Rating.Warm));
                yield return new TestCaseData(99).Returns(new OptionSetValue((int)Opportunity.Rating.Warm));
                yield return new TestCaseData(100).Returns(new OptionSetValue((int)Opportunity.Rating.Hot));
                yield return new TestCaseData(101).Returns(new OptionSetValue((int)Opportunity.Rating.Hot));
            }
        }
    }
}