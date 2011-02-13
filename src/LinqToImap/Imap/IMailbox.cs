namespace LinqToImap.Imap
{
    using System.Linq;

    public interface IMailbox : IQueryable<ImapMessage>
    {
        string Name { get; }
        int MessagesCount { get; }
        int RecentMessagesCount { get; }
        MessageFlags Flags { get; }
        IQueryable<ImapMessage> Messages { get; }
        bool ReadableAndWritable { get; }
    }
}