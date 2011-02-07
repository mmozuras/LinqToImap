namespace LinqToImap.Imap.Parsing
{
    using System;
    using System.Text.RegularExpressions;

    public class MailboxMessageParser : SingleLineParser<MailboxMessage>
    {
        public override MailboxMessage Parse(string input)
        {
            var messageFlagsParser = new MessageFlagsParser();
            var mailboxMessage = new MailboxMessage();

            input.RegexMatch(@"\* (\d*)", m => { mailboxMessage.Id = Convert.ToInt32(m); });
            input.RegexMatch(@"FLAGS \(([^\)]*)\)", m => { mailboxMessage.Flags = messageFlagsParser.Parse(m); });
            input.RegexMatch(@"INTERNALDATE ""([^""]+)""", m => { mailboxMessage.Received = DateTime.Parse(m); });
            input.RegexMatch(@"RFC822.SIZE (\d+)", m => { mailboxMessage.Size = Convert.ToInt32(m); });
            input = input.Replace("ENVELOPE", string.Empty);

            Match match = Regex.Match(input, @"\(""(?:\w{3}\, )?([^""]+)""");
            if (match.Success)
            {
                string value = match.Groups[1].ToString().Replace("GMT", "+0000");
                Match subMatch = Regex.Match(value, @"([\-\+]\d{4}.*|NIL.*)");
                mailboxMessage.Sent = DateTime.Parse(value.Remove(subMatch.Index));
                mailboxMessage.TimeZone = subMatch.Groups[1].ToString();
                input = input.Remove(0, match.Index + match.Length);
            }

            string subject = input.Substring(0, input.IndexOf("(("));
            subject.Trim().RegexMatch("^\"(.*)\"$", m => { mailboxMessage.Subject = QuotedPrintableDecoder.Decode(m); });

            input.Remove(0, subject.Length).RegexMatch(@"""<([^>]+)>""", m => { mailboxMessage.ReferenceId = m; });

            var addressesParser = new AddressesParser();
            mailboxMessage.Addresses = addressesParser.Parse(input);

            return mailboxMessage;
        }
    }
}