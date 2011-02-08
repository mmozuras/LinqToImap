namespace LinqToImap.Linq
{
    using System;
    using System.Linq;
    using Imap;
    using Remotion.Data.Linq.Clauses;
    using Remotion.Data.Linq.Clauses.ResultOperators;
    using Utils;

    internal class ResultOperatorVisitor
    {
        private readonly ICommandExecutor commandExecutor;

        public ResultOperatorVisitor(ICommandExecutor commandExecutor)
        {
            this.commandExecutor = commandExecutor;
        }

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
            if (resultOperator is LongCountResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the LongCount() method");
            if (resultOperator is ContainsResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the Contains() method");
            if (resultOperator is DefaultIfEmptyResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the DefaultIfEmpty() method");
            if (resultOperator is DistinctResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the Distinct() method");
            if (resultOperator is ExceptResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the Except() method");
            if (resultOperator is IntersectResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the Intersect() method");
            if (resultOperator is SingleResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the Single() method. Use the First() method instead");
            if (resultOperator is UnionResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the Union() method");

            if (resultOperator is AverageResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the Average() method");
            if (resultOperator is MaxResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the Max() method");
            if (resultOperator is MinResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the Min() method");
            if (resultOperator is SumResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the Sum() method");
            if (resultOperator is GroupResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the Group() method");
            if (resultOperator is OfTypeResultOperator)
                throw new NotSupportedException("LinqToImap does not provide support for the OfType() method");
            throw new NotSupportedException();
        }
    }
}