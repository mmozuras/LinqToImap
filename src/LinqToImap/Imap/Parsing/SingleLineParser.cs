namespace LinqToImap.Imap.Parsing
{
    using System;
    using System.Linq;
    using Commands;

    public abstract class SingleLineParser<T> : IParser<T>
    {
        public T Parse(Command command, Response response)
        {
            if (response.Data.Count() == 1)
            {
                return Parse(response.Data.Single());
            }
            throw new ArgumentException(GetType().Name + " can only parse a single line.", "response");
        }

        public abstract T Parse(string input);
    }
}