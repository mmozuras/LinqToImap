namespace LinqToGmail.Imap.Commands
{
    using System.Collections.Generic;
    using System.Linq;

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

        public Fetch(IEnumerable<int> ids)
        {
            if (ids.Any())
            {
                Text = string.Format("FETCH {0} ALL", string.Join(",", ids));
            }
            else
            {
                Text = "FETCH 1:* ALL";
            }
        }

        public override string Text { get; protected set; }
    }
}