namespace LinqToImap.Imap.Commands
{
    using System.Collections.Generic;
    using Utils;

    public sealed class Search : Command<IEnumerable<int>>
    {
        public Search(string query)
        {
            //TODO: Robust query validation or something strongly typed.
            Text = "SEARCH " + query;
        }

        public Search(IntRange range, string query)
        {
            Ensure.IsNotNull(range, "range");

            Text = string.Format("SEARCH {0} {1}", range, query);
        }

        public Search(IEnumerable<int> ids, string query)
        {
            Text = string.Format("SEARCH {0} {1}", string.Join(",", ids), query);
        }

        protected override string Text { get; set; }
    }
}