namespace LinqToGmail.Imap.Commands
{
    using System.Collections.Generic;

    public sealed class FetchAll : Fetch<IEnumerable<MailboxMessage>>
    {
        public FetchAll()
        {
        }

        public FetchAll(int begin, int end) : base(begin, end)
        {
        }

        public FetchAll(IEnumerable<int> ids) : base(ids)
        {
        }

        protected override string Modifier
        {
            get { return "ALL"; }
        }
    }
}