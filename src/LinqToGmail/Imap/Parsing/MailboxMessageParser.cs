namespace LinqToGmail.Imap.Parsing
{
    using System;
    using System.Dynamic;
    using System.Text.RegularExpressions;

    public class MailboxMessageParser : SingleLineParser<MailboxMessage>
    {
        public override MailboxMessage Parse(string input)
        {
            var messageFlagsParser = new MessageFlagsParser();

            //TODO: Using ExpandoObject for now, cause it's simplier, will refactor.
            dynamic message = new ExpandoObject();

            input.RegexMatch(@"\* (\d*)", m => { message.Id = Convert.ToInt32(m); });
            input.RegexMatch(@"FLAGS \(([^\)]*)\)", m => { message.Flags = messageFlagsParser.Parse(m); });
            input.RegexMatch(@"INTERNALDATE ""([^""]+)""", m => { message.Received = DateTime.Parse(m); });
            input.RegexMatch(@"RFC822.SIZE (\d+)", m => { message.Size = Convert.ToInt32(m); });
            input = input.Replace("ENVELOPE", string.Empty);

            Match match = Regex.Match(input, @"\(""(?:\w{3}\, )?([^""]+)""");
            if (match.Success)
            {
                string value = match.Groups[1].ToString().Replace("GMT", "+0000");
                Match subMatch = Regex.Match(value, @"([\-\+]\d{4}.*|NIL.*)");
                message.Sent = DateTime.Parse(value.Remove(subMatch.Index));
                message.TimeZone = subMatch.Groups[1].ToString();
                input = input.Remove(0, match.Index + match.Length);
            }

            string subject = input.Substring(0, input.IndexOf("(("));
            subject.Trim().RegexMatch("^\"(.*)\"$", m => { message.Subject = QuotedPrintableDecoder.Decode(m); });

            input.Remove(0, subject.Length).RegexMatch(@"""<([^>]+)>""", m => { message.ReferenceId = m; });

            var addressesParser = new AddressesParser();
            message.Addresses = addressesParser.Parse(input);

            return new MailboxMessage(message.Id, message.ReferenceId, message.Subject, message.Flags, message.Addresses,
                                      message.Received, message.Sent, message.TimeZone, message.Size);
        }
    }
}