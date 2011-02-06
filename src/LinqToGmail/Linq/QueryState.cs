namespace LinqToGmail.Linq
{
    using System.Collections.Generic;
    using Imap;

    internal class QueryState
    {
        public IEnumerable<Uid> Uids;
        public Uid? FromUid;
        public Uid? ToUid;

        public IEnumerable<int> Ids;
        public int? From;
        public int? To;
    }
}