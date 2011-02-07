namespace LinqToImap.Linq
{
    using System.Linq;
    using System.Linq.Expressions;
    using Imap;
    using Remotion.Data.Linq;
    using Remotion.Data.Linq.Parsing.Structure;

    public class ImapQueryable<T> : QueryableBase<T>
    {
        public ImapQueryable(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public ImapQueryable(string mailboxName, ICommandExecutor commandExecutor)
            : base(QueryParser.CreateDefault(), CreateExecutor(mailboxName, commandExecutor))
        {
        }

        private static IQueryExecutor CreateExecutor(string mailboxName, ICommandExecutor commandExecutor)
        {
            return new ImapQueryExecutor(mailboxName, commandExecutor);
        }
    }
}