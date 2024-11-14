using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows;
using System.IO;

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
