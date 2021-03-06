﻿namespace LinqToImap.Imap.Parsing
{
    using System;
    using System.Linq;
    using Commands;

    public class MailboxParser : IParser<IMailbox>
    {
        public IMailbox Parse(Command command, Response response)
        {
            var name = command.ToString().Split().Last();

            var imapMailbox = new Mailbox(name, CommandExecutor.Current);
            var messageFlagsParser = new MessageFlagsParser();

            foreach (string line in response.Data)
            {
                line.RegexMatch(@"(\d+) EXISTS", m => { imapMailbox.MessagesCount = Convert.ToInt32(m); });
                line.RegexMatch(@"(\d+) RECENT", m => { imapMailbox.RecentMessagesCount = Convert.ToInt32(m); });
                line.RegexMatch(@" FLAGS \((.*?)\)", m => { imapMailbox.Flags = messageFlagsParser.Parse(m); });
            }

            if (response.Status.Contains("READ-WRITE"))
            {
                imapMailbox.ReadableAndWritable = true;
            }
            return imapMailbox;
        }
    }
}