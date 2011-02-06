namespace LinqToGmail.Linq
{
    using System.Collections.Generic;

    internal class QueryState
    {
        public IEnumerable<int> Ids;
        public int? From;
        public int? To;
    }
}