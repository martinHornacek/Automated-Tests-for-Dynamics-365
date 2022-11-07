using Microsoft.Xrm.Sdk;
using OpportunityManagement.Common.Extensions;
using OpportunityManagement.Common.Metadata;
using OpportunityManagement.Common.Metadata.Exceptions;
using OpportunityManagement.Common.Repositories;
using OpportunityManagement.Common.Utilities;
using System;
using System.Configuration;

namespace OpportunityManagement.AcceptanceTests.Fixtures
{
    public class RatingFixture
    {
        public RatingFixture()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["TestingOrg"]?.ConnectionString;

            // running user configures the testing user before test execution,
            // i.e. assigns security roles, changes BU, etc.
            RunningUserService = OrganizationServiceFactory.GetOrganizationService(connectionString);
            var systemUserConfigurator = new SystemUserConfigurator(RunningUserService);
            var systemUserRepository = new SystemUserRepository(RunningUserService);

            // obtain and configure testing user
            var testingUserDomainName = ConfigurationManager.AppSettings["TestingUserDomainName"];
            var testingUser = systemUserRepository.GetUserByDomainName(testingUserDomainName);

            if (testingUser == null) throw new InvalidOperationException(ExceptionMessages.X300004.Message.FormatWith(testingUserDomainName));
            TestingUserService = OrganizationServiceFactory.GetImpersonatedOrganizationService(connectionString, testingUser.Id);

            systemUserConfigurator.ConfigureUser(testingUser.Id, SecurityRoleNames.Salesperson);

            AccountRepository = new AccountRepository(TestingUserService);
            OpportunityRepository = new OpportunityRepository(TestingUserService);
        }

        public IAccountRepository AccountRepository { get; }
        public IOpportunityRepository OpportunityRepository { get; }
        public IOrganizationService RunningUserService { get; }
        public IOrganizationService TestingUserService { get; }
    }
}