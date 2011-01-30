namespace LinqToGmail.Imap.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public class BaseCommand
    {
        public readonly ImapSslClient client;

        protected BaseCommand(ImapSslClient client)
        {
            this.client = client;
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
                string response = client.Read();
                yield return response;
                if (client.IsOk(response))
                {
                    break;
                }
            }
        }

        protected Mailbox ParseMailbox(string mailbox)
        {
            string response = client.Read();
            if (response.StartsWith("*"))
            {
                var imapMailbox = new Mailbox(mailbox);
                do
                {
                    response.RegexMatch(@"(\d+) EXISTS", m => { imapMailbox.MessagesCount = Convert.ToInt32(m); });
                    response.RegexMatch(@"(\d+) RECENT", m => { imapMailbox.RecentMessagesCount = Convert.ToInt32(m); });
                    response.RegexMatch(@" FLAGS \((.*?)\)", m => { imapMailbox.Flags = MessageFlags.Parse(m); });

                    response = client.Read();
                } while (response.StartsWith("*"));
                if (client.IsOk(response) && response.Contains("READ-WRITE"))
                {
                    imapMailbox.ReadableAndWritable = true;
                }
                return imapMailbox;
            }
            return null;
        }

        protected void Write(string message)
        {
            client.Write(message);
        }

        protected string Read()
        {
            return client.Read();
        }
    }
}