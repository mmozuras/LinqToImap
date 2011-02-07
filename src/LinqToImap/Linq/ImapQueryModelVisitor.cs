namespace LinqToImap.Linq
{
    using System.Linq;
    using System;
    using System.Collections.Generic;
    using Imap;
    using Imap.Commands;
    using Remotion.Data.Linq;
    using Remotion.Data.Linq.Clauses;
    using Remotion.Data.Linq.Clauses.Expressions;
    using Remotion.Data.Linq.Clauses.ResultOperators;
    using Utils;

    internal class ImapQueryModelVisitor : QueryModelVisitorBase
    {
        public IList<Action<ICommandExecutor>> Actions { get; private set; }

        public IEnumerable<int> Ids { get; private set; }
        public IntRange Range { get; private set; }
        public object Result { get; private set; }

        public ImapQueryModelVisitor(string mailboxName)
        {
            Actions = new List<Action<ICommandExecutor>>
                          {
                              executor =>
                                  {
                                      var select = new Select(mailboxName);
                                      Range = new IntRange(1, executor.Execute(select).MessagesCount);
                                  }
                          };
        }

        public override void VisitWhereClause(WhereClause whereClause, QueryModel queryModel, int index)
        {
            var where = new WhereExpressionVisitor();
            where.VisitExpression(whereClause.Predicate);

            Actions.Add(executor =>
                            {
                                if (Ids != null)
                                {
                                    var search = new Search(Ids, where.SearchParameters);
                                    Ids = executor.Execute(search);
                                }
                                else if (Range != null)
                                {
                                    var search = new Search(Range, where.SearchParameters);
                                    Ids = executor.Execute(search);
                                }

                                Range = null;
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
                                    if (Ids != null)
                                    {
                                        Ids = Ids.Take(count);
                                    }
                                    else if (Range != null)
                                    {
                                        Range = new IntRange(Range.From, Range.From + count - 1);
                                    }
                                });
            }
            else if (resultOperator is FirstResultOperator)
            {
                Actions.Add(executor =>
                                {
                                    if (Ids != null)
                                    {
                                        Ids = new[] {Ids.First()};
                                    }
                                    else if (Range != null)
                                    {
                                        Ids = new[] {Range.From};
                                        Range = null;
                                    }
                                });
            }
            else if (resultOperator is LastResultOperator)
            {
                Actions.Add(executor =>
                                {
                                    if (Ids != null)
                                    {
                                        Ids = new[] {Ids.Last()};
                                    }
                                    else if (Range != null)
                                    {
                                        Ids = new[] {Range.To};
                                        Range = null;
                                    }
                                });
            }
            else if (resultOperator is CountResultOperator)
            {
                Actions.Add(executor =>
                                {
                                    if (Ids != null)
                                    {
                                        Result = Ids.Count();
                                    }
                                    else if (Range != null)
                                    {
                                        Result = Range.To - Range.From + 1;
                                    }
                                });
            }
            else if (resultOperator is LongCountResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the LongCount() method");
            else if (resultOperator is ContainsResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the Contains() method");
            else if (resultOperator is DefaultIfEmptyResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the DefaultIfEmpty() method");
            else if (resultOperator is DistinctResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the Distinct() method");
            else if (resultOperator is ExceptResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the Except() method");
            else if (resultOperator is IntersectResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the Intersect() method");
            else if (resultOperator is SingleResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the Single() method. Use the First() method instead");
            else if (resultOperator is UnionResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the Union() method");

            else if (resultOperator is AverageResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the Average() method");
            else if (resultOperator is MaxResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the Max() method");
            else if (resultOperator is MinResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the Min() method");
            else if (resultOperator is SumResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the Sum() method");
            else if (resultOperator is GroupResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the Group() method");
            else if (resultOperator is OfTypeResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the OfType() method");

            base.VisitResultOperator(resultOperator, queryModel, index);
        }
    }
}