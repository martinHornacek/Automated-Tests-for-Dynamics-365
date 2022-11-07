using System;
using System.Linq;

namespace OpportunityManagement.Common.Extensions
{
    public static class EnumExtensions
    {
        public static int? ResolveEnumValue<T>(this string name) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(nameof(T) + " must be an enum type.");

            if (Enum.GetNames(typeof(T)).Contains(name)) return (int)Enum.Parse(typeof(T), name);
            return null;
        }
    }
}