namespace LinqToGmail.Imap.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public class BaseCommand
    {
        private readonly ImapSslClient client;

        protected BaseCommand()
        {
            client = ImapSslClient.Current;
        }

        protected Mailbox ParseMessages(Mailbox mailbox)
        {
            foreach (MailboxMessage message in from response in LoadMessages()
                                               where response.HasInfo()
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
                if (response.IsOk())
                {
                    break;
                }
            }
        }

        protected Mailbox ParseMailbox(string mailbox)
        {
            string response = client.Read();
            if (response.HasInfo())
            {
                var imapMailbox = new Mailbox(mailbox);
                do
                {
                    response.RegexMatch(@"(\d+) EXISTS", m => { imapMailbox.MessagesCount = Convert.ToInt32(m); });
                    response.RegexMatch(@"(\d+) RECENT", m => { imapMailbox.RecentMessagesCount = Convert.ToInt32(m); });
                    response.RegexMatch(@" FLAGS \((.*?)\)", m => { imapMailbox.Flags = MessageFlags.Parse(m); });

                    response = client.Read();
                } while (response.HasInfo());
                if (response.IsOk() && response.Contains("READ-WRITE"))
                {
                    imapMailbox.ReadableAndWritable = true;
                }
                return imapMailbox;
            }
            return null;
        }

        protected void Write(string message)
        {
            client.Write(message + "\r\n");
        }

        protected void Write(string message, params object[] args)
        {
            Write(string.Format(message, args));
        }

        protected string Read()
        {
            return client.Read();
        }
    }
}