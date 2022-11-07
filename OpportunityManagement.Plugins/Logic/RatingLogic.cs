using Microsoft.Xrm.Sdk;
using OpportunityManagement.Common.Metadata.Entity;
using OpportunityManagement.Common.Metadata.Exceptions;
using OpportunityManagement.Common.Repositories;
using System;

namespace OpportunityManagement.Plugins.Logic
{
    public class RatingLogic : IRatingLogic
    {
        public const int MinEmployeesToBeCold = 1;
        public const int MinEmployeesToBeHot = 100;
        public const int MinEmployeesToBeWarm = 10;
        private readonly IAccountRepository _accountRepository;

        public RatingLogic(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentException(nameof(accountRepository));
        }

        public OptionSetValue DetermineRating(Entity opportunity)
        {
            if (opportunity == null) throw new ArgumentNullException(nameof(opportunity));

            var account = opportunity.GetAttributeValue<EntityReference>(Opportunity.ParentAccountReference);
            if (account == null) throw new InvalidPluginExecutionException(ExceptionMessages.X300003.Message);

            var numberOfEmployees = _accountRepository.GetNumberOfEmployees(account.Id);

            if (numberOfEmployees >= MinEmployeesToBeHot) return new OptionSetValue((int)Opportunity.Rating.Hot);
            if (numberOfEmployees >= MinEmployeesToBeWarm) return new OptionSetValue((int)Opportunity.Rating.Warm);
            if (numberOfEmployees >= MinEmployeesToBeCold) return new OptionSetValue((int)Opportunity.Rating.Cold);

            return null;
        }
    }
}