namespace LinqToImap.Linq
{
    using System;
    using System.Collections.Generic;
    using Utils;

    internal class QueryState
    {
        public IEnumerable<int> Ids { get; private set; }
        public IntRange Range { get; internal set; }
        public object Result { get; private set; }

        public void Apply(Func<QueryState, IEnumerable<int>> ifIds, Func<QueryState, IEnumerable<int>> ifRange)
        {
            Ids = Ids != null ? ifIds(this) : ifRange(this);
            Range = null;
        }

        public void Apply(Func<QueryState, IEnumerable<int>> ifIds, Func<QueryState, IntRange> ifRange)
        {
            if (Ids != null)
            {
                Range = null;
                Ids = ifIds(this);
            }
            else
            {
                Ids = null;
                Range = ifRange(this);
            }
        }

        public void Apply(Func<QueryState, object> ifIds, Func<QueryState, object> ifRange)
        {
            Result = Ids != null ? ifIds(this) : ifRange(this);
            Ids = null;
            Range = null;
        }
    }
}