namespace LinqToImap.Imap.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using Utils;

    public sealed class Fetch : Command<IEnumerable<MailboxMessage>>
    {
        public Fetch()
        {
            Text = "FETCH 1:* ALL";
        }

        public Fetch(IntRange range)
        {
            Ensure.IsNotNull(range, "range");

            Text = string.Format("FETCH {0} ALL", range);
        }

        public Fetch(IEnumerable<int> ids)
        {
            if (ids.Any())
            {
                Text = string.Format("FETCH {0} ALL", string.Join(",", ids));
            }
            else
            {
                Text = "FETCH 4294967295 ALL";
            }
        }

        public Fetch(int id) : this(new[]{id})
        {
            
        }

        public override string Text { get; protected set; }
    }
}