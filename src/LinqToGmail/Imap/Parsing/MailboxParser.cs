namespace LinqToGmail.Imap.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MailboxParser : IParser<Mailbox>
    {
        #region IParser<Mailbox> Members

        public Mailbox Parse(IEnumerable<string> input)
        {
            var imapMailbox = new Mailbox();
            var messageFlagsParser = new MessageFlagsParser();

            foreach (string response in input)
            {
                response.RegexMatch(@"(\d+) EXISTS", m => { imapMailbox.MessagesCount = Convert.ToInt32(m); });
                response.RegexMatch(@"(\d+) RECENT", m => { imapMailbox.RecentMessagesCount = Convert.ToInt32(m); });
                response.RegexMatch(@" FLAGS \((.*?)\)", m => { imapMailbox.Flags = messageFlagsParser.Parse(m); });
            }

            if (input.Last().Contains("READ-WRITE"))
            {
                imapMailbox.ReadableAndWritable = true;
            }
            return imapMailbox;
        }

        #endregion
    }
}