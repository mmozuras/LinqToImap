namespace LinqToGmail.Imap.Commands
{
    using System.Collections.Generic;
    using System.Linq;

    public sealed class Search : Command<IEnumerable<int>>
    {
        public Search(IEnumerable<KeyValuePair<string, string>> query)
        {
            Text = query.Aggregate("SEARCH ", (current, pair) => current + (pair.Key + " " + pair.Value + " ")).Trim();
        }

        public override string Text { get; protected set; }
    }
}