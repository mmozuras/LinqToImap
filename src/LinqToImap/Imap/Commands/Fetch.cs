namespace LinqToImap.Imap.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using Utils;

    public sealed class Fetch : Command<IEnumerable<ImapMessage>>
    {
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
                Text = string.Empty;
            }
        }

        public Fetch(int id) : this(new[] {id})
        {
        }

        protected override string Text { get; set; }

        public bool IsEmpty
        {
            get { return string.IsNullOrWhiteSpace(Text); }
        }
    }
}