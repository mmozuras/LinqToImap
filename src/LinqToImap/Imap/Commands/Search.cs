namespace LinqToImap.Imap.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using Utils;

    public sealed class Search : Command<IEnumerable<int>>
    {
        public Search()
        {
            Text = "SEARCH ALL";
        }

        public Search(IntRange range, IEnumerable<KeyValuePair<string, string>> query)
        {
            Ensure.IsNotNull(range, "range");

            Text = string.Format("SEARCH {0} {1}", range, QueryToString(query));
        }

        public Search(IEnumerable<int> ids, IEnumerable<KeyValuePair<string, string>> query)
        {
            Text = string.Format("SEARCH {0} {1}", string.Join(",", ids), QueryToString(query));
        }

        public Search(IEnumerable<KeyValuePair<string, string>> query)
        {
            Text = "SEARCH " + QueryToString(query);
        }

        private static string QueryToString(IEnumerable<KeyValuePair<string, string>> query)
        {
            return query.Aggregate(string.Empty, (current, pair) => current + (pair.Key + " " + pair.Value + " ")).Trim();
        }

        public override string Text { get; protected set; }
    }
}