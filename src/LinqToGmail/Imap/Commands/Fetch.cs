namespace LinqToGmail.Imap.Commands
{
    using System.Collections.Generic;

    /// <summary>
    /// Obtains messages from a mailbox.
    /// </summary>
    public sealed class Fetch : BaseCommand
    {
        public Fetch()
        {
            Text = "FETCH 1:* ALL";
        }

        public Fetch(int begin, int end)
        {
            Text = string.Format("FETCH {0}:{1} ALL", begin, end);
        }

        public Fetch(IEnumerable<int> messages)
        {
            Text = string.Format("FETCH {0} ALL", string.Join(",", messages));
        }

        public override string Text { get; protected set; }
    }
}