namespace LinqToGmail.Imap
{
    using System.Linq;
    using Linq;

    public class Mailbox : IMailbox
    {
        public Mailbox(string name)
        {
            Name = name;
            Flags = new MessageFlags();

            //TODO: This should be fixed.
            Messages = new GmailQueryable<MailboxMessage>(name, CommandExecutor.Current);
        }

        public string Name { get; private set; }

        public int MessagesCount { get; internal set; }
        public int RecentMessagesCount { get; internal set; }

        public MessageFlags Flags { get; internal set; }

        public IQueryable<MailboxMessage> Messages { get; private set; }

        public bool ReadableAndWritable { get; internal set; }
    }
}