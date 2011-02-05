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
            if (input.Count() != 1)
            {
                throw new ArgumentException(GetType().Name + " can only parse a single line.", "input");
            }
            return Parse(input.Single());
        }

        #endregion

        protected abstract T Parse(string input);
    }
}