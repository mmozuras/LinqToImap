namespace LinqToImap.Imap.Parsing
{
    using System;
    using System.Text.RegularExpressions;

    public class ImapMessageParser : SingleLineParser<ImapMessage>
    {
        public override ImapMessage Parse(string input)
        {
            var messageFlagsParser = new MessageFlagsParser();
            var imapMessage = new ImapMessage();

            input.RegexMatch(@"\* (\d*)", m => { imapMessage.Id = Convert.ToInt32(m); });
            input.RegexMatch(@"FLAGS \(([^\)]*)\)", m => { imapMessage.Flags = messageFlagsParser.Parse(m); });
            input.RegexMatch(@"INTERNALDATE ""([^""]+)""", m => { imapMessage.Received = DateTime.Parse(m); });
            input.RegexMatch(@"RFC822.SIZE (\d+)", m => { imapMessage.Size = Convert.ToInt32(m); });
            input = input.Replace("ENVELOPE", string.Empty);

            Match match = Regex.Match(input, @"\(""(?:\w{3}\, )?([^""]+)""");
            if (match.Success)
            {
                string value = match.Groups[1].ToString().Replace("GMT", "+0000");
                Match subMatch = Regex.Match(value, @"([\-\+]\d{4}.*|NIL.*)");
                imapMessage.Sent = DateTime.Parse(value.Remove(subMatch.Index));
                imapMessage.TimeZone = subMatch.Groups[1].ToString();
                input = input.Remove(0, match.Index + match.Length);
            }

            string subject = input.Substring(0, input.IndexOf("(("));
            subject.Trim().RegexMatch("^\"(.*)\"$", m => { imapMessage.Subject = QuotedPrintableDecoder.Decode(m); });

            var addressesParser = new AddressesParser();
            imapMessage.Addresses = addressesParser.Parse(input);

            return imapMessage;
        }
    }
}