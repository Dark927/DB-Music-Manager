using System;

namespace MusicManager.Utilities
{
    internal static class TypesComparer
    {
        public static bool AreTypesEqual(Type first, Type second)
        {
            return first.Equals(second);
        }
    }
}
