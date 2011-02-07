namespace LinqToImap.Imap.Parsing
{
    using System.Collections.Generic;

    public interface IParser<out T>
    {
        T Parse(IEnumerable<string> input);
    }
}