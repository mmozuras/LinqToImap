namespace LinqToGmail.Linq
{
    using System;
    using Imap;
    using System.Collections.Generic;
    using System.Linq;
    using Imap.Commands;
    using Remotion.Data.Linq;
    using Remotion.Data.Linq.Clauses.ResultOperators;

    internal class GmailQueryExecutor : IQueryExecutor
    {
        private readonly string mailboxName;
        private readonly ICommandExecutor commandExecutor;

        public GmailQueryExecutor(string mailboxName, ICommandExecutor commandExecutor)
        {
            this.mailboxName = mailboxName;
            this.commandExecutor = commandExecutor;
        }

        #region IQueryExecutor Members

        public T ExecuteScalar<T>(QueryModel queryModel)
        {
            return ExecuteSingle<T>(queryModel, false);
        }

        public T ExecuteSingle<T>(QueryModel queryModel, bool returnDefaultWhenEmpty)
        {
            IEnumerable<T> results = ExecuteCollection<T>(queryModel);

            if (queryModel.ResultOperators.OfType<LastResultOperator>().Any())
            {
                return results.LastOrDefault();
            }

            return (returnDefaultWhenEmpty)
                       ? results.FirstOrDefault()
                       : results.First();
        }

        public IEnumerable<T> ExecuteCollection<T>(QueryModel queryModel)
        {
            var visitor = new GmailQueryModelVisitor(mailboxName);
            visitor.VisitQueryModel(queryModel);

            var actions = visitor.Actions;
            foreach (var action in actions)
            {
                action(commandExecutor);
            }

            if (visitor.QueryState.Ids != null)
            {
                //TODO: Temporary cast, will probably be removed when I'll figure out re-linq.
                var fetchAll = new FetchAll(visitor.QueryState.Ids);
                return (IEnumerable<T>) commandExecutor.Execute(fetchAll);
            }
            if (visitor.QueryState.From != null && visitor.QueryState.To != null)
            {
                var fetchAll = new FetchAll(visitor.QueryState.From.Value, visitor.QueryState.To.Value);
                return (IEnumerable<T>) commandExecutor.Execute(fetchAll);
            }
            throw new ApplicationException("Something went wrong with your LINQ query. Please report it as a bug.");
        }

        #endregion
    }
}