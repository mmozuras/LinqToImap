namespace LinqToGmail.Imap.Commands
{
    using System.Collections.Generic;

    /// <summary>
    /// Obtains messages from a mailbox.
    /// </summary>
    public class Fetch : BaseCommand
    {
        public IEnumerable<MailboxMessage> Execute(Mailbox mailbox)
        {
            Write("FETCH 1:* ALL");
            return ParseMessages();
        }

        public IEnumerable<MailboxMessage> Execute(Mailbox mailbox, int begin, int end)
        {
            Write("FETCH {0}:{1} ALL", begin, end);
            return ParseMessages();
        }

        public IEnumerable<MailboxMessage> Execute(Mailbox mailbox, IEnumerable<int> messages)
        {
            Write("FETCH {0} ALL", string.Join(",", messages));
            return ParseMessages();
        }
    }
}