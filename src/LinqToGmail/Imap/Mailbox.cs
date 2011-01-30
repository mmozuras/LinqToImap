namespace LinqToGmail.Imap
{
    using Query;

    public class Mailbox
    {
        public Mailbox(string name)
        {
            Name = name;
            Flags = new MessageFlags();

            //TODO: This looks fishy
            Messages = new GmailQueryable<MailboxMessage>(this);
        }

        public string Name { get; private set; }

        public int MessagesCount { get; internal set; }
        public int RecentMessagesCount { get; internal set; }

        public MessageFlags Flags { get; internal set; }

        public GmailQueryable<MailboxMessage> Messages { get; private set; }

        public bool ReadableAndWritable { get; internal set; }
    }
}