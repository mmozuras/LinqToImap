﻿namespace LinqToGmail.Query
{
    using Imap;
    using System.Collections.Generic;
    using System.Linq;
    using Remotion.Data.Linq;
    using Remotion.Data.Linq.Clauses.ResultOperators;

    internal class GmailQueryExecutor : IQueryExecutor
    {
        private readonly ICommandExecutor commandExecutor;

        public GmailQueryExecutor(ICommandExecutor commandExecutor)
        {
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
            var sqlVisitor = new GmailQueryModelVisitor();
            sqlVisitor.VisitQueryModel(queryModel);

            var actions = sqlVisitor.Actions;
            foreach (var action in actions)
            {
                action(commandExecutor);
            }

            //TODO: Temporary cast, will probably be removed when I'll figure out re-linq.
            return (IEnumerable<T>) sqlVisitor.Results;
        }

        #endregion
    }
}