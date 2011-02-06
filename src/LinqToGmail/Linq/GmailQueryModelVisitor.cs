namespace LinqToGmail.Linq
{
    using System.Linq;
    using Imap;
    using Imap.Commands;
    using System;
    using System.Collections.Generic;
    using Remotion.Data.Linq;
    using Remotion.Data.Linq.Clauses;
    using Remotion.Data.Linq.Clauses.Expressions;
    using Remotion.Data.Linq.Clauses.ResultOperators;

    internal class GmailQueryModelVisitor : QueryModelVisitorBase
    {
        public IList<Action<ICommandExecutor>> Actions { get; private set; }

        public QueryState QueryState { get; private set; }

        public GmailQueryModelVisitor(string mailboxName)
        {
            QueryState = new QueryState();
            Actions = new List<Action<ICommandExecutor>>
                          {
                              executor =>
                                  {
                                      var select = new Select(mailboxName);
                                      QueryState.To = executor.Execute(select).MessagesCount;
                                      QueryState.From = 1;
                                  }
                          };
        }

        public override void VisitWhereClause(WhereClause whereClause, QueryModel queryModel, int index)
        {
            var where = new WhereExpressionVisitor();
            where.VisitExpression(whereClause.Predicate);

            Actions.Add(executor =>
                            {
                                if (QueryState.Ids != null)
                                {
                                    var fetchUids = new FetchUids(QueryState.Ids);
                                    QueryState.Uids = executor.Execute(fetchUids);
                                }
                                else if (QueryState.From.HasValue && QueryState.To.HasValue)
                                {
                                    var fetchFrom = new FetchUids(new[] {QueryState.From.Value});
                                    var fetchTo = new FetchUids(new[] {QueryState.To.Value});

                                    var enumerable = executor.Execute(fetchFrom);
                                    QueryState.FromUid = enumerable.Single();
                                    QueryState.ToUid = executor.Execute(fetchTo).Single();
                                }

                                if (QueryState.Uids != null)
                                {
                                    where.SearchParameters.Add("UID", string.Join(",", QueryState.Uids));
                                }
                                else if (QueryState.From != null)
                                {
                                    where.SearchParameters.Add("UID", QueryState.FromUid + ":" + QueryState.ToUid);
                                }

                                var search = new Search(where.SearchParameters);
                                QueryState.Ids = executor.Execute(search);

                                QueryState.From = null;
                                QueryState.To = null;
                                QueryState.FromUid = null;
                                QueryState.ToUid = null;
                                QueryState.Uids = null;
                            }); 
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
            if (resultOperator is TakeResultOperator)
            {
                var take = resultOperator as TakeResultOperator;
                var count = int.Parse(take.Count.ToString());

                Actions.Add(executor =>
                                {
                                    if (QueryState.Ids != null)
                                    {
                                        QueryState.Ids = QueryState.Ids.Take(count);
                                    }
                                    else if (QueryState.From != null)
                                    {
                                        QueryState.To = QueryState.From + count - 1;
                                    }

                                    QueryState.FromUid = null;
                                    QueryState.ToUid = null;
                                    QueryState.Uids = null;
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