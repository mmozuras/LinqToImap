namespace LinqToGmail.Imap
{
    using System;
    using System.Text.RegularExpressions;

    public class MailboxMessage
    {
        public MailboxMessage()
        {
            Flags = new MessageFlags();
        }

        public int Id { get; private set; }
        public string ReferenceId { get; private set; }

        public string Subject { get; private set; }

        public MessageFlags Flags { get; private set; }
        public Addresses Addresses { get; private set; }

        public DateTime Received { get; private set; }
        public DateTime Sent { get; private set; }
        public string TimeZone { get; private set; }

        public int Size { get; private set; }

        public static MailboxMessage Parse(string input)
        {
            var message = new MailboxMessage();

            input.RegexMatch(@"\* (\d*)", m => { message.Id = Convert.ToInt32(m); });
            input.RegexMatch(@"FLAGS \(([^\)]*)\)", m => { message.Flags = MessageFlags.Parse(m); });
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

            message.Addresses = Addresses.Parse(input);

            return message;
        }

        public override string ToString()
        {
            return Subject;
        }
    }
}