namespace LinqToGmail.Query
{
    using Imap;
    using System.Linq;
    using System.Linq.Expressions;
    using Remotion.Data.Linq;
    using Remotion.Data.Linq.Parsing.Structure;

    public class GmailQueryable<T> : QueryableBase<T>
    {
        public GmailQueryable(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        internal GmailQueryable(Mailbox mailbox) : base(QueryParser.CreateDefault(), CreateExecutor(mailbox))
        {
        }

        private static IQueryExecutor CreateExecutor(Mailbox mailbox)
        {
            return new GmailQueryExecutor(mailbox);
        }
    }
}