namespace LinqToGmail.Imap.Commands
{
    using System.Collections.Generic;

    /// <summary>
    /// Obtains messages from a mailbox.
    /// </summary>
    public class Fetch : BaseCommand
    {
        public Fetch(ImapSslClient client) : base(client)
        {
        }

        public Mailbox Execute(Mailbox mailbox)
        {
            Write(string.Format("FETCH 1:* ALL\r\n"));
            return ParseMessages(mailbox);
        }

        public Mailbox Execute(Mailbox mailbox, int begin, int end)
        {
            Write(string.Format("FETCH {0}:{1} ALL\r\n", begin, end));
            return ParseMessages(mailbox);
        }

        public Mailbox Execute(Mailbox mailbox, IEnumerable<int> messages)
        {
            Write(string.Format("FETCH {0} ALL\r\n", string.Join(",", messages)));
            return ParseMessages(mailbox);
        }
    }
}