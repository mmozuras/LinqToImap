namespace LinqToImap.Imap.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Commands;

    public class MailboxParser : IParser<IMailbox>
    {
        public IMailbox Parse(Command command, IEnumerable<string> input)
        {
            var last = input.Last();
            var name = command.Text.Split().Last();

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
    }
}