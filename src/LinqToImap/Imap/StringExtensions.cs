namespace LinqToImap.Imap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        internal static IEnumerable<string> RegexMatches(this string input, string pattern)
        {
            return from Match match in Regex.Matches(input, pattern)
                   select match.Groups[1].ToString().Trim();
        }

        internal static void RegexMatch(this string input, string pattern, Action<string> ifSuccess)
        {
            var match = Regex.Match(input, pattern);
            if (match.Success)
            {
                ifSuccess(match.Groups[1].ToString().Trim());
            }
        }

        internal static bool IsOk(this string response)
        {
            return response.StartsWith("* OK") || (response.Length > 7 && response.Substring(7, 2) == "OK");
        }

        internal static bool HasInfo(this string response)
        {
            return response.StartsWith("*");
        }
    }
}