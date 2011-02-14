namespace LinqToImap.Linq
{
    using System;
    using System.Collections.Generic;
    using Imap;
    using Imap.Commands;
    using Remotion.Data.Linq;
    using Remotion.Data.Linq.Clauses;
    using Remotion.Data.Linq.Clauses.Expressions;
    using Utils;

    internal class ImapQueryModelVisitor : QueryModelVisitorBase
    {
        private readonly ICommandExecutor commandExecutor;
        public IList<Action<QueryState>> Actions { get; private set; }

        public ImapQueryModelVisitor(ICommandExecutor commandExecutor, string mailboxName)
        {
            this.commandExecutor = commandExecutor;
            Actions = new List<Action<QueryState>>
                          {
                              queryState =>
                                  {
                                      var select = new Select(mailboxName);
                                      var messagesCount = commandExecutor.Execute(select).MessagesCount;
                                      queryState.Range = 1.To(messagesCount);
                                  }
                          };
        }

        public override void VisitWhereClause(WhereClause whereClause, QueryModel queryModel, int index)
        {
            var where = new WhereExpressionVisitor();
            where.VisitExpression(whereClause.Predicate);

            Actions.Add(queryState => queryState.Apply(
                q => commandExecutor.Execute(new Search(q.Ids, where.Query)),
                q => commandExecutor.Execute(new Search(queryState.Range, where.Query))));
        }

        public override void VisitMainFromClause(MainFromClause fromClause, QueryModel queryModel)
        {
            if (fromClause.FromExpression is SubQueryExpression)
            {
                var subQuery = (SubQueryExpression) fromClause.FromExpression;
                VisitQueryModel(subQuery.QueryModel);
            }
            base.VisitMainFromClause(fromClause, queryModel);
        }

        public override void VisitResultOperator(ResultOperatorBase resultOperator, QueryModel queryModel, int index)
        {
            var resultOperatorVisitor = new ResultOperatorVisitor();
            var action = resultOperatorVisitor.Visit(resultOperator, index);
            Actions.Add(action);

            base.VisitResultOperator(resultOperator, queryModel, index);
        }
    }
}