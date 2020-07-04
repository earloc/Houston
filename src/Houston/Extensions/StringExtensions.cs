using System;
#nullable enable

namespace Houston.Extensions
{
    internal static class StringExtensions
    {
        public static string Expand(this string that)
            => that
            .Replace("{HOST}", Environment.MachineName)
        ;
    }
}
