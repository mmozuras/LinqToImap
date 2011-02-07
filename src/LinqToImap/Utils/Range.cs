namespace LinqToImap.Utils
{
    using System;

    public abstract class Range<T> : IEquatable<Range<T>>
    {
        protected Range(T @from, T to)
        {
            From = from;
            To = to;
        }

        public T From { get; private set; }
        public T To { get; private set; }

        #region IEquatable<Range<T>> Members

        public bool Equals(Range<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.From, From) && Equals(other.To, To);
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}:{1}", From, To);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Range<T>)) return false;
            return Equals((Range<T>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (From.GetHashCode()*397) ^ To.GetHashCode();
            }
        }
    }
}