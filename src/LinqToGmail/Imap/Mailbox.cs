namespace LinqToGmail.Imap
{
    using System.Linq;
    using Query;

    public class Mailbox
    {
        public Mailbox()
        {
            Flags = new MessageFlags();

            //TODO: This looks fishy
            Messages = new GmailQueryable<MailboxMessage>(this);
        }

        public int MessagesCount { get; internal set; }
        public int RecentMessagesCount { get; internal set; }

        public MessageFlags Flags { get; internal set; }

        public IQueryable<MailboxMessage> Messages { get; private set; }

        public bool ReadableAndWritable { get; internal set; }
    }
}