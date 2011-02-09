namespace LinqToImap.Imap.Parsing
{
    using System;
    using System.Linq;
    using Commands;

    public abstract class SingleLineParser<T> : IParser<T>
    {
        public T Parse(Command command, Response response)
        {
            var line = response.Data.SingleOrDefault();
            if (line != null)
            {
                return Parse(line);
            }
            throw new ArgumentException(GetType().Name + " can only parse a single line.", "response");
        }

        public abstract T Parse(string input);
    }
}