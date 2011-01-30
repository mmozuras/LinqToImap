namespace LinqToGmail.Imap
{
    using System.Collections.Generic;

    public class Mailbox
    {
        public Mailbox(string name)
        {
            Name = name;
            Messages = new List<MailboxMessage>();
            Flags = new MessageFlags();
        }

        public string Name { get; private set; }

        public int MessagesCount { get; internal set; }
        public int RecentMessagesCount { get; internal set; }

        public MessageFlags Flags { get; internal set; }

        public IList<MailboxMessage> Messages { get; private set; }

        public bool ReadableAndWritable { get; internal set; }
    }
}