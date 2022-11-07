using OpportunityManagement.SchemaTests.Logic;
using OpportunityManagement.SchemaTests.Models;
using System;
using System.Linq;

namespace OpportunityManagement.SchemaTests.Rules.Attribute
{
    public class DisplayNameShouldBeProperlyCapitalized : IMetadataRule<AttributeMetadata>
    {
        public static readonly string[] _exceptions = "a, an, the, for, and, nor, but, or, yet, so, a, abaft, abeam, aboard, about, above, absent, across, afore, after, against, along, alongside, amid, amidst, among, amongst, an, anenst, apropos, apud, around, as, aside, astride, at, athwart, atop, barring, before, behind, below, beneath, beside, besides, between, beyond, but, by, chez, circa, concerning, despite, down, during, except, excluding, failing, following, for, forenenst, from, given, in, including, inside, into, like, mid, midst, minus, modulo, near, next, notwithstanding, o, of, off, on, onto, opposite, out, outside, over, pace, past, per, plus, pro, qua, regarding, round, sans, save, since, than, through, throughout, till, times, to, toward, towards, under, underneath, unlike, until, unto, up, upon, versus, via, vice, vis, with, within, without, worth".Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

        public IValidationResult Validate(AttributeMetadata metadata)
        {
            if (metadata == null) return null;

            var result = new ValidationResult { IsValid = IsProperlyCapitalized(metadata.DisplayName) };
            if (!result.IsValid) result.ErrorMessage = $"DisplayName of '{metadata.EntityLogicalName}.{metadata.LogicalName}' attribute should be properly capitalized.";

            return result;
        }

        private bool IsProperlyCapitalized(string input)
        {
            if (string.IsNullOrEmpty(input)) return true;

            foreach (var word in input.Split(' '))
            {
                if (!_exceptions.Contains(word) && char.IsLetter(word[0]) && !char.IsUpper(word[0])) return false;
            }

            return true;
        }
    }
}