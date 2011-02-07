namespace LinqToImap.Imap
{
    using System.Linq;

    public interface IMailbox : IQueryable<MailboxMessage>
    {
        string Name { get; }
        int MessagesCount { get; }
        int RecentMessagesCount { get; }
        MessageFlags Flags { get; }
        IQueryable<MailboxMessage> Messages { get; }
        bool ReadableAndWritable { get; }
    }
}