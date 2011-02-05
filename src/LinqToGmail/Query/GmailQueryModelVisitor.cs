namespace LinqToGmail.Query
{
    using Imap;
    using Imap.Commands;
    using System;
    using System.Collections.Generic;
    using Remotion.Data.Linq;
    using Remotion.Data.Linq.Clauses;
    using Remotion.Data.Linq.Clauses.ResultOperators;

    internal class GmailQueryModelVisitor : QueryModelVisitorBase
    {
        public IList<Action<ICommandExecutor>> Actions { get; private set; }
        public IEnumerable<MailboxMessage> Results { get; private set; }

        public GmailQueryModelVisitor()
        {
            Actions = new List<Action<ICommandExecutor>>();
        }

        public override void VisitWhereClause(WhereClause whereClause, QueryModel queryModel, int index)
        {
            var where = new WhereClauseExpressionTreeVisitor();
            where.VisitExpression(whereClause.Predicate);

            Actions.Add(executor =>
                            {
                                var ids = executor.Execute<IEnumerable<int>>(where.Command);
                                var fetch = new Fetch(ids);
                                Results = executor.Execute<IEnumerable<MailboxMessage>>(fetch);
                            }); 
        }

        public override void VisitResultOperator(ResultOperatorBase resultOperator, QueryModel queryModel, int index)
        {
            if (resultOperator is TakeResultOperator)
            {
                var take = resultOperator as TakeResultOperator;
                var count = int.Parse(take.Count.ToString());

                Actions.Add(executor =>
                                {
                                    var fetch = new Fetch(1, count);
                                    Results = executor.Execute<IEnumerable<MailboxMessage>>(fetch);
                                });
            }
            else if (resultOperator is AverageResultOperator)
                throw new NotSupportedException("LinqToGmail does not provide support for the Average() method");
            else if (resultOperator is CountResultOperator)
                throw new NotSupportedException("LinqToGmail does not provide support for the Count() method");
            else if (resultOperator is LongCountResultOperator)
                throw new NotSupportedException("LinqToGmail does not provide support for the LongCount() method");
            else if (resultOperator is FirstResultOperator)
                throw new NotSupportedException("LinqToGmail does not provide support for the First() method");
            else if (resultOperator is MaxResultOperator)
                throw new NotSupportedException("LinqToGmail does not provide support for the Max() method");
            else if (resultOperator is MinResultOperator)
                throw new NotSupportedException("LinqToGmail does not provide support for the Min() method");
            else if (resultOperator is SumResultOperator)
                throw new NotSupportedException("LinqToGmail does not provide support for the Sum() method");
            else if (resultOperator is ContainsResultOperator)
                throw new NotSupportedException("LinqToGmail does not provide support for the Contains() method");
            else if (resultOperator is DefaultIfEmptyResultOperator)
                throw new NotSupportedException("LinqToGmail does not provide support for the DefaultIfEmpty() method");
            else if (resultOperator is DistinctResultOperator)
                throw new NotSupportedException("LinqToGmail does not provide support for the Distinct() method");
            else if (resultOperator is ExceptResultOperator)
                throw new NotSupportedException("LinqToGmail does not provide support for the Except() method");
            else if (resultOperator is GroupResultOperator)
                throw new NotSupportedException("LinqToGmail does not provide support for the Group() method");
            else if (resultOperator is IntersectResultOperator)
                throw new NotSupportedException("LinqToGmail does not provide support for the Intersect() method");
            else if (resultOperator is OfTypeResultOperator)
                throw new NotSupportedException("LinqToGmail does not provide support for the OfType() method");
            else if (resultOperator is SingleResultOperator)
                throw new NotSupportedException("LinqToGmail does not provide support for the Single() method. Use the First() method instead");
            else if (resultOperator is UnionResultOperator)
                throw new NotSupportedException("LinqToGmail does not provide support for the Union() method");

            base.VisitResultOperator(resultOperator, queryModel, index);
        }
    }
}