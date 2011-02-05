namespace LinqToGmail.Imap.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using System.Text;
    using System.Text.RegularExpressions;

    public class AddressesParser : SingleLineParser<Addresses>
    {
        public override Addresses Parse(string addresses)
        {
            IEnumerable<string> split = addresses.Trim()
                .Split(new[] {"))"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim());

            MailAddress from = ParseSingleAddress(split.ElementAtOrDefault(0));
            MailAddress sender = ParseSingleAddress(split.ElementAtOrDefault(1));
            MailAddress replyTo = ParseSingleAddress(split.ElementAtOrDefault(2));

            IEnumerable<MailAddress> to = ParseAddresses(split.ElementAtOrDefault(3));
            IEnumerable<MailAddress> cc = ParseAddresses(split.ElementAtOrDefault(4));
            IEnumerable<MailAddress> bcc = ParseAddresses(split.ElementAtOrDefault(5));

            return new Addresses(from, sender, replyTo, to, cc, bcc);
        }

        private static MailAddress ParseSingleAddress(string addresses)
        {
            return CreateAddressCollection(GetAddressListFrom(addresses)).First();
        }

        private static IEnumerable<MailAddress> ParseAddresses(string addresses)
        {
            return CreateAddressCollection(GetAddressListFrom(addresses));
        }

        private static string GetAddressListFrom(string addresses)
        {
            if (string.IsNullOrWhiteSpace(addresses) || addresses.StartsWith("NIL"))
            {
                return string.Empty;
            }
            return addresses.Substring(1, addresses.Length - 1) + ")";
        }

        private static IEnumerable<MailAddress> CreateAddressCollection(string addressList)
        {
            addressList = Regex.Replace(Regex.Replace(addressList, "\\\\\"", "'"), "\\.\\.", ".");

            foreach (string match in addressList.RegexMatches(@"\(((?>\((?<LEVEL>)|\)(?<-LEVEL>)|(?! \( | \) ).)+(?(LEVEL)(?!)))\)$"))
            {
                var displayName = new StringBuilder();
                string value = BuildDisplayName(match, displayName);

                var email = new StringBuilder();
                BuildEmail(value, email);

                yield return new MailAddress(email.ToString(), QuotedPrintableDecoder.Decode(displayName.ToString()));
            }
        }

        private static void BuildEmail(string value, StringBuilder email)
        {
            value = Build(value, email, m => m);
            Build(value, email, m => "@" + m);
        }

        private static string BuildDisplayName(string value, StringBuilder displayName)
        {
            value = Build(value, displayName, m => m);
            return Build(value, displayName, m => " " + m);
        }

        private static string Build(string value, StringBuilder stringBuilder, Func<string, string> func)
        {
            if (value.StartsWith("NIL"))
            {
                return value.Substring(3).Trim();
            }

            Match match = Regex.Match(value, @"""([^""]+)""");
            stringBuilder.Append(func(match.Groups[1].ToString()));
            return value.Substring(match.Length).Trim();
        }
    }
}