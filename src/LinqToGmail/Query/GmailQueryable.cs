namespace LinqToGmail.Query
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

        public GmailQueryable(ICommandExecutor commandExecutor)
            : base(QueryParser.CreateDefault(), CreateExecutor(commandExecutor))
        {
        }

        private static IQueryExecutor CreateExecutor(ICommandExecutor commandExecutor)
        {
            return new GmailQueryExecutor(commandExecutor);
        }
    }
}