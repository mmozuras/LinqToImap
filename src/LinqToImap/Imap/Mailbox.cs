namespace LinqToImap.Imap
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Linq;

    public class Mailbox : IMailbox
    {
        public Mailbox(string name, ICommandExecutor commandExecutor)
        {
            Name = name;
            Flags = new MessageFlags();
            Messages = new ImapQueryable<MailboxMessage>(name, commandExecutor);
        }

        public string Name { get; private set; }

        public int MessagesCount { get; internal set; }
        public int RecentMessagesCount { get; internal set; }

        public MessageFlags Flags { get; internal set; }

        public IQueryable<MailboxMessage> Messages { get; private set; }

        public bool ReadableAndWritable { get; internal set; }

        public IEnumerator<MailboxMessage> GetEnumerator()
        {
            return Messages.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Expression Expression
        {
            get { return Messages.Expression; }
        }

        public Type ElementType
        {
            get { return Messages.ElementType; }
        }

        public IQueryProvider Provider
        {
            get { return Messages.Provider; }
        }
    }
}