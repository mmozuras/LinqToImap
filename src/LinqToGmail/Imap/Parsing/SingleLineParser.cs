namespace LinqToGmail.Imap.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class SingleLineParser<T> : IParser<T>
    {
        #region IParser<T> Members

        public T Parse(IEnumerable<string> input)
        {
            if (input.Count() == 1)
            {
                return Parse(input.Single());
            }
            if (input.Count() == 2 && input.Last().IsOk())
            {
                return Parse(input.First());
            }
            throw new ArgumentException(GetType().Name + " can only parse a single line.", "input");
        }

        #endregion

        public abstract T Parse(string input);
    }
}