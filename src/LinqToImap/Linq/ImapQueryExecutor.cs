namespace LinqToImap.Linq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Imap;
    using Imap.Commands;
    using Remotion.Data.Linq;
    using Remotion.Data.Linq.Clauses.ResultOperators;

    internal class ImapQueryExecutor : IQueryExecutor
    {
        private readonly string mailboxName;
        private readonly ICommandExecutor commandExecutor;

        public ImapQueryExecutor(string mailboxName, ICommandExecutor commandExecutor)
        {
            this.mailboxName = mailboxName;
            this.commandExecutor = commandExecutor;
        }

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
            var visitor = new ImapQueryModelVisitor(mailboxName);
            visitor.VisitQueryModel(queryModel);

            var actions = visitor.Actions;
            foreach (var action in actions)
            {
                action(commandExecutor);
            }

            if (visitor.Result != null)
            {
                return new List<T> {(T) visitor.Result};
            }
            if (visitor.Ids != null)
            {
                //TODO: Temporary cast, will probably be removed when I'll figure out re-linq.
                var fetchAll = new Fetch(visitor.Ids);
                return (IEnumerable<T>) commandExecutor.Execute(fetchAll);
            }
            if (visitor.Range != null)
            {
                var fetchAll = new Fetch(visitor.Range);
                return (IEnumerable<T>) commandExecutor.Execute(fetchAll);
            }
            throw new ApplicationException("Something went wrong with your LINQ query. Please report it as a bug.");
        }
    }
}