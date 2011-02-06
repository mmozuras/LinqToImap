namespace LinqToGmail.Imap.Commands
{
    using System.Collections.Generic;

    public sealed class FetchUids : Fetch<IEnumerable<Uid>>
    {
        public FetchUids()
        {
        }

        public FetchUids(int begin, int end) : base(begin, end)
        {
        }

        public FetchUids(IEnumerable<int> ids) : base(ids)
        {
        }

        protected override string Modifier
        {
            get { return "UID"; }
        }
    }
}