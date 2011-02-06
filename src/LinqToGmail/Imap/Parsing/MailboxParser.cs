namespace LinqToGmail.Imap.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class MailboxParser : IParser<IMailbox>
    {
        #region IParser<Mailbox> Members

        public IMailbox Parse(IEnumerable<string> input)
        {
            var last = input.Last();
            var name = Regex.Match(last, "[\\w]+(?= selected)").Groups[0].Value;

            var imapMailbox = new Mailbox(name);
            var messageFlagsParser = new MessageFlagsParser();

            foreach (string response in input)
            {
                response.RegexMatch(@"(\d+) EXISTS", m => { imapMailbox.MessagesCount = Convert.ToInt32(m); });
                response.RegexMatch(@"(\d+) RECENT", m => { imapMailbox.RecentMessagesCount = Convert.ToInt32(m); });
                response.RegexMatch(@" FLAGS \((.*?)\)", m => { imapMailbox.Flags = messageFlagsParser.Parse(m); });
            }

            if (last.Contains("READ-WRITE"))
            {
                imapMailbox.ReadableAndWritable = true;
            }
            return imapMailbox;
        }

        #endregion
    }
}