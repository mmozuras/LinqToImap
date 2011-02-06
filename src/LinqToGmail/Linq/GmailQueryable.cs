namespace LinqToGmail.Linq
{
    using System.Linq;
    using System.Linq.Expressions;
    using Imap;
    using Remotion.Data.Linq;
    using Remotion.Data.Linq.Parsing.Structure;

    public class GmailQueryable<T> : QueryableBase<T>
    {
        public GmailQueryable(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public GmailQueryable(string mailboxName, ICommandExecutor commandExecutor)
            : base(QueryParser.CreateDefault(), CreateExecutor(mailboxName, commandExecutor))
        {
        }

        private static IQueryExecutor CreateExecutor(string mailboxName, ICommandExecutor commandExecutor)
        {
            return new GmailQueryExecutor(mailboxName, commandExecutor);
        }
    }
}