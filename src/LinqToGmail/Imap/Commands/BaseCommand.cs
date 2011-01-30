namespace LinqToGmail.Imap.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public class BaseCommand
    {
        protected readonly ImapSslClient Client;

        protected BaseCommand(ImapSslClient client)
        {
            Client = client;
        }

        protected Mailbox ParseMessages(Mailbox mailbox)
        {
            foreach (MailboxMessage message in from response in LoadMessages()
                                               where response.StartsWith("*")
                                               let mailboxMessage = MailboxMessage.Parse(response)
                                               where !mailbox.Messages.Contains(mailboxMessage)
                                               select mailboxMessage)
            {
                mailbox.Messages.Add(message);
            }

            return mailbox;
        }

        private IEnumerable<string> LoadMessages()
        {
            while (true)
            {
                string response = Client.Read();
                yield return response;
                if (Client.IsOk(response))
                {
                    break;
                }
            }
        }

        protected Mailbox ParseMailbox(string mailbox)
        {
            string response = Client.Read();
            if (response.StartsWith("*"))
            {
                var imapMailbox = new Mailbox(mailbox);
                do
                {
                    response.RegexMatch(@"(\d+) EXISTS", m => { imapMailbox.MessagesCount = Convert.ToInt32(m); });
                    response.RegexMatch(@"(\d+) RECENT", m => { imapMailbox.RecentMessagesCount = Convert.ToInt32(m); });
                    response.RegexMatch(@" FLAGS \((.*?)\)", m => { imapMailbox.Flags = MessageFlags.Parse(m); });

                    response = Client.Read();
                } while (response.StartsWith("*"));
                if (Client.IsOk(response) && response.Contains("READ-WRITE"))
                {
                    imapMailbox.ReadableAndWritable = true;
                }
                return imapMailbox;
            }
            return null;
        }
    }
}