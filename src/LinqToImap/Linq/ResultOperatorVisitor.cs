namespace LinqToImap.Linq
{
    using System;
    using System.Linq;
    using Remotion.Data.Linq.Clauses;
    using Remotion.Data.Linq.Clauses.ResultOperators;
    using Utils;

    internal class ResultOperatorVisitor
    {
        public Action<QueryState> Visit(ResultOperatorBase resultOperator, int index)
        {
            if (resultOperator is TakeResultOperator)
            {
                var take = resultOperator as TakeResultOperator;
                var count = int.Parse(take.Count.ToString());

                return queryState => queryState.Apply(
                    q => q.Ids.Take(count),
                    q => new IntRange(q.Range.From, q.Range.From + count - 1));
            }
            if (resultOperator is FirstResultOperator)
            {
                return queryState => queryState.Apply(
                    q => new[] {q.Ids.First()},
                    q => new[] {q.Range.From});
            }
            if (resultOperator is LastResultOperator)
            {
                return queryState => queryState.Apply(
                    q => new[] {queryState.Ids.Last()},
                    q => new[] {queryState.Range.To});
            }
            if (resultOperator is CountResultOperator)
            {
                return queryState => queryState.Apply(
                    q => queryState.Ids.Count(),
                    q => queryState.Range.To - queryState.Range.From + 1);
            }
            //LongCountResultOperator
            //ContainsResultOperator
            //DefaultIfEmptyResultOperator
            //DistinctResultOperator
            //ExceptResultOperator
            //IntersectResultOperator
            //SingleResultOperator
            //UnionResultOperator
            throw new NotSupportedException(string.Format("LinqToImap does not support {0}.", resultOperator));
        }
    }
}