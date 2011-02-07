namespace LinqToImap.Utils
{
    using System;

    public class IntRange : Range<int>
    {
        public IntRange(int from, int to) : base(from, to)
        {
            if (from > to)
            {
                throw new ArgumentException(string.Format("To {0} should be greater than from {1}.", to, from));
            }
        }
    }
}